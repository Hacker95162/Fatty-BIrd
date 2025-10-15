using Raylib_cs;
using System.Collections;
using System.IO;
using System.Numerics;

namespace Fatty_bird;
/* READ ME

These code is write for the sake of optimze, so it tried to minimize the try-catch as much as possible;
The user may unable to make any of these error, but pirate and other developer may;
Things must be have:

*/

public static class GraphicControl{	
	private static string[] DefaultPaths = [
        Path.Combine("imgs", "demo-img-4.png"),
		Path.Combine("imgs\\Player", "Bird1-6.png"),
		Path.Combine("imgs\\Tiles", "PipeStyle4.png"),
		Path.Combine("imgs\\Tiles", "TileStyle4.png"),
		Path.Combine("imgs\\BackGround", "Background7.png")
        // Add more background image paths as needed
    ];
	private static ArrayList PathChanged = new ArrayList();
	
	private static string assetsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "imgs");
	private static bool[] FileSafeToLoad = new bool[DefaultPaths.Length];
	private static bool FileSafeToLoadAll = false;
	public static bool FileChangeSensor = false;
	
	// ============================= Settings // default value - do not depend on it
	
	private static Texture2D BackgroundTextures;
	private static Texture2D BirdTextures;
	private static Texture2D PipeTextures;
	private static Texture2D TitleTextures;
	private static Texture2D GameplayBackground;
	
	
	
	// ============================= Default Functions
	
	public static void Load(){
		CheckFileValid();
		BackgroundTextures = LoadImageAssets(DefaultPaths[0]);
		BirdTextures = LoadImageAssets(DefaultPaths[1]);
		PipeTextures = LoadImageAssets(DefaultPaths[2]);
		TitleTextures = LoadImageAssets(DefaultPaths[3]);
		GameplayBackground = LoadImageAssets(DefaultPaths[4]);
		
		if(FileSafeToLoadAll){
		}else{
			throw new FileNotFoundException($"Missing files: ");//{string.Join(", ", missingFiles.ToArray())}");
		}
	}
	
    public static void Save(){}// image not required to save, atleast this game};
	
	
	
    private static void CheckFileValid(){
		bool AssumeFileSafeToLoad = true;
		
		for (int i = 0; i < DefaultPaths.Length; i++){
			if(!File.Exists(DefaultPaths[i])){
				AssumeFileSafeToLoad = false;
				PathChanged.Add(DefaultPaths[i]); 
				FileSafeToLoad[i] = false;
			}else{
				FileSafeToLoad[i] = true;
			}
		}
		FileSafeToLoadAll = AssumeFileSafeToLoad;
	}

	
	
	// ============================= Tools Functions
	
	private static Texture2D LoadImageAssets(String path){
        // Image backgroundImage = Raylib.GenImageColor(Screen.Width, Screen.Height, Color.Black); // Placeholder
        // BackgroundTexture = Raylib.LoadTextureFromImage(backgroundImage);
        // Raylib.UnloadImage(backgroundImage);
        // Actual loading: BackgroundTexture = Raylib.LoadTexture(Path.Combine(assetsPath, "background.png"));
		string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
        Texture2D texture = Raylib.LoadTexture(fullPath);
		return texture;
		//string assetsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images");
		// BackgroundTexture = Raylib.LoadTexture(Path.Combine(assetsPath, "background.png"));
    }
	
	

	
	
	// ============================= Logic Functions
	
	
	public static void DrawImage(Texture2D texture, int x, int y){
        Raylib.DrawTexture(texture, x, y, Color.White);
    }
	
	public static Texture2D GetBackgroundTexture(){return BackgroundTextures;}
	
	
	// ============================= Advanced Functions
	// ============================= This is one time use function or so
	
	public static bool BackgroundLoaded(){
		return FileSafeToLoad[0];
	}
	public static void DrawBackgroundFullScreenMainMenu(){
		float scale = 6;
		
		Raylib.DrawTexturePro(BackgroundTextures, 
                      new Rectangle(0, 0, BackgroundTextures.Width, BackgroundTextures.Height),
                      new Rectangle(-BackgroundTextures.Width/3, -BackgroundTextures.Height/3, BackgroundTextures.Width*scale, BackgroundTextures.Height*scale),
                      new Vector2(0, 0), 
                      0, 
                      Color.White);
	}
	public static void DrawBackgroundFullScreenOptions(){
		float scale = 6;
		
		Raylib.DrawTexturePro(BackgroundTextures, 
                      new Rectangle(0, 0, BackgroundTextures.Width, BackgroundTextures.Height),
                      new Rectangle(-BackgroundTextures.Width*2/4, -BackgroundTextures.Height*2/4, BackgroundTextures.Width*scale, BackgroundTextures.Height*scale),
                      new Vector2(0, 0), 
                      0, 
                      Color.White);
	}
	public static void DrawBackgroundFullScreenScoreboard(){
		float scale = 6;
		
		Raylib.DrawTexturePro(BackgroundTextures, 
                      new Rectangle(0, 0, BackgroundTextures.Width, BackgroundTextures.Height),
                      new Rectangle(-BackgroundTextures.Width/5, -BackgroundTextures.Height/5, BackgroundTextures.Width*scale, BackgroundTextures.Height*scale),
                      new Vector2(0, 0), 
                      0, 
                      Color.White);
	}
	public static void DrawBackgroundFullScreenCredits(){
		float scale = 6;
		
		Raylib.DrawTexturePro(BackgroundTextures, 
                      new Rectangle(0, 0, BackgroundTextures.Width, BackgroundTextures.Height),
                      new Rectangle(0, 0, BackgroundTextures.Width*scale, BackgroundTextures.Height*scale),
                      new Vector2(0, 0), 
                      0, 
                      Color.White);
	}
	
	
	public static bool BirdLoaded(){
		return FileSafeToLoad[1];
	}
	public static void DrawBird(int phase, float X, float Y, int Width, int Height){
		Raylib.DrawTexturePro(BirdTextures, 
                      new Rectangle(16*phase, 0, 16, 16),
                      new Rectangle(X, Y, Width, Height),
                      new Vector2(0, 0), 
                      0, 
                      Color.White);
	}
	
	
	public static bool PipeLoaded(){
		return FileSafeToLoad[2];
	}
	public static void DrawPipe(int phase, float X, float Y, int Width, int Height){
		if(phase <= 3){
			Raylib.DrawTexturePro(PipeTextures, 
						  new Rectangle(32*phase, 0, 32, 48),
						  new Rectangle(X, Y, Width, Height),
						  new Vector2(0, 0), 
						  0, 
						  Color.White);
		}else{
			Raylib.DrawTexturePro(PipeTextures, 
						  new Rectangle(32*(phase-4), 48, 32, 48),
						  new Rectangle(X, Y, Width, Height),
						  new Vector2(0, 0), 
						  0, 
						  Color.White);
		}
	}
	
	
	public static bool TitleLoaded(){
		return FileSafeToLoad[3];
	}
	public static void DrawTitle(bool Background, float X, float Y, int Width, int Height, bool Flip=false){
		int adjust = 0;
		if(Flip) adjust = 32;
		if(Background){
			Raylib.DrawTexturePro(TitleTextures, 
						  new Rectangle(32, 32, 16, 16),
						  new Rectangle(X, Y, Width, Height),
						  new Vector2(0, 0), 
						  0, 
						  Color.White);
		}else{
			Raylib.DrawTexturePro(TitleTextures, 
						  new Rectangle(32, 16+adjust, 16, 16),
						  new Rectangle(X, Y, Width, Height),
						  new Vector2(0, 0), 
						  0, 
						  Color.White);
		}
	}
	
	public static bool GameplayBackgroundLoaded(){
		return FileSafeToLoad[4];
	}
	public static void DrawGameplayBackground(){
		Raylib.DrawTexturePro(GameplayBackground, 
						  new Rectangle(0, 0, GameplayBackground.Width, GameplayBackground.Height),
						  new Rectangle(-GameplayBackground.Width, -GameplayBackground.Height, GameplayBackground.Width*6, GameplayBackground.Height*6),
						  new Vector2(0, 0), 
						  0, 
						  Color.White);
	}
}