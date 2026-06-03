using System;
using System.Collections;

namespace LiarsDice
{
    public class Game
    {
    public static void Start()
        {
            int[] dice = new int[5];
            RollDice(dice);
            int[] diceComputer = new int[5];
            RollDice(diceComputer);

            int[] currentBid = {0,0};
            Console.WriteLine("make the original bid!");
            currentBid = GetPlayerBid(currentBid); 

            while (true)
            {
                Console.Clear();
                PrintDice(dice);
                string action = return_action();
                if (action == "Bid")
                {
                    currentBid = GetPlayerBid(currentBid);
                }
                else if (action == "Liar")
                {
                    bool if_winner_called = Liar_choice(currentBid, dice, diceComputer);
                    if (if_winner_called== true)
                    {
                        Console.WriteLine("CONGRATS ON WINNING");
                    }
                    else if (if_winner_called == true)
                    {
                         Console.WriteLine("CONGRATS ON LOOSING");
                    }
                    else
                    {
                        Console.WriteLine("i have no idea who won, i am truly sorry for my incompentance, please do report. For the developer, this comment is under Game.Start");
                    }
                    //Console.WriteLine($"bool is {if_winner_called}");
                    break;
                }

            }
        }

        public static bool Liar_choice(int[] bid, int [] dice_player, int[] dice_Computer)
        {
            int dice_bid_count = 0;
            int[] dicepool = dice_player.Concat(dice_player).ToArray();


            for(int i = 0; i<dicepool.Length; i++)
            {
                Console.WriteLine($"the {i+1}'nth dice reads {dicepool[i]}");
                if(dicepool[i]== bid[1])
                {
                    dice_bid_count++;
                }
            }
            if(dice_bid_count < bid[0])
            {
                return false;
            }
            else if (dice_bid_count >= bid[0])
            {
                return true;
            }
            else
            {
                Console.WriteLine("how did we get here? - we are in the liar_choice function");
                return false;
            }
            
        }
        public static string return_action()
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
                    return return_action();
                }    

        }
        public static int[] GetPlayerBid(int[] currentBid)
        {
            try{
            Console.WriteLine("Enter your bid (quantity and face value): ");
            string input = Console.ReadLine();
            string[] parts = input.Split(' ');
            
            if (int.Parse(parts[1]) >= 7)
                {
                    throw new Exception();
                }
            if (int.Parse(parts[0]) <= currentBid[0] && int.Parse(parts[1]) <= currentBid[1])
                {
                    throw new Exception();
                }

            return new int[]{int.Parse(parts[0]), int.Parse(parts[1])};

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