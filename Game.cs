using System;
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
            outside = new Room("outside the main entrance of the university", false);
            theatre = new Room("in a lecture theatre", false);
            pub = new Room("in the campus pub,", false);
            lab = new Room("in a computing lab", false);
            office = new Room("in the computing admin office", false);
            basement = new Room("in the basement", true);

            // initialise room exits
            outside.setExit("east", theatre);
            outside.setExit("south", lab);
            outside.setExit("west", pub);
            pub.RoomInv.Add(new Vodka());
            pub.RoomInv.Add(new Key());
            pub.RoomInv.Add(new Crystal());
            outside.setExit("down", basement);

            theatre.setExit("west", outside);

            pub.setExit("east", outside);

            lab.setExit("north", outside);
            lab.setExit("east", office);

            office.setExit("west", lab);

            basement.setExit("up", outside);
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
                    player.CurrentRoom.RoomInv.listItemsRoom(player.CurrentRoom.RoomInv);
                    break;
                case "health":
                    Console.WriteLine("Health: " + player.health);
                    break;
                case "take":
                    player.CurrentRoom.RoomInv.Take(player.PlayerInv , command.getSecondWord());
                    break;
                case "drop":
                    player.PlayerInv.Drop(player.CurrentRoom.RoomInv , command.getSecondWord());
                    break;
                case "use":
                    use(command);
                    break;
                case "inventory":
                    player.PlayerInv.listItemsPlayer(player.PlayerInv);
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
            }
            else if (nextRoom.locked) {
                Console.WriteLine("The door is locked, you cannot enter without a key!");
            }
            else
            {
                player.CurrentRoom = nextRoom;
                Console.Clear();
                Console.WriteLine(player.CurrentRoom.getLongDescription());
                player.health -= player.damage;
            }


        }

        public void use(Command command)
        {
            Item i = null;

            if (command.hasSecondWord())
            {
                for (int b = player.PlayerInv.Inv.Count - 1; b >= 0; b--) {
                    if (command.getSecondWord() == player.PlayerInv.Inv[b].name)
                    {
                        i = player.PlayerInv.Inv[b];
                    }
                }

                if (command.hasThirdWord())
                {

                    Room roomToUnlock = player.CurrentRoom.getExit(command.getThirdWord());

                    if (roomToUnlock == null)
                    {

                        Console.WriteLine("There is no door to unlock in that direction!");
                        return;

                    }
                    else
                    {

                        if (i == null)
                        {

                            Console.WriteLine("This item does not exist in your inventory!");
                            return;
                        }
                        else
                        {

                            i.use(roomToUnlock);
                            return;
                        }

                    }

                }
                if (i == null)
                {
                    Console.WriteLine("That is not an item.");
                }
                else
                {
                    i.use(player);
                }

            } else
            {
                Console.WriteLine("Specify an item to use here.");
            }
        } 

    }
}
