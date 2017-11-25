using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthText : MonoBehaviour
{
    Text text;

    [SerializeField]
    private float value;
    [SerializeField]
    private float maxValue;

	// Use this for initialization
	void Awake () {
        text = GetComponent<Text>();
        
        HealthManager.SetHealth += SetValue;
        HealthManager.SetMaxHealth += SetMaxValue;
    }

    private void SetText()
    {
        text.text = value + "/" + maxValue;
    }

    private void SetValue(float change)
    {
        value = change;

        SetText();
    }

    private void SetMaxValue(float change)
    {
        maxValue = change;

        SetText();
    }
}
