### Unity Simulation SDK Data Capture Image Formats

Unity Simulation SDK Data Capture supports three image capture formats:
 - TGA
 - JPG
 - RAW
 
 
Note: The RAW format is a dump of the raw binary data so the files will not contain any header information.

Here is an example of how to convert a RAW image to a PNG format written in Python using Numpy and PIL.
```python
import numpy as np
from PIL import Image

height = 480 # height  of screen capture form Unity Simulation SDK. Default is 480
width = 680 # width of screen capture from Unity Simulation SDK. Default is 680
depth = 4 # RGBA

img_path = "/path/to/image/image_0.raw"

# Load image as numpy array
img = np.fromfile(img_path, dtype=np.uint8)

# Reshape the array to match the image's dimensions
img = img.reshape((height, width, depth))

# Drop the alpha channel from the image
output = img[:height, :width, :depth-1]

# Convert to image from array and save
img = Image.fromarray(output, 'RGB')
img.save('/path/to/image/converted.png')
```

