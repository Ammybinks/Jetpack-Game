using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ManaText : MonoBehaviour
{
    Text text;
    
    private float value;

    private float maxValue;

    // Use this for initialization
    void Awake()
    {
        text = GetComponent<Text>();

        ManaManager.SetMana += SetValue;
        ManaManager.SetMaxMana += SetMaxValue;
    }

    private void SetText()
    {
        text.text = value + "\n" + maxValue;
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
