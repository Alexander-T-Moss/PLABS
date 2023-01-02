// Rotates the skybox backround
// Attaches to Camera GameObject that has the skybox attached

using UnityEngine;

public class BackgroundRotator : MonoBehaviour
{
    // Variables
    public float SkyboxRotationSpeed = 5.0f;

    void Update ()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * SkyboxRotationSpeed);
    }
}