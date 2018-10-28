using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameRunner : MonoBehaviour  {

    /* Players playing */
    private ArrayList  playerAnimations = new ArrayList ();
    private int numberOfPlayers = 0;

    
    /* Active Player */
    private PlayerTurn ActivePlayer;
    private int playerTurn = 0;

    public Database database;
    

	
	void Start () {

        /* Store Locally a reference to all player's movement animation script*/

        GameObject player;
        PlayerTurn playerAnimation = null;

        GameObject[] playersList = GameObject.FindGameObjectsWithTag("Player");


        for (int i = 0; i< playersList.Length; i++) {


            player = playersList[i];

            if(player != null)
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

    // TODO : Erase
    void Update () {
       

    }
}
