using Raylib_cs;

namespace Fatty_bird;

public class EndGameState : GameStateTemplate
{
    private int Score;

    public EndGameState(int Score)
    {
        this.Score = Score;
		SaveHighScore("Player", Score);
    }

    public void Update()
    {
    }

	public void Draw()
	{
		Raylib.BeginDrawing();
		Raylib.ClearBackground(Color.Black);

		// Font sizes and spacing
		int titleFontSize = 40;
		int otherFontSize = 20;
		int lineSpacing = 30;

		// Calculate total height of text block
		int totalHeight = titleFontSize + 2 * otherFontSize + 2 * lineSpacing; // Title + 3 lines + 2 gaps
		int startY = GeneralControl.Screen.Height / 2 - totalHeight / 2; // Vertical center

		// Title text
		string endText = "Oh, your bird crash and destroy the world!";
		int endTextWidth = Raylib.MeasureText(endText, titleFontSize);
		Raylib.DrawText(endText, GeneralControl.Screen.Width / 2 - endTextWidth / 2, startY, titleFontSize, Color.White);

		// Score text
		string scoreText = $"Final Score: {Score}";
		int scoreTextWidth = Raylib.MeasureText(scoreText, otherFontSize);
		Raylib.DrawText(scoreText, GeneralControl.Screen.Width / 2 - scoreTextWidth / 2, startY + titleFontSize + lineSpacing, otherFontSize, Color.White);

		// Restart text
		string restartText = "Press SPACE to fly other world (Restart)";
		int restartTextWidth = Raylib.MeasureText(restartText, otherFontSize);
		Raylib.DrawText(restartText, GeneralControl.Screen.Width / 2 - restartTextWidth / 2, startY + titleFontSize + 2 * lineSpacing + otherFontSize, otherFontSize, Color.White);

		// Menu text
		string menuText = "Press ESC for Main Menu";
		int menuTextWidth = Raylib.MeasureText(menuText, otherFontSize);
		Raylib.DrawText(menuText, GeneralControl.Screen.Width / 2 - menuTextWidth / 2, startY + titleFontSize + 3 * lineSpacing + 2 * otherFontSize, otherFontSize, Color.White);

		Raylib.EndDrawing();
	}
		
	public void ChangeState(GameStateTemplate NextState){
		StateControl.requestChange(NextState);
	}
	
	public void HandleKeyboardInput(KeyboardKey Input){
		switch (Input){
			case KeyboardKey.Space:
				ChangeState(new GameplayState());
				break;
			case KeyboardKey.Escape:
				AudioControl.PlayMenuChooseSound();
				ChangeState(new MainMenuState());
				break;
		}
	}
	
	// visit ScoreboardState for load score
	private void SaveHighScore(string playerName, int score)
	{
		string filePath = "scores.txt";
		try
		{
			List<(string Player, int Score)> scores = new List<(string, int)>();
			if (File.Exists(filePath))
			{
				string[] lines = File.ReadAllLines(filePath);
				foreach (var line in lines)
				{
					var parts = line.Split(',');
					if (parts.Length == 2 && int.TryParse(parts[1], out int loadedScore))
					{
						scores.Add((parts[0], loadedScore));
					}
				}
			}
			scores.Add((playerName, score));
			scores.Sort((a, b) => b.Score.CompareTo(a.Score));
			if (scores.Count > 5) scores = scores.GetRange(0, 5);

			using (StreamWriter writer = new StreamWriter(filePath))
			{
				foreach (var scoreEntry in scores)
				{
					writer.WriteLine($"{scoreEntry.Player},{scoreEntry.Score}");
				}
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error saving score: {ex.Message}");
		}
	}
}