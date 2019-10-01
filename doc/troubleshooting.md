# Troubleshooting

## Data Generation
All data saved by the Unity Simulation SDK during a running simulation in the Editor is saved to a local directory and should be referenced to determine that the correct data is being generated, in the correct quantity, and in the correct form.

Attaching the following `DataCapturePath.cs` to any game object that is active when the scene begins will print the path where the Unity Simulation SDK will save all data for a particular run.
```csharp
using Unity.AI.DXCore;
using UnityEngine;

public class DataCapturePath : MonoBehaviour
{
    void Start()
    {
        // Print path where data will be saved by Unity Simulation SDK
        Debug.Log(Application.persistentDataPath + "/" + Configuration.Instance.GetAttemptId());
    }
}
```

## Linux Build
Please reference the [Local Testing](testing.md) guide for information on how to test a Linux build locally.

## SDK Integration

#### Using ML-Agents and Unity Simulation/Playtesting SDK together.

Both ML-Agents and Unity Simulation/Playtesting include the assembly ```system.interactive.async.dll```.
This will cause the build system to throw the following exception when building, and if both packages are present.
`PrecompiledAssemblyException: Multiple precompiled assemblies with the same name System.Interactive.Async.dll included for the current platform. Only one assembly with the same name is allowed per platform.`
This error is clearable and ignorable, but will occur each time you build.

This will be resolved soon™, but as a work around for now, you can delete one of the two copies of the assembly in either package after import.

#### Some cameras are rendering out grey images

Secondary cameras need to render into a render texture, and that RT may need a depth attachment.
The primary camera uses the display backbuffer with a depth attachment, but secondary cameras need to render into something.
Adding a render target will solve this issue. If your objects in the scene use shaders with depth testing enabled, then make sure that the RT has depth enabled.

## CLI commands

#### Too many projects when using `activate project`

The `activate project` command accepts an optional argument for the Unity project id. \
```bash
usim activate project xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx
```
This Unity Project ID value may be retrieved from:
* [Unity Developer Dashboard](https://developer.cloud.unity3d.com/projects/).
* Combine the `get projects` command with command-line tools such as `less` or redirecting the output to a file with `>`.
```bash
usim get projects > my_projects.txt
OR
usim get projects | less
```
