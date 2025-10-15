using Raylib_cs;

namespace Fatty_bird;

public class Point{
	private float[] Position;
	private int[] Size;
    private float VelocityX;
	
    public Point(float x, float InVelocityX = -5)
    {
        Position = [x, 0];
		Size = [5, GeneralControl.Screen.Height];
        VelocityX = InVelocityX;
    }

    public void Update()
    {
        Position[0] += VelocityX;
    }
    public void Draw(){Raylib.DrawRectangleRec(new Rectangle(Position[0], Position[1], Size[0], Size[1]), Color.White);}
	
	public Rectangle GetHitBox(){
		return new Rectangle(Position[0], Position[1], Size[0], Size[1]);
	}
	
	
}