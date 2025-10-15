using Raylib_cs;

namespace Fatty_bird;

public class Pipe{
	private float[] Position;
	private int[] Size;
    private float VelocityX;
	private int Type;
	
	private bool TextureLoaded;
	
    public Pipe(float x, float y, float InVelocityX = -5)
    {
        Position = [x, y];
		Size = [192, 288];
        VelocityX = InVelocityX;
		
		Type = Raylib.GetRandomValue(0, 7);
		TextureLoaded = GraphicControl.PipeLoaded();
    }

    public void Update()
    {
        Position[0] += VelocityX;
	}
	public void Draw(){
        if(TextureLoaded) GraphicControl.DrawPipe(Type, Position[0], Position[1], Size[0], Size[1]);
			else Raylib.DrawRectangleRec(new Rectangle(Position[0], Position[1], Size[0], Size[1]), Color.White);
    }
	public Rectangle GetHitBox(){
		return new Rectangle(Position[0], Position[1], Size[0], Size[1]);
	}
}