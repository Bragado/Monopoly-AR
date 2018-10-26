using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameRunner : MonoBehaviour  {

    /* Players playing */
    private ArrayList  playerAnimations = new ArrayList ();
    private int numberOfPlayers = 0;

    
    /* Active Player */
    private PlayerAnimations ActivePlayer;
    private int playerTurn = 0;
    private bool active = false;
 

	
	void Start () {

        /* Store Locally a reference to all player's movement animation script*/

        GameObject player;        
        PlayerAnimations playerAnimation = null;

        GameObject[] playersList = GameObject.FindGameObjectsWithTag("Player");


        for (int i = 0; i< playersList.Length; i++) {


            player = playersList[i];

            if(player != null)
            {
                playerAnimation = player.GetComponent<PlayerAnimations>();
               
            }
                
           

            if (playerAnimation != null)
            {
                playerAnimation.done = NextPlayer;
                playerAnimations.Add(playerAnimation); 
                numberOfPlayers++;
            }
                
        }
        ActivePlayer = (PlayerAnimations)playerAnimations[0];
         

    }


    /* A callback function called when players finish their movement */
    private void NextPlayer()
    {
        
        playerTurn = (playerTurn + 1) % numberOfPlayers;
        ActivePlayer = (PlayerAnimations)playerAnimations[playerTurn];
        active = false;
      
         
    }

    // TODO : Erase
    void Update () {
        if (Input.GetKey(KeyCode.Alpha1) && !active)
        {
            ActivePlayer.setOnMove(1);
            active = true;
            Debug.Log("KeyDown");
        }
        if (Input.GetKey(KeyCode.Alpha2) && !active)
        {
            active = true;
            ActivePlayer.setOnMove(2);

        }
        if (Input.GetKey(KeyCode.Alpha3) && !active)
        {
            ActivePlayer.setOnMove(3);
            active = true;
        }
        if (Input.GetKey(KeyCode.Alpha4) && !active)
        {
            ActivePlayer.setOnMove(4);
            active = true;
        }
        if (Input.GetKey(KeyCode.Alpha5) && !active)
        {
            ActivePlayer.setOnMove(5);
            active = true;
        }
        if (Input.GetKey(KeyCode.Alpha6) && !active)
        {
            ActivePlayer.setOnMove(6);
            active = true;
        }

    }
}
