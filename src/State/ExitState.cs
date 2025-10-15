using Raylib_cs;
namespace Fatty_bird;

public class ExitState : GameStateTemplate
{
    private float countdownTimer;
    private const float countdownDuration = 4.0f;
    public ExitState()
    {
        countdownTimer = countdownDuration;
    }

    public void Update()
    {
		StateControl.requestContinue();
        countdownTimer -= Raylib.GetFrameTime();
        if (countdownTimer <= 0)
        {
			/*
			WARNING: GLFW: Error: 65537 Description: The GLFW library is not initialized
			Fatal error. System.AccessViolationException: Attempted to read or write protected memory. This is often an indication that other memory is corrupt.
			Repeat 2 times:
			--------------------------------
			   at Raylib_cs.Raylib.DrawText(SByte*, Int32, Int32, Int32, Raylib_cs.Color)
			--------------------------------
			   at Raylib_cs.Raylib.DrawText(System.String, Int32, Int32, Int32, Raylib_cs.Color)
			   at PongGame.ExitState.Draw()
			   at PongGame.Program.Main(System.String[])
			*/
						// Bad example - should be fix later
            StateControl.requestClose();
        }
    }

	public void Draw()
	{
		Raylib.BeginDrawing();
		Raylib.ClearBackground(Color.Black);

		// Font size for all texts
		int fontSize = 40;
		int lineSpacing = 50; // Space between lines for better readability with large font

		// Calculate total height of the text block
		int totalLines = 3;
		int totalHeight = totalLines * fontSize + (totalLines - 1) * lineSpacing;
		int startY = GeneralControl.Screen.Height / 2 - totalHeight / 2;

		// First text
		string text1 = "Ok then, I will be go home, hope to see you again";
		int text1Width = Raylib.MeasureText(text1, fontSize);
		Raylib.DrawText(text1, GeneralControl.Screen.Width / 2 - text1Width / 2, startY, fontSize, Color.White);

		// Second text
		string text2 = $"Exiting in {countdownTimer:F3} seconds...";
		int text2Width = Raylib.MeasureText(text2, fontSize);
		Raylib.DrawText(text2, GeneralControl.Screen.Width / 2 - text2Width / 2, startY + fontSize + lineSpacing, fontSize, Color.White);

		// Third text
		string text3 = "Press ESC to go back";
		int text3Width = Raylib.MeasureText(text3, fontSize);
		Raylib.DrawText(text3, GeneralControl.Screen.Width / 2 - text3Width / 2, startY + 2 * (fontSize + lineSpacing), fontSize, Color.White);

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