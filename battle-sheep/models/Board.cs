namespace BattleSheep;

public class Board {
    private List<Coordinate> coordinates;

    public Board() {
        coordinates = new List<Coordinate>{};
    }

    public Board(List<Coordinate> coordinates){
        this.coordinates = coordinates;
    }

    public List<Coordinate> GetCoordinates() {
        return coordinates;
    }

    public void SetCoordinates(List<Coordinate> coordinates) {
        this.coordinates = coordinates;
    }

    public void AddTile(Tile tile) {
        tile.GetCoordinates().ForEach(c => coordinates.Add(c));
    }

    public void PlaceInitialTile() {
        Tile initTile = new Tile(Orientation.FLAT, new Coordinate(0,0));
        coordinates = initTile.GetCoordinates();
    }

    public List<Tile> ListAdjacentOptions() {
        List<Tile> options = new List<Tile>{};
        foreach(Coordinate c in this.GetCoordinates()) {
            foreach(Direction d in Enum.GetValues(typeof(Direction))){
                foreach(Orientation o in Enum.GetValues(typeof(Orientation))) {
                    Coordinate startingCoordinate = Coordinate.Move(c, d, 1);
                    Tile newTile = new Tile(o, startingCoordinate);
                    if (newTile.IsAdjacentTo(this.coordinates)) {
                        options.Add(newTile);
                    }
                }
            }
        }

        return options;
    }

    public int GetMinX() {
        return coordinates.MinBy(c => c.GetX()).GetX();
    }
    public int GetMinY() {
        return coordinates.MinBy(c => c.GetY()).GetY();
    }
    public int GetMaxX() {
        return coordinates.MaxBy(c => c.GetX()).GetX();
    }
    public int GetMaxY() {
        return coordinates.MaxBy(c => c.GetY()).GetY();
    }
    public Board ChangeCoordinates() {

        return new Board(
            coordinates.ConvertAll(
                c => new Coordinate(c.GetX() - this.GetMinX(), c.GetY() - this.GetMinY(), c.GetNumSheep(), c.GetPlayerSymbol())
            )
        );
    }

    public int GetMinXInRow(int y) {
        return coordinates.FindAll(c => c.GetY() == y).MinBy(c => c.GetX()).GetX();
    }
    public int GetMaxXInRow(int y) {
        return coordinates.FindAll(c => c.GetY() == y).MaxBy(c => c.GetX()).GetX();
    }
        public int GetMinYInCol(int x) {
        return coordinates.FindAll(c => c.GetX() == x).MinBy(c => c.GetY()).GetY();
    }
    public int GetMaxYInCol(int x) {
        return coordinates.FindAll(c => c.GetX() == x).MaxBy(c => c.GetY()).GetY();
    }


    public bool IsOnBorder(Coordinate c) {
        return (
            (new List<int> {GetMaxXInRow(c.GetY()), GetMinXInRow(c.GetY())}).Contains(c.GetX())
            || (new List<int> {GetMaxYInCol(c.GetX()), GetMinYInCol(c.GetX())}).Contains(c.GetY())
        );
    }

    public List<Coordinate> GetBorder() {
        return coordinates.FindAll(c => IsOnBorder(c));
    }

    public override string? ToString()
    {
        Board newBoard = this.ChangeCoordinates();
        int xMax = 3 * (newBoard.GetMaxX() + 1) + 3;
        int yMax = 2 * (newBoard.GetMaxY() + 1) + 5;
        string[,] toString = new string[xMax,yMax];
        for(int col=0;col<yMax;col++){  
            for(int row=0;row<xMax;row++){
                if(row % 3 == 2) {
                    toString[row, col] = "    ";
                }
                else{
                    toString[row, col] = " ";
                }
            }
        }
        foreach(Coordinate c in newBoard.coordinates) {
            toString[3*c.GetX() + 2, 2*c.GetY() + 0] = "____";

            toString[3*c.GetX() + 1, 2*c.GetY() + 1] = "/";
            toString[3*c.GetX() + 3, 2*c.GetY() + 1] = "\\";

            toString[3*c.GetX() + 0, 2*c.GetY() + 2] = "/";
            toString[3*c.GetX() + 2, 2*c.GetY() + 2] = c.GetNumSheepString();
            toString[3*c.GetX() + 4, 2*c.GetY() + 2] = "\\";

            toString[3*c.GetX() + 0, 2*c.GetY() + 3] = "\\";
            toString[3*c.GetX() + 2, 2*c.GetY() + 3] = c.GetPlayerSymbol();
            toString[3*c.GetX() + 4, 2*c.GetY() + 3] = "/";

            toString[3*c.GetX() + 1, 2*c.GetY() + 4] = "\\";
            toString[3*c.GetX() + 2, 2*c.GetY() + 4] = "____";
            toString[3*c.GetX() + 3, 2*c.GetY() + 4] = "/";
        }
        string res = "";
        for(int col=0;col<yMax;col++){  
            for(int row=0;row<xMax;row++){  
                res += toString[row, col];
            }  
            res += "\n";
        }
        return res;
    }

    public List<Coordinate> GetPlayerPiles(string playerSymbol) {
        return this.coordinates.FindAll(c => c.GetPlayerSymbol() == playerSymbol && c.GetNumSheep() > 1 && GetPossibleDirections(c).Count() > 0);
    }

    public List<DirectionVector> GetPossibleDirections(Coordinate hex) {
        List<DirectionVector> possibleDirections = new List<DirectionVector>{};
        foreach(Direction d in Enum.GetValues(typeof(Direction))){
            if (this.GetMaxDistance(d, hex) > 0) {
                possibleDirections.Add(new DirectionVector(d, 1));
            }
            if (this.GetMaxReverseDistance(d, hex) > 0) {
                possibleDirections.Add(new DirectionVector(d, -1));
            }
        }
        return possibleDirections;
    }

    public int GetMaxDistance(Direction d, Coordinate c) {
        int n;
        for(n=1; true; ++n) {
            Coordinate newCoordinate = Coordinate.Move(c, d, n);
            Coordinate? testCoordinate = coordinates.FirstOrDefault(
                testC => testC.GetX() == newCoordinate.GetX() && testC.GetY() == newCoordinate.GetY(),
                null
            );
            if (testCoordinate == null || testCoordinate.GetNumSheep() > 0) {
                break;
            }

        }
        return n - 1;
    }

    public int GetMaxReverseDistance(Direction d, Coordinate c) {
        int n;
        for(n=-1; true; --n) {
            Coordinate newCoordinate = Coordinate.Move(c, d, n);
            Coordinate? testCoordinate = coordinates.FirstOrDefault(
                testC => testC.GetX() == newCoordinate.GetX() && testC.GetY() == newCoordinate.GetY(),
                null
            );
            if (testCoordinate == null || testCoordinate.GetNumSheep() > 0) {
                break;
            }

        }
        return -1 * (n + 1);
    }
    public int GetScore(string playerSymbol) {
        return this.coordinates.Count(hex => hex.GetPlayerSymbol() == playerSymbol);
    }
}