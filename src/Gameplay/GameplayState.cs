using Raylib_cs;
using System.Collections;

namespace Fatty_bird;

public class GameplayState : GameStateTemplate
{
    private int Score;
	private float GameSpeed;
	
	// use for optimize the game, only check if the player touch the title under their feet
	// Since the game is moving environment, not the bird
	private float TitleWidth;
	private int TitleIndexUnderPlayer;
	
	// In Pipe.cs file, bad design, but I do lazy to optimize this part
	//private const int PipeWidth = 64;
	//private const int PipeHeight = 150;
	private PipeSpawner TopSpawner, BotSpawner;

	
	private Birdy Player;
	private ArrayList TopPipes, BotPipes;
	private ArrayList Ground, Sky, UnderGround, HigherSky;
	private ArrayList PointList;
	
	private float pipeSpawnTime; // Tracks elapsed time
	private const float PipeSpawnInterval = 1.5f; // Spawn every 2.5 seconds
	
	private bool InitPause;
	
    public GameplayState()
    {
		AudioControl.PlayGameplayBackgroundMusic();
		
		GameSpeed = -10;
		TitleWidth = 32;
		
		Score = 0;
		Player = new Birdy(GeneralControl.Screen.Width/8,GeneralControl.Screen.Height/2 ,64, 64);
		TopPipes = new ArrayList();
		BotPipes = new ArrayList();
		PointList = new ArrayList();
		Ground = generateTitles(0, (int)Math.Floor((double)(GeneralControl.Screen.Height * 6 / 7)), GeneralControl.Screen.Width);
		Sky = generateTitles(0, (int)Math.Floor((double)(GeneralControl.Screen.Height / 7)), GeneralControl.Screen.Width,false,true);

		UnderGround = generateTitles(0, (int)Math.Floor((double)(GeneralControl.Screen.Height * 6 / 7 + TitleWidth)), GeneralControl.Screen.Width, true);
		HigherSky = generateTitles(0, (int)Math.Floor((double)(GeneralControl.Screen.Height / 7 - TitleWidth)), GeneralControl.Screen.Width, true);
		
		TitleIndexUnderPlayer = (int)Math.Floor(Player.GetHitBox().X / TitleWidth);
		
		pipeSpawnTime = 0;
		
		TopSpawner = new PipeSpawner(GeneralControl.Screen.Width, 0-75, GameSpeed);
		BotSpawner = new PipeSpawner(GeneralControl.Screen.Width, GeneralControl.Screen.Height - 150, GameSpeed);
		
		SpawnAndCleanPipe(true); // Init the pipe
		
		InitPause = true;
		
		
    }

    public void Update(){
		if(InitPause){
			StateControl.requestPause();
			InitPause = false;
		}
		AudioControl.UpdateGameplayBackgroundMusic();
		SpawnAndCleanPipe();
		
		Player.Update();
		
		SpawnAndCleanTitle(Ground);
		SpawnAndCleanTitle(Sky);
		SpawnAndCleanTitle(UnderGround);
		SpawnAndCleanTitle(HigherSky);
		
		
		foreach(Pipe p in TopPipes) p.Update();
		foreach(Pipe p in BotPipes) p.Update();
		foreach(Title t in Ground) t.Update();
		foreach(Title t in Sky) t.Update();
		foreach(Title t in UnderGround) t.Update();
		foreach(Title t in HigherSky) t.Update();
		
		foreach(Point p in PointList) p.Update();
		
		if(CheckEndGame()) EndGame();
    }

    public void Draw(){
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.Black);
		if(GraphicControl.GameplayBackgroundLoaded()) GraphicControl.DrawGameplayBackground();
		
		if(StateControl.InPause()){
			string Message = "Press Space to continue the game";
			Raylib.DrawText(Message, GeneralControl.Screen.Width / 2 - Raylib.MeasureText(Message, 20) / 2, GeneralControl.Screen.Height / 2, 20, Color.White);
		}
		
        Player.Draw();
		
		foreach(Title t in Ground) t.Draw();
		foreach(Title t in Sky) t.Draw();
		foreach(Title t in UnderGround) t.Draw();
		foreach(Title t in HigherSky) t.Draw();
		
		// should be last, or else the ground will hide the pipe
		foreach(Pipe p in TopPipes) p.Draw();
		foreach(Pipe p in BotPipes) p.Draw();
		
		
		
        Raylib.EndDrawing();
    }
	
	public void ChangeState(GameStateTemplate NextState){
		StateControl.requestChange(NextState);
	}
	public void HandleKeyboardInput(KeyboardKey Input){
		if(Input == KeyboardKey.Escape){
			AudioControl.PlayMenuChooseSound();
			StateControl.requestPause();
		}
		if(StateControl.InPause() && Input == KeyboardKey.Space){
			StateControl.requestContinue();
			return;
		}
		Player.HandleKeyboardInput(Input);
	}
	
	
	
	
	// Generates the initial set of Titles for the ground, calculating the exact number needed
	// to cover 1.5x the screen width (for seamless scrolling).
	// Takes the initial Title as a template (e.g., starting position and properties).
	private ArrayList generateTitles(int FromX, int Y, int ToX, bool Background=false, bool Flip = false){
		ArrayList Titles = new ArrayList();
		Title currentTitle = new Title(FromX, Y, GameSpeed, Background, Flip); // Start with the provided initial Title
		Titles.Add(currentTitle);

		while(currentTitle.GetHitBox().X < ToX){
			currentTitle = currentTitle.MakeNextTitle();
			Titles.Add(currentTitle);
		} 
		return Titles;
	}
	private void SpawnAndCleanTitle(ArrayList Titles){
		Rectangle firstTitle = ((Title)Titles[0]).GetHitBox();
		if (firstTitle.X + firstTitle.Width < 0)
		{
			// Remove the first (off-screen) Title
			Titles.RemoveAt(0);

			// Get the last Title to create the new one adjacent to it
			Title lastTitle = (Title)Titles[Titles.Count - 1];
			Title newTitle = lastTitle.MakeNextTitle();

			// Add the new Title to the end
			Titles.Add(newTitle);
		}
	}
	
	
	
	
	private void SpawnAndCleanPipe(bool Init = false){
		pipeSpawnTime += Raylib.GetFrameTime();

		// Check if time to spawn
		if (pipeSpawnTime >= PipeSpawnInterval || Init){
			// Reset timer
			pipeSpawnTime = 0;
			TopPipes.Add(TopSpawner.RandomSpawn());
			BotPipes.Add(BotSpawner.RandomSpawn());
			PointList.Add(new Point(GeneralControl.Screen.Width, GameSpeed));
			
			// clean old pipe
			if(TopPipes.Count > 0){
				Pipe firstTop = (Pipe)TopPipes[0]; // bad design, call same function 2 times
				if (firstTop.GetHitBox().X+firstTop.GetHitBox().Width < 0)
				{
					TopPipes.RemoveAt(0);
					BotPipes.RemoveAt(0);
				}else{
					return;
				}
			}
		}
	}
	
	
	
	private bool CheckCollision(Rectangle rect1, Rectangle rect2){
		return Raylib.CheckCollisionRecs(rect1, rect2);
	}
	private bool CheckEndGame(){
		foreach(Pipe p in TopPipes){
			if(CheckCollision(Player.GetHitBox(), p.GetHitBox()))
				return true;
			if (p.GetHitBox().Y > Player.GetHitBox().Y)
				break;
		}
		foreach(Pipe p in BotPipes){
			if(CheckCollision(Player.GetHitBox(), p.GetHitBox()))
				return true;
			if (p.GetHitBox().Y > Player.GetHitBox().Y)
				break;
		}
		
		
		if(PointList.Count > 0){
			Point firstPoint = (Point)PointList[0];
			if(CheckCollision(Player.GetHitBox(), firstPoint.GetHitBox())){
				Score++;
				PointList.RemoveAt(0);
			}
		}
		    // Check ground Titles (optimized: only 1-3 Titles near player)
		Title[] TitlesNeedToCheck = [
			(Title)Ground[TitleIndexUnderPlayer-1],
			(Title)Ground[TitleIndexUnderPlayer],
			(Title)Ground[TitleIndexUnderPlayer+1],
			(Title)Sky[TitleIndexUnderPlayer-1],
			(Title)Sky[TitleIndexUnderPlayer],
			(Title)Sky[TitleIndexUnderPlayer+1]
		];
		foreach(Title t in TitlesNeedToCheck){
			if(CheckCollision(Player.GetHitBox(), t.GetHitBox()))
				return true;
		}
		return false;// No collision
	}
	
	
	
	private void EndGame(){
		ChangeState(new EndGameState(Score));
	}
}