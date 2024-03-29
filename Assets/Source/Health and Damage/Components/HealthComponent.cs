﻿using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game]
[Event(EventTarget.Self)]
public sealed class HealthComponent : IComponent
{
    public int currentHealth;
    public int maxHealth;
}