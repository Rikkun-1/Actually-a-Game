public static class GameTime
{
    public static float deltaTime => Contexts.sharedInstance.game.simulationTick.deltaTime;
}