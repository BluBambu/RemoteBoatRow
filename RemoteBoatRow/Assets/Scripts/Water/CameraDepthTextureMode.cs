using UnityEngine;

public class CameraDepthTextureMode : MonoBehaviour 
{
    [SerializeField]
    public DepthTextureMode DepthTextureMode;

    private void OnValidate()
    {
        SetCameraDepthTextureMode();
    }

    private void Awake()
    {
        SetCameraDepthTextureMode();
    }

    private void SetCameraDepthTextureMode()
    {
        GetComponent<Camera>().depthTextureMode = DepthTextureMode;
    }
}
