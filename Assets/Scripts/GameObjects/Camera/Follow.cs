using UnityEngine;

public class Follow : MonoBehaviour {

    public GameObject target;
    
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
	}
}
