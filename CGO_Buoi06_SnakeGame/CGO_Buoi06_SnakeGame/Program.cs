using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CGO_Buoi06_SnakeGame
{
    class Program
    {
        // Parameters
        public Random rand = new Random();
        public ConsoleKeyInfo keypress = new ConsoleKeyInfo();
        int score, headX, headY, fruitX, fruitY, nTail;
        int[] TailX = new int[100];
        int[] TailY = new int[100];
        const int height = 20;
        const int width = 60;
        const int panel = 10;
        bool gameOver, reset, isprinted, horizontal, vertical;
        string dir, pre_dir;
        int typeFruit;
        ConsoleColor fruitColor;
        int snakeSpeed;
        int highScore;

        // Show the start banner
        void ShowBanner()
        {
            Console.SetWindowSize(width, height + panel);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.CursorVisible = false;
            Console.WriteLine("=== SNAKE GAME ===");
            Console.WriteLine("How to play:");
            Console.WriteLine("- Press any key to start");
            Console.WriteLine("- Press P to pause the game");
            Console.WriteLine("- Press R to restart the game");
            Console.WriteLine("- Press Q to quit the game");

            keypress = Console.ReadKey();
            if (keypress.Key == ConsoleKey.Q) Environment.Exit(0);
        }

        // Initialize the game
        void Setup()
        {
            dir = "RIGHT";
            pre_dir = "";
            score = 0;
            nTail = 0;
            gameOver = reset = isprinted = false;
            headX = 30;
            headY = 10;
            randomQua();
            typeFruit = rand.Next(1, 3);
            SetFruitColor();
            snakeSpeed = 100;
        }

        // Generate random fruit coordinates
        void randomQua()
        {
            fruitX = rand.Next(1, width - 1);
            fruitY = rand.Next(1, height - 1);
        }

        // Set color for the fruit based on its type
        void SetFruitColor()
        {
            switch (typeFruit)
            {
                case 1:
                    fruitColor = ConsoleColor.Red;
                    break;
                case 2:
                    fruitColor = ConsoleColor.Yellow;
                    break;
                // Add more cases for additional fruit types
                default:
                    fruitColor = ConsoleColor.Red;
                    break;
            }
        }

        // Update the game state
        void Update()
        {
            while (!gameOver)
            {
                CheckInput();
                Logic();
                Render();

                if (reset) break;
                Thread.Sleep(snakeSpeed);
            }

            if (gameOver) Lose();
        }

        // Check for player input
        void CheckInput()
        {
            while (Console.KeyAvailable)
            {
                keypress = Console.ReadKey();
                pre_dir = dir;
                switch (keypress.Key)
                {
                    case ConsoleKey.LeftArrow:
                        dir = "LEFT";
                        break;
                    case ConsoleKey.RightArrow:
                        dir = "RIGHT";
                        break;
                    case ConsoleKey.DownArrow:
                        dir = "DOWN";
                        break;
                    case ConsoleKey.UpArrow:
                        dir = "UP";
                        break;
                    case ConsoleKey.P:
                        dir = "STOP";
                        break;
                    case ConsoleKey.Q:
                        Environment.Exit(0);
                        break;
                }
            }
        }

        // Handle game logic
        void Logic()
        {
            int preX = TailX[0], preY = TailY[0];
            int tempX, tempY;

            if (dir != "STOP")
            {
                TailX[0] = headX;
                TailY[0] = headY;

                for (int i = 1; i < nTail; i++)
                {
                    tempX = TailX[i];
                    tempY = TailY[i];
                    TailX[i] = preX;
                    TailY[i] = preY;
                    preX = tempX;
                    preY = tempY;
                }
            }

            switch (dir)
            {
                case "LEFT":
                    headX--;
                    break;
                case "RIGHT":
                    headX++;
                    break;
                case "UP":
                    headY--;
                    break;
                case "DOWN":
                    headY++;
                    break;
                case "STOP":
                    PauseGame();
                    dir = pre_dir;
                    break;
            }

            if (headX <= 0)
                headX = width - 2;
            else if (headX >= width - 1)
                headX = 1;
            if (headY <= 0)
                headY = height - 2;
            else if (headY >= height - 1)
                headY = 1;

            gameOver = CheckCollision();

            if (headX == fruitX && headY == fruitY)
            {
                score += GetFruitScore();
                nTail++;
                randomQua();
                typeFruit = rand.Next(1, 3);
                SetFruitColor();
                AdjustSpeed();
            }
        }

        // Check for collisions
        bool CheckCollision()
        {
            for (int i = 1; i < nTail; i++)
            {
                if (headX == TailX[i] && headY == TailY[i])
                    return true;
            }
            return false;
        }

        // Get the score for the current fruit type
        int GetFruitScore()
        {
            switch (typeFruit)
            {
                case 1:
                    return 2;
                case 2:
                    return 3;
                // Add more cases for additional fruit types
                default:
                    return 2;
            }
        }

        // Adjust snake speed based on the score
        void AdjustSpeed()
        {
            if (score < 20)
                snakeSpeed = 100;
            else if (score < 50)
                snakeSpeed = 80;
            else if (score < 100)
                snakeSpeed = 60;
            else
                snakeSpeed = 40;
        }

        // Render the game board
        void Render()
        {
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (i == 0 || i == height - 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("#");
                    }
                    else if (j == 0 || j == width - 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("#");
                    }
                    else if (fruitX == j && fruitY == i)
                    {
                        Console.ForegroundColor = fruitColor;
                        Console.Write("?");
                    }
                    else if (headX == j && headY == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("O");
                    }
                    else
                    {
                        isprinted = false;
                        for (int k = 0; k < nTail; k++)
                        {
                            if (TailX[k] == j && TailY[k] == i)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write("o");
                                isprinted = true;
                            }
                        }
                        if (!isprinted)
                            Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Score: " + score);
            Console.WriteLine("High Score: " + highScore);
        }

        // Pause the game
        void PauseGame()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== SNAKE GAME ===");
                Console.WriteLine("Game paused!");
                Console.WriteLine("- Press P to resume");
                Console.WriteLine("- Press R to restart");
                Console.WriteLine("- Press Q to quit");

                keypress = Console.ReadKey();
                if (keypress.Key == ConsoleKey.Q)
                    Environment.Exit(0);
                if (keypress.Key == ConsoleKey.R)
                {
                    reset = true;
                    break;
                }
                if (keypress.Key == ConsoleKey.P)
                    break;
            }
        }

        // Handle game over
        void Lose()
        {
            Console.WriteLine("=== SNAKE GAME ===");
            Console.WriteLine("Game over!");
            Console.WriteLine("- Press R to restart");
            Console.WriteLine("- Press Q to quit");

            if (score > highScore)
                highScore = score;

            while (true)
            {
                keypress = Console.ReadKey();
                if (keypress.Key == ConsoleKey.Q)
                    Environment.Exit(0);
                if (keypress.Key == ConsoleKey.R)
                {
                    reset = true;
                    break;
                }
            }
        }

        static void Main(string[] args)
        {
            Program snakegame = new Program();
            snakegame.ShowBanner();
            while (true)
            {
                snakegame.Setup();
                snakegame.Update();
                Console.Clear();
            }
        }
    }
}
