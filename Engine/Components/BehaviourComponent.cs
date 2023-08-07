using Microsoft.Xna.Framework;

namespace Engine.Components;

public abstract class BehaviourComponent : Component
{
    protected internal abstract void Init();
    protected internal abstract void Update(GameTime gameTime);
}