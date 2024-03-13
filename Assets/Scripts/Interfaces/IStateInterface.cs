
public abstract class IStateInterface
{
    public virtual void OnStateEnter() { }
    public virtual void Update() { }
    public virtual void OnStateExit() { }

    public virtual void HandleClickEvent() { }
}
