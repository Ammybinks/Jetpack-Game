using UnityEngine;
using System.Collections;

public class Expand : MonoBehaviour {

    public float XExpand;
    public float YExpand;

    Vector3 tempScale;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        tempScale = gameObject.transform.localScale;

        tempScale.x += XExpand;
        tempScale.y += YExpand;

        gameObject.transform.localScale = tempScale ;
	}
}
