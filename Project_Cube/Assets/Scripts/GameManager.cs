using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public enum Theme {
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
    [SerializeField] Rigidbody rbCameraSet;

    Camera physicsCamera;

    Unit _selectedUnit;

    float _referenceAngle;
    float _moveAmount;

    // Use this for initialization
    void Awake()
    {
        ShopButton.shops = new List<ShopButton>();
        Instance = this;

    }

    private void Start()
    {
        physicsCamera = gameObject.GetComponent<Camera>();
        _moveAmount = -1;
    }

    // Update is called once per frame
    void Update()
    {

        _moneyText.text = money.ToString();

        touch = false;

        if (_selectedUnit && Input.GetMouseButton(0))
        {

            RaycastHit hit;
            Ray ray = physicsCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {

                //basic set start
                Vector3 v3HitPos = hit.point - hit.transform.position;

                _selectedUnit.transform.position = v3HitPos.normalized * 3.5f;
                _selectedUnit.transform.up = v3HitPos.normalized;

            }
            return;
        }

        if (Input.GetMouseButton(0))
        {
            MouseHold();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_moveAmount < 0.2f && !_selectedUnit && _onMain)
                touch = true;
            _moveAmount = -1;
        }

    }

    void MouseHold() {

        if (!CalcNowAngle(out float angle)) return;

        if (_moveAmount == -1) {
            _referenceAngle = angle;
            _moveAmount = 0;
        }

        float rotateFactor = angle - _referenceAngle;

        if (rotateFactor > 180) rotateFactor -= 360;
        else if (rotateFactor < -180) rotateFactor += 360; 

        rbCameraSet.angularVelocity = Vector3.up * rotateFactor;
        _moveAmount += Mathf.Abs(rotateFactor);

        _referenceAngle = angle;

    }

    bool CalcNowAngle(out float angle)
    {
        RaycastHit hit;
        Ray ray = physicsCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit) && !hit.collider.CompareTag("Wall")) {
            Vector3 v3HitPos = hit.point - hit.transform.position;
            angle = Mathf.Atan2(v3HitPos.z, v3HitPos.x) * Mathf.Rad2Deg;
            return true;

        }

        angle = 0;
        return false;
    }

    public void StartEditing(Unit selected, int price)
    {
        _selectedUnit = selected;
        money -= price;
        _unitPrice = price;
        transform.eulerAngles = trCameraSet.eulerAngles;
    }

    public void EndEdit()
    {
        _selectedUnit = null;
        _onMain = true;
    }

    public void UndoBuy()
    {
        Destroy(_selectedUnit.gameObject);
        EndEdit();
        money += _unitPrice;
    }

}
