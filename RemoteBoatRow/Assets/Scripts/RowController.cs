using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowController : MonoBehaviour
{
    private const float TorqueInput = 80;
    private const float ForceInput = 80;

    private bool wasLastAccXPositive;
    private bool wasLastAccZPositive;
    private float lastPeakAccX;
    private float lastTroughAccX;
    private float lastPeakAccZ;
    private float lastTroughZ;

    private Rigidbody rigidbody;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        var linearAcc = Input.acceleration;

        ConsoleProDebug.Watch("Last linear acceleration", linearAcc.ToString());

        if (wasLastAccXPositive)
        {
            if (linearAcc.x > 0)
            {
                // User is doing upstroke, record the max up upswing
                if (linearAcc.x > lastPeakAccX)
                {
                    lastPeakAccX = linearAcc.x;
                }
            }
            else if (linearAcc.x < 0)
            {
                if (linearAcc.x < lastPeakAccX)
                {

                }
            }
        }
        else
        {

        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rigidbody.AddRelativeForce(0, 0, ForceInput * Time.deltaTime);
            rigidbody.AddRelativeTorque(Vector3.up * TorqueInput * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rigidbody.AddRelativeForce(0, 0, ForceInput * Time.deltaTime);
            rigidbody.AddRelativeTorque(Vector3.up * -TorqueInput * Time.deltaTime);
        }
    }
}
