using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PlayerAnimations : MonoBehaviour
{


    public Vector3[] target ;
    public float speedWalking;
    public float speedRunning;
    public float speedRotation;


    public float defaultXZ = 0.5f;
    public float defaultY = 0.075f;
    
    private float step = 0.1f;

    private float speed;
    private float weight = 0.0f;
    private Vector3 startPosition;
    private int current;
    private int walks;
    private bool onMove = false;
    private bool rotating = false;
    private int housesMoved = 0;
    private float controlAngle;


    private Animator animator = null;
    //public Transform StartingPoint;
    //public Transform Jail; 


    /* To notice the manager that my movement is over */

    public delegate void Done();
    public Done done;




    // Use this for initialization
    void Start()
    {
        target = new Vector3[100];
        animator = GetComponent<Animator>();
         
    }


    private void setAnimations(int houses)
    {
        if (houses < 3)
        {
            animator.SetBool("walking", true);
            speed = speedWalking;
            transform.Rotate(Vector3.right * Time.deltaTime);
        }
        else
        {
            animator.SetBool("running", true);
            speed = speedRunning;
        }
    }


    public void setOnMove(int houses)
    {
        if (onMove)
            return;


        onMove = true;

        setAnimations(houses);

        int angle = (int)transform.localEulerAngles.y;
        Debug.Log("ANGLE :             " + angle +"  ||!!!!!!");

        setAnimations(houses);


        //Vector3 newTarget;

        bool exit = true;
        startPosition = transform.localPosition;

        int housesAcc;

        walks = 1;
        do
        {

            // the player is in the corner
            if (housesMoved == 10)
            {
                housesMoved = 0;
                angle = (angle + 90)%360 ;
                rotating = true;
            }

            // the player will pass throught a corner 
            if (10 - housesMoved < houses)
            {
                Debug.Log("he entered in here");
                housesAcc = 10 - housesMoved;
                houses -= housesAcc;

            }

            //the player will not have to rotate
            else
            {
                housesAcc = houses;
                houses = 0;
            }


            housesMoved += housesAcc;
            Debug.Log("Houses Moved: " + housesMoved + "                      | Houses: " + housesAcc);
            exit = true;


            target[walks - 1] = getNewTarget(houses != 0 ? housesAcc - 1  : housesAcc , angle);

            if (houses > 0)
            {
                // create here the arc mouvement
               
                createArcMovement(target[walks - 1], angle);
                 
                angle = (angle + 90) % 360;
                housesMoved = 0;
                //walks++;
                exit = false;
                Debug.Log("houses Left: " + houses);
            }





        } while (!exit);




        current = 0;


    }

    private void createArcMovement(Vector3 vector3, int angle)
    {
        
        for(int i = 0; i <= 90; i+=10)
        {

            float X, Z;
            if (angle == 0)
            {
                X = vector3.x + (float)(step * Math.Sin(Math.PI * i / 180.0));
                Z = vector3.z + (float)(step * Math.Sin(Math.PI * i / 180.0));
            }else if(angle == 90)
            {
                X = vector3.x + (float)(step * Math.Sin(Math.PI * i / 180.0));
                Z = vector3.z - (float)(step * Math.Sin(Math.PI * i / 180.0));
            }else if(angle == 180)
            {
                X = vector3.x - (float)(step * Math.Sin(Math.PI * i / 180.0));
                Z = vector3.z - (float)(step * Math.Sin(Math.PI * i / 180.0));
            }
            else
            {
                X = vector3.x - (float)(step * Math.Sin(Math.PI * i / 180.0));
                Z = vector3.z + (float)(step * Math.Sin(Math.PI * i / 180.0));
            }
            
            target[walks] = new Vector3(X, 0.075f, Z);
            walks++;
        }
    }



    // calculates the next player's position based on the number of houses he has to walk
    private Vector3 getNewTarget(int housesAcc, int angle)
    {
        if (angle == 0)
        {
            return new Vector3(-defaultXZ, defaultY, startPosition.z + step * housesAcc);

        }
        if (angle == 90)
        {
            return new Vector3(startPosition.x + step * housesAcc, defaultY, defaultXZ);

        }
        if (angle == 180)
        {
            return new Vector3(defaultXZ, defaultY, startPosition.z - step * housesAcc);

        }


        return new Vector3(startPosition.x - step * housesAcc, defaultY, -defaultXZ);



    }


    void FixedUpdate()
    {
        if(rotating)
        {
            transform.Rotate((0), (10f), (0) * (2 * Time.deltaTime));
            if ((int)transform.localEulerAngles.y > controlAngle + 85 || (int)transform.localEulerAngles.y == 0)
                rotating = false;
        }


        if (onMove && !rotating)
        {

            if (transform.localPosition != target[current])
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, target[current], speed * Time.deltaTime);

            }
            else
            {
                
                Debug.Log("Got There");
                current++;
                
                
                 if(current < (walks -1) && !rotating)
                    transform.Rotate((0), (10f), (0) * (2 * Time.deltaTime));
            }

            if (current + 1 > walks)
            {
                
                AnimationFinished();
            }
        }


    }

    private void AnimationFinished()
    {
        correctAngles();
        Debug.Log("finishing animations!!!");
        animator.SetBool("walking", false);
        animator.SetBool("running", false);
        onMove = false;
        done();
    }

    private void correctAngles()
    {
        Vector3 temp = transform.localRotation.eulerAngles;
        int angle = (int)transform.localEulerAngles.y;
        controlAngle = angle;
        if (angle < 5)
        {
            
        }
        else if (angle > 85 && angle < 95)
        {
            temp.y = 90.0f;
        }
        else if(angle < 185)
        {
            temp.y = 180.0f;
        }
        else if(angle < 275)
        {
            temp.y = 270.0f;
        }
        else
        {
            temp.y = 0.0f;
        }
                
        transform.localRotation = Quaternion.Euler(temp);
    }
}