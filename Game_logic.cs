using System;
using System.Collections;

namespace LiarsDice
{
    public class Game
    {
    public static int dicecount = 5;
    public class Bid
    {
        public int quantity;
        public int faceValue;
    }
    public static void Start()
        {


            int[] dice = new int[dicecount];
            RollDice(dice);
            int[] diceComputer = new int[dicecount];
            RollDice(diceComputer);

            Bid currentBid = new Bid { quantity = 0, faceValue = 0 }; // currentBid[0] is the quantity, currentBid[1] is the face value
            PrintDice(dice);
            Console.WriteLine("make the original bid!");
            currentBid = GetPlayerBid(currentBid); 

            while (true)
            {
                Console.Clear();
                string computerAction = ComputerTurn(currentBid, diceComputer);
                if (computerAction == "Bid")
                {
                    currentBid = new Bid { quantity = currentBid.quantity + 1, faceValue = currentBid.faceValue };
                }
                else if (computerAction == "Liar")
                {
                    resolve_Liar(computerAction, currentBid, dice, diceComputer, "computer");
                }
                else
                {
                    Console.WriteLine("how did we get here? - we are in the main game loop");
                }
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                PrintDice(dice);
                string action = return_player_action();
                if (action == "Bid")
                {
                    currentBid = GetPlayerBid(currentBid);
                }
                else if (action == "Liar")
                {
                    resolve_Liar(action, currentBid, dice, diceComputer, "player");
                }
                else
                {
                    Console.WriteLine("how did we get here? - we are in the main game loop");
                }
    
            }
        }
        public static string ComputerTurn(Bid currentBid, int[] diceComputer)
        {
            // This is a very basic implementation of the computer's turn. 
            // It will always bid one more than the current bid, and will call "Liar" if it thinks the current bid is unlikely to be true based on its own dice.
            int computerBidQuantity = currentBid.quantity + 1;
            int computerBidFaceValue = currentBid.faceValue;
            int ComputerDiceCountOfCurrentFaceBid = 0;
            for (int i = 0; i < diceComputer.Length; i++)
            {
                if (diceComputer[i] == currentBid.faceValue)
                {
                    ComputerDiceCountOfCurrentFaceBid++;
                }
            }
            if ( 1 + ComputerDiceCountOfCurrentFaceBid <= currentBid.quantity)
            {
                Console.WriteLine($"Computer has {ComputerDiceCountOfCurrentFaceBid} of {currentBid.faceValue}'s and calls Liar!");
                Console.WriteLine("Computer calls Liar!");
                return "Liar";
            }
            
            Console.WriteLine($"Computer bids {computerBidQuantity} of {computerBidFaceValue}'s");
            return "Bid";
        }  

    
        public static void resolve_Liar(string action, Bid currentBid, int[] dice, int[] diceComputer, string resolver)
        {
        
            bool Is_DicePool_larger_than_Currentbid = Liar_choice_returns_winner(currentBid, dice, diceComputer);
            if (Is_DicePool_larger_than_Currentbid == true)
            {
                Console.WriteLine($"CONGRATS ON WINNING, {resolver} wins!");
            }
            else if (Is_DicePool_larger_than_Currentbid == false)
            {
                Console.WriteLine($"CONGRATS ON LOOSING, {resolver} loses!");
            }
            else
            {
                Console.WriteLine("i have no idea who won, i am truly sorry for my incompentance, please do report. For the developer, this comment is under Game.Start");
            }
            Environment.Exit(0);
        }

        public static bool Liar_choice_returns_winner(Bid bid, int [] dice_player, int[] dice_Computer)
        {
            int dice_bid_count = 0;
            int[] dicepool = new int[dice_player.Length + dice_Computer.Length];
            dice_player.CopyTo(dicepool, 0);
            dice_Computer.CopyTo(dicepool, dice_player.Length);

            for(int i = 0; i<dicepool.Length; i++)
            {
                Console.WriteLine($"the {i+1}'nth dice reads {dicepool[i]}");
                if(dicepool[i]== bid.faceValue)
                {
                    dice_bid_count++;
                }
            }
            if(dice_bid_count < bid.quantity)
            {
                return true;
            }
            else if (dice_bid_count >= bid.quantity)
            {
                return false;
            }
            else
            {
                Console.WriteLine("how did we get here? - we are in the liar_choice function");
                return false;
            }
            
        }
        public static string return_player_action()
        {
            Console.WriteLine("write \"Bid\" to make a higher bid \n write \"Liar\" to call the bluff");
            string input = Console.ReadLine();
            if (input == "Bid" || input == "Liar")
            {
                return input;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter \"Bid\" or \"Liar\".");
                return return_player_action();
            }    

        }
       
        public static Bid GetPlayerBid(Bid currentBid)
        {
            try
            {
                Console.WriteLine("Enter your bid (quantity and face value): ");
                string input = Console.ReadLine();
                string[] parts = input.Split(' ');

                int qty = int.Parse(parts[0]);
                int face = int.Parse(parts[1]);

                if (face < 1 || face > 6)
                    throw new Exception();

                if (qty <= currentBid.quantity && face <= currentBid.faceValue)
                    throw new Exception();

                return new Bid { quantity = qty, faceValue = face };
            }
            catch
            {
                Console.WriteLine("Invalid input. Please enter a quantity and a valid face value separated by a space.");
                return GetPlayerBid(currentBid);
            }
        }

        public static void PrintDice(int[] dice)
        {
            for (int i = 0; i < dice.Length; i++)
            {
                Console.WriteLine($"Die {i + 1}: {dice[i]}");
            }
        }
        public static int[] RollDice(int[] dice)
        {
            for (int i = 0; i < dice.Length; i++)
            {
                dice[i] = RandomInt(1, 7);
            }
            return dice;
        }
            public static int RandomInt(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
    }
}