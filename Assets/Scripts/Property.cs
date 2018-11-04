using System.Collections;
using System.Collections.Generic;
 

public class Property {

    public enum Type
    {
        Property,
        Taxes,
        Chance,
        Corner
    }

    public int x;
    public int y;
    public Type type;
    public string name;
    public PlayerInfo owner;
    public int number_of_houses = 0;
    public int number_of_hotels = 0;
    public bool hasOwner = false;
    public string ownerName; 


    /* value and rent */
    public int value;
    public int rent = 15;   // default value for now
    public int rent1house;
    public int rent2houses;
    public int rent3houses;
    public int rent4houses;
    public int renthotel;
    public int housevalue;
    public int hotelvalue;





    public Property(int x, int y, string name, int value, Type type)
    {
        this.x = x;
        this.y = y;
        this.name = name;
        this.value = value;
        this.type = type; 
    }


    public Property(int x, int y, string name, int value, int rent, int rent1house, int rent2houses, int rent3houses, int rent4houses, int renthotel, int housevalue, int hotelvalue)
    {
        this.x = x;
        this.y = y;
        this.name = name;
        this.value = value;
        this.rent = rent;
        this.rent1house = rent1house;
        this.rent2houses = rent2houses;
        this.rent3houses = rent3houses;
        this.rent4houses = rent4houses;
        this.renthotel = renthotel;
        this.housevalue = housevalue;
        this.hotelvalue = hotelvalue;
    }

    public void AssignOwner(string name)
    {
        this.hasOwner = true;
        this.ownerName = name;
    }

    public void UnassignOwner()
    {
        this.hasOwner = false;
    }
    
    public bool HasOwner()
    {
        return hasOwner;
    }

    public int calcRent()
    {
        if (number_of_houses == 0 && number_of_hotels == 0)
            return rent;
        if (number_of_houses == 1)
            return rent1house;
        if (number_of_houses == 2)
            return rent2houses;
        if (number_of_houses == 3)
            return rent3houses;
        if (number_of_houses == 4)
            return rent4houses;
        if (number_of_hotels == 1)
            return renthotel;


        return 200;
    }


}
