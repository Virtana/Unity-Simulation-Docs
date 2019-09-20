using UnityEngine;

[System.Serializable]
public class ObjectPosition : System.Object
{
    public ObjectPosition(Vector3 pos, string objectName)
    {
        this.pos = pos;
        this.objectName = objectName;
    }

    public Vector3 pos;
    public string objectName;
}