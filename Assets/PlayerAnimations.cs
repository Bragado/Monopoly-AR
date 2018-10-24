using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour {


    public Vector3[] target = new Vector3[4];
    public float speedWalking;
    public float speedRunning;
    public float speedRotation;


    private float boardLimitleft;
    private float boardLimitRight;
    private float boardLimitUp;
    private float boardLimitDown;
    private float step = 0.1f;

    private float speed;
    private float weight = 0.0f;
    private Vector3 startPosition;
    private int current;
    private int walks;
    private bool onMove = false;

    private int housesMoved = 0;



    private Animator animator; 
    //public Transform StartingPoint;
    //public Transform Jail; 
 

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        boardLimitleft = - transform.localPosition.z;
        boardLimitRight = transform.localPosition.z;
        boardLimitUp = -transform.localPosition.z;
        boardLimitDown = transform.localPosition.z; 
	}
	
	// Update is called once per frame
	void Update () {
      /*  bool walking = Input.GetKey(KeyCode.W);
        animator.SetBool("walking", walking);

        bool running = Input.GetKey(KeyCode.S);
        animator.SetBool("running", running);
    */
        if(Input.GetKey(KeyCode.Alpha1))
        {
            setOnMove(1);
            Debug.Log("KeyDown");
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            Debug.Log("KeyDown");
            setOnMove(2);

        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            setOnMove(3);
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            setOnMove(4);
        }
        if (Input.GetKey(KeyCode.Alpha5))
        {
            setOnMove(5);
        }
        if (Input.GetKey(KeyCode.Alpha6))
        {
            setOnMove(6);
        }


    }

    void setOnMove(int houses)
    {
        if (onMove)
            return;
        onMove = true;
        int angle = (int)transform.localEulerAngles.y; 

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



        /* startPosition = transform.localPosition;
         Vector3 newTarget = new Vector3(startPosition.x, startPosition.y, startPosition.z + step * houses);

         Debug.Log("From: " + startPosition.ToString() + "     |   TO:  " + newTarget.ToString() + "         |    Step:" + step + "      |   Angle: " + angle);
         target[0] = newTarget;*/

        Vector3 newTarget;


       // housesMoved += houses;

        bool exit = true;
        startPosition = transform.localPosition;

        int housesAcc ;
       
        walks = 1;
        do
        {

            if(housesMoved == 10)
            {
                housesMoved = 0;
                angle += 90;
                
            }
            if (10 - housesMoved < houses)
            {
                Debug.Log("he entered in here");
                housesAcc = 10 - housesMoved;
                houses -= housesAcc;

            }
            else
            {
                housesAcc = houses;
                houses = 0;
            }
                

            housesMoved += housesAcc;
            Debug.Log("Houses Moved: " + housesMoved + "                      | Houses: " + housesAcc);
            exit = true;

            if(angle == 0)
            {
                newTarget = new Vector3(/*startPosition.x*/ -0.5f, /*startPosition.y*/ 0.075f, startPosition.z + step * housesAcc);

            }
            else if (angle == 90)
            {
                newTarget = new Vector3(startPosition.x + step * housesAcc, /*startPosition.y*/ 0.075f, /*walks > 1 ? target[0].z : startPosition.z*/ 0.5f);

            }
            else if(angle == 180)
            {
                newTarget = new Vector3(/*startPosition.x*/0.5f, /*startPosition.y*/0.075f, startPosition.z - step * housesAcc);

            }
            else //if(angle == 270)
            {
                newTarget = new Vector3(startPosition.x - step * housesAcc, /*startPosition.y*/0.075f, /*startPosition.z*/ -0.5f);

                
            }
            target[walks - 1] = newTarget;

            if (houses > 0)
            {
                angle += 90;
                angle = angle % 360;
                housesMoved = 0;
                walks++;
                exit = false;
                Debug.Log("houses Left: " + houses);
            } 
            
           

                

        } while (!exit);



       
        current = 0;
        





        /* if (onMove)
            return;

        int housesAcc = houses;
        int angle = (int)(Quaternion.Angle(Quaternion.Euler(new Vector3(0, 0, 0)), transform.rotation));
        current = 0;
        walks = 1;
        startPosition = transform.localPosition;

        switch (angle)
        {
            case 0:

                Vector3 newTarget = new Vector3(startPosition.x, startPosition.y, startPosition.z + step * housesAcc);
                target[walks] = newTarget;
                     

                 
                break;
            case 90:
                Debug.Log("angle is 90");
                break;
            case 180:
                Debug.Log("angle is 180");
                break;
            case 270:
                Debug.Log("angle is 270");
                break;
          
                
                
        }


      

        onMove = true;
        walks++;



        if (houses < 3)
        {
            animator.SetBool("walking", true);
            speed = speedWalking;
        }
        else
        {
            animator.SetBool("running", true);
            speed = speedRunning;
        }*/



    }


    void FixedUpdate()
    {
        if(onMove)
        {
            
            if (transform.localPosition != target[current])
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, target[current], speed * Time.deltaTime);
                 
            }
            else
            {
                // Rotation
                Debug.Log("Got There");
                current++;
            }

            

            if (current + 1 > walks)
            {
                Debug.Log("finishing animations!!!");
                animator.SetBool("walking", false);
                animator.SetBool("running", false);
                onMove = false;
            }
        }

        
    }
}
