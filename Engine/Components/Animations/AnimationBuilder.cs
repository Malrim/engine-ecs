namespace Engine.Components.Animations;

public class AnimationBuilder
{
    private Animation _animation;

    public AnimationBuilder Create(string name, int animationType, bool waitForCompletion = false, bool isLoop = true)
    {
        _animation = new Animation(name, animationType, waitForCompletion, isLoop);
        return this;
    }

    public AnimationBuilder WithFrame(AnimationFrame frame)
    {
        if (_animation == null)
        {
            throw new NullReferenceException("Animation is null! You must call the 'Create' method before the 'WithFrame' method!");
        }
            
        _animation.AddFrame(frame);
        return this;
    }

    public AnimationBuilder WithFrames(int frameCount, int speed)
    {
        if (_animation == null)
        {
            throw new NullReferenceException("Animation is null! You must call the 'Create' method before the 'WithFrames' method!");
        }

        for (var i = 0; i < frameCount; i++)
        {
            _animation.AddFrame(new AnimationFrame(i, speed));
        }
        return this;
    }

    public void Build(Animator animator)
    {
        if (_animation == null)
        {
            throw new NullReferenceException("Animation is null! You must call the 'Create' method before the 'Build' method!");
        }

        animator.AddAnimation(_animation);
        _animation = null;
    }
}