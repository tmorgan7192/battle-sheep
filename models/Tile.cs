namespace BattleSheep;

public class Tile {
    private Orientation o;
    private List<Coordinate> coordinates;
    public Tile(Orientation o, Coordinate start) {
        this.o = o;
        switch(o) {
            case Orientation.FLAT:
                coordinates = new List<Coordinate> { 
                    new Coordinate(start.GetX() + 0, start.GetY() + 0), 
                    new Coordinate(start.GetX() + 1, start.GetY() + 1), 
                    new Coordinate(start.GetX() + 1, start.GetY() - 1), 
                    new Coordinate(start.GetX() + 2, start.GetY() + 0)
                };
                break;
            case Orientation.UP:
                coordinates = new List<Coordinate> { 
                    new Coordinate(start.GetX() + 0, start.GetY() + 0), 
                    new Coordinate(start.GetX() + 1, start.GetY() + 1), 
                    new Coordinate(start.GetX() + 0, start.GetY() + 2), 
                    new Coordinate(start.GetX() + 1, start.GetY() + 3)
                };
                break;
            case Orientation.DOWN:
                coordinates = new List<Coordinate> { 
                    new Coordinate(start.GetX() + 0, start.GetY() + 0), 
                    new Coordinate(start.GetX() + 1, start.GetY() - 1), 
                    new Coordinate(start.GetX() + 0, start.GetY() - 2), 
                    new Coordinate(start.GetX() + 1, start.GetY() - 3)
                };
                break;
            default:
                throw new SystemException("Missing enum value in switch");
        }
    }

    public List<Coordinate> GetCoordinates() {
        return coordinates;
    }

    public void SetCoordinates(List<Coordinate> modelCoordinates) {
        coordinates = modelCoordinates;
    }

    public void Move(List<Coordinate> coordindates, Direction dir, int n) {
        coordinates.ForEach( c => Coordinate.Move(c, dir, n));
    }

    public bool Intersects(List<Coordinate> cs) {
        return cs.Any<Coordinate>(c => coordinates.Contains(c));
    }

    public bool IsAdjacentTo(List<Coordinate> cs) {
        return !this.Intersects(cs) && cs.Any<Coordinate>(c => c.IsAdjacentTo(this.coordinates));
    }

    public override string? ToString()
    {
        string res = "";
        coordinates.ForEach(c => res += $"({c.GetX()}, {c.GetY()}) ");
        return res;
    }
}