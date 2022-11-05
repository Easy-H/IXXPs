﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public enum Theme{ 
   Green, Yellow, White
}

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }

    public bool _onMain = true;

    public static int money = 1000;
    int _unitPrice;

    public static bool touch;
    public Vector3 Axis;

    public Theme _planetTheme;

    [SerializeField] Text _moneyText;

    [SerializeField] Transform trCameraSet;
    [SerializeField] Transform trCollider;

    [SerializeField] Rigidbody rbCameraSet;

    [SerializeField] Camera physicsCamera;

    Unit _selectedUnit;

    float _referenceAngle;

    public bool _isEditing;

    float _moveAmout;

    // Use this for initialization
    void Awake() {
        _isEditing = false;
        ShopButton.shops = new List<ShopButton>();
        Instance = this;

    }

    // Update is called once per frame
    void Update() {

        _moneyText.text = money.ToString();

        touch = false;

        if (_isEditing && _selectedUnit && Input.GetMouseButton(0)) {
            
            if (Input.GetMouseButtonUp(0)) {
                _selectedUnit = null;
                return;
            }

            RaycastHit hit;
            Ray ray = physicsCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)) {

                //basic set start
                Vector3 v3HitPos = hit.point - hit.transform.position;

                _selectedUnit.transform.position = v3HitPos.normalized * 3.5f;
                _selectedUnit.transform.up = v3HitPos.normalized;

            }
            return;
        }

        if (Input.GetMouseButtonDown(0)) {
            MousePress();
        }

        if (Input.GetMouseButton(0)) {
            MouseHold();
        }

        if (Input.GetMouseButtonUp(0)) {
            if (_moveAmout < 0.2f && !_isEditing && _onMain)
                touch = true;
            //MouseRelease();
        }

    }

    void MousePress() {

        transform.eulerAngles = trCameraSet.eulerAngles;

        _moveAmout = 0;

        RaycastHit hit;
        Ray ray = physicsCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit)) {
            Vector3 v3HitPos = hit.point - hit.transform.position;

            _referenceAngle = Mathf.Atan2(v3HitPos.z, v3HitPos.x) * Mathf.Rad2Deg;

        }

    }

    void MouseHold() {

        RaycastHit hit;
        Ray ray = physicsCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit)) {

            //basic set start
            Vector3 v3HitPos = hit.point - hit.transform.position;

            //basic set end
            if (!hit.collider.CompareTag("Wall")) {

                float angle = Mathf.Atan2(v3HitPos.z, v3HitPos.x) * Mathf.Rad2Deg;

                float rotateFactor = angle - _referenceAngle;

                if (rotateFactor > 180) {
                    rotateFactor -= 360;
                }
                else if (rotateFactor < -180) {
                    rotateFactor += 360;
                }

                rbCameraSet.angularVelocity = Vector3.up * rotateFactor;

                _moveAmout += Mathf.Abs(rotateFactor);

                _referenceAngle = angle;

            }

        }

    }

    void MouseRelease() {

        RaycastHit hit;

        bool isHIt = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);

        return;

        if (isHIt && hit.collider.CompareTag("Object")) {
            //Check Edit Mode

            if (_isEditing) {
                _selectedUnit = hit.collider.gameObject.GetComponent<Unit>();

            }
            else {
                _selectedUnit = null;
                Debug.Log("Yatta");

            }

        }

    }

    public void StartEditing(Unit selected, int price) {
        _isEditing = true;
        money -= price;
        _selectedUnit = selected;
        _unitPrice = price;
        transform.eulerAngles = trCameraSet.eulerAngles;
    }

    public void EndEdit()
    {
        _isEditing = false;
        _onMain = true;
    }

    public void UndoBuy() {
        _isEditing = false;
        _onMain = true;
        Destroy(_selectedUnit.gameObject);
        money += _unitPrice;
    }

}
