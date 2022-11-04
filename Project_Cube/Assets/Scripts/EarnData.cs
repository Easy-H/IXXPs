using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EarnData : MonoBehaviour
{

    [SerializeField] Text _earnedMoney;
    [SerializeField] Text _earnedExp;

    [SerializeField] Transform _standard;

    public Vector3 _startPos;
    public Vector3 _up;

    private void Start()
    {
        _startPos = transform.localPosition;
        _up = _standard.up;
        gameObject.SetActive(false);
    }

    public void PopUp(int money, int exp) {
        StopAllCoroutines();

        transform.localPosition = _startPos;
        gameObject.SetActive(true);

        _earnedMoney.text = money.ToString();
        _earnedExp.text = exp.ToString();

        Color moneyTextColor = _earnedMoney.color;
        Color expTextColor = _earnedExp.color;

        moneyTextColor = new Color(moneyTextColor.r, moneyTextColor.g, moneyTextColor.b, 1);
        expTextColor = new Color(expTextColor.r, expTextColor.g, expTextColor.b, 1);

        _earnedMoney.color = moneyTextColor;
        _earnedExp.color = expTextColor;

        StartCoroutine(Disapear());

    }

    IEnumerator Disapear() {
        float spendTime = 0;
        while (spendTime < 1f) {
            transform.LookAt(Camera.main.transform);
            spendTime += Time.deltaTime;

            float alpha = Mathf.Lerp(2f, 0, spendTime);

            Color moneyTextColor = _earnedMoney.color;
            Color expTextColor = _earnedExp.color;

            transform.position += _up * Time.deltaTime;

            moneyTextColor = new Color(moneyTextColor.r, moneyTextColor.g, moneyTextColor.b, alpha);
            expTextColor = new Color(expTextColor.r, expTextColor.g, expTextColor.b, alpha);

            _earnedMoney.color = moneyTextColor;
            _earnedExp.color = expTextColor;

            yield return null;
        }

        gameObject.SetActive(false);
    }


}
