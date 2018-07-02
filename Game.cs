﻿using System;
using System.Collections.Generic;

namespace ZuulCS
{
    public class Game
    {
        public bool Drink;
        private Parser parser;
        private Player player;
        public Player value;
        public Player PubInv;
        public Player inventory1;

        public Game()
        {

            parser = new Parser();
            player = new Player();
            createRooms();
        }

        private void createRooms()
        {
            Room outside, theatre, pub, lab, office, basement;

            // create the rooms
            outside = new Room("outside the main entrance of the university");
            theatre = new Room("in a lecture theatre");
            pub = new Room("in the campus pub,");
            lab = new Room("in a computing lab");
            office = new Room("in the computing admin office");
            basement = new Room("in the basement");

            // initialise room exits
            outside.setExit("east", theatre);
            outside.setExit("south", lab);
            outside.setExit("west", pub);
            outside.setExit("down", basement);

            theatre.setExit("west", outside);

            pub.setExit("east", outside);
            pub.Items.Add(new Inventory("Vodka", 1, "Heals you because alcohol is healthy. ", true, true));

            lab.setExit("north", outside);
            lab.setExit("east", office);

            office.setExit("west", lab);

            basement.setExit("up", outside);
            basement.Items.Add(new Inventory("Sword ", 3, "an old rusty sword. ", true, false));
            player.CurrentRoom = outside;  // start game outside


        }


        /**
	     *  Main play routine.  Loops until end of play.
	     */
        public void play()
        {
            printWelcome();

            // Enter the main command loop.  Here we repeatedly read commands and
            // execute them until the game is over.
            bool finished = false;
            while (!finished)
            {
                Command command = parser.getCommand();
                finished = processCommand(command);
            }
            Console.WriteLine("Thank you for playing.");
        }

        /**
	     * Print out the opening message for the player.
	     */
        private void printWelcome()
        {
            Console.WriteLine();
            Console.WriteLine("Welcome to Zuul!");
            Console.WriteLine("Zuul is a new, incredibly boring adventure game.");
            Console.WriteLine("Type 'help' if you need help.");
            Console.WriteLine();
            Console.WriteLine(player.CurrentRoom.getLongDescription());

        }

        /**
	     * Given a command, process (that is: execute) the command.
	     * If this command ends the game, true is returned, otherwise false is
	     * returned.
	     */
        private bool processCommand(Command command)
        {
            bool wantToQuit = false;

            if (command.isUnknown())
            {
                Console.WriteLine("I don't know what you mean...");
                return false;
            }

            string commandWord = command.getCommandWord();
            switch (commandWord)
            {
                case "help":
                    printHelp();
                    break;
                case "go":
                    goRoom(command);
                    player.health -= 2;
                    if (player.health <= 0)
                    {
                        wantToQuit = true;
                    }
                    Console.WriteLine("Health: " + player.health);
                    break;
                case "quit":
                    wantToQuit = true;
                    break;
                case "look":
                    Console.WriteLine(player.CurrentRoom.getLongDescription());
                    break;
                case "die":
                    Console.WriteLine("You died");
                    break;
                case "health":
                    Console.WriteLine("Health: " + player.health);
                    break;
                case "inventory":
                    player.ListIt();
                    break;
                case "take":
                    TakeItem();
                    break;
            }

            return wantToQuit;
        }

        // implementations of user commands:

        /**
	     * Print out some help information.
	     * Here we print some stupid, cryptic message and a list of the
	     * command words.
	     */
        private void printHelp()
        {
            Console.WriteLine("You are lost. You are alone, so lonely.");
            Console.WriteLine("You wander around at the university.");
            Console.WriteLine();
            Console.WriteLine("Your command words are:");
            parser.showCommands();
        }

        /**
	     * Try to go to one direction. If there is an exit, enter the new
	     * room, otherwise print an error message.
	     */
        private void goRoom(Command command)
        {
            if (!command.hasSecondWord())
            {
                // if there is no second word, we don't know where to go...
                Console.WriteLine("Go where?");
                return;
            }
            string direction = command.getSecondWord();

            // Try to leave current room.
            Room nextRoom = player.CurrentRoom.getExit(direction);

            if (nextRoom == null)
            {
                Console.WriteLine("There is no door to " + direction + "!");
                player.health += 2;
            }
            else
            {
                player.CurrentRoom = nextRoom;

                Console.WriteLine(player.CurrentRoom.getLongDescription());
                foreach (Inventory Item in player.CurrentRoom.Items)
                {
                    Console.WriteLine(Item.toString());
                }
            }


        }
        public void TakeItem()

        {

            foreach (Inventory Item in player.CurrentRoom.Items)
            {
                player.Inventory1.Add(Item.toString());
                Console.WriteLine("You take the item stored here ");
                
            }

        }
    }
}
