[System.Serializable]
public class InstanceCount : System.Object
{
    public string screenCaptureName; // Name of saved image to correlate data
    public string labelInstances;
    
    public InstanceCount(string screenCaptureName, string labelInstances)
    {
        this.screenCaptureName = screenCaptureName;
        this.labelInstances = labelInstances;
    }
}