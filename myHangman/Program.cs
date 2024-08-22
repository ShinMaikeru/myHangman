using System;
using System.Collections.Generic;
using static System.Random;
using System.Text;
using System.Linq;
using System.IO;

namespace myHangman
{
    internal class Program
    {
        private static void PrintHangman(int wrong)
        {
            string[] hangman = {
                        "\n+---+\n    |\n    |\n    |\n   ===",
                        "\n+---+\no   |\n    |\n    |\n   ===",
                        "\n+---+\no   |\n|   |\n    |\n   ===",
                        "\n+---+\n o  |\n/|  |\n    |\n   ===",
                        "\n+---+\n o  |\n/|\\ |\n    |\n   ===",
                        "\n+---+\n o  |\n/|\\ |\n/   |\n   ===",
                        "\n+---+\n o  |\n/|\\ |\n/ \\ |\n   ==="
                    };

            Console.WriteLine(hangman[wrong]);
        }

        private static int PrintWord(List<char> guessedLetters, string randomWord)
        {
            int rightLetters = 0;
            Console.WriteLine();
            foreach (char c in randomWord)
            {
                if (guessedLetters.Contains(c))
                {
                    Console.Write(c + " ");
                    rightLetters++;
                }
                else
                {
                    Console.Write("_ ");
                }
            }
            return rightLetters;
        }

        private static void PrintLines(string randomWord)
        {
            Console.WriteLine();
            foreach (char c in randomWord)
            {
                Console.OutputEncoding = Encoding.Unicode;
                Console.Write("\u0305 ");
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Hangman!!");
            Console.WriteLine("Pokemon Edition");
            Console.WriteLine("----------------------------");

            Random random = new Random();
            // Read the words from the file
            List<string> wordDictionary = File.ReadAllLines("words.txt").ToList();
            string randomWord = wordDictionary[random.Next(wordDictionary.Count)];

            Console.WriteLine(string.Join(" ", Enumerable.Repeat("_", randomWord.Length)));

            int lengthOfWordToGuess = randomWord.Length;
            int amountOfTimesWrong = 0;
            List<char> currentLettersGuessed = new List<char>();
            int currentLettersRight = 0;

            while (amountOfTimesWrong != 6 && currentLettersRight != lengthOfWordToGuess)
            {
                Console.Write("\nLetters guessed so far: ");
                Console.WriteLine(string.Join(" ", currentLettersGuessed));

                Console.Write("\nGuess a letter: ");
                char letterGuessed = char.ToLower(Console.ReadLine()[0]);

                if (currentLettersGuessed.Contains(letterGuessed))
                {
                    Console.WriteLine("\r\nYou have already guessed this letter. :p");
                    PrintHangman(amountOfTimesWrong);
                    currentLettersRight = PrintWord(currentLettersGuessed, randomWord);
                    PrintLines(randomWord);
                }
                else
                {
                    bool right = false;
                    for (int i = 0; i < randomWord.Length; i++)
                    {
                        if (letterGuessed == randomWord[i])
                        {
                            right = true;
                            break;
                        }
                    }

                    if (right)
                    {
                        PrintHangman(amountOfTimesWrong);
                        currentLettersGuessed.Add(letterGuessed);
                        currentLettersRight = PrintWord(currentLettersGuessed, randomWord);
                        Console.WriteLine();
                        PrintLines(randomWord);
                    }
                    else
                    {
                        amountOfTimesWrong++;
                        currentLettersGuessed.Add(letterGuessed);
                        PrintHangman(amountOfTimesWrong);
                        currentLettersRight = PrintWord(currentLettersGuessed, randomWord);
                        Console.WriteLine();
                        PrintLines(randomWord);
                    }
                }
            }

            Console.WriteLine("\r\nGame Over! See ya!");
            Console.ReadLine();
        }
    }
}
