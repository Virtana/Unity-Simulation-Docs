# Unity Simulation Local Testing Guide
The goal of this guide is to walk through the steps to test a Linux Unity build on a non Linux OS using a Docker container. Testing a simulation locally before submitting to Unity Simulation will help to surface any issues experienced during execution.

## Download Docker
Go to https://www.docker.com/ to sign up and install Docker Desktop.
Please reference the [getting started guide](https://docs.docker.com/get-started/) for general information about Docker.

Platform Specific Documentation:
- [Docker Desktop for Mac](https://docs.docker.com/docker-for-mac/)
- [Docker Desktop for Windows](https://docs.docker.com/docker-for-windows/)

Find the `Advanced` section in either of the platform specific guides for information on resource allocation for Docker. **NOTE:** You may need to increase the allocation memory or CPU if the Unity build you are trying to execute is sufficiently large.

## Testing Quickstart Materials Linux Build
This guide assumes that you are following the Quickstart Guide and have downloaded the [Quickstart Materials](quickstart.md#download-unity-simulation-quickstart-materials).


Open the Quickstart Materials directory and navigate to the `RollaballLinuxBuild` directory. This directory should contain a single zipped file name `rollaball_linux_build.zip`. Unzip this file and ensure that the directory structure resembles the following.
```
  |-RollaballLinuxBuild
  |  |-rollaball_linux_build
  |  |  |-rollaball_linux_build_Data
  |  |  |-rollaball_linux_build.x86_64
  |  |-rollaball_linux_build.zip
```


After unzipping the `rollaball_linux_build.zip` file, open the terminal and change your current directory to that of the Quickstart Materials to confirm contents.
```
$ cd /PATH/TO/QUICKSTART/MATERIALS
```
MacOS
```bash
$ls
```
Win
```
>dir
```
```
Outputs:
AppParams		Dockerfile		USimCLI			RollaballLinuxBuild		RunDefinitions		RunExecutionData
```


Next let's build the image defined in the `Dockerfile` file. NOTE: This command must be run from the Quickstart materials directory.
```
$docker build -t test-container .
...
...
...
...
...
Successfully built 856f2c231b06
Successfully tagged test-container:latest
```


If the image has been successfully built you can confirm by running the following command.

MacOS
```bash
$docker images | grep "test-container"

Outputs:
test-container      latest              856f2c231b06        2 minutes ago       1.1GB
```
Win
```
>docker images | findstr test-container

Outputs:
test-container      latest              856f2c231b06        2 minutes ago       1.1GB
```

Now that the image is built let's start it up and attach to the running container.
```
$docker run -it test-container /bin/bash
```

Once working in the container the terminal prompt should change to resemble something like `root@ff69abdf956e:/#`.

Let's first check that the executable and _Data directory was copied over correctly by running

MacOS
```bash
$ls /tmp/
```

Win
```
>dir /tmp/
```
```
Expected Output:
linux_build.x86_64  linux_build_Data
```

Now let's execute the build by running the follow command:

```
xvfb-run --auto-servernum --server-args="-screen 0 640x480x24:32" /tmp/linux_build.x86_64
```
`xvfb` is a command that is used to run executables on machines with no display hardware and in this case, our container.



If everything went well you should get the output below and it will take about thirty seconds before the process finishes.
```
Found path: /tmp/linux_build.x86_64
Mono path[0] = '/tmp/linux_build_Data/Managed'
Mono config path = '/tmp/linux_build_Data/MonoBleedingEdge/etc'
Preloaded 'ScreenSelector.so'
Preloaded 'libjpeg.so'
Preloaded 'libturbojpeg.so'
Display 0 'screen': 640x480 (primary device).
PlayerPrefs - Creating folder: /root/.config/unity3d/Unity Technologies
PlayerPrefs - Creating folder: /root/.config/unity3d/Unity Technologies/Roll-a-ball
Logging to /root/.config/unity3d/Unity Technologies/Roll-a-ball/Player.log
```


One important thing to note is the path on last line. This is the file where all logging by the executing sim is stored.

```
/root/.config/unity3d/Unity Technologies/Roll-a-ball/Player.log
```

Let's read the contents of that file by running:
```
cat /root/.config/unity3d/Unity\ Technologies/Roll-a-ball/Player.log
```

```
Desktop is 640 x 480 @ 0 Hz
Initialize engine version: 2018.3.8f1 (fc0fe30d6d91)
GfxDevice: creating device client; threaded=1
Renderer: Gallium 0.4 on llvmpipe (LLVM 3.9, 256 bits)
Vendor:   VMware, Inc.
Version:  3.3 (Core Profile) Mesa 13.0.6
GLES:     0
 GL_AMD_conservative_depth GL_AMD_draw_buffers_blend GL_AMD_seamless_cubemap_per_texture GL_AMD_shader_stencil_export GL_AMD_shader_trinary_minmax GL_AMD_vertex_shader_layer GL_AMD_vertex_shader_viewport_index GL_ANGLE_texture_compression_dxt3 GL_ANGLE_texture_compression_dxt5 GL_ARB_ES2_compatibility GL_ARB_ES3_compatibility GL_ARB_arrays_of_arrays GL_ARB_base_instance GL_ARB_blend_func_extended GL_ARB_buffer_storage GL_ARB_clear_buffer_object GL_ARB_clip_control GL_ARB_compressed_texture_pixel_storage GL_ARB_conditional_render_inverted GL_ARB_conservative_depth GL_ARB_copy_buffer GL_ARB_copy_image GL_ARB_cull_distance GL_ARB_debug_output GL_ARB_depth_buffer_float GL_ARB_depth_clamp GL_ARB_direct_state_access GL_ARB_draw_buffers GL_ARB_draw_buffers_blend GL_ARB_draw_elements_base_vertex GL_ARB_draw_indirect GL_ARB_draw_instanced GL_ARB_enhanced_layouts GL_ARB_explicit_attrib_location GL_ARB_explicit_uniform_location GL_ARB_fragment_coord_conventions GL_ARB_fragment_layer_viewport GL_ARB_fragment_shader GL_A
RB_framebuffer_object GL_ARB_framebuffer_sRGB GL_ARB_get_program_binary GL_ARB_get_texture_sub_image GL_ARB_gpu_shader_fp64 GL_ARB_half_float_pixel GL_ARB_half_float_vertex GL_ARB_instanced_arrays GL_ARB_internalformat_query GL_ARB_internalformat_query2 GL_ARB_invalidate_subdata GL_ARB_map_buffer_alignment GL_ARB_map_buffer_range GL_ARB_multi_bind GL_ARB_multi_draw_indirect GL_ARB_occlusion_query2 GL_ARB_pixel_buffer_object GL_ARB_point_sprite GL_ARB_program_interface_query GL_ARB_provoking_vertex GL_ARB_robustness GL_ARB_sampler_objects GL_ARB_seamless_cube_map GL_ARB_seamless_cubemap_per_texture GL_ARB_separate_shader_objects GL_ARB_shader_bit_encoding GL_ARB_shader_objects GL_ARB_shader_stencil_export GL_ARB_shader_subroutine GL_ARB_shader_texture_lod GL_ARB_shading_language_420pack GL_ARB_shading_language_packing GL_ARB_stencil_texturing GL_ARB_sync GL_ARB_texture_buffer_object GL_ARB_texture_buffer_object_rgb32 GL_ARB_texture_buffer_range GL_ARB_texture_compression_rgtc GL_ARB_texture_cube_map_array GL_
ARB_texture_float GL_ARB_texture_gather GL_ARB_texture_mirror_clamp_to_edge GL_ARB_texture_multisample GL_ARB_texture_non_power_of_two GL_ARB_texture_query_levels GL_ARB_texture_rectangle GL_ARB_texture_rg GL_ARB_texture_rgb10_a2ui GL_ARB_texture_stencil8 GL_ARB_texture_storage GL_ARB_texture_storage_multisample GL_ARB_texture_swizzle GL_ARB_texture_view GL_ARB_timer_query GL_ARB_transform_feedback2 GL_ARB_transform_feedback3 GL_ARB_transform_feedback_instanced GL_ARB_uniform_buffer_object GL_ARB_vertex_array_bgra GL_ARB_vertex_array_object GL_ARB_vertex_attrib_64bit GL_ARB_vertex_attrib_binding GL_ARB_vertex_shader GL_ARB_vertex_type_10f_11f_11f_rev GL_ARB_vertex_type_2_10_10_10_rev GL_ARB_viewport_array GL_ATI_blend_equation_separate GL_ATI_texture_float GL_ATI_texture_mirror_once GL_EXT_abgr GL_EXT_blend_equation_separate GL_EXT_draw_buffers2 GL_EXT_draw_instanced GL_EXT_framebuffer_blit GL_EXT_framebuffer_multisample GL_EXT_framebuffer_multisample_blit_scaled GL_EXT_framebuffer_sRGB GL_EXT_packed_depth_s
tencil GL_EXT_packed_float GL_EXT_pixel_buffer_object GL_EXT_polygon_offset_clamp GL_EXT_provoking_vertex GL_EXT_shader_integer_mix GL_EXT_texture_array GL_EXT_texture_compression_dxt1 GL_EXT_texture_compression_rgtc GL_EXT_texture_compression_s3tc GL_EXT_texture_integer GL_EXT_texture_mirror_clamp GL_EXT_texture_sRGB GL_EXT_texture_sRGB_decode GL_EXT_texture_shared_exponent GL_EXT_texture_snorm GL_EXT_texture_swizzle GL_EXT_timer_query GL_EXT_transform_feedback GL_EXT_vertex_array_bgra GL_IBM_multimode_draw_arrays GL_KHR_context_flush_control GL_KHR_debug GL_MESA_pack_invert GL_MESA_shader_integer_functions GL_MESA_texture_signed_rgba GL_MESA_ycbcr_texture GL_NV_conditional_render GL_NV_depth_clamp GL_NV_packed_depth_stencil GL_OES_EGL_image GL_S3_s3tc
OPENGL LOG: Creating OpenGL 3.3 graphics device ; Context level  <OpenGL 3.3> ; Context handle 40159856
FMOD failed to initialize any audio devices, running on emulated software output with no sound. Please check your audio drivers and/or hardware for malfunction.
Begin MonoManager ReloadAssembly
- Completed reload, in  0.117 seconds
Default vsync count 1
requesting resize 640 x 480
Using native desktop resolution 640 x 480
requesting fullscreen 640 x 480 at 0 Hz
Desktop is 640 x 480 @ 0 Hz
WARNING: RGB Compressed BC6H UFloat format is not supported, decompressing texture
WARNING: Shader Unsupported: 'Standard' - Pass 'META' has no vertex shader
WARNING: Shader Unsupported: 'Standard' - Pass 'META' has no vertex shader
WARNING: Shader Unsupported: 'Legacy Shaders/Self-Illumin/VertexLit' - Pass 'META' has no vertex shader
WARNING: Shader Unsupported: 'Legacy Shaders/Self-Illumin/Diffuse' - Pass 'Meta' has no vertex shader
WARNING: Shader Unsupported: 'Legacy Shaders/Self-Illumin/Specular' - Pass 'Meta' has no vertex shader
UnloadTime: 1.594000 ms
Count: 0

(Filename: ./Runtime/Export/Debug.bindings.h Line: 45)

Setting up 3 worker threads for Enlighten.
  Thread -> id: 7f0a82ffd700 -> priority: 1
  Thread -> id: 7f0a827fc700 -> priority: 1
  Thread -> id: 7f0a81ffb700 -> priority: 1
DC[V]: Manager file queued /root/.config/unity3d/Unity Technologies/Roll-a-ball/b2711fed-2219-4f03-b7ba-c73855628dd5/Screencapture/Camera0_0.jpg
DC[V]: Manager file queued /root/.config/unity3d/Unity Technologies/Roll-a-ball/b2711fed-2219-4f03-b7ba-c73855628dd5/Screencapture/Camera0_1.jpg
Count: 1

(Filename: ./Runtime/Export/Debug.bindings.h Line: 45)

DC[V]: Manager file queued /root/.config/unity3d/Unity Technologies/Roll-a-ball/b2711fed-2219-4f03-b7ba-c73855628dd5/Screencapture/Camera0_2.jpg
DC[V]: Manager file queued /root/.config/unity3d/Unity Technologies/Roll-a-ball/b2711fed-2219-4f03-b7ba-c73855628dd5/Screencapture/Camera0_3.jpg
DC[V]: Manager file queued /root/.config/unity3d/Unity Technologies/Roll-a-ball/b2711fed-2219-4f03-b7ba-c73855628dd5/Screencapture/Camera0_4.jpg
DC[V]: Manager file queued /root/.config/unity3d/Unity Technologies/Roll-a-ball/b2711fed-2219-4f03-b7ba-c73855628dd5/Screencapture/Camera0_5.jpg
DC[V]: Manager file queued /root/.config/unity3d/Unity Technologies/Roll-a-ball/b2711fed-2219-4f03-b7ba-c73855628dd5/Screencapture/Camera0_6.jpg
DC[V]: Manager file queued /root/.config/unity3d/Unity Technologies/Roll-a-ball/b2711fed-2219-4f03-b7ba-c73855628dd5/Screencapture/Camera0_7.jpg
DC[V]: Manager file queued /root/.config/unity3d/Unity Technologies/Roll-a-ball/b2711fed-2219-4f03-b7ba-c73855628dd5/Screencapture/Camera0_8.jpg
DC[V]: Manager file queued /root/.config/unity3d/Unity Technologies/Roll-a-ball/b2711fed-2219-4f03-b7ba-c73855628dd5/Screencapture/Camera0_9.jpg
DC[V]: Manager file queued /root/.config/unity3d/Unity Technologies/Roll-a-ball/b2711fed-2219-4f03-b7ba-c73855628dd5/Screencapture/Camera0_10.jpg
DC[V]: Manager file queued /root/.config/unity3d/Unity Technologies/Roll-a-ball/b2711fed-2219-4f03-b7ba-c73855628dd5/Screencapture/Camera0_11.jpg
DC[V]: Manager file queued /root/.config/unity3d/Unity Technologies/Roll-a-ball/b2711fed-2219-4f03-b7ba-c73855628dd5/Screencapture/Camera0_12.jpg
DC[V]: Manager file queued /root/.config/unity3d/Unity Technologies/Roll-a-ball/b2711fed-2219-4f03-b7ba-c73855628dd5/Screencapture/Camera0_13.jpg
DC[V]: Manager file queued /root/.config/unity3d/Unity Technologies/Roll-a-ball/b2711fed-2219-4f03-b7ba-c73855628dd5/Screencapture/Camera0_14.jpg
DC[V]: Manager file queued /root/.config/unity3d/Unity Technologies/Roll-a-ball/b2711fed-2219-4f03-b7ba-c73855628dd5/Screencapture/Camera0_15.jpg
DC[V]: Manager file queued /root/.config/unity3d/Unity Technologies/Roll-a-ball/b2711fed-2219-4f03-b7ba-c73855628dd5/Screencapture/Camera0_16.jpg
DC[V]: Manager file queued /root/.config/unity3d/Unity Technologies/Roll-a-ball/b2711fed-2219-4f03-b7ba-c73855628dd5/Screencapture/Camera0_17.jpg
DC[V]: Manager file queued /root/.config/unity3d/Unity Technologies/Roll-a-ball/b2711fed-2219-4f03-b7ba-c73855628dd5/Screencapture/Camera0_18.jpg
DC[V]: Manager file queued /root/.config/unity3d/Unity Technologies/Roll-a-ball/b2711fed-2219-4f03-b7ba-c73855628dd5/Screencapture/Camera0_19.jpg
DC[V]: Manager file queued /root/.config/unity3d/Unity Technologies/Roll-a-ball/b2711fed-2219-4f03-b7ba-c73855628dd5/Screencapture/Camera0_20.jpg
DC[V]: Manager file queued /root/.config/unity3d/Unity Technologies/Roll-a-ball/b2711fed-2219-4f03-b7ba-c73855628dd5/Screencapture/Camera0_21.jpg
DC[V]: Manager file queued /root/.config/unity3d/Unity Technologies/Roll-a-ball/b2711fed-2219-4f03-b7ba-c73855628dd5/Screencapture/Camera0_22.jpg
DC[V]: Manager file queued /root/.config/unity3d/Unity Technologies/Roll-a-ball/b2711fed-2219-4f03-b7ba-c73855628dd5/Screencapture/Camera0_23.jpg
DC[V]: Manager file queued /root/.config/unity3d/Unity Technologies/Roll-a-ball/b2711fed-2219-4f03-b7ba-c73855628dd5/Screencapture/Camera0_24.jpg
DC[V]: Manager file queued /root/.config/unity3d/Unity Technologies/Roll-a-ball/b2711fed-2219-4f03-b7ba-c73855628dd5/Screencapture/Camera0_25.jpg
DC[V]: Manager file queued /root/.config/unity3d/Unity Technologies/Roll-a-ball/b2711fed-2219-4f03-b7ba-c73855628dd5/Screencapture/Camera0_26.jpg
DC[V]: Manager file queued /root/.config/unity3d/Unity Technologies/Roll-a-ball/b2711fed-2219-4f03-b7ba-c73855628dd5/Screencapture/Camera0_27.jpg
DC[V]: Manager file queued /root/.config/unity3d/Unity Technologies/Roll-a-ball/b2711fed-2219-4f03-b7ba-c73855628dd5/Screencapture/Camera0_28.jpg
DC[V]: Manager file queued /root/.config/unity3d/Unity Technologies/Roll-a-ball/b2711fed-2219-4f03-b7ba-c73855628dd5/Screencapture/Camera0_29.jpg
DC[V]: Manager upload player log: /root/.config/unity3d/Unity Technologies/Roll-a-ball/Player.log
DC[V]: Manager file queued /root/.config/unity3d/Unity Technologies/Roll-a-ball/Player.log
DC[V]: Application quit
```

Most of the information in this file can be ignored however the `Manager file queued` lines will show where the screen captures taken by the Unity Simulation SDK are stored.

One thing to note is the `b2711fed-2219-4f03-b7ba-c73855628dd5` portion of the path is a randomly generated GUID that will change each time a sim is executed so be sure to check the Player.log to find the correct GUID for the latest execution.
```
/root/.config/unity3d/Unity\ Technologies/Roll-a-ball/b2711fed-2219-4f03-b7ba-c73855628dd5/Screencapture/
```

List all of the screen captures taken by the Unity Simulation SDK
```
ls /root/.config/unity3d/Unity\ Technologies/Roll-a-ball/b2711fed-2219-4f03-b7ba-c73855628dd5/Screencapture/

Outputs:

Camera0_0.jpg	Camera0_11.jpg	Camera0_14.jpg	Camera0_17.jpg	Camera0_2.jpg	Camera0_22.jpg	Camera0_25.jpg	Camera0_28.jpg	Camera0_4.jpg  Camera0_7.jpg
Camera0_1.jpg	Camera0_12.jpg	Camera0_15.jpg	Camera0_18.jpg	Camera0_20.jpg	Camera0_23.jpg	Camera0_26.jpg	Camera0_29.jpg	Camera0_5.jpg  Camera0_8.jpg
Camera0_10.jpg	Camera0_13.jpg	Camera0_16.jpg	Camera0_19.jpg	Camera0_21.jpg	Camera0_24.jpg	Camera0_27.jpg	Camera0_3.jpg	Camera0_6.jpg  Camera0_9.jpg
```

## Copying Files from Running Container to Local
Let's copy the generated screen captures from the running container to our local file system so we can view them easier.

First we need to get the running container's container id. In the terminal window running the container the `ff69abdf956e` portion of the prompt `root@ff69abdf956e:/#` is the container id although you can also get the container id by running the `docker ps` command in a **different** terminal window.

```
$docker ps

OUTPUTS:

CONTAINER ID        IMAGE               COMMAND             CREATED             STATUS              PORTS               NAMES
ff69abdf956e        test-container      "/bin/bash"         2 minutes ago       Up 2 minutes                            clever_mestorf
```

To Copy an entire directory from a running container to our local file system:
```
docker cp ff69abdf956e:"/root/.config/unity3d/Unity Technologies/Roll-a-ball/b2711fed-2219-4f03-b7ba-c73855628dd5/Screencapture/" /PATH/TO/SAVE/SCREEN_CAPTURES/
```

To copy a single file:

```
docker cp ff69abdf956e:/PATH/TO/FILE/FILENAME.txt FILENAME.txt
```

## Testing Your Own Linux Build

You can also test your own project's Linux builds by only making a few changes.

Use the Dockerfile below or copy the `Dockerfile` in the Quickstart Materials to the directory where your Linux build was saved and change the `COPY` lines at the bottom to point at your executable and _Data directory.
```
# What is the base image to pull
FROM python:3.7.2-stretch

# Update package lists
RUN apt-get update

# Install xvfb to execute the Unity build on the container
RUN apt-get install -y xvfb

# Copy the executable and _Data directory to /tmp
COPY [YOUR_BUILD].x86_64 /tmp/linux_build.x86_64
COPY [YOUR_BUILD]_Data /tmp/linux_build_Data
```

## Loading App Params

If you are loading your app-params using the `Application.dataPath + "/StreamingAssets/"` as mentioned in the [SDK Integration guide](integrate.md#integrating-app-params) they should load just as they do when pressing Play with your simulation scene loaded from within the Unity Editor.
