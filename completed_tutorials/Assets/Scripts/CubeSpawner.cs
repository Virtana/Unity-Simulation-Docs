using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Simulation;
using Random = UnityEngine.Random;

public class CubeSpawner : MonoBehaviour
{
    public GameObject CubePrefab;
    public GameObject SpherePrefab;
    
    public bool AppParamToggle;
    
    private Unity.AI.Simulation.Logger paramLogger;
    private Unity.AI.Simulation.Logger cubeLogger;
    private MountainParam appParams;
    private static float quitAfterSeconds;
    private static float simElapsedSeconds;
    private static int curNumCubes;

    private int red;
    private int green;
    private int blue;
    private string whichObjects;

    private Dictionary<string, GameObject> objDict;
    
    
    // Cube replication vars
    private static float cubeCadence = 0.5f;
    private float lastCubeCreated;

    /*
 *  Instantiates copies of Cube,
 *  moves them to a new location in the scene,
 *  and creates and logs dataPoint object.
 */
    public void ReplicateCube(int cubeNum)
    {
        GameObject newCube = GameObject.Instantiate(objDict[appParams.whichObjects]);
        newCube.transform.position = new Vector3(Random.Range(-0.0f, 1.0f),5.71f,-5.7f);
        newCube.name = newCube.name + "_" + cubeNum;
        newCube.GetComponent<Renderer>().material.color = new Color32((Byte)red,(Byte)green,(Byte)blue,255);
        

        //Create a new data point
        ObjectPosition cubeDataPoint = new ObjectPosition(newCube.transform.position, newCube.name);
        cubeLogger.Log(cubeDataPoint);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        objDict = new Dictionary<string, GameObject>();
        objDict.Add("cube", CubePrefab);
        objDict.Add("sphere", SpherePrefab);
        
        // Create a specific logger for AppParams for debugging purposes
        paramLogger = new Unity.AI.Simulation.Logger("ParamReader");
        cubeLogger = new Unity.AI.Simulation.Logger("CubeLogger");
        simElapsedSeconds = 0;

        // NOTE: AppParams can be loaded anytime except during `RuntimeInitializeLoadType.BeforeSceneLoad`
        // If the simulation is running locally load app_param_0.json
        if (!Configuration.Instance.IsSimulationRunningInCloud())
        {
            string app_param_name = AppParamToggle ? "mountain_app_param_1.json" : "mountain_app_param_0.json";
            Configuration.Instance.SimulationConfig.app_param_uri =
                "file://" + Application.dataPath + "/StreamingAssets/" + app_param_name;
            Debug.Log(Configuration.Instance.SimulationConfig.app_param_uri);
        }

        appParams = Configuration.Instance.GetAppParams<MountainParam>();

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
            GameObject.FindGameObjectsWithTag("DataCapture")[0].GetComponent<CameraGrab>()._screenCaptureInterval =
                screenCaptureInterval;

            // Set the Simulation exit time.
            quitAfterSeconds = appParams.quitAfterSeconds;
            red = appParams.red;
            green = appParams.green;
            blue = appParams.blue;
            whichObjects = appParams.whichObjects;
        }
        else
        {
            Debug.Log("NULL");
        }
    }

    // Exit sim after simulation has ran for quitAfterSeconds defined in AppParams.
    void Update()
    {
        simElapsedSeconds += Time.deltaTime;
        
        if (curNumCubes <= appParams.replicateCube && simElapsedSeconds - lastCubeCreated > cubeCadence)
        {
            ReplicateCube(curNumCubes);
            lastCubeCreated = simElapsedSeconds;
            curNumCubes += 1;
        }

        if (simElapsedSeconds >= quitAfterSeconds)
        {
            Debug.Log("Quitting...");
            // Flush all Cube data point data to file before exiting
            cubeLogger.Flushall();
            Application.Quit();
        }
    }
}
