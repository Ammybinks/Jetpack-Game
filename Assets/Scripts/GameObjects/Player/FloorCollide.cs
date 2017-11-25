using UnityEngine;
using System.Collections;

public class FloorCollide : MonoBehaviour {

    GameObject parent;

    MovementManager moveScript;

	// Use this for initialization
	void Start () {
        parent = gameObject.transform.parent.gameObject;

        moveScript = (MovementManager)parent.GetComponent("Physics Move");
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position = parent.transform.position;
	}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Floor")
    //    {
    //        moveScript.TempJumpSpeed = moveScript.JumpSpeed;

    //        moveScript.Floored = true;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Floor")
    //    {
    //        moveScript.Floored = false;

    //        if (moveScript.TempJumpSpeed == moveScript.JumpSpeed)
    //        {
    //            moveScript.TempJumpSpeed = 0;
    //        }
    //    }
    //}
}
