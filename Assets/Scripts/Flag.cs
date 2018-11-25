using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour {
    Transform cloth;
	// Use this for initialization
	void Start () {
        cloth = transform.Find("Cloth");
	}
	
	// Update is called once per frame
	void Update () {
        int direct = Time.time % 1 - 0.5f >= 0 ? -1 : 1;
        cloth.transform.Rotate(direct*Vector3.forward*0.3f);
	}
}
