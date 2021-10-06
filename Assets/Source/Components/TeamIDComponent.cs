using UnityEngine;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game]
public sealed class TeamIDComponent : IComponent
{
    [EntityIndex]
    public int value;
}