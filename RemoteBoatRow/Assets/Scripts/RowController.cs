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
        // var gyro = Input.gyro;
        // gyro.enabled = true;

        // ConsoleProDebug.Watch("Gyro", gyro.rotationRate.ToString());

        // if (Mathf.Abs(gyro.rotationRate.x) + Mathf.Abs(gyro.rotationRate.y) > .5)
        // {
        //     Debug.Log(Mathf.Abs(gyro.rotationRate.x) + Mathf.Abs(gyro.rotationRate.y));
        // }
        

        var linearAcc = Input.acceleration;

        ConsoleProDebug.Watch("Last linear acceleration", linearAcc.ToString());

        // if (wasLastAccXPositive)
        {
            if (wasLastAccXPositive)
            {
                lastTroughAccX = 0;
            }
            else
            {
                lastPeakAccX = 0;
            }

            if (linearAcc.x > 0)
            {
                // User is doing upstroke, record the max up upswing
                if (linearAcc.x > lastPeakAccX)
                {
                    lastPeakAccX = linearAcc.x;
                }

                wasLastAccXPositive = true;
            }
            else if (linearAcc.x < 0)
            {
                if (linearAcc.x < lastPeakAccX)
                {
                    lastTroughAccX = linearAcc.x;
                }

                if (wasLastAccXPositive)
                {
                    var delta = lastPeakAccX - lastTroughAccX;

                    if (delta > .1)
                    {
                        Debug.Log("New accelerometer delta: " + delta);

                        rigidbody.AddRelativeForce(0, 0, ForceInput * Time.deltaTime * (delta * 2));
                        rigidbody.AddRelativeTorque(Vector3.up * TorqueInput * Time.deltaTime * (delta * 2));
                    }
                }

                wasLastAccXPositive = false;
            }
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
