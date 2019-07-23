using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BoatManager : NetworkBehaviour
{
    private const float TorqueInput = 80;
    private const float ForceInput = 80;

    private Rigidbody rigidbody;

    private bool isLeftOarTaken = false;

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
            rigidbody = GetComponent<Rigidbody>();
        }
    }

    public bool isLeftOar()
    {
        var returnVal = !isLeftOarTaken;

        isLeftOarTaken = !isLeftOarTaken;

        return returnVal;
    }

    public void RowLeftOar(float rowFactor)
    {
        rigidbody.AddRelativeForce(0, 0, ForceInput * Time.deltaTime * (rowFactor * 2));
        rigidbody.AddRelativeTorque(Vector3.up * TorqueInput * Time.deltaTime * (rowFactor * 2));
    }

    public void RowRightOar(float rowFactor)
    {
        rigidbody.AddRelativeForce(0, 0, ForceInput * Time.deltaTime * (rowFactor * 2));
        rigidbody.AddRelativeTorque(Vector3.up * -TorqueInput * Time.deltaTime * (rowFactor * 2));
    }
}
