 
using UnityEngine;
using UnityEngine.UI;
public class BuyPropertyMenu : MonoBehaviour {

    public delegate void Responde(bool ans);
    public Responde done;

    public Text m_MyText;

    public Button CancelButton;
    public Button AcceptButton;
    public Button AcceptCenterButton;

    public void GenericMenu(string message)
    {
        AcceptCenterButton.gameObject.SetActive(true);
        m_MyText.text = message;
        AcceptCenterButton.gameObject.SetActive(true);
    }


    public void PropertyArriveMessage(Property property, PlayerInfo player, Database database)
    {
        string message = "";

        AcceptCenterButton.gameObject.SetActive(true);

        switch(property.type)
        {
            case Property.Type.Property:
                if(property.hasOwner)
                {
                    if(property.owner != player)    // not my property
                    {
                        // TODO: Calc the rent value
                        message = "Looks like you arrived to " + property.name + " and this is not your property. You will have to pay the rent to it's owner. The rent is " + property.calcRent() + " coins.";
                    }
                    else                            // my property
                    {
                        message = "Hello chief, thanks for the visit! ";
                    }
                }
                else                                // available for sell
                {
                    CancelButton.gameObject.SetActive(true);
                    AcceptButton.gameObject.SetActive(true);
                    AcceptCenterButton.gameObject.SetActive(false);

                    message = "Hello Entrepreneur! This property, " + property.name + ", is now available for sale. Don't miss this opportunity to buy this land just for " + property.value + " coins. Do we have a deal?";
                }
                

                break;
            case Property.Type.Chance:
                message = "Is it your lucky day? Let's find out! Reach for a Chance card and find out";

                break;
            case Property.Type.Taxes:
                message = "Oops, such a shame, you did not payed your Taxes. Seems like you have to pay now " + property.value + " coins in taxes"; 
                break;
            case Property.Type.Corner:
                if (property.x == 0 && property.y == 0)         // Start Point
                {
                    message = "Thanks for the visit";
                }
                else if (property.x == 10 && property.y == 0)   // Jail Visit
                {
                    message = "Thanks for the visit";
                }else if (property.x == 10 && property.y == 10) // Park 
                {
                    message = "Congratulations, you received all the money collected from taxes. We will deposit in your account " + database.GetCenterMoney() + " coins";
                }else                                           // GO TO JAIL
                {
                    message = "YOU GOT ARRESTED !! You will be moved to prison for the next three turns";
                }

                break;
        }

        // display message
        m_MyText.text = message;
    }


    public void Accept()
    {
        CancelButton.gameObject.SetActive(false);
        AcceptButton.gameObject.SetActive(false);
        AcceptCenterButton.gameObject.SetActive(false);
        done(true);
    }

    public void Cancel()
    {
        CancelButton.gameObject.SetActive(false);
        AcceptButton.gameObject.SetActive(false);
        AcceptCenterButton.gameObject.SetActive(false);
        done(false);
    }
}
