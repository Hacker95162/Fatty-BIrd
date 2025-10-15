using Raylib_cs;
using System;
using System.Collections.Generic;
using System.IO;

namespace Fatty_bird;

public class ScoreboardState : GameStateTemplate
{
    private List<(string Player, int Score)> highScores;

    public ScoreboardState()
    {
		highScores = new List<(string Player, int Score)>();
        LoadHighScores();
    }

	// Visit EndGameState for more save 
    private void LoadHighScores()
    {
        string filePath = "scores.txt";
        try
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int score))
                    {
                        highScores.Add((parts[0], score));
                    }
                }
                highScores.Sort((a, b) => b.Score.CompareTo(a.Score));
                if (highScores.Count > 5) highScores = highScores.GetRange(0, 5);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading scores: {ex.Message}");
        }
    }

    public void Update(){
		AudioControl.UpdateBackgroundMusic();
	}


	public void Draw()
	{
		Raylib.BeginDrawing();
		Raylib.ClearBackground(Color.Black);
		
		GraphicControl.DrawBackgroundFullScreenScoreboard();

		// Font sizes and spacing
		int titleFontSize = 40;
		int otherFontSize = 20;
		int lineSpacing = 30;

		// Calculate total height of text block
		int totalHeight;
		int scoreLines = highScores.Count == 0 ? 1 : highScores.Count;
		totalHeight = titleFontSize + scoreLines * otherFontSize + otherFontSize + (scoreLines + 1) * lineSpacing;
		int startY = GeneralControl.Screen.Height / 2 - totalHeight / 2;

		// Title with black outer glow
		string title = "SCOREBOARD";
		int titleWidth = Raylib.MeasureText(title, titleFontSize);
		int xTitle = GeneralControl.Screen.Width / 2 - titleWidth / 2;
		int yTitle = startY;
		Raylib.DrawText(title, xTitle + 1, yTitle + 1, titleFontSize, Color.Black);
		Raylib.DrawText(title, xTitle - 1, yTitle - 1, titleFontSize, Color.Black);
		Raylib.DrawText(title, xTitle + 1, yTitle - 1, titleFontSize, Color.Black);
		Raylib.DrawText(title, xTitle - 1, yTitle + 1, titleFontSize, Color.Black);
		Raylib.DrawText(title, xTitle, yTitle, titleFontSize, Color.White);

		// High scores or "No scores yet!" with black outer glow
		if (highScores.Count == 0)
		{
			string noScores = "No scores yet!";
			int noScoresWidth = Raylib.MeasureText(noScores, otherFontSize);
			int xNoScores = GeneralControl.Screen.Width / 2 - noScoresWidth / 2;
			int yNoScores = startY + titleFontSize + lineSpacing;
			Raylib.DrawText(noScores, xNoScores + 1, yNoScores + 1, otherFontSize, Color.Black);
			Raylib.DrawText(noScores, xNoScores - 1, yNoScores - 1, otherFontSize, Color.Black);
			Raylib.DrawText(noScores, xNoScores + 1, yNoScores - 1, otherFontSize, Color.Black);
			Raylib.DrawText(noScores, xNoScores - 1, yNoScores + 1, otherFontSize, Color.Black);
			Raylib.DrawText(noScores, xNoScores, yNoScores, otherFontSize, Color.White);
		}
		else
		{
			for (int i = 0; i < highScores.Count; i++)
			{
				string scoreText = $"{i + 1}. {highScores[i].Player}: {highScores[i].Score}";
				int scoreWidth = Raylib.MeasureText(scoreText, otherFontSize);
				int xScore = GeneralControl.Screen.Width / 2 - scoreWidth / 2;
				int yScore = startY + titleFontSize + lineSpacing + i * (otherFontSize + lineSpacing);
				Raylib.DrawText(scoreText, xScore + 1, yScore + 1, otherFontSize, Color.Black);
				Raylib.DrawText(scoreText, xScore - 1, yScore - 1, otherFontSize, Color.Black);
				Raylib.DrawText(scoreText, xScore + 1, yScore - 1, otherFontSize, Color.Black);
				Raylib.DrawText(scoreText, xScore - 1, yScore + 1, otherFontSize, Color.Black);
				Raylib.DrawText(scoreText, xScore, yScore, otherFontSize, Color.White);
			}
		}

		// ESC prompt with black outer glow
		string escText = "Press ESC to return to Main Menu";
		int escWidth = Raylib.MeasureText(escText, otherFontSize);
		int xEsc = GeneralControl.Screen.Width / 2 - escWidth / 2;
		int yEsc = startY + titleFontSize + lineSpacing + scoreLines * (otherFontSize + lineSpacing);
		Raylib.DrawText(escText, xEsc + 1, yEsc + 1, otherFontSize, Color.Black);
		Raylib.DrawText(escText, xEsc - 1, yEsc - 1, otherFontSize, Color.Black);
		Raylib.DrawText(escText, xEsc + 1, yEsc - 1, otherFontSize, Color.Black);
		Raylib.DrawText(escText, xEsc - 1, yEsc + 1, otherFontSize, Color.Black);
		Raylib.DrawText(escText, xEsc, yEsc, otherFontSize, Color.White);

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