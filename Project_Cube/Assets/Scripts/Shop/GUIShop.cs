using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;


public class GUIShop : GUIPopUp {

    static readonly float GridSize = 0.4f;

    [SerializeField] ShopButton _shopUnit;

    [SerializeField] RectTransform _shopUnitContainer;

    // Start is called before the first frame update
    void Start()
    {
        SetShop();
    }
    public void LevelUp()
    {

    }

    void SetShop()
    {
        int count = ShopManager.Instance._list.Count;

        SetGrid(count * GridSize);

        float gridSize = 1f / count;

        for (int i = 0; i < count; i++) {
            ShopButton shopButton = CreateShopUnit(i * gridSize, (i + 1) * gridSize);
            ShopManager.ShopData shopData = ShopManager.Instance._list[i];

            shopButton.Set(shopData.unitCode, shopData.price, shopData.level);
        }

    }

    void SetGrid(float size)
    {
        _shopUnitContainer.anchorMin = Vector2.zero;
        _shopUnitContainer.anchorMax = new Vector2(size, 1);

    }

    ShopButton CreateShopUnit(float startAt, float endAt)
    {
        ShopButton shopUnit = Instantiate(_shopUnit, _shopUnitContainer);

        RectTransform rect = shopUnit.GetComponent<RectTransform>();

        rect.anchorMin = new Vector2(startAt, 0);
        rect.anchorMax = new Vector2(endAt, 1);

        return shopUnit;

    }

}