using UnityEngine;

public class CubeSpinner : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(0, 50 * Time.deltaTime, 50 * Time.deltaTime);
    }
}