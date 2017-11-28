using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthText : MonoBehaviour
{
    Text text;

    [SerializeField]
    private int value;
    [SerializeField]
    private int maxValue;

	// Use this for initialization
	void Awake () {
        text = GetComponent<Text>();
        
        TempHurt.SetHealth += SetValue;
        TempHurt.SetMaxHealth += SetMaxValue;
    }

    private void SetText()
    {
        text.text = value + "/" + maxValue;
    }

    private void SetValue(int change)
    {
        value = change;

        SetText();
    }

    private void SetMaxValue(int change)
    {
        maxValue = change;

        SetText();
    }
}
