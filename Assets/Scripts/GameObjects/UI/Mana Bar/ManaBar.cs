using UnityEngine;
using System.Collections;

public class ManaBar : MonoBehaviour
{

    float size;

    private float maxValue;

    public float value;

    float maxSize;

    new RectTransform transform;

    Vector2 temp;


    // Use this for initialization
    void Awake()
    {
        transform = (RectTransform)gameObject.transform;

        maxSize = (transform.parent.transform as RectTransform).sizeDelta.x;
        size = (transform.parent.transform as RectTransform).sizeDelta.x;

        ManaManager.SetMana += SetValue;
        ManaManager.SetMaxMana += SetMaxValue;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void SetWidth(float value, float inMax, float outMax)
    {
        size = (value - 0) * (outMax - 0) / (inMax - 0) + 0;

        if (size > maxSize)
        {
            size = maxSize;
        }
        if (size < 0)
        {
            size = 0;
        }

        temp = transform.sizeDelta;

        temp.x = size;
        temp.y = size;

        transform.sizeDelta = temp;
    }

    private void SetValue(float change)
    {
        value = change;

        SetWidth(value, maxValue, maxSize);
    }

    private void SetMaxValue(float change)
    {
        maxValue = change;

        SetWidth(value, maxValue, maxSize);
    }
}
