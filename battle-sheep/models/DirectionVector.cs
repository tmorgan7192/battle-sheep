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
        if(direction == Direction.STRAIGHTDOWN && sign == 1){
            return "Straight Down";
        }
        if(direction == Direction.STRAIGHTDOWN && sign == -1){
            return "Straight Up";
        }
        if(direction == Direction.DOWNLEFT && sign == 1){
            return "Down Left";
        }
        if(direction == Direction.DOWNLEFT && sign == -1){
            return "Up Left";
        }
        if(direction == Direction.DOWNRIGHT && sign == 1){
            return "Down Right";
        }
        if(direction == Direction.DOWNRIGHT && sign == -1){
            return "Up Right";
        }
        else{
            throw new SystemException("DirectionVector missing enum value");
        }
    }
}