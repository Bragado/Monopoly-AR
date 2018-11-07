using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class BuyHousesManager : MonoBehaviour, ITrackableEventHandler
{
    /* Callback to PlayTurn */
    public delegate void Done();
    public Done done;
    

    // whether should buy a house or an hotel
     
    public bool active = false;

    public bool startCounting = false;
    public float timeHolding = 2.5f;
    public GameObject propertyHouses = null;


    private float time = 0.0f;
    
    private TrackableBehaviour mTrackableBehaviour;

    void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }


    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
             newStatus == TrackableBehaviour.Status.TRACKED ||
             newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            startCounting = true;
            Debug.Log("Counter Started !!!!!");
           
        }else
        {
            startCounting = false;
            time = 0.0f;
            Debug.Log("Counter Stopping !!!!!");
        }

    }

   
	// Update is called once per frame
	void Update () {

        if(startCounting && active)
        {
            time += Time.deltaTime;

            if(time > timeHolding)
            {

                // carta identificada
                Debug.Log("Time REACHED !!!!!");
                BuyHouse buyHouse = propertyHouses.GetComponent<BuyHouse>();
                buyHouse.addHouse();

                done();

                

            }

        }
        


    }

    


}
