using Microsoft.Xna.Framework;

namespace Engine.ECS.Components;

public abstract class BehaviourComponent : Component
{
    protected internal abstract void Start();
    protected internal abstract void Update(GameTime gameTime);
}