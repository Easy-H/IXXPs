using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class ShopButton : MonoBehaviour {

    [SerializeField] Color _canBuyColor;
    [SerializeField] Color _canNotBuyColor;

    public static List<ShopButton> shops = new List<ShopButton>();

    [SerializeField] int _needLevel;

    [SerializeField] Unit _obj;
    [SerializeField] int _price;

    [SerializeField] UnityEvent _canBuyEvent;
    [SerializeField] UnityEvent _canNotBuyEvent;

    [SerializeField] Image _img;

    public bool _canBuy;

    private void Start()
    {
        Set();
        shops.Add(this);
    }

    public static void LevelUp()
    {
        for (int i = 0; i < shops.Count; i++)
        {
            shops[i].Set();
        }
    }

    void Set()
    {
        if (EXP.Instance._level + 1 < _needLevel)
        {
            _img.color = _canNotBuyColor;
            _canBuy = false;
            return;
        }
        _img.color = _canBuyColor;
        _canBuy = true;
    }

    public void Shooping()
    {
        if (!_canBuy)
            return;

        if (GameManager.money < _price)
        {
            _canNotBuyEvent.Invoke();
            return;
        }
        Unit created = Instantiate(_obj);

        created.transform.localScale = created.transform.localScale * Random.Range(.9f, 1.1f);

        GameManager.Instance.StartEditing(created, _price);

        _canBuyEvent.Invoke();


    }
}
