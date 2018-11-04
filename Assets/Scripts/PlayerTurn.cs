using UnityEngine;

public class PlayerTurn : MonoBehaviour
{
    /* Callback to GameRunner */
    public delegate void Done();
    public Done done;
    
    /* Menus */
    public GameObject Menu;
    private BuyPropertyMenu bpm;

    public GameObject ChoosePropertyCard;
    private ChoosePropertyCardMenu choosePropertyCardMenu;

    /* Player Animation */
    private PlayerAnimations playerAnimations;

    /* Control Variables */
    private bool active = false;
    private bool AnimationActive = false;
    private Database.PROPERTY_ACTION PropAction;
    private int NoPlayTurns = 0;

    /* Player Info */
    PlayerInfo playerInfo = new PlayerInfo();

    /* Database */
    Database database = null;
    public GameObject Manager;
    public GameObject dice1;
    public GameObject dice2;

    void Start()
    {
        this.playerAnimations = this.GetComponent<PlayerAnimations>();
        GameRunner gr = Manager.GetComponent<GameRunner>();
        this.database = gr.database;
        this.playerAnimations.done = FinishedMovement;
    }

    // Update is called once per frame
    void Update()
    {
        if (!active )
            return; 

        if (dice1.GetComponent<DiceScript>().hasLanded && dice2.GetComponent<DiceScript>().hasLanded && !AnimationActive)
        {
            dice1.GetComponent<DiceScript>().hasLanded = false;
            dice1.GetComponent<Rigidbody>().isKinematic = true;
            dice2.GetComponent<DiceScript>().hasLanded = false;
            dice2.GetComponent<Rigidbody>().isKinematic = true;

            Move(dice1.GetComponent<DiceScript>().GetDiceCount() + dice2.GetComponent<DiceScript>().GetDiceCount());
        }
    }

    public void Move(int moves)
    {
        if(NoPlayTurns > 0)
        {
            this.AnimationActive = true;
            NoPlayTurns--;
            Menu.SetActive(true);
            bpm = Menu.GetComponent<BuyPropertyMenu>();
            bpm.done = InJail;
            bpm.GenericMenu("You Are Still In Prison");
            if(NoPlayTurns == 0) {
                PropAction = Database.PROPERTY_ACTION.NONE;
            }  
        }
        else
        {
            playerAnimations.setOnMove(moves);
            AnimationActive = true;
            playerInfo.Move(moves);
        }

        dice1.transform.localPosition = dice1.GetComponent<DiceScript>().initPosition;
        dice2.transform.localPosition = dice2.GetComponent<DiceScript>().initPosition;
    }

    public void activate()
    {
        this.active = true;
    }

    public void FinishedMovement()
    {
        Debug.Log("Player Coordinate: [X, Y] = [" + playerInfo.x + ", " + playerInfo.y + "]");


        // Just To be sure
        if (database == null)
        {
            GameRunner gr = Manager.GetComponent<GameRunner>();
            this.database = gr.database;
        }

        if(PropAction == Database.PROPERTY_ACTION.INJAIL)     // If he was supposed to go to jail, no action is needed
        {
            NoPlayTurns = 3;
            done();
            return;
        }

           
        // Get Property
        Property prop = database.GetProperty(playerInfo.x, playerInfo.y);
        PropAction = database.GetNextAction(prop, playerInfo);

        Menu.SetActive(true);
        
        bpm = Menu.GetComponent<BuyPropertyMenu>();
        bpm.PropertyArriveMessage(prop, playerInfo, database);
        bpm.done = MenuAnswer; 
    }

    public void showPickCardMenu()
    {
        choosePropertyCardMenu = ChoosePropertyCard.GetComponent<ChoosePropertyCardMenu>();
        ChoosePropertyCard.SetActive(true);
        choosePropertyCardMenu.done = CardPicked;
        choosePropertyCardMenu.SetMessage("This Menu Will help you choose your Card. The Property Card you desire will appear market. When you find select OK to close this menu.");
        // TODO: Mark The Card the bought
    }

    public void CardPicked()
    { 
        ChoosePropertyCard.SetActive(false);
        // TODO: Unmark The Card the bought
        done();
    }

    public void InJail(bool answer)
    {
        Menu.SetActive(false);
        this.AnimationActive = false;
        this.active = false;
        done();
    }


    public void MenuAnswer(bool answer)
    {
        if(answer)
        {
            switch(PropAction)
            {
                case Database.PROPERTY_ACTION.BUYPROPERTY:
                    database.BuyProperty(playerInfo, playerInfo.x, playerInfo.y);
                    PropAction = Database.PROPERTY_ACTION.WAITINGFORUSERTOGETPROPERTYCARD;
                    
                    showPickCardMenu();
                    break;
                case Database.PROPERTY_ACTION.RENTPROPERTY:
                     
                    database.RentProperty(playerInfo);
                    PropAction = Database.PROPERTY_ACTION.NONE;
                    break;
                case Database.PROPERTY_ACTION.PAYTAXES:
                    database.PayTaxes(playerInfo, database.GetProperty(playerInfo.x, playerInfo.y).value);
                    PropAction = Database.PROPERTY_ACTION.NONE;
                    break;
                case Database.PROPERTY_ACTION.READCHANCECARD:
                    //PropAction = Database.PROPERTY_ACTION.CHANGECARD; 
                    PropAction = Database.PROPERTY_ACTION.NONE;
                    break;
                case Database.PROPERTY_ACTION.GOTOJAIL:
                    // GO TO JAIL
                    
                    playerAnimations.setOnMove(20);
                    playerInfo.Move(20);
                    PropAction = Database.PROPERTY_ACTION.INJAIL;
                    break;

                case Database.PROPERTY_ACTION.RETRIEVECENTERMONEY:
                    database.GiveCenterMoney2Player(playerInfo);
                    PropAction = Database.PROPERTY_ACTION.NONE;
                    break;
                case Database.PROPERTY_ACTION.VISITINGPROPERTY:
                    PropAction = Database.PROPERTY_ACTION.NONE;
                    break;
                
                        
            }
            

        }else
        {
            PropAction = Database.PROPERTY_ACTION.NONE;
            this.AnimationActive = false;
            this.active = false;
        }

        Menu.SetActive(false);
        this.AnimationActive = false;
        this.active = false;
        Debug.Log("PLAYER MONEY: " + playerInfo.GetMoney() + " !!!!");
        if (PropAction ==   Database.PROPERTY_ACTION.NONE )
        {
            done();
        }
    }
}
