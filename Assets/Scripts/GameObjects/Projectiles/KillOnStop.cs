using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOnStop : MonoBehaviour {

	// Use this for initialization
	void Awake ()
    {
        AbilityManager.StopFiring += StopFiring;
    }
	
    void StopFiring()
    {
        AbilityManager.StopFiring -= StopFiring;

        Destroy(gameObject);
    }
}
