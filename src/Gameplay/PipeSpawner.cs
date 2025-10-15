using Raylib_cs;

namespace Fatty_bird;

public class PipeSpawner{
	private float[] Position;
	private float GameSpeed;

	
    public PipeSpawner(float x, float y, float GameSpeed)
    {
        Position = [x, y];
		this.GameSpeed = GameSpeed;
    }

    public void Update(){}
    public void Draw(){}
	
	public Pipe Spawn(){
		return new Pipe(Position[0], Position[1], GameSpeed);
	}
	public Pipe RandomSpawn(){
		int XAdjust = Raylib.GetRandomValue(32, -32);
		int YAdjust = Raylib.GetRandomValue(50, -50);
		return new Pipe(Position[0]+XAdjust, Position[1]+YAdjust, GameSpeed);
	}

}
