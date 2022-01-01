public static class GameTime
{
    public static float deltaTime => Contexts.sharedInstance.game.simulationTick.deltaTime;
    public static float timeFromStart => Contexts.sharedInstance.game.simulationTick.timeFromStart;
}