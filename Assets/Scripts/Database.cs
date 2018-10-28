using System;
using System.Collections;
using System.Collections.Generic;


public class Database  {

    public enum PROPERTY_ACTION
    {
        BUYPROPERTY,
        RENTPROPERTY,
        VISITINGPROPERTY,
        PAYTAXES,
        GOTOJAIL,
        READCHANCECARD,
        CHANGECARDFINISH,
        RETRIEVECENTERMONEY,
        NONE
    }


    private Property[,] properties = new Property[11, 11];
    private ArrayList players = new ArrayList();

    public Database()
    {
        InitializeDatabase();
    }

    public PROPERTY_ACTION GetNextAction(Property property, PlayerInfo player)
    {
        if (CheckPropertyIsAvailable(property))
            return PROPERTY_ACTION.BUYPROPERTY;
        if (!CheckPropertyIsAvailable(property) && property.owner != player)
            return PROPERTY_ACTION.RENTPROPERTY;
        if (!CheckPropertyIsAvailable(property) && property.owner == player)
            return PROPERTY_ACTION.VISITINGPROPERTY;
        if (property.type == Property.Type.Chance)
            return PROPERTY_ACTION.READCHANCECARD;
        if (property.x == 0 && property.y == 10)
            return PROPERTY_ACTION.GOTOJAIL;
        if (property.x == 10 && property.y == 10)
            return PROPERTY_ACTION.RETRIEVECENTERMONEY;
        if (property.type == Property.Type.Taxes)
            return PROPERTY_ACTION.PAYTAXES;
        return PROPERTY_ACTION.NONE;
    }


    public void SubscribePlayer(PlayerInfo player)
    {
        players.Add(player);
    }

    public Property.Type GetTypeOfPropertyAction(PlayerInfo palayer, int x, int y)
    {
        return properties[x, y].type;
    }

    public Property GetProperty(int x, int y)
    {
        return properties[x, y];
    }


    // Returns yes if the property is available for sol
    private bool CheckPropertyIsAvailable(  Property prop)
    {
        return !properties[prop.x, prop.y].HasOwner();
    }

    public bool BuyProperty(PlayerInfo player, int x, int y)
    {
        Property property = properties[x, y];
        property.hasOwner = true;
        property.owner = player;
        player.DisccountMoney(property.value);
        return true;
    }



    public void RentProperty(PlayerInfo player)
    {
        Property property = properties[player.x, player.y];
        PlayerInfo playerOwner = property.owner;
        playerOwner.AddMoney(property.rent);
        player.DisccountMoney(property.calcRent());

    }
    

    private void InitializeDatabase()
    {
        properties[0, 0] = new Property(0, 0, "START POINT", 0, Property.Type.Corner);
        properties[1, 0] = new Property(1, 0, "OLD KENT ROAD", 60, Property.Type.Property);
        properties[2, 0] = new Property(2, 0, "COMMUNITY CHEST", 0, Property.Type.Chance);
        properties[3, 0] = new Property(3, 0, "WHITECHAPEL ROAD", 100, Property.Type.Property);
        properties[4, 0] = new Property(4, 0, "INCOME TAX", 200, Property.Type.Taxes);
        properties[5, 0] = new Property(5, 0, "KINGS CROSS STATION", 200, Property.Type.Property);
        properties[6, 0] = new Property(6, 0, "THE ANGEL ISLINGTON", 100, Property.Type.Property);
        properties[7, 0] = new Property(7, 0, "CHANCE", 0, Property.Type.Chance);
        properties[8, 0] = new Property(8, 0, "EUSTON ROAD", 100, Property.Type.Property);
        properties[9, 0] = new Property(9, 0, "PENTOVILLE ROAD", 120, Property.Type.Property);
        properties[10, 0] = new Property(10, 0, "VISITING JAIL", 0, Property.Type.Corner);
        properties[10, 1] = new Property(10, 1, "PALL MALL", 140, Property.Type.Property);
        properties[10, 2] = new Property(10, 2, "ELETRIC COMPANY", 150, Property.Type.Property);
        properties[10, 3] = new Property(10, 3, "WHITEHALL", 140, Property.Type.Property);
        properties[10, 4] = new Property(10, 4, "NORTHUMRL'D AVENUE", 160, Property.Type.Property);
        properties[10, 5] = new Property(10, 5, "MARYLEBONE STATION", 200, Property.Type.Property);
        properties[10, 6] = new Property(10, 6, "BOW STREET", 180, Property.Type.Property);
        properties[10, 7] = new Property(10, 7, "COMMUNITY CHEST", 0, Property.Type.Chance);
        properties[10, 8] = new Property(10, 8, "MARLBOROUGH STREET", 180, Property.Type.Property);
        properties[10, 9] = new Property(10, 9, "VINE STREET", 200, Property.Type.Property);
        properties[10, 10] = new Property(10, 10, "FREE PARKING", 0, Property.Type.Corner);
        properties[9, 10] = new Property(9, 10, "STRAND", 220, Property.Type.Property);
        properties[8, 10] = new Property(8, 10, "CHANCE", 0, Property.Type.Chance);
        properties[7, 10] = new Property(7, 10, "FLEET STREET", 220, Property.Type.Property);
        properties[6, 10] = new Property(6, 10, "TRAFALGAR SQUARE", 240, Property.Type.Property);
        properties[5, 10] = new Property(5, 10, "FENCHURCH ST. STATION", 200, Property.Type.Property);
        properties[4, 10] = new Property(4, 10, "LEICESTER SQUARE", 260, Property.Type.Property);
        properties[3, 10] = new Property(3, 10, "COVENTRY STREET", 260, Property.Type.Property);
        properties[2, 10] = new Property(2, 10, "WATER WORKS", 150, Property.Type.Property);
        properties[1, 10] = new Property(1, 10, "PICCADILLY", 280, Property.Type.Property);
        properties[0, 10] = new Property(0, 10, "GO TO JAIL", 0, Property.Type.Corner);
        properties[0, 9] = new Property(0, 9, "REGENT STREET", 300, Property.Type.Property);
        properties[0, 8] = new Property(0, 8, "OXFORD STREET", 300, Property.Type.Property);
        properties[0, 7] = new Property(0, 7, "COMMUNITY CHEST", 0, Property.Type.Chance);
        properties[0, 6] = new Property(0, 6, "BOND STREET", 320, Property.Type.Property);
        properties[0, 5] = new Property(0, 5, "LIVERPOOL ST. STATION", 200, Property.Type.Property);
        properties[0, 4] = new Property(0, 4, "CHANCE", 0, Property.Type.Chance);
        properties[0, 3] = new Property(0, 3, "PARK LANE", 350, Property.Type.Property);
        properties[0, 2] = new Property(0, 2, "SUPER TAX", 150, Property.Type.Taxes);
        properties[0, 1] = new Property(0, 1, "MAYFAIR", 400, Property.Type.Property);


    }




}
