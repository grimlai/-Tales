using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(OrderComponent))]
public class OrderComponentEditor : Editor
{

    OrderComponent Target { get { return target as OrderComponent; } }

    void Awake()
    {
        Selection.selectionChanged = HierarchyChanged;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUILayout.Label("", GUILayout.MinHeight((Target.childs.Count - 1) * 20));
        if (GUILayout.Button("Обновить"))
            Target.ReadChilds();
        Target.startOrder = EditorGUILayout.IntField("Начальное положение", Target.startOrder);
        string[] layers = new string[SortingLayer.layers.Length];
        for (int i = 0; i < SortingLayer.layers.Length; i++)
            layers[i] = SortingLayer.layers[i].name;
        int layerValue = EditorGUILayout.Popup(Target.layer, layers);
        foreach (SortingLayer layer in SortingLayer.layers)
        {
            if (layerValue == layer.value - SortingLayer.layers[0].value)
                Target.layer = layerValue;
        }
        //Target.layer = layers[EditorGUILayout.Popup(Target.layer.value, layers)];
        if (Event.current.type == EventType.MouseDrag || Event.current.type == EventType.MouseDown || Event.current.type == EventType.MouseUp)
            Repaint();
    }

    void HierarchyChanged()
    {
        Target.focusObj = null;
        //HierarchyDraw.focusindex = 0;
        EditorApplication.RepaintHierarchyWindow();
        SceneView.RepaintAll();
    }
}

[CustomPropertyDrawer(typeof(OrderElementsArray))]
public class OrderComponentPropertyDrawer : PropertyDrawer
{
    int indexDrag = -1;
    float heightRect = 18;
    float step = 20;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        OrderComponent parent = property.serializedObject.targetObject as OrderComponent;
        OrderElementsArray childs = parent.childs;
        int sch = 0;

        Rect currentRect = position;
        currentRect.height = heightRect;
        for (int i = 0; i < childs.Count; i++)
        {
            if (Event.current.type == EventType.MouseDown && currentRect.Contains(Event.current.mousePosition))
            {
                indexDrag = sch;
                parent.focusObj = childs[i].gameObject;
                //HierarchyDraw.focusindex = childs[i].gameObject.GetInstanceID();
                EditorApplication.RepaintHierarchyWindow();
                SceneView.RepaintAll();
            }
            Component component = childs[i];
            if (indexDrag == sch)
            {
                currentRect.y += step;
                sch++;
                continue;
            }
            if (component is OrderComponent)
                DrawChild(component as OrderComponent, currentRect);
            else DrawElement(component, currentRect);
            currentRect.y += step;
            sch++;
        }
        float mouseY = Mathf.Clamp(Event.current.mousePosition.y - heightRect / 2, position.y, position.y + step * (childs.Count - 1));
        if (indexDrag != -1 && position.y != 0)
        {
            Rect tempRect = new Rect(position.x, mouseY, position.width, heightRect);
            DrawElement(childs[indexDrag], tempRect);
            float value = (mouseY - position.y) - indexDrag * step;
            if (Mathf.Abs(value) >= step)
            {
                int change = (int)(value / step);
                property.FindPropertyRelative("childs").MoveArrayElement(indexDrag, indexDrag + change);
                indexDrag += change;
                EditorUtility.SetDirty(property.serializedObject.targetObject);
            }
        }
        if (Event.current.type == EventType.MouseUp)
            indexDrag = -1;

        EditorGUI.EndProperty();
    }

    void DrawElement(Component obj, Rect rect)
    {
        GUI.Box(rect, "");
        EditorGUI.LabelField(rect, obj.name);
        SpriteRenderer rend = obj as SpriteRenderer;
        if (rend != null && rend.sprite != null)
            GUI.DrawTexture(new Rect(rect.width - 3, rect.y + 1, heightRect - 2, heightRect - 2), rend.sprite.texture, ScaleMode.StretchToFill, true, 0, rend.color, 0, 0);
    }

    void DrawChild(OrderComponent child, Rect rect)
    {
        EditorGUI.LabelField(rect, child.name);
    }
}