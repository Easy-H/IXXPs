using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAxis : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.eulerAngles = GameManager.Instance.Axis;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
