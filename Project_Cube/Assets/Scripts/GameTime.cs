﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTime : MonoBehaviour {

    Transform _target;
    [SerializeField] SpriteRenderer _background;
    [SerializeField] Light _light;

    [SerializeField] float _maxLight;
    [SerializeField] float _minLight;

    float _alpha;

    public static GameTime Instance { get; private set; }

    public float _gameTime;

    private void Start() {
        Instance = this;
        _target = GameObject.FindWithTag("CamersSet").transform;
    }

    private void Update() {
        
        Vector3 v = _target.forward;

        float realTime = (float)(DateTime.Now.Hour * 15 + DateTime.Now.Minute * 0.25);

        float timeAngle = Mathf.Atan2(v.x, v.z) * Mathf.Rad2Deg + realTime;
        
        if (timeAngle > 180) timeAngle -= 360;
        else if (timeAngle < -180) timeAngle += 360;

        _gameTime = (timeAngle + 360) % 360;

        UIManager.Instance.InforBox.SetTimeText(_gameTime);

        Color color = _background.color;

        _alpha = (Mathf.Abs(timeAngle) - 60f) / 60;

        _background.color = new Color(color.r, color.g, color.b, Mathf.Clamp(_alpha, 0.2f, 0.8f));

        _light.intensity = Mathf.Clamp(_alpha, _minLight, _maxLight);

    }

    public bool IsDay() {
        if (_alpha > 0) return true;
        return false;

    }

}