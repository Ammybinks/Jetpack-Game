using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

    GameObject player;

	// Use this for initialization
	void Awake () {
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
	}
}
