using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GUIDefault : GUIFullScreen {

    [SerializeField] Rigidbody rbCameraSet;
    [SerializeField] Camera physicsCamera;

    float _referenceAngle;
    float _moveAmount;


    private void Start()
    {
        _moveAmount = -1;
        rbCameraSet = GameObject.FindWithTag("CameraSet").GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        GameManager.touch = false;

        if (Input.GetMouseButton(0))
        {
            MouseHold();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_moveAmount < 0.2f)
            {
                GameManager.touch = true;
            }
            _moveAmount = -1;
        }

    }

    void MouseHold()
    {

        if (!CalcNowAngle(out float angle)) return;

        if (_moveAmount == -1)
        {
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

        if (Physics.Raycast(ray, out hit) && !hit.collider.CompareTag("Wall"))
        {
            Vector3 v3HitPos = hit.point - hit.transform.position;
            angle = Mathf.Atan2(v3HitPos.z, v3HitPos.x) * Mathf.Rad2Deg;
            return true;

        }

        angle = 0;
        return false;
    }

    public void OpenShop() {
        UIManager.OpenGUI<GUIShop>("Shop");
    }

}
