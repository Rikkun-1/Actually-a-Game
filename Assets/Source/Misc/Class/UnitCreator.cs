using System;
using UnityEngine;

public static class UnitCreator
{
    public static GameEntity CreateUnit(Vector2Int position, UnitClass unitClass)
    {
        var e = CreateUnitBase(position);
        switch (unitClass)
        {
            case UnitClass.Shotgun: SetupShotgun(e); break;
            case UnitClass.Rifle:   SetupRifle(e);   break;
            case UnitClass.Sniper:  SetupSniper(e); break;
            default:                throw new ArgumentOutOfRangeException(nameof(unitClass), unitClass, null);
        }

        return e;
    }

    private static GameEntity CreateUnitBase(Vector2Int position)
    {
        var e = GameEntityCreator.CreateEntity(position);
        
        e.enableCalculateVelocityByPositionChanges = true;
        e.AddTraversalSpeed(1.8f);
        e.AddHealth(200, 200);
        e.isPlayer = true;

        return e;
    }

    private static void SetupShotgun(GameEntity e)
    {
        e.AddVision(0, 30, 500, 400);
        e.AddViewPrefab("SwatModel/SwatShotgun");
        WeaponProvider.GiveShotgun(e);
        e.AddReactionDelay(0.75f);
    }

    private static void SetupRifle(GameEntity e)
    {
        e.AddVision(0, 30, 500, 200);
        e.AddViewPrefab("SwatModel/SwatRifle");
        WeaponProvider.GiveRiffle(e);
        e.AddReactionDelay(1.25f);
    }

    private static void SetupSniper(GameEntity e)
    {
        e.AddVision(0, 30, 500, 70);
        e.AddViewPrefab("SwatModel/SwatSniper");
        WeaponProvider.GiveSniper(e);
        e.AddReactionDelay(2.25f);
    }
}
