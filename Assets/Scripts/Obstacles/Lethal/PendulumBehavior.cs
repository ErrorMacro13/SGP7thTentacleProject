using UnityEngine;
using System.Collections;

public class PendulumBehavior : MonoBehaviour
{
    BoxCollider2D pendulumCollider;
    public Rigidbody2D player;

    public float motorSpeed = 120f;
    float CurrGameSpeed = 1.0f;
    bool nonlethal;
    bool isCatapult;
    bool collision;
    Vector2 direction;

    public HingeJoint2D hinge;
    JointMotor2D motor;
    bool swingingLeft;
    bool enabled = false;
    // Use this for initialization
    void Start()
    {
        hinge = gameObject.GetComponent<HingeJoint2D>();
        //motor = hinge.motor;
        motor.maxMotorTorque = 5000;
        motor.motorSpeed = 120;
        swingingLeft = true;
        direction = new Vector2(50, 0);
        pendulumCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
     
           
            if ((hinge.jointAngle >= hinge.limits.max && swingingLeft) || (hinge.jointAngle <= hinge.limits.min && !swingingLeft))
            {
                swingingLeft = !swingingLeft;
                motorSpeed = -motorSpeed;
            }



            if (nonlethal)
            {
                pendulumCollider.isTrigger = false;
                tag = "Ground";
                //if(hinge.jointAngle >= hinge.limit)
                //{

                //}
            }

            if (!collision && CurrGameSpeed > 0.25f && !nonlethal)
            {
                pendulumCollider.isTrigger = true;
                tag = "Lethal";
            }

            if (collision && CurrGameSpeed > 0.25)
            {
                isCatapult = true;
            }
            else
            {
                isCatapult = false;
            }
            //if (hinge.jointAngle <= hinge.limits.min && !swingingLeft)
            //{
            //    swingingLeft = true;
            //    motorSpeed = -motorSpeed;
            //}


            motor.motorSpeed = motorSpeed * CurrGameSpeed;
            hinge.motor = motor;
        


    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            collision = true;
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            collision = true;
            player = other.gameObject.GetComponent<Rigidbody2D>();

            if(isCatapult)
            {
                player.AddForce(direction);
            }
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            collision = false;
            nonlethal = false;

            if (isCatapult)
            {
                player.AddForce(direction);
            }
        }
    }

    void ToggleActive(bool isActive)
    {
        if (isActive)
            hinge.useMotor = true;
        else
            hinge.useMotor = false;
    }

    void SetTime(short GameSpeed)
    {
        switch (GameSpeed)
        {
            case 1:
                CurrGameSpeed = 0.5f;
                
                break;
            case 2:
                CurrGameSpeed = 0.25f;
                nonlethal = true;
                break;
            case 3:
                CurrGameSpeed = 0.0f;
                nonlethal = true;
                break;
            default:
                CurrGameSpeed = 1.0f;
                
                break;
        }
    }
}
