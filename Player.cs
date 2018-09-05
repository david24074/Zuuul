using System;
using System.Collections.Generic;
namespace ZuulCS
{
    public class Player

    {
        private Room currentRoom;
        public int damage;
        public int maxHealth;
        public int health;
        private Inventory playerInv;

        public Room CurrentRoom { get { return currentRoom; } set { currentRoom = value; } }
        
        internal Inventory PlayerInv { get => playerInv; }

        public Player()
        {
            damage = 10;
            health = 100;
            maxHealth = 100;
            playerInv = new Inventory(20);
        }
        public void Heal(int amount) {

            if (health == maxHealth) {
                Console.WriteLine("You already are at max health!");
                return;
            } else {
                health += amount;

                if (health > maxHealth) {
                    health = maxHealth;
                }
            }

        }
    }
}
