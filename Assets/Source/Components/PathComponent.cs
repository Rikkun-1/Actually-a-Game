using Entitas;
using Roy_T.AStar.Paths;

[Game]
public class PathComponent : IComponent
{
    public Path Path;
    public int CurrentIndex;
}