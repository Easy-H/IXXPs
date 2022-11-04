using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    [SerializeField] MonoBehaviour[] _onDays;
    [SerializeField] MonoBehaviour[] _onNights;

    [SerializeField] Zone zone = null;
    [SerializeField] float height = 1;

    private void Update()
    {
        bool state = TimeSet.Instance.IsDay();
        for (int i = 0; i < _onDays.Length; i++)
        {
            _onDays[i].enabled = state;

        }
    
        for (int i = 0; i<_onNights.Length; i++)
        {
            _onNights[i].enabled = !state;

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
