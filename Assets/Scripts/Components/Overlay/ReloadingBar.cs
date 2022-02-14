using UnityEngine.UI;

public class ReloadingBar : BaseWeaponListener
{
    public Image reloadingBarImage;

    private float _timeOfLastShot;
    private float _delayBetweenShots;
    
    public override void OnWeapon(GameEntity e, Weapon weapon, WeaponVFX weaponView) => UpdateReloadingBar(weapon);

    private void UpdateReloadingBar(Weapon weapon)
    {   
        _timeOfLastShot    = weapon.timeOfLastShot;
        _delayBetweenShots = weapon.delayBetweenShots;
    }

    private void Update()
    {
        var timePassed = GameTime.timeFromStart - _timeOfLastShot;
        var percent    = timePassed / _delayBetweenShots;
        
        reloadingBarImage.fillAmount = 1 - percent;
    }
}