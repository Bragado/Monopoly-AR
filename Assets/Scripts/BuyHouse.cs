using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyHouse : MonoBehaviour {

    public GameObject house1;
    public GameObject house2;
    public GameObject house3;
    public GameObject house4;

    public GameObject hotel;

    private int counter = 0;

    public void addHouse()
    {
        switch(counter)
        {
            case 0:
                house1.SetActive(true);
                break;
            case 1:
                house2.SetActive(true);
                break;
            case 2:
                house3.SetActive(true);
                break;
            case 3:
                house4.SetActive(true);
                break;
        }
        counter++;

    }

    public void addHotel()
    {
        house1.SetActive(false);
        house2.SetActive(false);
        house3.SetActive(false);
        house4.SetActive(false);

        hotel.SetActive(true);
    }



}
