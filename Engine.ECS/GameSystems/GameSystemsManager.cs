using Engine.ECS.Entities;
using Engine.ECS.GameScenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.ECS.GameSystems;

internal sealed class GameSystemsManager
{
    private readonly IList<UpdatableGameSystem> _updatableGameSystems = new List<UpdatableGameSystem>();
    private readonly IList<DrawableGameSystem> _drawableGameSystems = new List<DrawableGameSystem>();

    public void RegisterSystem(GameSystem gameSystem, GameScene gameScene, EntityController entityController)
    {
        if (gameSystem == null)
        {
            throw new NullReferenceException("Game system must not be null!");
        }
        
        switch (gameSystem)
        {
            case UpdatableGameSystem updatableGameSystem:
                if (_updatableGameSystems.Contains(updatableGameSystem))
                {
                    throw new Exception($"Updatable system '{updatableGameSystem.GetType().Name}' is exists!");
                }
                _updatableGameSystems.Add(updatableGameSystem);
                break;
            case DrawableGameSystem drawableGameSystem:
                if (_drawableGameSystems.Contains(drawableGameSystem))
                {
                    throw new Exception($"Drawable system '{drawableGameSystem.GetType().Name}' is exists!");
                }
                _drawableGameSystems.Add(drawableGameSystem);
                break;
            default:
                throw new Exception($"Unknown system: {gameSystem.GetType().Name}!");
        }
        
        gameSystem.Initialize(gameScene, entityController);
    }

    public void Update(GameTime gameTime)
    {
        foreach (var updatableGameSystem in _updatableGameSystems)
        {
            updatableGameSystem.Update(gameTime);
        }
    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        foreach (var drawableGameSystem in _drawableGameSystems)
        {
            drawableGameSystem.Draw(spriteBatch, gameTime);
        }
    }
}