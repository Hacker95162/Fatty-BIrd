using Raylib_cs;

namespace Fatty_bird;

public class Birdy{
	private float[] Position;
	private int[] Size;
    private float VelocityY;
	private float Gravity;
	private int Phase;
	
	private bool TextureLoaded;
	
    public Birdy(float x, float y, int width, int height)
    {
        Position = [x, y];
		Size = [width, height];
        VelocityY = 0;
		Gravity = 0.5f;
		
		TextureLoaded = GraphicControl.BirdLoaded();
		Phase = 0;
    }

    public void Update(){
		VelocityY += Gravity;
        Position[1] += VelocityY;
		
		if(VelocityY<-4) Phase = 1;
		else if(VelocityY<0) Phase = 2;
		else Phase = 3;
    }
    public void Draw(){
        if(TextureLoaded) GraphicControl.DrawBird(Phase, Position[0], Position[1], Size[0], Size[1]);
			else Raylib.DrawRectangleRec(new Rectangle(Position[0], Position[1], Size[0], Size[1]), Color.White);
    }
	public void HandleKeyboardInput(KeyboardKey Input){
		if(Input == KeyboardKey.Space){
			AudioControl.PlayHitSound();
			Jump();
		}
	}


	private void Jump(){
		VelocityY = -10;
	}
	
	
	public Rectangle GetHitBox(){
		return new Rectangle(Position[0], Position[1], Size[0], Size[1]);
	}
	
}