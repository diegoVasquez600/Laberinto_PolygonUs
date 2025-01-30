using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    void Start()
    {
        Camera miniMapCamera = GetComponent<Camera>();
        if (miniMapCamera != null)
        {
            miniMapCamera.rect = new Rect(0.50f, 0.50f, 0.25f, 0.25f);
        }
    }
}
