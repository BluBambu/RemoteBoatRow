using UnityEngine;
using Mirror;

public class BoatManager : NetworkBehaviour
{
    private const float TorqueInput = 80;
    private const float ForceInput = 80;

    private Rigidbody _rigidbody;

    private bool isLeftOarTaken;

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
        var returnVal = isLeftOarTaken;

        if (isLeftOarTaken)
        {
            isLeftOarTaken = false;
        }

        return returnVal;
    }

    public void RowLeftOar(float rowFactor)
    {
        _rigidbody.AddRelativeForce(0, 0, ForceInput * Time.deltaTime * (rowFactor * 2));
        _rigidbody.AddRelativeTorque(Vector3.up * TorqueInput * Time.deltaTime * (rowFactor * 2));
    }

    public void RowRightOar(float rowFactor)
    {
        _rigidbody.AddRelativeForce(0, 0, ForceInput * Time.deltaTime * (rowFactor * 2));
        _rigidbody.AddRelativeTorque(Vector3.up * -TorqueInput * Time.deltaTime * (rowFactor * 2));
    }
}
