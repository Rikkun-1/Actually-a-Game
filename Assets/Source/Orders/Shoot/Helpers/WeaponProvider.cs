public static class WeaponProvider
{
    public static void GiveShotgun(GameEntity e)
    {
        e.AddWeapon(0, 1.5f, 9, 15, 20, 12, "bullet");
    }
    
    public static void GiveRiffle(GameEntity e)
    {
        e.AddWeapon(0, 0.2f, 5.5f, 30, 25, 1, "bullet");
    }
    
    public static void GiveSniper(GameEntity e)
    {
        e.AddWeapon(0, 2.5f, 1.5f, 100, 50, 1, "bullet");
    }
}
