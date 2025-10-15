using Raylib_cs;

namespace Fatty_bird;

public class IntroState : GameStateTemplate
{
	private float timer;
	private float FadeInAt;
	private float HoldAt;
	private float FadeOutAt;
	private Color TextColor;
	
    public IntroState()
    {
		//AudioControl.PlayBackgroundMusic();
		timer = 0f;
		TextColor = new Color(255, 255, 255);
		float FadeIn = 2f;
		float Hold = 2f;
		float FadeOut = 1.2f;
		
		FadeInAt = FadeIn;
		HoldAt = FadeInAt + Hold;
		FadeOutAt = HoldAt + FadeOut;
	}

	public void Update()
	{
		timer += Raylib.GetFrameTime(); // Increment timer by frame time (seconds)

		// Update alpha based on timer
		if (timer <= FadeInAt)
		{
			// Fade-in: Increase alpha from 0 to 255 over 0.5s
			float alpha = timer / FadeInAt; // 0.0 to 1.0
			TextColor = new Color(255, 255, 255, (int)(alpha * 255));
			
		}
		else if (timer <= HoldAt)
		{
			// Hold: Keep alpha at 255 for 1s
			TextColor = new Color(255, 255, 255, 255);
		}
		else if (timer <= FadeOutAt)
		{
			// Fade-out: Decrease alpha from 255 to 0 over 0.3s
			float alpha = 1.0f - (timer - HoldAt) / (FadeOutAt - HoldAt); // 1.0 to 0.0
			TextColor = new Color(255, 255, 255, (int)(Math.Clamp(alpha * 255, 0, 255)));
		}else{
			ChangeState(new MainMenuState());
		}
	}

	public void Draw()
	{
		Raylib.BeginDrawing();
		Raylib.ClearBackground(Color.Black);

		// Center text using Raylib's MeasureText
		string text = "A game, made by Hacker95162"; // "A game, made by Hacker95162"
		int fontSize = 40;
		int textWidth = Raylib.MeasureText(text, fontSize);
		int x = GeneralControl.Screen.Width / 2 - textWidth / 2; // Center horizontally
		int y = 100; // Fixed Y position

		Raylib.DrawText(text, x, y, fontSize, TextColor);
		Raylib.EndDrawing();
	}
	
	public void ChangeState(GameStateTemplate NextState){
		StateControl.requestChange(NextState);
	}
	public void HandleKeyboardInput(KeyboardKey Input){}
}