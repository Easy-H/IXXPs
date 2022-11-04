using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class ShopButton : MonoBehaviour
{

    public static List<ShopButton> shops = new List<ShopButton>();

    [SerializeField] int _needLevel;

    [SerializeField] Unit _obj;
    [SerializeField] int _price;

    [SerializeField] UnityEvent[] _canBuyEvent;
    [SerializeField] UnityEvent[] _canNotBuyEvent;

    [SerializeField] RawImage _img;

    public bool _canBuy;

    private void Start()
    {
        Set();
        shops.Add(this);
    }

    public static void LevelUp() {
        for (int i = 0; i < shops.Count; i++)
        {
            shops[i].Set();
        }
    }

    void Set() {
        if (EXP.Instance._level + 1 < _needLevel) {
            _img.color = Color.gray;
            _canBuy = false;
            return;
        }
        _img.color = Color.white;
        _canBuy = true;
    }

    public void Shooping()
    {
        if (!_canBuy)
            return;

        if (GameManager.money < _price) {
            for (int i = 0; i < _canNotBuyEvent.Length; i++) {
                _canNotBuyEvent[i].Invoke();
            }
            return;
        }
        Unit created = Instantiate(_obj);

        GameManager.money -= _price;
        GameManager.Instance.StartEditing(created);


        for (int i = 0; i < _canBuyEvent.Length; i++)
        {
            _canBuyEvent[i].Invoke();
        }

    }
}
