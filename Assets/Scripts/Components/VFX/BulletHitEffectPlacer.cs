using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR 
[InitializeOnLoad] 
#endif
public class BulletHitEffectPlacer : MonoBehaviour
{
    private static ParticleSystem _hitEffectCachedInstance;
    public         ParticleSystem hitEffectPrefab;
    
#if UNITY_EDITOR 
    static BulletHitEffectPlacer() 
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state) {
        if (state == PlayModeStateChange.ExitingPlayMode)
        {
            _hitEffectCachedInstance = null;
        }
    }
#endif

    private void Start()
    {
        _hitEffectCachedInstance ??= Instantiate(hitEffectPrefab);

        if (TryGetComponent<BulletHitChecker>(out var bulletHitChecker))
        {
            bulletHitChecker.OnHit += PlaceHitEffect;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        var contact = other.GetContact(0);
        PlaceHitEffect(contact.point, contact.normal);
    }

    private static void PlaceHitEffect(RaycastHit hitInfo)
    {
        PlaceHitEffect(hitInfo.point, hitInfo.normal);
    }

    private static void PlaceHitEffect(Vector3 point, Vector3 normal)
    {
        var hitTransform = _hitEffectCachedInstance.transform;

        hitTransform.position = point;
        hitTransform.forward  = normal;

        _hitEffectCachedInstance.Emit(1);
    }
}