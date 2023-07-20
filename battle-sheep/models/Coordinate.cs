namespace BattleSheep;
public class Coordinate {
    private int x;
    private int y;
    private int numSheep = 0;
    private char? playerSymbol;
    public Coordinate(int x, int y) {
        this.x = x;
        this.y = y;
        this.numSheep = 0;
    }

    public Coordinate(int x, int y, int numSheep, char? playerSymbol) {
        this.x = x;
        this.y = y;
        this.numSheep = numSheep;
        this.playerSymbol = playerSymbol;
    }

    public int GetX() {
        return x;
    }

    public void SetX(int x) {
        this.x = x;
    }
    
    public int GetY() {
        return y;
    }

    public void SetY(int y) {
        this.y = y;
    }

    public int GetNumSheep() {
        return numSheep;
    }

    public void SetNumSheep(int numSheep) {
        this.numSheep = numSheep;
    }
    
        public char? GetPlayerSymbol() {
        return playerSymbol;
    }

    public void SetPlayerSymbol(char playerSymbol) {
        this.playerSymbol = playerSymbol;
    }
    public static Coordinate Move(Coordinate c, Direction dir, int n, int numSheep = 0, char? playerSymbol = null) {
        int x = c.GetX();
        int y = c.GetY();
        switch(dir) {
            case Direction.STRAIGHTDOWN:
                return new Coordinate(x, y + 2 * n, numSheep, playerSymbol);
            case Direction.DOWNLEFT:
                return new Coordinate(x - n, y + n, numSheep, playerSymbol);
            case Direction.DOWNRIGHT:
                return new Coordinate(x + n, y + n, numSheep, playerSymbol);
            default:
                throw new SystemException("Not all cases covered by enum");
        }
        
    }

    public override string ToString()
    {
        return $"({this.x}, {this.y})";
    }

    public override bool Equals(Object? obj) {
        if ((obj == null) || ! this.GetType().Equals(obj.GetType()))
        {
            return false;
        }
        else {
            Coordinate c = (Coordinate) obj;
            return c.x == x && c.y == y;
        }
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public bool IsAdjacentTo(Coordinate c) {
        return (
            this.Equals(Move(c, Direction.STRAIGHTDOWN, 1)) 
            || this.Equals(Move(c, Direction.STRAIGHTDOWN, -1)) 
            || this.Equals(Move(c, Direction.DOWNLEFT, 1))
            || this.Equals(Move(c, Direction.DOWNLEFT, -1))
            || this.Equals(Move(c, Direction.DOWNRIGHT, 1))
            || this.Equals(Move(c, Direction.DOWNRIGHT, -1))
        );
    }

    public bool IsAdjacentTo(List<Coordinate> cs) {
        return !cs.Contains(this) && cs.Any<Coordinate>(c => c.IsAdjacentTo(this));
    }
}