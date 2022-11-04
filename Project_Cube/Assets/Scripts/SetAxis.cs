using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAxis : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Set();
    }

    // Update is called once per frame
    public void Set()
    {
        transform.eulerAngles = GameManager.Instance.Axis;

    }
}
