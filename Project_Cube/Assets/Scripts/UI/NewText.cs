using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewText : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] string _key;

    Text _text;

    Text text {
        get {
            if (_text == null)
            {
                _text = GetComponent<Text>();
                if (_text == null)
                {
                    _text = gameObject.AddComponent<Text>();
                }
            }

            return _text;
        }
    }

    void OnEnable()
    {
        SetText(_key);
    }

    public void SetText(string key)
    {

        text.text = StringManager.Instance.GetStringByKey(_key);

    }
}
