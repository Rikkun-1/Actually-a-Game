public static class WeaponProvider
{
    public static void GiveShotgun(GameEntity e)
    {
        e.AddWeapon(new Weapon(0, 1.5f, 1.5f, 18, 25, 12, "Prefabs/Bullet"), null);
    }

    public static void GiveRiffle(GameEntity e)
    {
        e.AddWeapon(new Weapon(0, 0.2f, 1f, 30, 35, 1, "Prefabs/Bullet"), null);
    }

    public static void GiveSniper(GameEntity e)
    {
        e.AddWeapon(new Weapon(0, 2.5f, 0.25f, 175, 50, 1, "Prefabs/Bullet"), null);
    }
}   