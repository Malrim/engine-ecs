using Engine.ECS.Components.Animations;
using Engine.ECS.Entities;
using Microsoft.Xna.Framework;

namespace Engine.ECS.GameSystems.Updatables;

public class AnimatorSystem : UpdatableGameSystem
{
    protected override ComponentTypes ComponentTypes => new ComponentTypes().WithRequiredType<Animator>();
    
    protected override void Update(Entity entity, GameTime gameTime)
    {
        var animator = entity.GetComponent<Animator>();
        PlayAnimation(animator.GetPlayableAnimation(), gameTime);
    }
    
    private static void PlayAnimation(Animation animation, GameTime gameTime)
    {
        if (!animation.IsLoop && animation.IsCompleted)
        {
            return;
        }

        var frame = animation.GetFrame();
        if (animation.CurrentFrame == 0)
        {
            frame.Action?.Invoke();
        }
            
        animation.ElapsedTime += (float) gameTime.ElapsedGameTime.TotalMilliseconds;
        if (animation.ElapsedTime < frame.Speed)
        {
            return;
        }
            
        UpdateAnimation(animation);
            
        frame.Action?.Invoke();
        animation.ElapsedTime = 0f;
    }
        
    private static void UpdateAnimation(Animation animation)
    {
        animation.IsCompleted = false;
        animation.CurrentFrame++;

        if (animation.CurrentFrame < animation.NumberFrames)
        {
            return;
        }
            
        animation.CurrentFrame = animation.IsLoop ? 0 : animation.NumberFrames - 1;
        animation.IsCompleted = true;
    }
}