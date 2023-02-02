using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class LevelBox : IObserver {

    [SerializeField] Image LevelAmount;

    public void UpdateData()
    {
        LevelAmount.fillAmount = EXP.Instance.GetNowExpAmount() / (float)EXP.Instance.GetNeedExpAmount();
    }
    public void Attach()
    {
        EXP.Instance.RegisterObserver(this);
        UpdateData();
    }

}
[System.Serializable]
public class LevelText : IObserver {

    [SerializeField] Text _label;

    public void UpdateData()
    {
        _label.text = EXP.Instance.GetLevel().ToString();
    }
    public void Attach()
    {
        EXP.Instance.RegisterObserver(this);
        UpdateData();
    }

}

public class GUIInforBox : GUIPopUp {

    [SerializeField] LevelBox _levelBox;
    [SerializeField] LevelText _levelText;

    [SerializeField] Text _timeHourTenText;
    [SerializeField] Text _timeHourOneText;
    [SerializeField] Text _timeMinuteText;

    void Start() {
        UIManager.Instance.InforBox = this;
        _levelBox.Attach();
        _levelText.Attach();
    }

    public void SetTimeText(float gameTime) {

        int planetTime = (int)(gameTime / 15);

        _timeHourTenText.text = (planetTime / 10).ToString();
        _timeHourOneText.text = (planetTime % 10).ToString();

        _timeMinuteText.text = (((int)((gameTime % 15) * 0.4f))).ToString();
    }
    
}
