using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    [SerializeField] private Transform _enemy;

    private void Update()
    {
        var lookDirection = _enemy.position - transform.position;
        var rotationAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(rotationAngle, Vector3.forward);

        // transform.right = _enemy.position - transform.position;
    }
}
