using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RagdollControl : MonoBehaviour
{
    public Animator    _animator;
    public Rigidbody[] _rigidbodies;

    private void Start()
    {
        _animator    ??= GetComponent<Animator>();
        _rigidbodies ??= GetComponentsInChildren<Rigidbody>();
    }

    public void MakePhysical()
    {
        _animator.enabled = false;
        
        foreach (var body in _rigidbodies)
        {
            body.isKinematic = false;
        }
    }
}