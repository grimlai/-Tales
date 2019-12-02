using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ParallaxMoveType
{
    Press,
    Tracking,
}

[ExecuteInEditMode]
public class Parallax : MonoBehaviour
{
    static Parallax current;

    public AnimationCurve curve;
    public ParallaxMoveType type;
    public float targetSize;
    public Vector2 offset, targetPointMove, clampScale = new Vector2(.5f, 1);
    public List<ParallaxElement> elements;
    [HideInInspector]
    public bool fixedPosition;
    [SerializeField]
    [HideInInspector]
    Camera cam;

    Coroutine moveCoroutine;
    Vector3 oldMousePosition, startPosition;
    Vector2 inertia, startInertia, startMove, oldPositionTouch1, oldPositionTouch2;
    float speedOffset;
    float startTargetSize, startSize, scale;
    bool move, zoom;

    public static Parallax Current
    {
        get
        {
            if (!current)
                current = FindObjectOfType<Parallax>();
            return current;
        }
    }

    public bool IsMoved { get { return false; /*(startMove - offset).magnitude > .02f; */} }

    void Start()
    {
        cam = GetComponent<Camera>();                          // берёт камеру с персонажа и записывает в переменную cam 
        startPosition = transform.position;                    // сохраняет стартовую позицию
        startTargetSize = targetSize;                          // сохраняет стартовый масштаб
        scale = 1;
        if (!Application.isEditor)
            cam.orthographicSize = targetSize * (4f / 3f) / ((float)Screen.width / Screen.height);  // если не эдитор, то подбирает масштаб
        startSize = cam.orthographicSize;                      // сохраняет стартовый размер камеры
        speedOffset = 4;
        zoom = false;
    }

    void Update()
    {
        if (Application.isEditor)
        {
            if (startTargetSize == 0)
            {
                startTargetSize = targetSize;
                scale = 1;
            }
        }
        cam.orthographicSize = targetSize * (4f / 3f) / ((float)Screen.width / Screen.height);
        float changeScale = scale;
        float valueDeltaScale = Input.mouseScrollDelta.y / 10f;
        Vector2 centerScroll = Input.mousePosition;
        if (Input.touchCount == 2)                             // МАСШТАБИРОВАНИЕ!!!
        {
            if (!zoom)
            {
                zoom = true;
                oldPositionTouch1 = Input.GetTouch(0).position;
                oldPositionTouch2 = Input.GetTouch(1).position;
                centerScroll = (oldPositionTouch1 + oldPositionTouch2) / 2;
            }
            else
            {
                valueDeltaScale = ((Input.GetTouch(0).position - Input.GetTouch(1).position).magnitude - (oldPositionTouch1 - oldPositionTouch2).magnitude) / (Screen.dpi * 2);
                oldPositionTouch1 = Input.GetTouch(0).position;
                oldPositionTouch2 = Input.GetTouch(1).position;
            }
        }
        else zoom = false;
        scale *= 1 - valueDeltaScale;
        scale = Mathf.Clamp(scale, clampScale.x, clampScale.y);
        changeScale -= scale;
        if (changeScale != 0)
        {
            Vector2 offsetScroll = new Vector2(centerScroll.x / Screen.width - .5f, centerScroll.y / Screen.height - .5f);
            offset -= offsetScroll * changeScale * 5;
            UpdateOffset();
        }
        Debug.Log(startTargetSize);
        targetSize = startTargetSize * scale;
        switch (type)
        {
            case ParallaxMoveType.Press:
                if (Input.touchCount < 2)
                {
                    if (Input.GetMouseButtonDown(0))
                        if (UnityEngine.EventSystems.EventSystem.current == null || UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject == null)
                        {
                            oldMousePosition = Input.mousePosition;
                            startMove = offset;
                            move = true;
                        }
                    if (move && Input.GetMouseButton(0) && /*(SceneManager.Current == null || SceneManager.Current.State == SceneState.FindObjects) &&*/ !fixedPosition)
                    {
                        Vector2 delta = Input.mousePosition - oldMousePosition;
                        delta = new Vector2(delta.x / Screen.width, delta.y / Screen.height) * speedOffset * scale;
                        offset += delta;
                        UpdateOffset();
                        oldMousePosition = Input.mousePosition;
                        startInertia = inertia = delta;
                    }
                    if (Input.GetMouseButtonUp(0))
                    {
                        move = false;
                        startMove = offset;
                    }
                }
                break;
            case ParallaxMoveType.Tracking:
                if (moveCoroutine != null)
                    StopCoroutine(moveCoroutine);
                Vector2 target = Input.mousePosition;
                if (targetPointMove != Vector2.zero)
                    target = targetPointMove;
                else
                {
                    target.x -= Screen.width / 2;
                    target.x /= Screen.width / 2;
                    target.y -= Screen.height / 2;
                    target.y /= Screen.height / 2;
                    target = -target;
                }
                if ((target - offset).magnitude > .001f)
                    moveCoroutine = StartCoroutine(MoveToPosition(target, (target - offset).magnitude, true));
                break;
        }
        if (inertia != Vector2.zero && !move)
        {
            offset += inertia;
            UpdateOffset();
            if (inertia.x > 0)
            {
                inertia.x -= Time.deltaTime * startInertia.x * 2;
                if (inertia.x < 0)
                    inertia.x = 0;
            }
            else if (inertia.x < 0)
            {
                inertia.x -= Time.deltaTime * startInertia.x * 2;
                if (inertia.x > 0)
                    inertia.x = 0;
            }
            if (inertia.y > 0)
            {
                inertia.y -= Time.deltaTime * startInertia.y * 2;
                if (inertia.y < 0)
                    inertia.y = 0;
            }
            else if (inertia.y < 0)
            {
                inertia.y -= Time.deltaTime * startInertia.y * 2;
                if (inertia.y > 0)
                    inertia.y = 0;
            }
        }
        if (startSize != 0)
        {
            float deltaSize = startSize - cam.orthographicSize;
            transform.position = startPosition + new Vector2(deltaSize * Screen.width / Screen.height, deltaSize).Multiply(-offset).ToVector3();
        }
    }

    void UpdateOffset()
    {
        offset.x = Mathf.Clamp(offset.x, -1, 1);
        offset.y = Mathf.Clamp(offset.y, -1, 1);
        foreach (ParallaxElement element in elements)
            element.Move(offset, targetSize - cam.orthographicSize);
    }

    public IEnumerator MoveToPosition(Vector2 targetPosition, float time, bool linearly = false)
    {
        fixedPosition = true;
        float maxTime = time;
        targetPosition.x = Mathf.Clamp(targetPosition.x, -1, 1);
        targetPosition.y = Mathf.Clamp(targetPosition.y, -1, 1);
        Vector2 startOffset = offset;
        while (time > 0)
        {
            if (!linearly)
                offset = startOffset + (targetPosition - startOffset) * curve.Evaluate(1 - time / maxTime);
            else offset = startOffset + (targetPosition - startOffset) * Time.deltaTime * 5;
            UpdateOffset();
            time -= Time.deltaTime;
            yield return null;
        }
        fixedPosition = false;
    }
}

[System.Serializable]
public class ParallaxElement
{
    public Transform obj;
    public Vector2 offset;

    public ParallaxElement(Transform obj)
    {
        this.obj = obj;
    }

    public void Move(Vector2 target, float difference)
    {
        obj.transform.localPosition = new Vector3(offset.x * target.x, (offset.y + difference) * target.y, obj.transform.localPosition.z);
    }
}
