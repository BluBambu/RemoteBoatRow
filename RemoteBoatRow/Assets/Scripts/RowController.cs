using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RowController : NetworkBehaviour
{
    private bool wasLastAccXPositive;
    private bool wasLastAccZPositive;
    private float lastPeakAccX;
    private float lastTroughAccX;
    private float lastPeakAccZ;
    private float lastTroughZ;

    private BoatManager boatManager;

    private bool isLeftOar;

    private void Start()
    {
        boatManager = GameObject.FindGameObjectWithTag("Boat").GetComponent<BoatManager>();
        isLeftOar = boatManager.isLeftOar();
    }

    private void Update()
    {
        if (isLocalPlayer)
        {
            var linearAcc = Input.acceleration;

            ConsoleProDebug.Watch("Last linear acceleration", linearAcc.ToString());

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
                        var rowFactor = delta * 2;

                        Debug.Log("New accelerometer delta: " + delta);
                        Debug.Log("Rowing by a factor of " + rowFactor);

                        CmdRowOar(rowFactor);
                    }
                }
                wasLastAccXPositive = false;
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                CmdRowOar(1);
            }
        }
    }

    [Command]
    public void CmdRowOar(double rowFactor)
    {
        Debug.Log("Moving oar: " + isLeftOar + " IsLeft, " + rowFactor + " rowFactor");

        if (isLeftOar)
        {
            boatManager.RowLeftOar((float)rowFactor);
        }
        else
        {
            boatManager.RowRightOar((float)rowFactor);
        }
    }
}
