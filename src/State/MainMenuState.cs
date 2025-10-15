using Raylib_cs;

namespace Fatty_bird;

public class MainMenuState : GameStateTemplate
{
	private int PlayerOption;
	private String[] Options = [
		"Start!",
		"Options",
		"Scoreboard",
		"Credits",
		"Exit :<"
	];
	private String[] OptionsExtend = [
		"Take off!",
		"Check Wings",
		"Other Peers",
		"Parents",
		"Go Home :<"
	];
	// design, bad for memory, good for performance and clean
	/*private GameStateTemplate[] OptionState = [
		new GamePlayState(),
		new OptionsState(),
		new ScoreboardState(),
		new CreditsState(),
		new ExitState()
	];*/
	
	private GameStateTemplate[] OptionState = [
		new GameplayState(),
		new OptionsState(),
		new ScoreboardState(),
		new CreditsState(),
		new ExitState()
	]; // recursion error if it call itself
	
	
	
    public MainMenuState(){
		PlayerOption = 0;
		AudioControl.PlayBackgroundMusic();
    }

    public void Update(){
		AudioControl.UpdateBackgroundMusic();
	}

	public void Draw()
	{
		Raylib.BeginDrawing();
		Raylib.ClearBackground(Color.Black);
		
		GraphicControl.DrawBackgroundFullScreenMainMenu();

		// Text1 with black outer glow
		string text1 = "Fatty Bird!";
		int text1Width = Raylib.MeasureText(text1, 40);
		int x = GeneralControl.Screen.Width / 2 - text1Width / 2;
		int y = 100;
		Raylib.DrawText(text1, x + 1, y + 1, 40, Color.Black); // Outer glow (bottom-right)
		Raylib.DrawText(text1, x - 1, y - 1, 40, Color.Black); // Outer glow (top-left)
		Raylib.DrawText(text1, x + 1, y - 1, 40, Color.Black); // Outer glow (top-right)
		Raylib.DrawText(text1, x - 1, y + 1, 40, Color.Black); // Outer glow (bottom-left)
		Raylib.DrawText(text1, x, y, 40, Color.White); // Main text

		// Calculate total height of text block
		int fontSizeSmall = 20;
		int fontSizeLarge = 40;
		int lineSpacing = 30;
		int totalHeight = (2 * fontSizeSmall) + fontSizeLarge + (2 * fontSizeSmall) + 3 * lineSpacing;
		int startY = GeneralControl.Screen.Height / 2 - totalHeight / 2;

		// Text2 with black outer glow
		string text2 = "W/S to up and down, Space to choose";
		int text2Width = Raylib.MeasureText(text2, fontSizeSmall);
		x = GeneralControl.Screen.Width / 2 - text2Width / 2;
		y = startY;
		Raylib.DrawText(text2, x + 1, y + 1, fontSizeSmall, Color.Black);
		Raylib.DrawText(text2, x - 1, y - 1, fontSizeSmall, Color.Black);
		Raylib.DrawText(text2, x + 1, y - 1, fontSizeSmall, Color.Black);
		Raylib.DrawText(text2, x - 1, y + 1, fontSizeSmall, Color.Black);
		Raylib.DrawText(text2, x, y, fontSizeSmall, Color.White);

		// Text3 with black outer glow
		string text3 = Options[GetPlayerOption(-1)];
		int text3Width = Raylib.MeasureText(text3, fontSizeSmall);
		x = GeneralControl.Screen.Width / 2 - text3Width / 2;
		y = startY + fontSizeSmall + lineSpacing;
		Raylib.DrawText(text3, x + 1, y + 1, fontSizeSmall, Color.Black);
		Raylib.DrawText(text3, x - 1, y - 1, fontSizeSmall, Color.Black);
		Raylib.DrawText(text3, x + 1, y - 1, fontSizeSmall, Color.Black);
		Raylib.DrawText(text3, x - 1, y + 1, fontSizeSmall, Color.Black);
		Raylib.DrawText(text3, x, y, fontSizeSmall, new Color(255, 255, 255, 100));

		// Text5 with black outer glow
		string text5 = OptionsExtend[PlayerOption];
		int text5Width = Raylib.MeasureText(text5, fontSizeLarge);
		x = GeneralControl.Screen.Width / 2 - text5Width / 2;
		y = startY + fontSizeSmall + lineSpacing + fontSizeSmall + 30;
		Raylib.DrawText(text5, x + 1, y + 1, fontSizeLarge, Color.Black);
		Raylib.DrawText(text5, x - 1, y - 1, fontSizeLarge, Color.Black);
		Raylib.DrawText(text5, x + 1, y - 1, fontSizeLarge, Color.Black);
		Raylib.DrawText(text5, x - 1, y + 1, fontSizeLarge, Color.Black);
		Raylib.DrawText(text5, x, y, fontSizeLarge, Color.White);

		// Text4 with black outer glow
		string text4 = Options[PlayerOption];
		int text4Width = Raylib.MeasureText(text4, fontSizeSmall);
		x = GeneralControl.Screen.Width / 2 - text4Width / 2;
		y = startY + fontSizeSmall + lineSpacing + fontSizeSmall + 30 + fontSizeLarge + 30;
		Raylib.DrawText(text4, x + 1, y + 1, fontSizeSmall, Color.Black);
		Raylib.DrawText(text4, x - 1, y - 1, fontSizeSmall, Color.Black);
		Raylib.DrawText(text4, x + 1, y - 1, fontSizeSmall, Color.Black);
		Raylib.DrawText(text4, x - 1, y + 1, fontSizeSmall, Color.Black);
		Raylib.DrawText(text4, x, y, fontSizeSmall, Color.White);

		// Text6 with black outer glow
		string text6 = Options[GetPlayerOption(+1)];
		int text6Width = Raylib.MeasureText(text6, fontSizeSmall);
		x = GeneralControl.Screen.Width / 2 - text6Width / 2;
		y = startY + fontSizeSmall + lineSpacing + fontSizeSmall + 30 + fontSizeLarge + 30 + fontSizeSmall + lineSpacing;
		Raylib.DrawText(text6, x + 1, y + 1, fontSizeSmall, Color.Black);
		Raylib.DrawText(text6, x - 1, y - 1, fontSizeSmall, Color.Black);
		Raylib.DrawText(text6, x + 1, y - 1, fontSizeSmall, Color.Black);
		Raylib.DrawText(text6, x - 1, y + 1, fontSizeSmall, Color.Black);
		Raylib.DrawText(text6, x, y, fontSizeSmall, new Color(255, 255, 255, 100));

		Raylib.EndDrawing();
	}
	
	public void ChangeState(GameStateTemplate NextState){
		StateControl.requestChange(NextState);
	}
	
	public void HandleKeyboardInput(KeyboardKey Input){
		switch (Input){
			case KeyboardKey.W:
				AudioControl.PlayMenuChooseSound();
				SetPlayerOption(-1);
				break;
			case KeyboardKey.S:
				AudioControl.PlayMenuChooseSound();
				SetPlayerOption(1);
				break;
			case KeyboardKey.Space:
				AudioControl.PlayMenuChooseSound();
				ChangeState(OptionState[PlayerOption]);
				return;
		}
	}
	
	
	private int SetPlayerOption(int n){
		n += PlayerOption;
		if(n > Options.Length-1) PlayerOption = 0;
		else if(n < 0) PlayerOption = Options.Length-1;
		else PlayerOption = n;
		return PlayerOption;
	}
	private int GetPlayerOption(int n){
		n += PlayerOption;
		if(n > Options.Length-1) return 0;
		else if(n < 0) return Options.Length-1;
		return n;
	}
}