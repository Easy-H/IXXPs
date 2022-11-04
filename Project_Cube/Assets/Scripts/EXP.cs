using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EXP : MonoBehaviour
{
    public static EXP Instance { get; private set; }

    [SerializeField] Text _levelText;
    public int _level;

    [SerializeField] int[] _needExp;
    int _expAmount = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void AddExp(int amount) {
        _expAmount += amount;
        if (_needExp[_level] <= _expAmount) {
            _level++;
            _levelText.text = (_level + 1).ToString();
            ShopButton.LevelUp();
        }
    }

}
