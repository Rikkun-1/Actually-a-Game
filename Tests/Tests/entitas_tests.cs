using NSpec;
using Entitas;

public class entitas_tests: nspec
{
    protected Contexts contexts = null;
    protected Systems  systems  = null;
    
    protected void Setup()
    {
        contexts = new Contexts();
        systems  = new Systems();
    }

    protected GameEntity CreateEntity()
    {
        return EntityCreator.CreateGameEntity(contexts);
    }
}
