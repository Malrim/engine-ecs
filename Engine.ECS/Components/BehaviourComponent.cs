using Engine.ECS.GameScenes;
using Microsoft.Xna.Framework;

namespace Engine.ECS.Components;

public abstract class BehaviourComponent : Component
{
    protected GameScene GameScene { get; private set; }

    protected abstract void Start();
    protected internal abstract void Update(GameTime gameTime);
    
    internal void Start(GameScene gameScene)
    {
        GameScene = gameScene;
        Start();
    }
}