
using System;

class Program
{
    static void Main(string[] args)
    {
        //here i identified the needed variables 

        int[] playerAnswer = new int[4];
        int[] secretCode = new int[4];
        int currentNum = 0;

        bool gameOn = true;
        int attemepts = 0;
        int maxAttemepts = 9;
        string? codeFromArgs = null;
        int maxAttempts = 10;
        // we take argument as needed 
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "-c" && i + 1 < args.Length)
            {
                codeFromArgs = args[i + 1];
            }
            // second -t for attempts
            else if (args[i] == "-t" && i + 1 < args.Length && int.TryParse(args[i + 1], out int attempts))
            {
                maxAttempts = attempts;
            }
        }


        secretCode = new int[4];

        if (!string.IsNullOrEmpty(codeFromArgs))
        {
            if (codeFromArgs.Length == 4 && codeFromArgs.All(char.IsDigit))
            {
                for (int i = 0; i < 4; i++)
                    secretCode[i] = codeFromArgs[i] - '0';
            }
            else
            {
                Console.WriteLine("Invalid code it must be 4 numbers ");
                return;
            }
        }
        else
        {

            Random randomSecretCode = new Random();
            for (int i = 0; i < 4; i++)
            {
                secretCode[i] = randomSecretCode.Next(0, 8);
            }
        }

        while (gameOn == true && attemepts < 9)
        {
            // here where we ask to guess and then make sure its a valid guess
            Console.WriteLine("Can you break the code? Enter a valid guess (4 numbers with no Space betweenThem)");
            // as requested in the documents 
            String? guess = Console.ReadLine()!;
            if (guess == null)
            {
                Console.WriteLine("game has Ended By User");
                break;
            }
            Console.WriteLine(guess);

            playerAnswer = new int[4];
            currentNum = 0;
            for (int i = 0; i < 4; i++)
            {
                if (char.IsDigit(guess[i]) && guess[i] >= '0' && guess[i] <= '8')
                {
                    playerAnswer[i] = guess[i] - '0';
                }
                else
                {
                    currentNum = i + 1;
                    Console.WriteLine("Invalid guess Number " + currentNum + " please number with no Space between Them between 0-8");

                }
            }
            // we need to reset each time the player take a guess 
            int wellPlaced = 0;
            int misPlaced = 0;
            bool[] secretUsed = new bool[4];
            bool[] guessUsed = new bool[4];
            // now we check well placed and misplaced

            for (int i = 0; i < 4; i++)
            {
                if (playerAnswer[i] == secretCode[i])
                {
                    wellPlaced++;
                    secretUsed[i] = true;
                    guessUsed[i] = true;
                }
            }
            for (int i = 0; i < 4; i++)
            {


                for (int j = 0; j < 4; j++)
                {
                    if (!secretUsed[j] && playerAnswer[i] == secretCode[j])
                    {
                        misPlaced++;
                        secretUsed[j] = true;
                        break;
                    }
                }
            }
            Console.WriteLine("Round" + attemepts );
            Console.WriteLine("well-Placed " + wellPlaced);
            Console.WriteLine("misplaced " + misPlaced);
            attemepts++;
            if (wellPlaced == 4)
            {
                Console.WriteLine("Congratz! you did it");
                break;
            }



        }



    }
}
