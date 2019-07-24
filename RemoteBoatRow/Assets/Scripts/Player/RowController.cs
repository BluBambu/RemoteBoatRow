using UnityEngine;
using Mirror;

public class RowController : NetworkBehaviour
{
    private bool _wasLastAccXPositive;
    private float _lastPeakAccX;
    private float _lastTroughAccX;

    private BoatManager _boatManager;

    private bool _isLeftOar;

    private void Start()
    {
        _boatManager = GameObject.FindGameObjectWithTag("Boat").GetComponent<BoatManager>();
        _isLeftOar = _boatManager.isLeftOar();
    }

    private void Update()
    {
        if (isLocalPlayer)
        {
            #if ENABLE_WINMD_SUPPORT
                UpdateUsingBuiltinSensors();
            #else
                UpdateUsingWinrtSensors();
            #endif

            if (Input.GetKey(KeyCode.UpArrow))
            {
                CmdRowOar(1.0);
            }
        }
    }

    private void UpdateUsingBuiltinSensors()
    {
        var linearAcc = Input.acceleration;

        ConsoleProDebug.Watch("Last linear acceleration", linearAcc.ToString());

        if (_wasLastAccXPositive)
        {
            _lastTroughAccX = 0;
        }
        else
        {
            _lastPeakAccX = 0;
        }

        if (linearAcc.x > 0)
        {
            // User is doing upstroke, record the max up upswing
            if (linearAcc.x > _lastPeakAccX)
            {
                _lastPeakAccX = linearAcc.x;
            }

            _wasLastAccXPositive = true;
        }
        else if (linearAcc.x < 0)
        {
            if (linearAcc.x < _lastPeakAccX)
            {
                _lastTroughAccX = linearAcc.x;
            }

            if (_wasLastAccXPositive)
            {
                var delta = _lastPeakAccX - _lastTroughAccX;

                if (delta > .1)
                {
                    double rowFactor = delta * 2;

                    Debug.Log("New accelerometer delta: " + delta);
                    Debug.Log("Rowing by a factor of " + rowFactor);

                    CmdRowOar(rowFactor);
                }
            }
            _wasLastAccXPositive = false;
        }
    }

    private void UpdateUsingWinrtSensors()
    {
        #if ENABLE_WINMD_SUPPORT
        #endif
    }

    [Command]
    public void CmdRowOar(double rowFactor)
    {
        Debug.Log("Moving oar: " + _isLeftOar + " IsLeft, " + rowFactor + " rowFactor");

        if (_isLeftOar)
        {
            _boatManager.RowLeftOar((float)rowFactor);
        }
        else
        {
            _boatManager.RowRightOar((float)rowFactor);
        }
    }
}
