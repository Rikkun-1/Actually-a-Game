using Source.TestSystems;

public sealed class TestSystems : Feature
{
    public TestSystems(Contexts contexts)
    {
        Add(new TestGridNonWalkableSystem(contexts));
        Add(new TestGridWallsSystem(contexts));
        Add(new TestPathSystem(contexts));
    }
}