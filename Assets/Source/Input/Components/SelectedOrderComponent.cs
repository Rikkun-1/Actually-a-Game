using Entitas;
using Entitas.CodeGeneration.Attributes;

[Input]
[Unique]
public sealed class SelectedOrderComponent : IComponent
{
    public string orderName;
}