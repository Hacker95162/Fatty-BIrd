using Raylib_cs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Fatty_bird;

public static class GeneralControl{	
	private static String DefaultPaths = "";
	private static bool FileSafeToLoad = File.Exists(DefaultPaths);
	
	private const string ChecksumKey = "Fattychicken123";
    private const char Delimiter = '|';
	
	
	// ============================= Enum
	public enum ScreenType{
		Window,      // Regular windowed mode
		Maxsize,     // Maximized window (not fullscreen)
		Fullscreen   // Fullscreen mode
	}
	
	
	// ============================= Settings // default value - do not depend on it
	
	
	
	public static (int Width, int Height, string Title, int FPS, ScreenType screenType) Screen { get; private set; }
	private static bool ShouldClose;



	
	// ============================= Default Functions
	
	public static void Load()
	{
		if (FileSafeToLoad)
		{
			string[] lines = File.ReadAllLines(DefaultPaths);
			if (lines.Length >= 2)
			{
				string[] parts = lines[0].Split(Delimiter);
				if (parts.Length == 7 && // avoid try catch, but still return correct error
					int.TryParse(parts[0], out int width) &&
					int.TryParse(parts[1], out int height) &&
					int.TryParse(parts[3], out int fps) &&
					Enum.TryParse(parts[4], out ScreenType screenType))
				{
					string title = parts[2];
					string data = $"{width}{Delimiter}{height}{Delimiter}{title}{Delimiter}{fps}{Delimiter}{screenType}";
					string storedChecksum = lines[1];
					string computedChecksum = ComputeChecksum(data + ChecksumKey);

					if (storedChecksum == computedChecksum)
					{
						Screen = (width, height, title, fps, screenType);
						return;
					}else{Console.WriteLine("Settings file tampered or corrupted. Loading defaults.");}
				}else{Console.WriteLine("Invalid settings file format. Loading defaults.");}
			}else{Console.WriteLine("Settings file incomplete. Loading defaults.");}
		}else{Console.WriteLine("Settings file not found. Creating with defaults.");}

		SetDefaultSettings();
		Save();
	}
	
	
    public static void Save(){
		 try
        {
            string data = $"{Screen.Width}{Delimiter}{Screen.Height}{Delimiter}{Screen.Title}{Delimiter}{Screen.FPS}{Delimiter}{Screen.screenType}";
            string checksum = ComputeChecksum(data + ChecksumKey);

            File.WriteAllLines(DefaultPaths, new[]
            {
                data,
                checksum
            });
            Console.WriteLine($"Saved settings to {DefaultPaths}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving settings: {ex.Message}");
        }
	}
	
	
	
	
	
	// ============================= Tools Functions
	
	
	private static void SetDefaultSettings(){
        int width = 1280;
        int height = 720;
		string title = "Fatty Bird";
		int fps = 60;
		ScreenType screenType = ScreenType.Window;
		
        Screen = (width, height, title, fps, screenType);
    }
	
	private static string ComputeChecksum(string data){
        using (var md5 = MD5.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            byte[] hash = md5.ComputeHash(bytes);
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }
	
	
	// ============================= Logic Functions
	

	public static bool IsFullScreen(){
		return Screen.screenType == ScreenType.Fullscreen;
	}
	
	
}