using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class ShopButton : MonoBehaviour
{
    [SerializeField] Unit _obj;
    [SerializeField] int _price;

    public void Shooping()
    {
        Unit created = Instantiate(_obj);
        GameManager.money -= _price;

        GameManager.Instance.StartEditing(created);

    }
}
