namespace Wordle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameTitle();
            bool exitGame = false;
            int score = 0;
            int maxScore = 0;
            List<string> word = new List<string>();
            Random rnd = new Random();
            string puzzleWord;
            string userInput = "";
            word = GetWordList("wordleList.txt");

            Console.ForegroundColor = ConsoleColor.Gray;
            while(!exitGame)
            {

                //get random word and remove from the list.
                puzzleWord = word[rnd.Next(0, word.Count - 1)].ToLower();
                word.Remove(puzzleWord);                           //remove from list.

                Console.WriteLine("\n~ Current Word ~");
                Console.WriteLine(new string('#', puzzleWord.Length));
                Console.WriteLine(puzzleWord + " " + puzzleWord.Length);          //Delete

                for (int i = 1; i <= 6; i++ )
                {
                    Console.WriteLine("Tries left " + (6 - i));

                    //User Guess the word
                    Console.Write("\nGuess your word: ");
                    userInput = GetInput(puzzleWord.Length);

                    if(CheckWord(puzzleWord, userInput))
                    {
                        Console.WriteLine("Puzzle solved!");
                        score += i;
                        Console.WriteLine("Total Score: " + score);
                        break;
                    }
                    else 
                    {
                        Console.WriteLine("\nNice try.");
                    }

                }

                Console.WriteLine("Do you wish to exit? Y/N: ");
                userInput = Console.ReadKey().ToString().ToLower();
                if(userInput == "Y")
                {
                    exitGame = true;
                }
                else
                {
                    Console.Clear();
                    exitGame = false;
                }

            }


            // Remove when finished
            Console.ReadLine();
        }

        static string GetInput(int maxLen)
        {
            string input = "";

            while(input.Length != maxLen)
            {
                try
                {
                    input = Console.ReadLine().ToLower();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                if(input.Length != maxLen)
                {
                    Console.WriteLine("Please enter the correct sized word of {0}\n", maxLen);
                }
            }

            return input;
        }

        static bool CheckWord(string curWord, string input)
        {
            bool wrongChar = false;

            if(curWord == input)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(input);
                Console.ForegroundColor = ConsoleColor.Gray;
                wrongChar = false;

                return true;
            }


            for(int i = 0; i < input.Length; i++)
            {

                for(int j = 0; j < curWord.Length; j++)
                {

                    if (input[i] == curWord[i])
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(input[i]);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        wrongChar = false;
                        break;
                    }


                    if (i == j && input[i] == curWord[j])
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(input[i]);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        wrongChar = false;
                        break;
                    }

                    if (input[i] == curWord[j])
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(input[i]);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        wrongChar = false;
                        break;
                    }

                    wrongChar = true;
                }

                if(wrongChar)
                {
                    Console.Write(input[i]);
                    wrongChar = false;
                }
            }

            return false;
        }

        static void GameTitle()
        {
            string txtPadding = new string('=', 57);
            Console.WriteLine(txtPadding);
            Console.WriteLine(" °º¤ø,¸¸,ø¤º°`°º¤ø,¸ WORDLE C# eidtion ¸,ø¤º°`°º¤ø,¸,ø¤°");
            Console.WriteLine(txtPadding);
        }

        static List<string> GetWordList(string txtFile)
        {
            List<string> randomWord = new List<string>();

            try 
            {
                foreach(string line in System.IO.File.ReadAllLines(@txtFile)) 
                {
                    randomWord.Add(line);
                }
                
            }catch(Exception e) 
            {
                Console.WriteLine(e.Message);
            }

            return randomWord;
        }
    }
}