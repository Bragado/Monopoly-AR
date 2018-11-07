using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameRunner : MonoBehaviour {

    /* Players playing */
    private ArrayList playerAnimations = new ArrayList();
    private int numberOfPlayers = 0;


    private GameObject[] totalProperties = null;


    /* Active Player */
    private PlayerTurn ActivePlayer;
    private int playerTurn = 0;

    public Database database;


    public GameObject ChoosePropertyCard;
    private ChoosePropertyCardMenu choosePropertyCardMenu;



    void Start() {

        /* Store Locally a reference to all player's movement animation script*/

        GameObject player;
        PlayerTurn playerAnimation = null;

        GameObject[] playersList = GameObject.FindGameObjectsWithTag("Player");


        for (int i = 0; i < playersList.Length; i++) {


            player = playersList[i];

            if (player != null)
            {
                playerAnimation = player.GetComponent<PlayerTurn>();

            }

            if (playerAnimation != null)
            {
                playerAnimation.done = NextPlayer;
                playerAnimations.Add(playerAnimation);
                numberOfPlayers++;
            }

        }
        ActivePlayer = (PlayerTurn)playerAnimations[0];
        ActivePlayer.activate();

        database = new Database();
    }


    /* A callback function called when players finish their movement */
    private void NextPlayer()
    {

        playerTurn = (playerTurn + 1) % numberOfPlayers;
        ActivePlayer = (PlayerTurn)playerAnimations[playerTurn];
        ActivePlayer.activate();
    }

    public PlayerInfo GetActivePlayerInfo()
    {
        return ActivePlayer.GetPlayerInfo();
    }

    public PlayerTurn GetActivePlayer()
    {
        return ActivePlayer;
    }

    public void buyHouse()
    {
        if (totalProperties == null)
        {

            totalProperties = GameObject.FindGameObjectsWithTag("GameController");
            Debug.Log("Total number of imageTargets for property cards: ");
        }


        for (int i = 0; i < totalProperties.Length; i++)
        {
            BuyHousesManager bm = totalProperties[i].GetComponent<BuyHousesManager>();
            bm.active = true;
            bm.done = finishBuyHouse;
        }


        choosePropertyCardMenu = ChoosePropertyCard.GetComponent<ChoosePropertyCardMenu>();
        ChoosePropertyCard.SetActive(true);
        choosePropertyCardMenu.done = response;
        choosePropertyCardMenu.SetMessage("In order to buy a house you must show us the property card for 3 seconds. Close this menu and let us know.");


    }

    public void response() { ChoosePropertyCard.SetActive(false); }


    public void finishBuyHouse()
    {
        for (int i = 0; i < totalProperties.Length; i++)
        {
            BuyHousesManager bm =  totalProperties[i].GetComponent<BuyHousesManager>();
            bm.active = false;
        }
    }
}
