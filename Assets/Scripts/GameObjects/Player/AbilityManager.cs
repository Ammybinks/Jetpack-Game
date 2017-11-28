using UnityEngine;
using System.Collections;

public class AbilityManager : MonoBehaviour {

    public delegate void EventFlag();
    public static event EventFlag StopFiring;

    public delegate float SetFiring(float radians);
    public static event SetFiring Fire;
    public static event SetFiring BeginFiring;
    
    public delegate void ChangeInt(int val);
    public static event ChangeInt SetIndex;
    public static event ChangeInt SetMaxMana;

    public delegate void ChangeFloat(float val);
    public static event ChangeFloat ChangeMana;

    [SerializeField]
    private int abilityCount;
    
    private int ability = 0;
    
    private bool isFiring;

    private Camera mainCamera;

    private Vector3 mousePosition;

    private Rigidbody2D rb2d;

    private float mousePositionY;
    private float mousePositionX;
    private float degrees;
    private float radians;

    private float mana;

    // Use this for initialization
    void Start ()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();

        //Get and store a reference to the main camera.
        mainCamera = Camera.main;

        ManaManager.SetMana += SetMana;

        if(SetIndex != null)
        {
            SetIndex(ability);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        int temp2 = (int)Input.GetAxis("Mouse ScrollWheel");
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            ability += (int)(Input.GetAxis("Mouse ScrollWheel") * 10);

            if(ability < 0)
            {
                ability = abilityCount + ability;
            }
            if(ability > (abilityCount - 1))
            {
                ability = 0 + (ability - abilityCount);
            }
            
            if(isFiring)
            {
                isFiring = false;

                if (StopFiring != null)
                {
                    StopFiring();
                }
            }

            if (SetIndex != null)
            {
                SetIndex(ability);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            float temp = BeginFiring(FireSetup());

            if (temp > 0)
            {
                if (!isFiring)
                {
                    isFiring = true;
                }

                ChangeMana(-temp);
            }
        }
        else if (Input.GetMouseButton(0))
        {
            if (Fire != null)
            {
                float temp = Fire(FireSetup());

                if(temp > 0)
                {
                    if (!isFiring)
                    {
                        isFiring = true;
                    }

                    ChangeMana(-temp);
                }
                else if (temp == 0)
                {
                    if(isFiring)
                    {
                        isFiring = false;

                        if(StopFiring != null)
                        {
                            StopFiring();
                        }
                    }
                }
                else
                {
                    if(!isFiring)
                    {
                        isFiring = true;
                    }
                }
            }
        }
        
        if(!Input.GetMouseButton(0))
        {
            if(isFiring)
            {
                isFiring = false;

                if (StopFiring != null)
                {
                    StopFiring();
                }
            }
        }
    }
    
    private float FireSetup()
    {
        //Save the current position of the mouse in relation to the main camera.
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        mousePositionY = mousePosition.y - transform.position.y;
        mousePositionX = mousePosition.x - transform.position.x;

        //Get the angle between the mouse and camera in radians.
        radians = (Mathf.Atan2(mousePositionY, mousePositionX)); 
        
        return radians;
    }

    private void SetMana(float change)
    {
        if(change == 0)
        {
            if (isFiring)
            {
                isFiring = false;

                if (StopFiring != null)
                {
                    StopFiring();
                }
            }
        }
    }
}
