using UnityEngine;

public class Follow : MonoBehaviour {

    Transform targetTransform;
    
    void Start()
    {
        HealthManager.EndGame += EndGame;

        targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

	// Update is called once per frame
	void FixedUpdate () {
        if(targetTransform)
        {
            transform.position = new Vector3(targetTransform.position.x, targetTransform.position.y, transform.position.z);
        }
	}

    void EndGame()
    {
        transform.position = new Vector3(0, 0, -10);
        GetComponent<Camera>().orthographicSize = 10;
    }
}
