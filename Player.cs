using System;
namespace ZuulCS
{
    public class Player

    {
        private Room currentRoom;
        private uint damage;
        private uint health;
        private Boolean isAlive;

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

        public Player()
        {
            isAlive = true;
            damage = 2;
            health = 10;
        
        }

        public void Update()
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
