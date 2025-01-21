using UnityEngine;

public class playermove1 : MonoBehaviour
{
    [SerializeField]
    Transform groundCheck;
    [SerializeField]
    float groundDistance;
    [SerializeField]
    LayerMask groundMask;

    [SerializeField]
    float maxSpeed, acceleration, gravity;


    CharacterController control;
    FloatingJoystick _input;
    void Start()
    {
        _input = GameObject.FindWithTag("MobileJoystick").GetComponent<FloatingJoystick>();
        control = this.GetComponent<CharacterController>();
    }
    void Update()
    {
        GroundCheck();     // output : IsGrounded
        TakeInput();       // output : x, z
        ChangeSpeed();     // output : realSpeed
        VelocityChange();  // output : move player
    }

    bool IsGrounded;
    void GroundCheck()
    {
        IsGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    float x, z;
    void TakeInput()
    {
        x = _input.Horizontal;
        z = _input.Vertical;
    }

    float realSpeed, speed;
    void ChangeSpeed()
    {
        if(IsGrounded)
        {
            if (x == 0 && z == 0)
            {
                speed = 0;
            }
            else
            {
                speed = maxSpeed;
            }
        } else
        {
            speed = 0;
        }

        if (realSpeed < speed)
        {
            realSpeed += Time.deltaTime * 20 * acceleration;

            if (realSpeed > speed)
            {
                realSpeed = speed;
            }
        }
        else if (realSpeed > speed)
        {
            realSpeed -= Time.deltaTime * 20 * acceleration;

            if (realSpeed < speed)
            {
                realSpeed = speed;
            }
        }
    }

    Vector3 gravityVelocity;
    void VelocityChange()
    {
        if(!IsGrounded)
        {
            gravityVelocity = -transform.up * gravity;
        }
        else
        {
            gravityVelocity = -transform.up;
        }
        Vector3 move = transform.right * x + transform.forward * z;
        control.Move((move + gravityVelocity) * realSpeed * Time.deltaTime);
    }
}