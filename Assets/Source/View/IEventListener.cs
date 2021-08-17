using Entitas;

public interface IEventListener
{
    void RegisterEventListeners(IEntity entity);

    void UnregisterEventListeners();
}