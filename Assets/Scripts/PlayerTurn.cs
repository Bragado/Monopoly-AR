using UnityEngine;

public class PlayerTurn : MonoBehaviour
{
    


    /* Callback to GameRunner */
    public delegate void Done();
    public Done done;



    
    /* Menus */
    public GameObject Menu;
    private BuyPropertyMenu bpm;

    /* Player Animation */
    private PlayerAnimations playerAnimations;

    /* Control Variables */
    private bool active = false;
    private bool AnimationActive = false;
    private Database.PROPERTY_ACTION PropAction;

    /* Player Info */
    PlayerInfo playerInfo = new PlayerInfo();

    /* Database */
    Database database = null;
    public GameObject Manager;

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
       // Debug.Log("Entaooooo");

        if (Input.GetKey(KeyCode.Alpha1) && !AnimationActive)
        {
            playerAnimations.setOnMove(1);

            AnimationActive = true;
            playerInfo.Move(1);
            Debug.Log("KeyDown");
        }
        if (Input.GetKey(KeyCode.Alpha2) && !AnimationActive)
        {
            AnimationActive = true;
            playerAnimations.setOnMove(2);
            playerInfo.Move(2);
        }
        if (Input.GetKey(KeyCode.Alpha3) && !AnimationActive)
        {
            playerAnimations.setOnMove(3);
            AnimationActive = true;
            playerInfo.Move(3);
        }
        if (Input.GetKey(KeyCode.Alpha4) && !AnimationActive)
        {
            playerAnimations.setOnMove(4);
            AnimationActive = true;
            playerInfo.Move(4);
        }
        if (Input.GetKey(KeyCode.Alpha5) && !AnimationActive)
        {
            playerAnimations.setOnMove(5);
            AnimationActive = true;
            playerInfo.Move(5);
        }
        if (Input.GetKey(KeyCode.Alpha6) && !AnimationActive)
        {
            playerAnimations.setOnMove(6);
            AnimationActive = true;
            playerInfo.Move(6);
        }
    }


    public void activate()
    {
        Debug.Log("Im active");
        this.active = true;
    }

    public void FinishedMovement()
    {
        Debug.Log("Player Coordinate: [X, Y] = [" + playerInfo.x + ", " + playerInfo.y + "]");

        if (database == null)
        {
            GameRunner gr = Manager.GetComponent<GameRunner>();
            this.database = gr.database;
        }


           

        Property prop = database.GetProperty(playerInfo.x, playerInfo.y);
        PropAction = database.GetNextAction(prop, playerInfo);



        Menu.SetActive(true);
        
        bpm = Menu.GetComponent<BuyPropertyMenu>();
        bpm.PropertyArriveMessage(prop, playerInfo);
        bpm.done = MenuAnswer;
                


       
    }


    public void MenuAnswer(bool answer)
    {


        if(answer)
        {
            switch(PropAction)
            {
                case Database.PROPERTY_ACTION.BUYPROPERTY:
                    database.BuyProperty(playerInfo, playerInfo.x, playerInfo.y);
                    PropAction = Database.PROPERTY_ACTION.NONE;
                    break;
                case Database.PROPERTY_ACTION.RENTPROPERTY:
                     Debug.Log("RENT PROPERTY");
                    database.RentProperty(playerInfo);
                    PropAction = Database.PROPERTY_ACTION.NONE;
                    break;
                case Database.PROPERTY_ACTION.PAYTAXES:
                    //database.PayTaxes(playerInfo),
                    PropAction = Database.PROPERTY_ACTION.NONE;
                    break;
                case Database.PROPERTY_ACTION.READCHANCECARD:
                    //PropAction = Database.PROPERTY_ACTION.CHANGECARD; 
                    PropAction = Database.PROPERTY_ACTION.NONE;
                    break;
                case Database.PROPERTY_ACTION.GOTOJAIL:
                    // GO TO JAIL
                    // playerAnimations.setOnMove(20);
                    // playerInfo.Move(20);
                    PropAction = Database.PROPERTY_ACTION.NONE;
                    break;
                case Database.PROPERTY_ACTION.RETRIEVECENTERMONEY:
                    // database.AddCenterMoneyToPlayer(playerInfo);
                    PropAction = Database.PROPERTY_ACTION.NONE;
                    break;
                case Database.PROPERTY_ACTION.VISITINGPROPERTY:
                    PropAction = Database.PROPERTY_ACTION.NONE;
                    break;
            }
            

        }else
        {
            // dont buy the property
            Debug.Log("Do not Buy the Property!!!!");
        }

        Menu.SetActive(false);
        this.AnimationActive = false;
        if (PropAction ==   Database.PROPERTY_ACTION.NONE)
        {
            this.active = false;
        }


        Debug.Log("PLAYER MONEY: " + playerInfo.GetMoney() + " !!!!");
        
         
        done();

    }
}
