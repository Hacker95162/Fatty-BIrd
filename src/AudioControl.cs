using Raylib_cs;
using System.Collections;

namespace Fatty_bird;
/* READ ME

These code is write for the sake of optimze, so it tried to minimize the try-catch as much as possible;
The user may unable to make any of these error, but pirate and other developer may;
Things must be have:

*/

public static class AudioControl{	
	private static String[] DefaultPaths = [ // handconfig path
		Path.Combine("audio", "Background.mp3"),
		Path.Combine("audio", "GameplayBackground.mp3"),
		Path.Combine("audio", "hit.wav"),
		Path.Combine("audio", "menu_select.wav")
	];
	private static ArrayList PathChanged = new ArrayList();
	
	private static string assetsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "audio");
	private static bool[] FileSafeToLoad = new bool[DefaultPaths.Length];
	private static bool FileSafeToLoadAll = false;
	private static bool FileChangeSensor = false;
	
	
	// ============================= Settings // default value - do not depend on it
	
	private static float MusicVolume;
	private static float SoundVolume;
	
	private static Music BackgroundMusic;
	private static Music GameplayBackgroundMusic;
	private static Sound HitSound;
	private static Sound MenuChooseSound;
	
	
	// ============================= Default Functions
	
	public static void Load(){
		CheckFileValid();
		if(FileSafeToLoadAll){
			Raylib.InitAudioDevice();
			BackgroundMusic = LoadMusicAssets(DefaultPaths[0]);
			GameplayBackgroundMusic = LoadMusicAssets(DefaultPaths[1]);
			HitSound = LoadSoundAssets(DefaultPaths[2]);
			MenuChooseSound = LoadSoundAssets(DefaultPaths[3]);
			SetDefaultSettings();
			//MusicVolume = Math.Clamp(musicVol, 0.0f, 1.0f);
			//SoundVolume = Math.Clamp(soundVol, 0.0f, 1.0f);
		}else{
			throw new FileNotFoundException($"Missing files: ");//{string.Join(", ", missingFiles.ToArray())}");
		}
	}
	
    public static void Save(){}// audio not required to save, at least this game};
	
	
	
    public static void CheckFileValid(){
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
	
	public static void Unload()
    {
        StopMusicAll();
        Raylib.UnloadMusicStream(BackgroundMusic);
        Raylib.UnloadMusicStream(GameplayBackgroundMusic);
        Raylib.UnloadSound(HitSound);
        Raylib.UnloadSound(MenuChooseSound);
        //if (Raylib.IsAudioDeviceInitialized()) Raylib.CloseAudioDevice();
    }
	
	
		
	// ============================= Tools Functions
	
	
	private static Music LoadMusicAssets(string path){
		string assetsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
		return Raylib.LoadMusicStream(assetsPath);
		//FlapSound = Raylib.LoadSound(Path.Combine(assetsPath, "flap.wav"));
    }
	private static Sound LoadSoundAssets(string path){
		string assetsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
		return Raylib.LoadSound(assetsPath);
    }
	

    private static void ApplyVolumes()
    {/*
        if (Raylib.IsMusicValid(BackgroundMusic))
            Raylib.SetMusicVolume(BackgroundMusic, MusicVolume);
		
        if (Raylib.IsSoundValid(FlapSound))
			Raylib.SetSoundVolume(FlapSound, SoundVolume);
		*/
		
		Raylib.SetMusicVolume(BackgroundMusic, MusicVolume);
		Raylib.SetMusicVolume(GameplayBackgroundMusic, MusicVolume);
		Raylib.SetSoundVolume(HitSound, SoundVolume);
		Raylib.SetSoundVolume(MenuChooseSound, SoundVolume);
    }
	
	private static void SetDefaultSettings(){
		MusicVolume = 0.5f;
        SoundVolume = 0.8f;
		
		ApplyVolumes();
    }
	
	
	// ============================= Logic Functions
	
	public static float GetMusicVolume() => MusicVolume;
    public static float GetSoundVolume() => SoundVolume;
	public static void SetMusicVolume(float musicVolume){
		MusicVolume = Math.Clamp(musicVolume, 0.0f, 1.0f);
		ApplyVolumes();
	}

	public static void SetSoundVolume(float soundVolume){
		SoundVolume = Math.Clamp(soundVolume, 0.0f, 1.0f);
		ApplyVolumes();
	}
	
	
	public static void PlayMusic(Music music){
        Raylib.PlayMusicStream(music);
    }
	
    public static void StopMusic(Music music){
        Raylib.StopMusicStream(music);
    }
	public static void StopMusicAll(){
        Raylib.StopMusicStream(BackgroundMusic);
		Raylib.StopMusicStream(GameplayBackgroundMusic);
    }

    public static void ResumeMusic(Music music){

        Raylib.ResumeMusicStream(music);

    }

    public static void UpdateMusic(Music music){
        Raylib.UpdateMusicStream(music);
    }

    public static void PlaySound(Sound sound){
        Raylib.PlaySound(sound);
    }

	
	
	// ============================= Advance Function - use once function
	
	public static void PlayBackgroundMusic(){
		//StopMusicAll();
		PlayMusic(BackgroundMusic);
	}
	public static void UpdateBackgroundMusic(){
		UpdateMusic(BackgroundMusic);
	}
	public static void PlayGameplayBackgroundMusic(){
		StopMusicAll();
		PlayMusic(GameplayBackgroundMusic);
	}
	public static void UpdateGameplayBackgroundMusic(){
		UpdateMusic(GameplayBackgroundMusic);
	}
	public static void PlayHitSound(){
		PlaySound(HitSound);
	}
	public static void PlayMenuChooseSound(){
		PlaySound(MenuChooseSound);
	}
}