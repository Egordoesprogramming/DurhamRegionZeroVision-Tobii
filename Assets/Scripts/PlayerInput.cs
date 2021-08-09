using UnityEngine;


public class PlayerInput : MonoBehaviour
{
    public float Acceleration
    {
        get { return m_Acceleration; }
    }
    public float Steering
    {
        get { return m_Steering; }
    }


    float m_Acceleration;
    float m_Steering;


    bool m_FixedUpdateHappened;

    private bool accelerating = false;
    private bool breaking = false;
    private bool turningLeft = false;
    private bool turningRight = false;

    public float wheelDampening;

    void Update()
    {
        GetPlayerInput();

        if (accelerating)
        {
            m_Acceleration = 1f;
            wheelDampening = 500f;

        }
        else if (breaking)
        {
            m_Acceleration = -1f;
            wheelDampening = 1000f;
        }
        else
        {
            m_Acceleration = 0f;
            wheelDampening = 5f;
        }


        if (turningLeft)
            m_Steering = -1f;
        else if (!turningLeft && turningRight)
            m_Steering = 1f;
        else
            m_Steering = 0f;


    }

    private void GetPlayerInput()
    {
        /*if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            accelerating = true;
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            accelerating = false;
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickDown, OVRInput.Controller.RTouch))
        {
            breaking = true;
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryThumbstickDown, OVRInput.Controller.RTouch))
        {
            breaking = false;
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickLeft, OVRInput.Controller.RTouch))
        {
            turningLeft = true;
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryThumbstickLeft, OVRInput.Controller.RTouch))
        {
            turningLeft = false;
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickRight, OVRInput.Controller.RTouch))
        {
            turningRight = true;
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryThumbstickRight, OVRInput.Controller.RTouch))
        {
            turningRight = false;
        }*/

        if (Input.GetKeyDown(KeyCode.W))
        {
            accelerating = true;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            accelerating = false;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            breaking = true;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            breaking = false;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            turningLeft = true;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            turningLeft = false;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            turningRight = true;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            turningRight = false;
        }
        
    }

}

