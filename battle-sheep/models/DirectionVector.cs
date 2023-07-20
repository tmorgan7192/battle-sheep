namespace BattleSheep;

public class DirectionVector {
    private Direction direction;
    private int sign;

    public DirectionVector(Direction direction, int distance) {
        this.direction = direction;
        this.sign = distance;
    }

    public Direction GetDirection() {
        return direction;
    }

    public void SetDirection(Direction direction) {
        this.direction = direction;
    }

    public int GetSign() {
        return sign;
    }

    public void SetSign(int sign) {
        this.sign = sign;
    }

    public override string ToString()
    {
        return $"{direction} {sign}";
    }
}