using Entitas;
using Entitas.CodeGeneration.Attributes;

[Input]
[Unique]
public sealed class SelectedEntityComponent : IComponent
{
    public long gameEntityID;
}