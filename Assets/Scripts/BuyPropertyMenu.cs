 
using UnityEngine;
using UnityEngine.UI;
public class BuyPropertyMenu : MonoBehaviour {

    public delegate void Responde(bool ans);
    public Responde done;

    public Text m_MyText;

    public void PropertyArriveMessage(Property property, PlayerInfo player)
    {
        string message = "";

        switch(property.type)
        {
            case Property.Type.Property:
                if(property.hasOwner)
                {
                    if(property.owner != player)    // not my property
                    {
                        // TODO: Calc the rent value
                        message = "Looks like you arrived to " + property.name + " and this is not your property. You will pay the rent to it's owner. The rent is " + property.rent + " coins.";
                    }
                    else                            // my property
                    {
                        message = "Hello chief, thanks for the visit! ";
                    }
                }
                else                                // available for sold
                {
                    message = "Hello Entrepreneur! This property, " + property.name + ", is now available for sale. Don't miss this opportunity to buy this land just for " + property.value + " coins. Do we have a deal?";
                }
                

                break;
            case Property.Type.Chance:
                break;
            case Property.Type.Taxes:
                break;
            case Property.Type.Corner:
                break;
        }

        // display message
        m_MyText.text = message;
    } 

    public void Accept()
    {
        done(true);
    }

    public void Cancel()
    {
        done(false);
    }
}
