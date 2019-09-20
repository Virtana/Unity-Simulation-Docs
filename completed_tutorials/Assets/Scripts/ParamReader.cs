/*
 * Get AppParams values from AppParam file passed during execution from command line.
 * https://docs.google.com/document/d/1B-2hopfujGalnwmvImK-8cNsjGEwNQp7JNwMMs44kqY/edit#heading=h.y4ljk2hpw5fz
 *
 * Update corresponding variables in scene.
 */
using UnityEngine;
using Unity.AI.Simulation;

public class ParamReader : MonoBehaviour
{
    private Unity.AI.Simulation.Logger paramLogger;
    private Unity.AI.Simulation.Logger cubeLogger;
    private CubeAppParam appParams;
    private static float quitAfterSeconds;
    private static float simElapsedSeconds;


    /*
     *  Instantiates copies of Cube,
     *  moves them to a new location in the scene,
     *  and creates and logs dataPoint object.
     */
    public void ReplicateCube(GameObject cube, int replicateNum)
    {
        for (int i = 0; i < replicateNum; i++)
        {
            Vector3 cubePosition = new Vector3((i + 1) * 2.0F, 0, 0);
            GameObject newCube = Instantiate(cube, cubePosition, Quaternion.identity);
            newCube.name = newCube.name + "_" + i;

            //Create a new data point
            ObjectPosition cubeDataPoint = new ObjectPosition(cubePosition, newCube.name);
            cubeLogger.Log(cubeDataPoint);
        }
    }

    void Start()
    {
        // Create a specific logger for AppParams for debugging purposes
        paramLogger = new Unity.AI.Simulation.Logger("ParamReader");
        cubeLogger = new Unity.AI.Simulation.Logger("CubeLogger");
        simElapsedSeconds = 0;

        // NOTE: AppParams can be loaded anytime except during `RuntimeInitializeLoadType.BeforeSceneLoad`
        // If the simulation is running locally load app_param_0.json
        if (!Configuration.Instance.IsSimulationRunningInCloud())
        {
            Configuration.Instance.SimulationConfig.app_param_uri = "file://" + Application.dataPath + "/StreamingAssets/app_param_0.json";
            Debug.Log(Configuration.Instance.SimulationConfig.app_param_uri);
        }

        appParams = Configuration.Instance.GetAppParams<CubeAppParam>();

        // Check if AppParam file was passed during command line execution
        if (appParams != null)
        {
            // Log AppParams to Player.Log file and Editor Console
            Debug.Log(appParams.ToString());
            
            // Log AppParams to DataLogger
            paramLogger.Log(appParams);
            paramLogger.Flushall();

            // Update the screen capture interval through an app-param
            float screenCaptureInterval = Mathf.Min(Mathf.Max(0, appParams.screenCaptureInterval), 100.0f);
            GameObject.FindGameObjectsWithTag("DataCapture")[0].GetComponent<CameraGrab>()._screenCaptureInterval = screenCaptureInterval;
            // ReplicateCube
            ReplicateCube(GameObject.FindGameObjectsWithTag("Cube")[0], appParams.replicateCube);

            // Set the Simulation exit time.
            quitAfterSeconds = appParams.quitAfterSeconds;
        }
    }

    // Exit sim after simulation has ran for quitAfterSeconds defined in AppParams.
    private void Update()
    {
        simElapsedSeconds += Time.deltaTime;

        if (simElapsedSeconds >= quitAfterSeconds)
        {
            // Flush all Cube data point data to file before exiting
            cubeLogger.Flushall();
            Application.Quit();
        }

    }
}