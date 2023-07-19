﻿namespace BattleSheep
{
    
    class Program
    {
        const int PILE_SIZE = 16;
        const int MIN_PLAYERS = 2;
        const int MAX_PLAYERS = 4;
        static int numPlayers;
        static int currentPlayer;
        static Board board = new Board();

        static void nextPlayer() {
            currentPlayer = (currentPlayer + 1) % numPlayers;
        }

        static int getNumberInInterval(int min, int max) {
            int num = min - 1;
            var res = Console.ReadLine();
            while (num < min || num > max)
            try{
                num = Convert.ToInt32(res);
                if (num < min || num > max) {
                    Console.WriteLine($"Please enter a number between {min} and {max}");
                    res = Console.ReadLine();
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"{res} is not formatted correctly.  Please input an integer between {min} and {max}.");
                res = Console.ReadLine();
            }
            return num;
        }

        static void Main(string[] args)
        {
            Console.WriteLine($"How many players ({MIN_PLAYERS}-{MAX_PLAYERS})?");
            numPlayers = getNumberInInterval(MIN_PLAYERS, MAX_PLAYERS);
            currentPlayer = 0;
            int numTiles = numPlayers * 4;

            board.PlaceInitialTile();
            --numTiles;
            nextPlayer();

            while (numTiles > 0) {
                List<Tile> adjacentOptions = board.ListAdjacentOptions();
                Console.WriteLine($"Player {currentPlayer}: Which tile would you like to add?");
                adjacentOptions.ForEach(tile =>  Console.WriteLine($"{adjacentOptions.IndexOf(tile)}: {tile}"));
                int index = getNumberInInterval(0, adjacentOptions.Count);
                board.AddTile(adjacentOptions[index]);
                --numTiles;
                nextPlayer();
            }

            Console.WriteLine(board);

        }
    }
}