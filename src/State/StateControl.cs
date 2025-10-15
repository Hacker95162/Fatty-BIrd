using Raylib_cs;
using System.Collections;

namespace Fatty_bird;
/* READ ME

These code is write for the sake of optimze, so it tried to minimize the try-catch as much as possible;
The user may unable to make any of these error, but pirate and other developer may;
Things must be have:

*/

public static class StateControl{	
	
	private static bool ShouldClose = false;
	private static bool Pause = false;
	private static bool StateChange = false;
	private static bool Drawing = false;
	private static bool Updating = false;
	private static GameStateTemplate CurrentState;
	
	
	//private static ArrayList RequestTickets = new ArrayList(); // Not use, for multi-Threads
	
	// ============================= Settings // default value - do not depend on it
	
	// ============================= Default Functions
	public static void requestUpdate(){
		if(Pause || Updating) return;
		CurrentState.Update();
	} 
	public static void requestDraw(){
		if(Drawing) return;
		CurrentState.Draw();
	}
	// ============================= Tools Functions


	
	
	// ============================= Logic Functions
	public static void requestChange(GameStateTemplate NextState){
		NextState.Update();
		NextState.Draw();
		CurrentState = NextState;
		
	}
	public static bool StillOpen(){
		return !ShouldClose;
	}
	public static bool InPause(){
		return Pause;
	}
	
	public static void HandleKeyboardInput(KeyboardKey Input){
		CurrentState.HandleKeyboardInput(Input);
	}
	public static void requestPause(){
		Pause = true;
	}
	public static void requestContinue(){
		Pause = false;
	}
	public static void requestClose(){
		ShouldClose = true;
	}
}