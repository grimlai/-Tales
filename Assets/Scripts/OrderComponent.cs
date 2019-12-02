using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OrderElementsArray
{
    public List<Component> childs = new List<Component>();

    public Component this[int i] { get { return childs[i]; } }

    public int Count { get { return childs.Count; } }

    public void Add(Component element)
    {
        if (!childs.Contains(element))
            childs.Add(element);
    }

    public void Insert(Component element, int index)
    {
        if (!childs.Contains(element))
            childs.Insert(index, element);
    }

    public void Remove(Component obj)
    {
        childs.Remove(obj);
    }

    public void RemoveAt(int i)
    {
        childs.RemoveAt(i);
    }

    public void Clear()
    {
        childs.Clear();
    }
}

[ExecuteInEditMode]
public class OrderComponent : MonoBehaviour
{
    public OrderElementsArray childs = new OrderElementsArray();
    [HideInInspector]
    public GameObject focusObj;
    [HideInInspector]
    public int layer = 0;
    [HideInInspector]
    public int startOrder, lastOrder;
    OrderComponent parent;

    void Start()
    {
        ReadChilds();
        //Simple.StartMethodDelayFrames(() => { UpdateChilds(startOrder); }, 5);
    }

    void Update()
    {
        //if (parent == null && !Application.isPlaying)
        ReadChilds();
        UpdateChilds(startOrder);
    }

    void OnDrawGizmos()
    {
        if (focusObj == null)
            return;
        Gizmos.color = Color.red;
        Renderer rend = focusObj.GetComponent<Renderer>();
        if (rend == null)
            return;
        Bounds bounds = rend.bounds;
        Gizmos.DrawWireCube(bounds.center, bounds.size);
    }

    public void ReadChilds()
    {
        for (int i = 0; i < childs.Count; i++)
            if (childs[i] == null)
            {
                childs.RemoveAt(i);
                i--;
            }
        for (int i = 0; i < transform.childCount; i++)
            AddChild(transform.GetChild(i));
    }

    public void AddChild(Transform child, int index = -1)
    {
        OrderComponent comp = child.GetComponent<OrderComponent>();
        if (comp)
        {
            comp.parent = this;
            if (index == -1)
                childs.Add(comp);
            else childs.Insert(comp, index);
            for (int i = 0; i < child.childCount; i++)
                RemoveChild(child.GetChild(i));
            return;
        }
        Renderer renderer = child.GetComponent<Renderer>();
        if (renderer)
            if (index == -1)
                childs.Add(renderer);
            else childs.Insert(renderer, index);
        for (int i = 0; i < child.childCount; i++)
            AddChild(child.GetChild(i), index);
    }

    public void RemoveChild(Transform child)
    {
        OrderComponent comp = child.GetComponent<OrderComponent>();
        if (comp)
        {
            comp.parent = this;
            childs.Remove(comp);
            return;
        }
        Renderer renderer = child.GetComponent<Renderer>();
        if (renderer)
            childs.Remove(renderer);
        for (int i = 0; i < child.childCount; i++)
            RemoveChild(child.GetChild(i));
    }

    public int UpdateChilds(int startOrder)
    {
        this.startOrder = lastOrder = startOrder;
        for (int i = 0; i < childs.Count; i++)
        {
            Component child = childs.childs[i];
            if (child == null)
            {
                childs.RemoveAt(i);
                i--;
                continue;
            }
            if (child is OrderComponent)
                lastOrder = (child as OrderComponent).UpdateChilds(lastOrder + 1);
            if (child is Renderer)
            {
                Renderer r = child as Renderer;
                r.sortingOrder = lastOrder++;
                r.sortingLayerName = SortingLayer.layers[layer].name;
            }
        }


        return lastOrder;
    }
}
