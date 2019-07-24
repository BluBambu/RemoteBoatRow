using UnityEngine;
using Mirror;

public class BoatManager : NetworkBehaviour
{
    private const float TorqueInput = 80;
    private const float ForceInput = 80;
    private const float RotationInput = 200;

    private Rigidbody _rigidbody;

    private bool isLeftOarTaken;

    public Transform leftOar;
    public Transform rightOar;

    public bool isRightOarDirectionForward = false;
    public bool isLeftOarDirectionForward = false;

    public float rightRotationCounter = 35;
    public float leftRotationCounter = 35;

    public override void OnStartClient()
    {
        Debug.Log("BoatManager: Client started");

        if (!hasAuthority)
        {
            Destroy(GetComponent<Rigidbody>());
            Destroy(GetComponent<Collider>());
            Destroy(GetComponent<WaterBob>());
        }
        else
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
    }

    public bool isLeftOar()
    {
        isLeftOarTaken = !isLeftOarTaken;
        return isLeftOarTaken;
    }

    public void RowLeftOar(float rowFactor)
    {
        _rigidbody.AddRelativeForce(0, 0, ForceInput * Time.deltaTime * (rowFactor * 2));
        _rigidbody.AddRelativeTorque(Vector3.up * TorqueInput * Time.deltaTime * (rowFactor * 2));

        leftRotationCounter += RotationInput * Time.deltaTime;
        if (leftRotationCounter >= 70)
        {
            isLeftOarDirectionForward = !isLeftOarDirectionForward;
            leftRotationCounter -= 70;
        }

        if (isLeftOarDirectionForward)
        {
            leftOar.Rotate(0, -1 * RotationInput * Time.deltaTime, 0, Space.Self);
        }
        else
        {
            leftOar.Rotate(0, RotationInput * Time.deltaTime, 0, Space.Self);
        }
    }

    public void RowRightOar(float rowFactor)
    {
        _rigidbody.AddRelativeForce(0, 0, ForceInput * Time.deltaTime * (rowFactor * 2));
        _rigidbody.AddRelativeTorque(Vector3.up * -TorqueInput * Time.deltaTime * (rowFactor * 2));

        rightRotationCounter += RotationInput * Time.deltaTime;
        if (rightRotationCounter >= 70)
        {
            isRightOarDirectionForward = !isRightOarDirectionForward;
            rightRotationCounter -= 70;
        }

        if (isRightOarDirectionForward)
        {
            rightOar.Rotate(0, RotationInput * Time.deltaTime, 0, Space.Self);
        }
        else
        {
            rightOar.Rotate(0, -1 * RotationInput * Time.deltaTime, 0, Space.Self);
        }
    }
}
