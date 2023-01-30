using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EXP : MonoBehaviour
{
    public static EXP Instance { get; private set; }

    [SerializeField] Text _levelText;
    [SerializeField] Image _fillNextLevel;
    public int _level = 1;

    [SerializeField] int[] _needExp;
    int _expAmount = 0;

    private void Awake()
    {
        Instance = this;
        float t = (float)(_expAmount - _needExp[_level - 1]) / (float)(_needExp[_level] - _needExp[_level - 1]);

        _fillNextLevel.fillAmount = t;
    }

    public void AddExp(int amount) {
        _expAmount += amount;

        if (_needExp[_level] <= _expAmount) {
            _level++;
            _levelText.text = (_level).ToString();
        }

        float t = (float)(_expAmount - _needExp[_level - 1]) / (float)(_needExp[_level] - _needExp[_level - 1]);

        _fillNextLevel.fillAmount = t;
    }

}
