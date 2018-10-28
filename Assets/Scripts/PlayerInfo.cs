using System;
using System.IO; 
using System.Collections;

 




public class PlayerInfo  {

    private int Money = 1500;
    public int x = 0;
    public int y = 0;

    private ArrayList properties = new ArrayList();




	public PlayerInfo() { }

    public void Move(int num_houses)
    {
        // TODO: put this into a cycle
        int totalHouses = 0;
        
        do
        {
            if((y == 0 && x != 10) || ((x == 0 && y == 0)))   // going horizontal left
            {

                totalHouses = num_houses + x;

                if (totalHouses > 10)
                {
                    x = 10;
                    num_houses = totalHouses % 11 + 1;
                }
                else
                {
                    
                    x = totalHouses;
                    num_houses = 0;
                }
            }
            else if ((x == 10 && y != 10) || (y == 0 && x == 10)) // ascending
            {
                totalHouses = y + num_houses;
                if (totalHouses > 10)
                {
                    y = 10;
                    num_houses = totalHouses % 11 + 1;
                }
                else
                {
                    y = totalHouses;
                    num_houses = 0;
                }



            }
            else if((x == 0 && y != 0) || (x == 0 && y == 10)) // descending vertically
            {
                totalHouses = y - num_houses;
                if (totalHouses < 0)
                {
                    y = 0;
                   num_houses = -(totalHouses % 11) + 1;
                }
                else
                {
                    y = totalHouses;
                    num_houses = 0;
                }
                    

            }          
            else if ((y == 10 && x != 0) || (x == 10 && y == 10))   // going right horizontal
            {
                totalHouses = x - num_houses;
                if (totalHouses < 0)
                {
                    x = 0;
                    num_houses = -(totalHouses % 11) + 1;
                }
                else
                {
                    x = totalHouses;
                    num_houses = 0;
                }
                    
            }



        } while (num_houses > 0);


        


        

    }

    public int GetMoney()
    {
        return Money;
    }


    public void AddMoney(int money)
    {
        this.Money += money;
    }

    public void DisccountMoney(int money)
    {
        this.Money -= money;
    }

}
