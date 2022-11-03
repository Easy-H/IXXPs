using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    [SerializeField] MonoBehaviour _onDay;
    [SerializeField] MonoBehaviour _onNight;

    [SerializeField] Zone zone = null;
    [SerializeField] float height = 1;

    private void Update()
    {
        if (_onDay) {
            if (TimeSet.Instance.IsDay()) _onDay.enabled = true;
            else _onDay.enabled = false;
        }
        if (_onNight)
        {
            if (TimeSet.Instance.IsDay()) _onNight.enabled = false;
            else _onNight.enabled = true;
        }
    }

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
