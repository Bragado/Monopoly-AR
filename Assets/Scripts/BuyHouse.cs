using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyHouse : MonoBehaviour {

    public GameObject house1;
    public GameObject house2;
    public GameObject house3;
    public GameObject house4;

    public GameObject hotel;

    public int x, y;


    private int counter = 0;
    private GameRunner gr = null;


    public void addHouse()
    {
        if(gr == null)
        {
            GameObject g = GameObject.Find("TurnManager");
            gr = g.GetComponent<GameRunner>();
        }

        Property property = gr.database.GetProperty(x, y);
        if (property.owner != gr.GetActivePlayerInfo())
        {
            Debug.Log("You are not the owner of this property!");
            return;
        }
            


        switch(counter)
        {
            case 0:
                house1.SetActive(true);
                property.number_of_houses++;
                property.owner.DisccountMoney(property.calcRent());
                break;
            case 1:
                house2.SetActive(true);
                property.number_of_houses++;
                property.owner.DisccountMoney(property.calcRent());
                break;
            case 2:
                house3.SetActive(true);
                property.number_of_houses++;
                property.owner.DisccountMoney(property.calcRent());
                break;
            case 3:
                house4.SetActive(true);
                property.number_of_houses++;
                property.owner.DisccountMoney(property.calcRent());
                break;
            case 4:
                addHotel();
                property.number_of_houses = 0;
                property.number_of_hotels = 1;
                property.owner.DisccountMoney(property.calcRent());
                break;
            default:
                break;
        }
        counter++;
        gr.GetActivePlayer().updateBalance();
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
