using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    float width;
    
    [SerializeField]
    private float maxValue;

    [SerializeField]
    public float value;

    float maxWidth;

    new RectTransform transform;

    Vector2 temp;


    // Use this for initialization
    void Awake () {
        transform = (RectTransform)gameObject.transform;

        maxWidth = (transform.parent.transform as RectTransform).sizeDelta.x;
        width = (transform.parent.transform as RectTransform).sizeDelta.x;

        HealthManager.SetHealth += SetValue;
        HealthManager.SetMaxHealth += SetMaxValue;
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    private void SetWidth(float value, float inMax, float outMax)
    {
        width = (value - 0) * (outMax - 0) / (inMax - 0) + 0;

        if (width > maxWidth)
        {
            width = maxWidth;
        }
        if (width < 0)
        {
            width = 0;
        }

        temp = transform.sizeDelta;

        temp.x = width;

        transform.sizeDelta = temp;
    }
    
    private void SetValue(float change)
    {
        value = change;
        
        SetWidth(value, maxValue, maxWidth);
    }

    private void SetMaxValue(float change)
    {
        maxValue = change;

        SetWidth(value, maxValue, maxWidth);
    }
}
