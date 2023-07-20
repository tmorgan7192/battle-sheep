namespace BattleSheep
{
    
    class Program
    {
        const int PILE_SIZE = 16;
        const int MIN_PLAYERS = 2;
        const int MAX_PLAYERS = 4;
        static int numPlayers;
        static int currentPlayer;
        static Board board = new Board();

        static void NextPlayer() {
            currentPlayer = (currentPlayer + 1) % numPlayers;
        }

        static int GetNumberInInterval(int min, int max) {
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

        static char GetPlayerSymbol() {
            switch (currentPlayer) {
                case 0: 
                    return 'R';
                case 1:
                    return 'B';
                case 2:
                    return 'W';
                case 3:
                    return 'P';
                default:
                    return (char) currentPlayer;
            }
        }

        static void PlaceTiles() {
            int numTiles = numPlayers * 4;

            board.PlaceInitialTile();
            --numTiles;
            NextPlayer();

            while (numTiles > 0) {
                List<Tile> adjacentOptions = board.ListAdjacentOptions();
                Console.WriteLine($"Player {currentPlayer}: Which tile would you like to add?");
                adjacentOptions.ForEach(tile =>  Console.WriteLine($"{adjacentOptions.IndexOf(tile)}: {tile}"));
                int index = GetNumberInInterval(0, adjacentOptions.Count);
                board.AddTile(adjacentOptions[index]);
                --numTiles;
                NextPlayer();
            }

            board = board.ChangeCoordinates();
        }

        static void PlaceSheep() {
            List<Coordinate> borderHexes = board.GetBorder();
            while(true) {
                Console.WriteLine($"Player {currentPlayer}: Where would you like to place your sheep?");
                borderHexes.ForEach(hex =>  Console.WriteLine($"{borderHexes.IndexOf(hex)}: {hex}"));
                int index = GetNumberInInterval(0, borderHexes.Count);
                Coordinate coordinate = borderHexes[index];
                borderHexes.Remove(coordinate);
                int boardIndex = board.GetCoordinates().IndexOf(coordinate);
                board.GetCoordinates()[boardIndex].SetNumSheep(PILE_SIZE);
                board.GetCoordinates()[boardIndex].SetPlayerSymbol(GetPlayerSymbol());
                NextPlayer();
                if (currentPlayer == 0){
                    break;
                }
            }
        }

        static bool MakeMove() {
            List<Coordinate> playerPiles = board.GetPlayerPiles(GetPlayerSymbol());
            if (playerPiles.Count() == 0) {
                return false;
            }

            Console.WriteLine($"Player {currentPlayer}: Which pile would you like to move from?");
            playerPiles.ForEach(hex =>  Console.WriteLine($"{playerPiles.IndexOf(hex)}: {hex}"));
            int index = GetNumberInInterval(0, playerPiles.Count);
            Coordinate hex = playerPiles[index];

            Console.WriteLine($"How many sheep would you like to move? 1-{hex.GetNumSheep() - 1}");
            int numSheep = GetNumberInInterval(1, hex.GetNumSheep() - 1);

            List<DirectionVector> possibleDirections = board.GetPossibleDirections(hex);
            Console.WriteLine($"Which direction would you like to move?");
            possibleDirections.ForEach(d =>  Console.WriteLine($"{possibleDirections.IndexOf(d)}: {d}"));
            int dirIndex = GetNumberInInterval(0, possibleDirections.Count);
            DirectionVector d = possibleDirections[dirIndex];
            int maxDistance = 0;
            if (d.GetSign() > 0) {
                maxDistance = board.GetMaxDistance(d.GetDirection(), hex);
            }
            else if (d.GetSign() < 0) {
                maxDistance = board.GetMaxReverseDistance(d.GetDirection(), hex);
            }
            
            Console.WriteLine($"How far would you like to move? 1-{maxDistance}");
            int distance = GetNumberInInterval(1, maxDistance);
        
            Coordinate newHex = Coordinate.Move(hex, d.GetDirection(), distance * d.GetSign(), numSheep, GetPlayerSymbol());
            int hexIndex = board.GetCoordinates().IndexOf(hex);
            board.GetCoordinates()[hexIndex].SetNumSheep(board.GetCoordinates()[hexIndex].GetNumSheep() - numSheep);
            int newHexIndex = board.GetCoordinates().FindIndex(c => c.GetX() == newHex.GetX() && c.GetY() == newHex.GetY());
            board.GetCoordinates()[newHexIndex].SetNumSheep(numSheep);
            board.GetCoordinates()[newHexIndex].SetPlayerSymbol(GetPlayerSymbol());

            return true;
        }

        static void Main(string[] args)
        {
            Console.WriteLine($"How many players ({MIN_PLAYERS}-{MAX_PLAYERS})?");
            numPlayers = GetNumberInInterval(MIN_PLAYERS, MAX_PLAYERS);
            currentPlayer = 0;
            bool[] canPlay = new bool[numPlayers];
            for (int i=0; i<numPlayers; ++i) {
                canPlay[i] = true;
            }

            PlaceTiles();
            Console.WriteLine($"\n{board}");

            PlaceSheep();
            Console.WriteLine($"\n{board}");

            while (canPlay.Any(p => p)) {
                canPlay[currentPlayer] = MakeMove();
                NextPlayer();
                Console.WriteLine($"\n{board}");
            }

            Console.WriteLine("Game Over!");
        }
    }
}