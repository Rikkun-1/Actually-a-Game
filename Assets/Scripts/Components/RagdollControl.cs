using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RagdollControl : MonoBehaviour
{
    private Animator    _animator;
    private Rigidbody[] _rigidbodies;

    private void Start()
    {
        _animator    = GetComponent<Animator>();
        _rigidbodies = GetComponentsInChildren<Rigidbody>();

        MakeAnimated();
    }

    public void MakeAnimated()
    {
        _animator.enabled = true;
        foreach (var body in _rigidbodies)
        {
            body.isKinematic = true;
        }
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