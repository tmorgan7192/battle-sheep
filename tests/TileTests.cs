namespace tests;

public class TileTests
{
    [Fact]
    public void testIntersects()
    {
        Tile flat = new Tile(Orientation.FLAT, new Coordinate(0,0));
        Tile up = new Tile(Orientation.UP, new Coordinate(0,0));
        Tile down = new Tile(Orientation.DOWN, new Coordinate(0,0));
        Tile flat1 = new Tile(Orientation.FLAT, new Coordinate(0,2)); 
        Tile up1 = new Tile(Orientation.UP, new Coordinate(0,2));
        Tile down1 = new Tile(Orientation.DOWN, new Coordinate(0,2));
        Tile flat2 = new Tile(Orientation.FLAT, new Coordinate(1,1)); 
        Tile up2 = new Tile(Orientation.UP, new Coordinate(1,1));
        Tile down2 = new Tile(Orientation.DOWN, new Coordinate(1,1));

        Assert.True(flat.Intersects(flat.GetCoordinates()));
        Assert.True(flat.Intersects(flat1.GetCoordinates()));
        Assert.True(flat.Intersects(flat2.GetCoordinates()));
        Assert.True(flat.Intersects(up.GetCoordinates()));
        Assert.False(flat.Intersects(up1.GetCoordinates()));
        Assert.True(flat.Intersects(up2.GetCoordinates()));
        Assert.True(flat.Intersects(down.GetCoordinates()));
        Assert.True(flat.Intersects(down1.GetCoordinates()));
        Assert.True(flat.Intersects(down2.GetCoordinates()));

        Assert.True(up.Intersects(flat.GetCoordinates()));
        Assert.True(up.Intersects(flat1.GetCoordinates()));
        Assert.True(up.Intersects(flat2.GetCoordinates()));
        Assert.True(up.Intersects(up.GetCoordinates()));
        Assert.True(up.Intersects(up1.GetCoordinates()));
        Assert.True(up.Intersects(up2.GetCoordinates()));
        Assert.True(up.Intersects(down.GetCoordinates()));
        Assert.True(up.Intersects(down1.GetCoordinates()));
        Assert.True(up.Intersects(down2.GetCoordinates()));

        Assert.True(down.Intersects(flat.GetCoordinates()));
        Assert.False(down.Intersects(flat1.GetCoordinates()));
        Assert.False(down.Intersects(flat2.GetCoordinates()));
        Assert.True(down.Intersects(up.GetCoordinates()));
        Assert.False(down.Intersects(up1.GetCoordinates()));
        Assert.False(down.Intersects(up2.GetCoordinates()));
        Assert.True(down.Intersects(down.GetCoordinates()));
        Assert.True(down.Intersects(down1.GetCoordinates()));
        Assert.True(down.Intersects(down2.GetCoordinates()));
    }

    [Fact]
    public void testIsAdjacentTo()
    {
        Tile flat = new Tile(Orientation.FLAT, new Coordinate(0,0));
        Tile up = new Tile(Orientation.UP, new Coordinate(0,0));
        Tile down = new Tile(Orientation.DOWN, new Coordinate(0,0));
        Tile flat1 = new Tile(Orientation.FLAT, new Coordinate(0,2)); 
        Tile up1 = new Tile(Orientation.UP, new Coordinate(0,2));
        Tile down1 = new Tile(Orientation.DOWN, new Coordinate(0,2));
        Tile flat2 = new Tile(Orientation.FLAT, new Coordinate(1,1)); 
        Tile up2 = new Tile(Orientation.UP, new Coordinate(1,1));
        Tile down2 = new Tile(Orientation.DOWN, new Coordinate(1,1));

        Assert.False(flat.IsAdjacentTo(flat.GetCoordinates()));
        Assert.False(flat.IsAdjacentTo(flat1.GetCoordinates()));
        Assert.False(flat.IsAdjacentTo(flat2.GetCoordinates()));
        Assert.False(flat.IsAdjacentTo(up.GetCoordinates()));
        Assert.True(flat.IsAdjacentTo(up1.GetCoordinates()));
        Assert.False(flat.IsAdjacentTo(up2.GetCoordinates()));
        Assert.False(flat.IsAdjacentTo(down.GetCoordinates()));
        Assert.False(flat.IsAdjacentTo(down1.GetCoordinates()));
        Assert.False(flat.IsAdjacentTo(down2.GetCoordinates()));

        Assert.False(up.IsAdjacentTo(flat.GetCoordinates()));
        Assert.False(up.IsAdjacentTo(flat1.GetCoordinates()));
        Assert.False(up.IsAdjacentTo(flat2.GetCoordinates()));
        Assert.False(up.IsAdjacentTo(up.GetCoordinates()));
        Assert.False(up.IsAdjacentTo(up1.GetCoordinates()));
        Assert.False(up.IsAdjacentTo(up2.GetCoordinates()));
        Assert.False(up.IsAdjacentTo(down.GetCoordinates()));
        Assert.False(up.IsAdjacentTo(down1.GetCoordinates()));
        Assert.False(up.IsAdjacentTo(down2.GetCoordinates()));

        Assert.False(down.IsAdjacentTo(flat.GetCoordinates()));
        Assert.True(down.IsAdjacentTo(flat1.GetCoordinates()));
        Assert.True(down.IsAdjacentTo(flat2.GetCoordinates()));
        Assert.False(down.IsAdjacentTo(up.GetCoordinates()));
        Assert.True(down.IsAdjacentTo(up1.GetCoordinates()));
        Assert.True(down.IsAdjacentTo(up2.GetCoordinates()));
        Assert.False(down.IsAdjacentTo(down.GetCoordinates()));
        Assert.False(down.IsAdjacentTo(down1.GetCoordinates()));
        Assert.False(down.IsAdjacentTo(down2.GetCoordinates()));
    }
}