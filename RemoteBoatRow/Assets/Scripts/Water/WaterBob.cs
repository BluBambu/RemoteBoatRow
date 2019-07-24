using UnityEngine;

public class WaterBob : MonoBehaviour
{
    [SerializeField]
    public float height = 0.1f;

    [SerializeField]
    public float period = 1;

    private Vector3 _initialPosition;
    private float _offset;

    private void Awake()
    {
        _initialPosition = transform.position;
        _offset = 1 - (Random.value * 2);
    }

    private void Update()
    {
        var newHeight = _initialPosition.y - Mathf.Sin((Time.time + _offset) * period) * height;
        transform.position = new Vector3(transform.position.x, newHeight, transform.position.z);
    }
}
