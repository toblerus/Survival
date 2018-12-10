using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour {

    GameObject northpole;

	void Start () {

        northpole = GameObject.Find("NorthPole");
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 targetPosition = new Vector3(northpole.transform.position.x, transform.position.y, northpole.transform.position.z);
        transform.LookAt(targetPosition);
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
    }
}
