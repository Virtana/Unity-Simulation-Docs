using Unity.AI.Simulation;
using UnityEngine;
using UnityEngine.Profiling;

public class TimeQuit : MonoBehaviour
{

    private int maxSimElapsed = 10;
    private float simElapsed;

    void Start()
    {
        var profilingAreas = new ProfilerArea[] { ProfilerArea.CPU, ProfilerArea.GPU, ProfilerArea.Physics };
        DXProfilerManager.EnableProfiling(profilingAreas);
    }    
    
    void Update()
    {
        if (simElapsed >= maxSimElapsed)
        {
            Debug.Log("Quitting");
            Application.Quit();
        }
        
        simElapsed += Time.deltaTime;
    }
}
