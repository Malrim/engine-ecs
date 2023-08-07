namespace Engine.Components.Animations;

public class Animation
{
    public string Name { get; }
    public int AnimationType { get; }
    public bool WaitForCompletion { get; }
    public bool IsLoop { get; }
        
    internal bool IsCompleted { get; set; }
    internal int CurrentFrame { get; set; }
    internal int NumberFrames => _frames.Count;
    internal float ElapsedTime { get; set; }
    
    private readonly IDictionary<int, AnimationFrame> _frames = new Dictionary<int, AnimationFrame>();
    
    public Animation(string name, int animationType, bool waitForCompletion = false, bool isLoop = true)
    {
        Name = name;
        AnimationType = animationType;
        WaitForCompletion = waitForCompletion;
        IsLoop = isLoop;
    }
    
    public void AddFrame(AnimationFrame frame)
    {
        _frames.Add(_frames.Count, frame);
    }
    
    internal AnimationFrame GetFrame() => _frames[CurrentFrame];
        
    internal void Reset()
    {
        CurrentFrame = 0;
        ElapsedTime = 0f;
        IsCompleted = false;
    }
}