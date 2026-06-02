namespace LiarsDice
{
    public class Game
    {
    public static void Start()
        {
            int[] dice = new int[5];
            RollDice(dice);

            int[] currentBid = {0,0};
            while (true)
            {
                Console.Clear();
                PrintDice(dice);
                currentBid = GetPlayerBid(currentBid); 
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