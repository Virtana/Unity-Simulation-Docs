using UnityEngine;
using Unity.AI.Simulation;
using UnityEngine.Experimental.Rendering;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class InstanceDataCapture : MonoBehaviour
{
    // Camera object in scene
	public Camera _camera;

	private int duration = 10;
	private int captureInterval = 1;
	private int lastCapture;
	private float simElapsed;
	private bool quit; // Editor use only, minimizes screen/data captures 
	private Unity.AI.Simulation.Logger dataLogger;
	private string screenCapturePath;

	private void Start()
	{
		Debug.Log(Application.persistentDataPath + "/" + Configuration.Instance.GetAttemptId());
		screenCapturePath = DXManager.Instance.GetDirectoryFor(DataCapturePaths.ScreenCapture);
		// Data logger defaults to the same run directory as ScreenCapture
		dataLogger = new Unity.AI.Simulation.Logger("DataCapture");
	}

	private void Capture(int num)
	{
		string imageName = _camera.name + "_" + num;

		Dictionary<string, int> labelInstances = new Dictionary<string, int>();

		// Call Screen Capture
		var screen = CaptureCamera.Capture(_camera, request =>
		{
			string path = screenCapturePath + "/" + imageName + ".jpg";

			// Convert the screen capture to a byte array
			Array image = CaptureImageEncoder.Encode(request.data.colorBuffer as Array, 640, 480, GraphicsFormat.R8G8B8A8_UNorm,
					CaptureImageEncoder.ImageFormat.Jpg, true);

            // Write the screen capture to a file
			var result = DXFile.Write(path, image);

            // Wait for Async screen capture request to return and then log data point
			if (result)
			{
				labelInstances.Add("Cube", 100);
				labelInstances.Add("Sphere", 111);
				labelInstances.Add("Cylinder", 131);
				string temp = JsonConvert.SerializeObject(labelInstances);
				InstanceCount instanceCount = new InstanceCount(imageName, temp);
				// Log data point to file
				dataLogger.Log(instanceCount);

				return AsyncRequest.Result.Completed;
			}

			return AsyncRequest.Result.Error;
		});
	}

	private void Update()
	{
        // Get total time sim elapsed
		simElapsed += Time.deltaTime;

		// Sim has hit duration, flush all data and quit application
		if (simElapsed >= duration && !quit)
		{
			dataLogger.Flushall();
			quit = true;
			Debug.Log("Quitting...");
			Unity.AI.Simulation.Logger successLogger = new Unity.AI.Simulation.Logger("_Success");
			Application.Quit();
        }

		// Capture Data if last time capture time was over 1 second ago
		if ((int)simElapsed - lastCapture >= captureInterval && !quit)
		{
            Capture(lastCapture);
			lastCapture = (int)simElapsed;
		}
	}
}