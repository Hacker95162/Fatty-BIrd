using Raylib_cs;

namespace Fatty_bird;

public class Title{
	private float[] Position;
	private int[] Size;
    private float VelocityX;
	private bool Background;
	private bool Flip;
	
	private bool TextureLoaded;
	
    public Title(float x, float y, float InVelocityX=-5, bool Background=false,bool Flip=false)
    {
        Position = [x, y];
		Size = [32, 32];
        VelocityX = InVelocityX;
		
		this.Background = Background;
		this.Flip = Flip;
		TextureLoaded = GraphicControl.TitleLoaded();
    }

    public void Update(){
        Position[0] += VelocityX;
    }
	public void Draw(){
        if(TextureLoaded) GraphicControl.DrawTitle(Background, Position[0], Position[1], Size[0], Size[1], Flip);
			else Raylib.DrawRectangleRec(new Rectangle(Position[0], Position[1], Size[0], Size[1]), Color.White);
    }
	public Title MakeNextTitle(){
		return new Title(Position[0]+Size[0], Position[1], VelocityX, Background, Flip);
	}
	public Rectangle GetHitBox(){
		return new Rectangle(Position[0], Position[1], Size[0], Size[1]);
	}
}
