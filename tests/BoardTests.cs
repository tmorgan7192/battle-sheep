namespace tests;

public class BoardTests
{
    private Board TestBoard() {
        return new Board(
            new List<Coordinate>{
                new Coordinate(0,0),
                new Coordinate(1,1),
                new Coordinate(1,-1),
                new Coordinate(2,2),
                new Coordinate(2,0),
                new Coordinate(2,-2),
                new Coordinate(3,3),
                new Coordinate(3,1),
                new Coordinate(3,-1),
                new Coordinate(3,-3),
                new Coordinate(4,4),
                new Coordinate(4,2),
                new Coordinate(4,-2),
                new Coordinate(4,-4),
                new Coordinate(5,5),
                new Coordinate(5,3),
                new Coordinate(5,-3),
                new Coordinate(5,-5),
                new Coordinate(6,4),
                new Coordinate(6,2),
                new Coordinate(6,-4),
                new Coordinate(6,-2),
                new Coordinate(7,3),
                new Coordinate(7,1),
                new Coordinate(7,-1),
                new Coordinate(7,-3),
                new Coordinate(8,2),
                new Coordinate(8,0),
                new Coordinate(8,-2),
                new Coordinate(9,1),
                new Coordinate(9,-1),
                new Coordinate(10,0)
            }
        );
    }

    [Fact]
    public void TestBoundaries()
    {
        Board board = TestBoard();
        Assert.True(board.GetMinX() == 0, $"Min X is {board.GetMinX()}, not 0");
        Assert.True(board.GetMaxX() == 10, $"Max X is {board.GetMaxX()}, not 10");
        Assert.True(board.GetMinY() == -5, $"Min Y is {board.GetMinY()}, not -5");
        Assert.True(board.GetMaxY() == 5, $"Max Y is {board.GetMaxY()}, not -5");

        Assert.True(board.GetMinXInRow(2) == 2, $"Min X in row 2 is {board.GetMinXInRow(2)}, not 2");
        Assert.True(board.GetMaxXInRow(2) == 8, $"Max X in row 2 is {board.GetMaxXInRow(2)}, not 8");
        Assert.True(board.GetMinYInCol(4) == -4, $"Min Y in col 4 is {board.GetMinYInCol(4)}, not -4");
        Assert.True(board.GetMaxYInCol(4) == 4, $"Max Y in col 4 is {board.GetMaxYInCol(4)}, not 4");

        Assert.True(board.IsOnBorder(new Coordinate(5, 5)), "(5,5) is not on border");
        Assert.True(board.IsOnBorder(new Coordinate(5, -5)), "(5,-5) is not on border");
        Assert.False(board.IsOnBorder(new Coordinate(5, 3)), "(5,3) is on border");
        Assert.False(board.IsOnBorder(new Coordinate(5, -3)), "(5,-3) is on border");

        Assert.True(board.IsOnBorder(new Coordinate(2, 2)), "(2, 2) is not on border");
        Assert.True(board.IsOnBorder(new Coordinate(8, 2)), "(8, 2) is not on border");
        Assert.False(board.IsOnBorder(new Coordinate(4, 2)), "(4, 2) is on border");
        Assert.False(board.IsOnBorder(new Coordinate(6, 2)), "((6, 2) is on border");
    }

    [Fact]
    public void TestChangeCoordinates()
    {
        Board board = TestBoard();
        board = board.ChangeCoordinates();

        Assert.True(board.GetMinX() == 0, $"Min X is {board.GetMinX()}, not 0");
        Assert.True(board.GetMaxX() == 10, $"Max X is {board.GetMaxX()}, not 10");
        Assert.True(board.GetMinY() == 0, $"Min Y is {board.GetMinY()}, not 0");
        Assert.True(board.GetMaxY() == 10, $"Max Y is {board.GetMaxY()}, not 10");
    }

    [Fact]
    public void TestPossibleDirections()
    {
        Board board = TestBoard();
        board = board.ChangeCoordinates();
        List<DirectionVector> possibleDirections = board.GetPossibleDirections(board.GetCoordinates()[0]);
        Assert.True(possibleDirections.Count > 0);
        board.GetCoordinates()[0].SetNumSheep(16);
        board.GetCoordinates()[0].SetPlayerSymbol('R');
        board.GetMaxDistance()
    }
}
