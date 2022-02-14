using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game] [Unique] [IgnoreSave]
public sealed class GameSettingsComponent : IComponent
{
    public GameSettings value;
}
