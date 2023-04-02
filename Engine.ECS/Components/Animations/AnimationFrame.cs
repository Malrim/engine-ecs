namespace Engine.ECS.Components.Animations;

public struct AnimationFrame
{
    public int Id { get; }
    public float Speed { get; }
    public Action Action { get; }

    public AnimationFrame(int id, int speed, Action action = null)
    {
        Id = id;
        Speed = speed;
        Action = action;
    }
}