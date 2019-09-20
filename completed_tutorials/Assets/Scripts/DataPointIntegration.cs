using UnityEngine;

[System.Serializable]
public class DataPointIntegration : System.Object
{
    public DataPointIntegration(Vector3 pos, string objectName)
    {
        this.pos = pos;
        this.objectName = objectName;
    }

    public Vector3 pos;
    public string objectName;
}