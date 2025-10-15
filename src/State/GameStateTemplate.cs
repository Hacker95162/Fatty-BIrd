using Raylib_cs;

namespace Fatty_bird;
public interface GameStateTemplate
{
    void Update();
    void Draw();
	void ChangeState(GameStateTemplate NextState);
	void HandleKeyboardInput(KeyboardKey Input);
}