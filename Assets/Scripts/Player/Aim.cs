using UnityEngine;
using System.Collections;

public class Aim : MonoBehaviour {

    public Camera mainCamera;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 rotation = transform.eulerAngles;
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        float mousePositionY = mousePosition.y - transform.position.y;
        float mousePositionX = mousePosition.x - transform.position.x;

        rotation.z = (Mathf.Atan2(mousePositionY, mousePositionX) * (180 / Mathf.PI) + 90);

        transform.eulerAngles = rotation;
	}
}
