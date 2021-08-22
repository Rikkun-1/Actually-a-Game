using Entitas;
using Roy_T.AStar.Paths;

[Game]
public class PathComponent : IComponent
{
    public int  currentIndex;
    public Path path;
}