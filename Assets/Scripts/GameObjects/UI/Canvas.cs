using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        HealthManager.EndGame += EndGame;
	}
	
    void EndGame()
    {
        gameObject.SetActive(false);
    }
}
