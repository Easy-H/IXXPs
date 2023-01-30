using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    [SerializeField] Vector3 rotateFactor;
    
    
	// Update is called once per frame
	void Update () {
		//if (!GameManager.Instance._isEditing)
			transform.Rotate (rotateFactor * Time.deltaTime * GameManager.TimeScale);

	}
}
