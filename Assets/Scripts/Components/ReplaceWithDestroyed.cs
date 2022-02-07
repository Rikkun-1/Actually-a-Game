using UnityEngine;

public class ReplaceWithDestroyed : MonoBehaviour
{
    public GameObject replacementPrefab;

    [ContextMenu("Replace")]
    public void Replace()
    {
        gameObject.SetActive(false);
        Instantiate(replacementPrefab, transform.position, transform.rotation, transform.parent);
        Destroy(gameObject);
    }
}
