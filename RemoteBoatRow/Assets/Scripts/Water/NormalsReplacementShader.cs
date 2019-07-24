using UnityEngine;

public class NormalsReplacementShader : MonoBehaviour
{
    [SerializeField]
    public Shader NormalsShader;

    private RenderTexture _renderTexture;
    private Camera _camera;

    private void Start()
    {
        Camera thisCamera = GetComponent<Camera>();

        // Create a render texture matching the main camera's current dimensions.
        _renderTexture = new RenderTexture(thisCamera.pixelWidth, thisCamera.pixelHeight, 24);
        // Surface the render texture as a global variable, available to all shaders.
        Shader.SetGlobalTexture("_CameraNormalsTexture", _renderTexture);

        // Setup a copy of the camera to render the scene using the normals shader.
        GameObject copy = new GameObject("Normals camera");
        _camera = copy.AddComponent<Camera>();
        _camera.CopyFrom(thisCamera);
        _camera.transform.SetParent(transform);
        _camera.targetTexture = _renderTexture;
        _camera.SetReplacementShader(NormalsShader, "RenderType");
        _camera.depth = thisCamera.depth - 1;
    }
}
