using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.U2D;

public class ShopButton : MonoBehaviour {

    [SerializeField] Image _img;

    [SerializeField] Color _canBuyColor;
    [SerializeField] Color _canNotBuyColor;

    [SerializeField] Text _needLevelText;
    [SerializeField] Text _moneyAmountText;

    [SerializeField] SpriteAtlas _atlas;
    [SerializeField] Image _unitImage;

    string _unitCode;

    int _price;
    int _needLevel;

    [SerializeField] UnityEvent _canBuyEvent;

    public bool _canBuy;
    
    public void Set(string unitCode, int price, int level)
    {
        _unitCode = unitCode;
        _price = price;
        _needLevel = level;

        SetOnScene();
    }

    public void SetOnScene()
    {
        _needLevelText.text = _needLevel.ToString();
        _moneyAmountText.text = _price.ToString();
        ShopManager.Instance.shops.Add(this);

        _unitImage.sprite = _atlas.GetSprite(UnitManager.Instance.GetSpriteKey(_unitCode));

        if (EXP.Instance._level < _needLevel)
        {
            _img.color = _canNotBuyColor;
            _needLevelText.color = Color.red;
            _canBuy = false;
            return;
        }

        _img.color = _canBuyColor;
        _needLevelText.color = Color.clear;
        _canBuy = true;

    }

    public void Shopping()
    {
        if (!_canBuy)
            return;

        if (GameManager.money < _price)
        {
            UIManager.Instance.PrintMessage("NeedMoreMoney");
            return;
        }
        
        Unit created = UnitManager.Instance.CreateUnit(_unitCode);

        created.transform.localScale = created.transform.localScale * Random.Range(.9f, 1.1f);

        GameManager.Instance.StartEditing(created, _price);

        _canBuyEvent.Invoke();


    }
}
