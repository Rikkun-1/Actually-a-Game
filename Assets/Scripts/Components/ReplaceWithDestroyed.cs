using UnityEngine;

public class ReplaceWithDestroyed : MonoBehaviour
{
    public GameObject replacementPrefab;

    [ContextMenu("Replace")]
    public void Replace()
    {
        gameObject.SetActive(false);
        var destroyedObject = Instantiate(replacementPrefab, transform.position, transform.rotation);
        destroyedObject.transform.localScale = transform.localScale;
        destroyedObject.transform.SetParent(transform.parent);
        Destroy(gameObject);
    }
}
