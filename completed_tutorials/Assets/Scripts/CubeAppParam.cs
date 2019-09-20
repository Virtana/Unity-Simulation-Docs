[System.Serializable]
public class CubeAppParam
{
    public int replicateCube;
    public int quitAfterSeconds;
    public float screenCaptureInterval;

    public override string ToString()
    {
        return "CubeAppParam: " +
               "\nreplicateCube: " + replicateCube + 
               " \nquitAfterSeconds: " + quitAfterSeconds + 
               "\nscreenCaptureInterval: " + screenCaptureInterval;
    }
}