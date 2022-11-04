using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    [SerializeField] MonoBehaviour[] _onActive;

    [SerializeField] EarnData _earnData;

    //[SerializeField] Zone zone = null;
    [SerializeField] float _height = 1;

    [SerializeField] bool _activeOnDay;
    [SerializeField] int _earnMoney;
    [SerializeField] int _earnExp;

    private void LateUpdate()
    {
        bool state = (TimeSet.Instance.IsDay() == _activeOnDay);
        
        for (int i = 0; i < _onActive.Length; i++)
        {
            _onActive[i].enabled = state;

        }

        if (state) {
            if (GameManager.touch) {
                GameManager.money += _earnMoney;
                EXP.Instance.AddExp(_earnExp);
                if (_earnData)
                    _earnData.PopUp(_earnMoney, _earnExp);
            }
        }

    }

    public void Selected(Transform parent)
    {
        transform.parent = parent;
        //zone.Set();
    }

    public void Free() {
        transform.parent = null;
        //zone.Clear();
    }

    public float GetSize() {
        return _height;
    }

}
