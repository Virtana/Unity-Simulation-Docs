# Unity Simulation How To's

## Change Screen Capture Resolution
By default Unity Simulation's resolution of screen captures is 640x480. The resolution of screen captures can be altered by using a Render Texture and following the steps below.

First create a Render Texture by going to, `Assets` -> `Create` -> `Render Texture`
. The desired resolution is then set in the Inspector Window under `Size`.

![render_texture](images/render_texture.png "render_texture")

And finally for each camera in your scene drag the render texture onto `Target Texture` while also making sure that `HDR` is set to `Off`.

![render_texture_cam](images/render_texture_cam.png "render_texture_cam")

You can find the official Unity documentation on Render Textures [here](https://docs.unity3d.com/Manual/class-RenderTexture.html).


## Retrieve Authentication token for Direct API calls

The recommended method of retrieving your bearer token is by using the `usim` Command Line Interface tool from the [latest](https://github.com/Unity-Technologies/Unity-Simulation-Docs/releases) release of the Unity Simulation bundle.

Use the [usim login auth](cli.md#usim-login-auth) command to authenticate with your Unity ID. After successfully authenticating you can find your token by executing the `inspect auth` command. This command will read from the usim CLI configuration files in your Home directory and print your token information.

example command:
```
usim inspect auth
```
| output field | description |
|--------------|-------------|
| access token: | set your `Authorization` http header to this value |
| expires in: | expected expiration in number of Days, HH:MM:SS |
| expired: | True/False whether the access token has expired |
| refresh token: | single-use token to refresh the access token |
| updated: | last time this token was updated |

This acccess token has a relatively short expiration and may need to be periodically refreshed by using the [usim login refresh](cli.md#usim-login-refresh) command.

example curl command:

```bash
curl <your_url_and_flags> -H 'Authorization: Bearer <your_access_token>'
```
