using UnityEngine;
using System.Collections;

public class Collide : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D collidedObject)
    {
        // If the player hits the trigger...
        if (collidedObject.gameObject.tag != "Player" && collidedObject.gameObject.tag != "Projectile")
        {
            Destroy(gameObject);
        }
    }
}
