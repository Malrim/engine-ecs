namespace Engine.ECS.Components.Animations;

public class Animator : Component
{
    private string _currentAnimation;
    private readonly IDictionary<string, Animation> _animations = new Dictionary<string, Animation>();
    
    public void AddAnimation(Animation animation)
    {
        if (animation == null)
        {
            throw new NullReferenceException("Animation is null!");
        }

        if (_animations.ContainsKey(animation.Name))
        {
            throw new Exception($"The animator already contains animation with name '{animation.Name}'!");
        }

        _currentAnimation = string.IsNullOrEmpty(_currentAnimation) ? animation.Name : _currentAnimation;
        _animations.Add(animation.Name, animation);
    }
    
    internal Animation GetPlayableAnimation()
    {
        if (_animations.Count == 0)
        {
            throw new Exception("Animator has no animations!");
        }
            
        return _animations[_currentAnimation];
    }
    
    public void Play(string animationName)
    {
        if (animationName == null || !_animations.ContainsKey(animationName))
        {
            throw new Exception("Animation does not exist!");
        }

        var playableAnimation = GetPlayableAnimation();
        if (animationName == playableAnimation.Name || (playableAnimation.WaitForCompletion && !playableAnimation.IsCompleted))
        {
            return;
        }
            
        playableAnimation.Reset();
        _currentAnimation = animationName;
    }
}