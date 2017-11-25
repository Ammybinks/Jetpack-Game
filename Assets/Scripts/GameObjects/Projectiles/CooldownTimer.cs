using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CooldownTimer : MonoBehaviour {

    [HideInInspector]
    public float timer = 0;

    Text text;

    // Use this for initialization
    void Awake () {
        text = gameObject.GetComponent<Text>();
	}

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if (timer < 0)
        {
            timer = 0;
        }

        text.text = "Cooldown: " + (Mathf.Round(timer));
    }
}
