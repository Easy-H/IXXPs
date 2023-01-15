using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIMessageBox : MonoBehaviour
{
    [SerializeField] NewText _textField;

    public void SetMessage(string key)
    {
        _textField.SetText(key);
    }
}
