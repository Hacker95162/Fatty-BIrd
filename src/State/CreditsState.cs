using Raylib_cs;
namespace Fatty_bird;

public class CreditsState : GameStateTemplate
{
    public CreditsState(){}

    public void Update(){
		AudioControl.UpdateBackgroundMusic();
	}

	public void Draw()
	{
		Raylib.BeginDrawing();
		Raylib.ClearBackground(Color.Black);
		
		GraphicControl.DrawBackgroundFullScreenCredits();

		// Calculate total height of text block
		int titleFontSize = 50;
		int otherFontSize = 25;
		int lineSpacing = 30;
		int totalHeight = titleFontSize + 3 * otherFontSize + 3 * lineSpacing;
		int startY = GeneralControl.Screen.Height / 2 - totalHeight / 2;

		// Title with black outer glow
		string title = "CREDITS";
		int titleWidth = Raylib.MeasureText(title, titleFontSize);
		int xTitle = GeneralControl.Screen.Width / 2 - titleWidth / 2;
		int yTitle = startY;
		Raylib.DrawText(title, xTitle + 1, yTitle + 1, titleFontSize, Color.Black);
		Raylib.DrawText(title, xTitle - 1, yTitle - 1, titleFontSize, Color.Black);
		Raylib.DrawText(title, xTitle + 1, yTitle - 1, titleFontSize, Color.Black);
		Raylib.DrawText(title, xTitle - 1, yTitle + 1, titleFontSize, Color.Black);
		Raylib.DrawText(title, xTitle, yTitle, titleFontSize, Color.White);

		// Line1 with black outer glow
		string line1 = "Game Design: Hacker95162";
		int line1Width = Raylib.MeasureText(line1, otherFontSize);
		int xLine1 = GeneralControl.Screen.Width / 2 - line1Width / 2;
		int yLine1 = startY + titleFontSize + lineSpacing;
		Raylib.DrawText(line1, xLine1 + 1, yLine1 + 1, otherFontSize, Color.Black);
		Raylib.DrawText(line1, xLine1 - 1, yLine1 - 1, otherFontSize, Color.Black);
		Raylib.DrawText(line1, xLine1 + 1, yLine1 - 1, otherFontSize, Color.Black);
		Raylib.DrawText(line1, xLine1 - 1, yLine1 + 1, otherFontSize, Color.Black);
		Raylib.DrawText(line1, xLine1, yLine1, otherFontSize, Color.White);

		// Line2 with black outer glow
		string line2 = "Programming: Hacker95162";
		int line2Width = Raylib.MeasureText(line2, otherFontSize);
		int xLine2 = GeneralControl.Screen.Width / 2 - line2Width / 2;
		int yLine2 = startY + titleFontSize + 2 * lineSpacing + otherFontSize;
		Raylib.DrawText(line2, xLine2 + 1, yLine2 + 1, otherFontSize, Color.Black);
		Raylib.DrawText(line2, xLine2 - 1, yLine2 - 1, otherFontSize, Color.Black);
		Raylib.DrawText(line2, xLine2 + 1, yLine2 - 1, otherFontSize, Color.Black);
		Raylib.DrawText(line2, xLine2 - 1, yLine2 + 1, otherFontSize, Color.Black);
		Raylib.DrawText(line2, xLine2, yLine2, otherFontSize, Color.White);

		// Line3 with black outer glow
		string line3 = "Powered by Raylib-cs";
		int line3Width = Raylib.MeasureText(line3, otherFontSize);
		int xLine3 = GeneralControl.Screen.Width / 2 - line3Width / 2;
		int yLine3 = startY + titleFontSize + 3 * lineSpacing + 2 * otherFontSize;
		Raylib.DrawText(line3, xLine3 + 1, yLine3 + 1, otherFontSize, Color.Black);
		Raylib.DrawText(line3, xLine3 - 1, yLine3 - 1, otherFontSize, Color.Black);
		Raylib.DrawText(line3, xLine3 + 1, yLine3 - 1, otherFontSize, Color.Black);
		Raylib.DrawText(line3, xLine3 - 1, yLine3 + 1, otherFontSize, Color.Black);
		Raylib.DrawText(line3, xLine3, yLine3, otherFontSize, Color.White);

		// Line4 with black outer glow
		string line4 = "Press ESC to return to Main Menu";
		int line4Width = Raylib.MeasureText(line4, otherFontSize);
		int xLine4 = GeneralControl.Screen.Width / 2 - line4Width / 2;
		int yLine4 = startY + titleFontSize + 4 * lineSpacing + 3 * otherFontSize;
		Raylib.DrawText(line4, xLine4 + 1, yLine4 + 1, otherFontSize, Color.Black);
		Raylib.DrawText(line4, xLine4 - 1, yLine4 - 1, otherFontSize, Color.Black);
		Raylib.DrawText(line4, xLine4 + 1, yLine4 - 1, otherFontSize, Color.Black);
		Raylib.DrawText(line4, xLine4 - 1, yLine4 + 1, otherFontSize, Color.Black);
		Raylib.DrawText(line4, xLine4, yLine4, otherFontSize, Color.White);

		Raylib.EndDrawing();
	}
	
	public void ChangeState(GameStateTemplate NextState){
		StateControl.requestChange(NextState);
	}
	public void HandleKeyboardInput(KeyboardKey Input){
		if(Input == KeyboardKey.Escape){
			AudioControl.PlayMenuChooseSound();
			ChangeState(new MainMenuState());
		}
	}
}