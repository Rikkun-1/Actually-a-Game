using UnityEngine;

public class BulletHitChecker : MonoBehaviour
{
    public delegate void HitAction(RaycastHit hitInfo);
    public event HitAction OnHit;

    private Vector3 _currentPosition;
    private Vector3 _previousPosition;

    private void Start()
    {
        _currentPosition  = transform.position;
        _previousPosition = _currentPosition;

        OnHit += RegisterCollision;
    }

    private void Update()
    {
        _currentPosition = transform.position;
        CheckCollision(_previousPosition, _currentPosition);
        _previousPosition = _currentPosition;
    }


    private void CheckCollision(Vector3 previousPosition, Vector3 currentPosition)
    {
        var direction = currentPosition - previousPosition;
        var distance  = Vector3.Distance(currentPosition, previousPosition);

        if (Physics.Raycast(previousPosition, direction, out var hitInfo, distance))
        {
            OnHit?.Invoke(hitInfo);
        }
    }

    private void RegisterCollision(RaycastHit hit)
    {
        var firstEntity  = this.GetGameEntity();
        var secondEntity = hit.collider.GetGameEntity();

        if (!BulletCollisionHelper.IsCollisionBetweenBulletAndOtherEntity(firstEntity, secondEntity)) return;

        EntityCreator.CreateGameEntity()
                     .AddCollision(firstEntity.id.value, secondEntity.id.value);
    }
}