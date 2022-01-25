using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(WeaponListener))]
public class ReloadingBar : MonoBehaviour
{
    public Image reloadingBarImage;

    private float _timeOfLastShot;
    private float _delayBetweenShots;
    
    private void Start()
    {
        GetComponent<WeaponListener>().OnWeaponChanged += UpdateReloadingBar;
    }

    private void UpdateReloadingBar(Weapon weapon, WeaponVFX weaponView)
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