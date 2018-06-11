using System;
namespace ZuulCS
{
    public class Player

    {
        private Room currentRoom;
        private uint damage;
        public uint health;
        private Boolean isAlive;

        public Room CurrentRoom { get { return currentRoom; } set { currentRoom = value; } }

        public uint Health
        {

            get
            {
                return health;
            }
            set
            {
                if (health <= 0)
                {
                    Console.WriteLine("You died");
                }
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
