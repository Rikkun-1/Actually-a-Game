using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform      barrelEnd;
    public ParticleSystem muzzleFlash;
    public AudioClip      shootSound;

    public void PlayShootEffects()
    {
        AudioSource.PlayClipAtPoint(shootSound, barrelEnd.position);
        muzzleFlash.Emit(1);
    }
}