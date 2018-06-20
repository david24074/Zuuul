using System;
using System.Collections.Generic;
namespace ZuulCS
{
    public class Player

    {
        private Room currentRoom;
        private uint damage;
        public uint health;
        private Boolean isAlive;

        public List<string> Inventory1 = new List<string>();
        public Room CurrentRoom { get { return currentRoom; } set { currentRoom = value; } }


        public void ListIt()
        {
            Inventory1.Add("Medkit");
            Inventory1.Add("Shotgun");

            foreach (string value in Inventory1)
            {
                Console.WriteLine(value);
            }


        }

        public uint Health
        {

            get
            {
                return health;
            }
            set
            {
            }
        }

        public uint Damage
        {

            get
            {
                return damage;
            }
            set
            {
                damage = 2;
            }
        }

        public Player()
        {
            isAlive = true;
            damage = 2;
            health = 10;
        
        }

        public void Death()
        {
            if (health <= 0)
            {
                isAlive = false;
            }
            if (isAlive == false)
            {
                
            }
        }

        


    }
}
