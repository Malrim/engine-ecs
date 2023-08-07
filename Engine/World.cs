using Engine.GameScenes;
using Microsoft.Xna.Framework;

namespace Engine;

public sealed class World
{
    private readonly Game _game;
    private GameScene _newGameScene;
    private GameScene _activeGameScene;

    public World(Game game)
    {
        _game = game;
    }

    public void LoadGameScene(GameScene gameScene)
    {
        _newGameScene = gameScene ?? throw new NullReferenceException("Game scene is null!");
    }
    
    public void Update(GameTime gameTime)
    {
        if (_newGameScene != null)
        {
            _activeGameScene?.Unload();
            _activeGameScene = _newGameScene;
            _newGameScene = null;
            _activeGameScene.Load(this, _game);
        }
        
        InputManager.Update();
        _activeGameScene?.Update(gameTime);
    }

    public void Draw(GameTime gameTime)
    {
        _activeGameScene?.Draw(gameTime);
    }
}