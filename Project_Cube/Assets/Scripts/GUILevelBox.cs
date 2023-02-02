using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class LevelAmountText : IObserver {

    [SerializeField] Text _label;

    public void UpdateData()
    {
        _label.text = EXP.Instance.GetNowExpAmount().ToString() + " / " + EXP.Instance.GetNeedExpAmount().ToString();
    }

    public void Attach() {
        EXP.Instance.RegisterObserver(this);
        UpdateData();
    }

}

[System.Serializable]
public class LevelTextLabel : IObserver {

    [SerializeField] Text _label;

    public void UpdateData()
    {
        _label.text = "level " + EXP.Instance.GetLevel().ToString();
    }
    public void Attach()
    {
        EXP.Instance.RegisterObserver(this);
        UpdateData();
    }

}

public class GUILevelBox : GUIPopUp
{
    [SerializeField] LevelTextLabel _levelTextLabel;
    [SerializeField] LevelAmountText _levelAmountText;
    [SerializeField] LevelBox _levelBox;

    // Start is called before the first frame update
    void Start()
    {
        _levelTextLabel.Attach();
        _levelAmountText.Attach();
        _levelBox.Attach();
    }

    public override void Close()
    {
        EXP.Instance.RemoveObserver(_levelTextLabel);
        EXP.Instance.RemoveObserver(_levelAmountText);
        EXP.Instance.RemoveObserver(_levelBox);
        base.Close();
    }

}
