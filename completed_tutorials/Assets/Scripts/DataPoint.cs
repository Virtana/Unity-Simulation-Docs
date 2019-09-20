using UnityEngine;

[System.Serializable]
public class DataPoint : System.Object
{
    public string objectName;        // Name of displaying camera
    public float x;                  // X Rotation of game object
    public float y;                  // Y Rotation of game object
    public float z;                  // Z Rotation of game object
    public float time;              // Time of screen/data capture
    public string screenCaptureName; // Name of saved image to correlate data
    
    public DataPoint(string objectName, Quaternion rotation, float time, string screenCaptureName)
    {
        this.objectName = objectName;
        this.x = rotation.x;
        this.y = rotation.y;
        this.z = rotation.z;
        this.time = time;
        this.screenCaptureName = screenCaptureName;
    }
}