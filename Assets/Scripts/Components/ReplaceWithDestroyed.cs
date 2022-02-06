using UnityEngine;

public class ReplaceWithDestroyed : MonoBehaviour
{
    public GameObject replacementPrefab;

    public void Replace()
    {
        Instantiate(replacementPrefab, transform.position, transform.rotation, transform.parent);
        Destroy(gameObject);
    }
}
