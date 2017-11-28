using UnityEngine;
using System.Collections;

public class AbilitySelector : MonoBehaviour {

    private Vector3 origin;

    private Vector3 temp;

    [SerializeField]
    private float stepSize;

	// Use this for initialization
	void Awake ()
    {
        origin = (transform as RectTransform).anchoredPosition;

        AbilityManager.SetIndex += SetPosition;
    }
	
    void SetPosition(int val)
    {
        temp = origin;

        temp.x += stepSize * val;

        (transform as RectTransform).anchoredPosition = temp;
    }
}
