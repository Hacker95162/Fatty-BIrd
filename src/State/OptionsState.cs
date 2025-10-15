using Raylib_cs;
namespace Fatty_bird;

public class OptionsState : GameStateTemplate
{
    private float musicVolume;
    private float soundVolume;

    public OptionsState()
    {
        musicVolume = AudioControl.GetMusicVolume();
        soundVolume = AudioControl.GetSoundVolume();
    }

    public void Update()
    {
		AudioControl.UpdateBackgroundMusic();
		musicVolume = AudioControl.GetMusicVolume();
        soundVolume = AudioControl.GetSoundVolume();		
	}

	public void Draw()
	{
		Raylib.BeginDrawing();
		Raylib.ClearBackground(Color.Black);
		
		GraphicControl.DrawBackgroundFullScreenOptions();

		// "OPTIONS" with black outer glow
		string textOptions = "OPTIONS";
		int textOptionsWidth = Raylib.MeasureText(textOptions, 40);
		int xOptions = GeneralControl.Screen.Width / 2 - textOptionsWidth / 2;
		int yOptions = 100;
		Raylib.DrawText(textOptions, xOptions + 1, yOptions + 1, 40, Color.Black);
		Raylib.DrawText(textOptions, xOptions - 1, yOptions - 1, 40, Color.Black);
		Raylib.DrawText(textOptions, xOptions + 1, yOptions - 1, 40, Color.Black);
		Raylib.DrawText(textOptions, xOptions - 1, yOptions + 1, 40, Color.Black);
		Raylib.DrawText(textOptions, xOptions, yOptions, 40, Color.White);

		// "Music Volume" with black outer glow
		string textMusic = $"Music Volume: {musicVolume:F1} (Up/Down to adjust)";
		int textMusicWidth = Raylib.MeasureText(textMusic, 20);
		int xMusic = GeneralControl.Screen.Width / 2 - textMusicWidth / 2;
		int yMusic = 250;
		Raylib.DrawText(textMusic, xMusic + 1, yMusic + 1, 20, Color.Black);
		Raylib.DrawText(textMusic, xMusic - 1, yMusic - 1, 20, Color.Black);
		Raylib.DrawText(textMusic, xMusic + 1, yMusic - 1, 20, Color.Black);
		Raylib.DrawText(textMusic, xMusic - 1, yMusic + 1, 20, Color.Black);
		Raylib.DrawText(textMusic, xMusic, yMusic, 20, Color.White);

		// "Sound Volume" with black outer glow
		string textSound = $"Sound Volume: {soundVolume:F1} (Left/Right to adjust)";
		int textSoundWidth = Raylib.MeasureText(textSound, 20);
		int xSound = GeneralControl.Screen.Width / 2 - textSoundWidth / 2;
		int ySound = 280;
		Raylib.DrawText(textSound, xSound + 1, ySound + 1, 20, Color.Black);
		Raylib.DrawText(textSound, xSound - 1, ySound - 1, 20, Color.Black);
		Raylib.DrawText(textSound, xSound + 1, ySound - 1, 20, Color.Black);
		Raylib.DrawText(textSound, xSound - 1, ySound + 1, 20, Color.Black);
		Raylib.DrawText(textSound, xSound, ySound, 20, Color.White);

		// "Press ENTER" with black outer glow
		string textEnter = "Press ENTER to save and return";
		int textEnterWidth = Raylib.MeasureText(textEnter, 20);
		int xEnter = GeneralControl.Screen.Width / 2 - textEnterWidth / 2;
		int yEnter = 350;
		Raylib.DrawText(textEnter, xEnter + 1, yEnter + 1, 20, Color.Black);
		Raylib.DrawText(textEnter, xEnter - 1, yEnter - 1, 20, Color.Black);
		Raylib.DrawText(textEnter, xEnter + 1, yEnter - 1, 20, Color.Black);
		Raylib.DrawText(textEnter, xEnter - 1, yEnter + 1, 20, Color.Black);
		Raylib.DrawText(textEnter, xEnter, yEnter, 20, Color.White);

		// "Press ESC" with black outer glow
		string textEsc = "Press ESC to cancel";
		int textEscWidth = Raylib.MeasureText(textEsc, 20);
		int xEsc = GeneralControl.Screen.Width / 2 - textEscWidth / 2;
		int yEsc = 380;
		Raylib.DrawText(textEsc, xEsc + 1, yEsc + 1, 20, Color.Black);
		Raylib.DrawText(textEsc, xEsc - 1, yEsc - 1, 20, Color.Black);
		Raylib.DrawText(textEsc, xEsc + 1, yEsc - 1, 20, Color.Black);
		Raylib.DrawText(textEsc, xEsc - 1, yEsc + 1, 20, Color.Black);
		Raylib.DrawText(textEsc, xEsc, yEsc, 20, Color.White);

		Raylib.EndDrawing();
	}
	
	
	public void ChangeState(GameStateTemplate NextState){
		StateControl.requestChange(NextState);
	}
	public void HandleKeyboardInput(KeyboardKey Input){
		switch (Input){
			case KeyboardKey.Up:
				AudioControl.SetMusicVolume(musicVolume + 0.1f);
				AudioControl.PlayMenuChooseSound();
				break;
			case KeyboardKey.Down:
				AudioControl.SetMusicVolume(musicVolume - 0.1f);
				AudioControl.PlayMenuChooseSound();
				break;
			case KeyboardKey.Right:
				AudioControl.SetSoundVolume(soundVolume + 0.1f);
				AudioControl.PlayMenuChooseSound();
				break;
			case KeyboardKey.Left:
				AudioControl.SetSoundVolume(soundVolume - 0.1f);
				AudioControl.PlayMenuChooseSound();
				break;
			case KeyboardKey.Enter:
				AudioControl.PlayMenuChooseSound();
				ChangeState(new MainMenuState());
				break;
			case KeyboardKey.Escape:
				AudioControl.PlayMenuChooseSound();
				ChangeState(new MainMenuState());
				break;
			default:
				break;
		}
	}
}