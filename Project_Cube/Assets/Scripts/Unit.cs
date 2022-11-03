using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    [SerializeField] Zone zone = null;
    [SerializeField] float height = 1;

    public void Selected(Transform parent)
    {
        transform.parent = parent;
        zone.Set();
    }

    public void Free() {
        transform.parent = null;
        zone.Clear();
    }

    public float GetSize() {
        return height;
    }

}
