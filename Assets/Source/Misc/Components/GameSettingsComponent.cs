using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game]
[Unique]
public class GameSettingsComponent : IComponent
{
    public GameSettings value;
}
