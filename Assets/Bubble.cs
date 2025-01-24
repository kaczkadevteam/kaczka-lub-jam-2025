using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Bubble : MonoBehaviour
{
    [SerializeField] public Transform blower;
    [SerializeField] private Vector3 movementDirection;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float currentSpeed;

    void Start()
    {
        movementDirection = Vector3.Normalize(transform.position - blower.position);
        currentSpeed = maxSpeed;
    }

    void FixedUpdate()
    {
        transform.position += movementDirection * currentSpeed / 100;
        currentSpeed -= maxSpeed / 100;
        if (currentSpeed <= maxSpeed / 10) {
            Destroy(gameObject);
        }
    }
}
