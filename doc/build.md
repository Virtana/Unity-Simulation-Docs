# Creating a Linux build

This guide will walk you through creating a Linux build of a Unity project and zipping it so that it may be submitted and executed on Unity Simulation Cloud.

## Requirements

For this guide we will be using the [RollABall](https://github.com/Unity-Technologies/Unity-Simulation-RollABall) project to walk through the necessary steps of creating a Linux build. You can download a zip of the project by clicking on the green `clone or download` button.

Skip to the [building](#building) section for instructions on creating a build for your own Unity project.

After unzipping the quickstart materials, open the `Unity-Simulation-RollABall-master` directory (aka. the Unity project) using Unity. In this example, we open the project using Unity Hub using Unity (version 2018.3.7f1) as shown below.

![opendlg](images/build-1.png "opendlg")

Once Unity loads the project, open the scene “Roll-a-Ball” as shown below

![openscn](images/build-2.png "opensn")

Click on the play button to play the sample tutorial to ensure that the sample compiles and runs locally on your machine, as shown below.

![playscn](images/build-3.png "playscn")

## Testing
If you are working on a non Linux OS and would like to ensure that your Linux build executes successfully please follow the [local testing guide](testing.md).

## Building

Build a Linux standalone version by navigating to the [Build Settings](https://docs.unity3d.com/Manual/BuildSettings.html) dialog.  Make sure that the scene “Roll-a-Ball”, or your target scene if this is a personal project, is selected in the “Scenes In Build”, target platform is “Linux” and Architecture is “x86_64”.

The player settings are setup correctly for this project, but please double check that the settings match those described in the prerequisites section of the [Unity Simulation SDK document](doc/integrate.md).

![savebld](images/build-4.png "savebld")

Click on “Build” and save the created output to a directory. For this example we are saving our build as “es_demo” to a directory named “final” as shown below.

![savedlg](images/build-5.png "savedlg")

## Zip the build

You can either zip the build using the CLI or using your own zip utility. We recommend using the Unity Simulation CLI as zipped builds require a specific directory structure to be executed on Unity Simulation.

### Zip using Unity Simulation CLI

Create a zipped build of final by navigating to the `final` directory where the Linux build was saved and run `$ usim zip build . --save-as=unity_simulation_rollaball_tutorial.zip`

Once the build has been successfully zipped you will be provided with the option to upload the build.
```console
Successfully zipped unity_simulation_rollaball_tutorial.zip
Would you like to upload the build now? (Y/n)
````
Enter `y` to upload the build to Unity Simulation.


### Zip using Zip program

Manually create a zipped build by navigating to the `final` directory and running
```console
$ zip -r unity_simulation_rollaball_tutorial.zip .
```

*Note*: It is important that when this file is unzipped the exported build files are located at the root. Example build files
* us_demo.x86_64
* us_demo_Data

That is, when zipped its structure should be:

```
|____ProjectBuildName.zip
| |____ProjectBuildName_Data (the data directory)
| |____ProjectBuildName.x86_64 (the executable)
```
