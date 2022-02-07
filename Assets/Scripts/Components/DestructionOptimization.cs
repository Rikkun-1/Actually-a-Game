using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class DestructionOptimization : MonoBehaviour
{
    [Range(0, 1)]
    public float threshold;

    private void Start()
    {
        StartCoroutine(CheckIfSleeping());
    }
    
    private IEnumerator CheckIfSleeping()
    {
        var rigidbodies = GetComponentsInChildren<Rigidbody>();

        var wait = new WaitForSeconds(2.5f);
        while (GetSleepingBodiesPercent(rigidbodies) < threshold)
        {
            yield return wait;
        }
        CombineMeshes();
    }

    private void CombineMeshes()
    {
        var meshFilter       = gameObject.GetComponent<MeshFilter>();
        var meshFilters = GetComponentsInChildren<MeshFilter>().ToList();
        meshFilters.Remove(meshFilter);
        
        var combineInstances = new CombineInstance[meshFilters.Count];
        
        for (var i = 0; i < meshFilters.Count; i++)
        {
            combineInstances[i].mesh = meshFilters[i].sharedMesh;
            combineInstances[i].transform = meshFilters[i].transform.localToWorldMatrix;

            meshFilters[i].gameObject.SetActive(false);
            Destroy(meshFilters[i].gameObject);
        }

        meshFilter.mesh = new Mesh();
        meshFilter.mesh.CombineMeshes(combineInstances);

        SetupCollider();
        ResetTransform();
    }

    private void ResetTransform()
    {
        transform.localScale = Vector3.one;
        transform.rotation   = Quaternion.identity;
        transform.position   = Vector3.zero;
    }

    private void SetupCollider()
    {
        gameObject.AddComponent<MeshCollider>()
                  .convex = true;
        gameObject.isStatic = true;
    }

    private static float GetSleepingBodiesPercent(Rigidbody[] rigidbodies)
    {
        var sleepingCount = rigidbodies.Count(body => body.IsSleeping());
        return (float)sleepingCount / rigidbodies.Length;
    }
}
