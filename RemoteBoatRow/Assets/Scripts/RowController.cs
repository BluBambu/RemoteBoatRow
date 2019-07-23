using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowController : MonoBehaviour
{
    private bool wasLastAccXPositive;
    private bool wasLastAccZPositive;
    private float lastPeakAccX;
    private float lastTroughAccX;
    private float lastPeakAccZ;
    private float lastTroughZ;

    void Start()
    {
        
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
    }
}
