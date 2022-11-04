using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;

public class TimeSet : MonoBehaviour {

    [SerializeField] Transform _target;
    [SerializeField] SpriteRenderer _background;
    [SerializeField] Light _light;

    [SerializeField] float _maxLight;
    [SerializeField] float _minLight;

    [SerializeField] Text _timeText;

    float _alpha;

    public static TimeSet Instance { get; private set; }

    public float _gameTime;

    private void Start() {
        Instance = this;
    }

    private void Update() {
        
        Vector3 v = _target.forward;

        float realTime = (float)(System.DateTime.Now.Hour * 15 + System.DateTime.Now.Minute * 0.25);

        float timeAngle = Mathf.Atan2(v.x, v.z) * Mathf.Rad2Deg + realTime;
        
        if (timeAngle > 180) timeAngle -= 360;
        else if (timeAngle < -180) timeAngle += 360;

        _gameTime = (timeAngle + 360) % 360;
        _timeText.text = ((int)_gameTime / 15).ToString("00") + ":" + (((int)((_gameTime % 15) * 0.4f)) * 10).ToString("00");

        Color color = _background.color;

        _alpha = (Mathf.Abs(timeAngle) - 60f) / 60;

        _background.color = new Color(color.r, color.g, color.b, Mathf.Clamp(_alpha, 0.2f, 0.9f));

        _light.intensity = Mathf.Clamp(_alpha, _minLight, _maxLight);

    }

    public bool IsDay() {
        if (_alpha > 0) return true;
        return false;

    }

}
