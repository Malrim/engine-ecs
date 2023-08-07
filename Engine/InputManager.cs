using Microsoft.Xna.Framework.Input;

namespace Engine;

public static class InputManager
{
    private static KeyboardState _currentKey;
    private static KeyboardState _lastKey;
    private static MouseState _currentMouseState;
    private static MouseState _lastMouseState;
        
    internal static void Update()
    {
        _lastKey = _currentKey;
        _currentKey = Keyboard.GetState();

        _lastMouseState = _currentMouseState;
        _currentMouseState = Mouse.GetState();
    }
        

    public static bool PressedKey(Keys key) => _currentKey.IsKeyDown(key) && _lastKey.IsKeyUp(key);

    public static bool HoldKey(Keys key) => _currentKey.IsKeyDown(key) && _lastKey.IsKeyDown(key);

    public static bool ReleasedKey(Keys key) => _currentKey.IsKeyUp(key) && _lastKey.IsKeyDown(key);


    public static bool RightPressedButton() => _currentMouseState.RightButton == ButtonState.Pressed &&
                                               _lastMouseState.RightButton == ButtonState.Released;

    public static bool RightHoldButton() => _currentMouseState.RightButton == ButtonState.Pressed &&
                                            _lastMouseState.RightButton == ButtonState.Pressed;

    public static bool RightReleasedButton() => _currentMouseState.RightButton == ButtonState.Released &&
                                                _lastMouseState.RightButton == ButtonState.Pressed;


    public static bool MiddlePressedButton() => _currentMouseState.MiddleButton == ButtonState.Pressed &&
                                                _lastMouseState.MiddleButton == ButtonState.Released;

    public static bool MiddleHoldButton() => _currentMouseState.MiddleButton == ButtonState.Pressed &&
                                             _lastMouseState.MiddleButton == ButtonState.Pressed;

    public static bool MiddleReleasedButton() => _currentMouseState.MiddleButton == ButtonState.Released &&
                                                 _lastMouseState.MiddleButton == ButtonState.Pressed;


    public static bool LeftPressedButton() => _currentMouseState.LeftButton == ButtonState.Pressed &&
                                              _lastMouseState.LeftButton == ButtonState.Released;

    public static bool LeftHoldButton() => _currentMouseState.LeftButton == ButtonState.Pressed &&
                                           _lastMouseState.LeftButton == ButtonState.Pressed;

    public static bool LeftReleasedButton() => _currentMouseState.LeftButton == ButtonState.Released &&
                                               _lastMouseState.LeftButton == ButtonState.Pressed;
}