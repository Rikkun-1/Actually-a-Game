﻿using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game]
public sealed class IdComponent : IComponent
{
    [PrimaryEntityIndex]
    public long value;
}