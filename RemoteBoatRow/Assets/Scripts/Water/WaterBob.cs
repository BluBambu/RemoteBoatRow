using UnityEngine;

public class WaterBob : MonoBehaviour
{
    [SerializeField]
    float height = 0.1f;

    [SerializeField]
    float period = 1;

    private Vector3 initialPosition;
    private float offset;

    private void Awake()
    {
        initialPosition = transform.position;

        offset = 1 - (Random.value * 2);
    }

    private void Update()
    {
        var newHeight = initialPosition.y - Mathf.Sin((Time.time + offset) * period) * height;

        transform.position = new Vector3(transform.position.x, newHeight, transform.position.z);
    }
}
