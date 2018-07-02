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
        public Player player;



        public List<string> Inventory1 = new List<string>();
        public List<string> Inventory2 = new List<string>();
        private List<Inventory> inventory3 = new List<Inventory>();
        public List<Inventory> Inventory3 { get { return inventory3; } set{inventory3 = value; } }
        public List<string> pub1 = new List<string>();
        public Room CurrentRoom { get { return currentRoom; } set { currentRoom = value; } }


        public void ListIt()
        {
            Inventory1.Add("Medkit");

            foreach (string value in Inventory1)
            {
                Console.WriteLine("You have a " + value + " with you");
            }
        }

        public void Listall()
        {
            
            foreach (string value in Inventory2)
            {
                Console.WriteLine("Theres a " + value + " here");
            }
        }

        public void TakeItem(Inventory Inventory1)
        {

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
