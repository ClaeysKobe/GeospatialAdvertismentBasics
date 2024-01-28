# GeospatialAdvertismentBasics
> **This project was tested on iOS, so a device running a recent MacOS version and an iPhone are recommended. Android built support has been added, but not tested.**

This project tries to combine AR and advertisment. However, this project can also be used as basis for a Geosptial AR app. There is a sample scene you can use, or start immediately with a AR Ready Main scene. Please read the requirements before getting started.

## Requirements
1. Google Cloud Services
   - You can use the same project for both APIs, so you only need 1 API key
   - Google Maps Tiles API
     - **IMPORTANT: YOU WILL NEED TO ENTER CREDIT CARD CREDENTIALS TO USE THIS API ON YOUR ACCOUNT**
     - Go to [Google Cloud Console](https://console.cloud.google.com)
     - Create a new project or select an existing one
     - Navigate to the "APIs & Services" Page
     - Click on the top of the screen on the "ENABLE APIS AND SERVICES" button and search for "Map Tiles API"
     - Enable the API (you might be asked to fill in your credentials here)
     - Get your API key from the credentials page
   -  Google ARCore API
     - Go to [Google Cloud Console](https://console.cloud.google.com)
     - Create a new project or select an existing one
     - Navigate to the "APIs & Services" Page
     - Click on the top of the screen on the "ENABLE APIS AND SERVICES" button and search for "ARCore API"
     - Enable the API
     - Get your API key from the credentials page
2. Unity Version
   - Install Unity Hub and Unity version 2022.3.10f1 with the iOS Build Support package
   - To install 2022.3.10f1, head over to the [Unity Archive](https://unity.com/releases/editor/archive)
   - Make sure to add iOS / Android Build Support when installing the correct version. If not, this can still be configured later

## Getting Started
1. Clone the GitHub repository on your pc via your preferred tool (e.g. GitHub Desktop)
2. Open Unity Hub and navigate to the "projects" page
3. Click on "Add"
4. Browse to the location where you cloned this repo and select the folder
5. Open the project

## Configuring your API Keys
1. ARCore API Key
  - Go to Edit > Project Settings, located at the top right of your Unity Editor
  - Navigate to XR Plugin Management > ARCore Extensions
  - Paste the API Key for the Google ARCore API in both the Android and iOS API Key Field
2. Google Maps Tiles API
  - This key will need to be placed in every AR Geospatial Creator Origin, located in every scene (only 1 per scene)

## Trying out the sample scene
> This scene was build at Howest Kortrijk, Belgium. If you are not near this location to test, I recommend changing the coordinates (explained later) to test somewhere near you
1. In the assets folder (usually the bottom part of your Unity Editor), navigate to the "Ads_Sample" folder
2. Double click on SampleScene
3. Click on "AR Geospatial Creator Origin" on the left of your screen
4. Enter your Google Maps Tiles API key in the "Google Maps Tiles API key" field in the Inspector (Usually located at the right of your Unity Editor)
5. (optional) Move the project to a different location:
   - Go to a Google Maps, Earth, Apple Maps... and search for the desired location
   - Get the Latitude and Longitude from this location and paste them in the correct field of the "AR Geospatial Creator Origin" and the "AR Geospatial Creator Anchor"
   - Change the height so it is around the same height of the Unity Grid for convenience
![Image for clarification](https://i.imgur.com/gkX39AA.png)

## Testing the sample project on your iOS device
1. Build the project
   - Go to "File" > "Build Settings", or press Ctrl + Shift + B
   - Select iOS and click on "Switch Platform"
   - Click on "Add Open Scenes". You should see "Scenes/SampleScene" appear
   - Click on "Build" and choose a location to build the project, **I recommend too not build directly in your Unity folder! If you wish to build there, make a separate folder in your Unity Project**
   - Wait until the project is Build. Once done, open the _your_chosen_name_.xcworkspace file
     - If you do not see a .xcworkspace file, but only a .xcodeproj file, please follow this first:
       - copy the file path to your build
       - Open "Terminal"
       - type: cd _your copied filed path from previous step_
       - press enter
       - type: pod init
       - press enter
       - type: pod install
       - press enter
       - done
    - Go to File > Add Packages... (in the top right bar)
    - Search "arcore-ios-sdk"
    - Clik "Add Package"
    - Check ARCoreGeospatial and set Target to Unity-iPhone
    - Go to Unity-iPhone
    - Select Build Settings
    - Search for Bitcode
    - Set "Enable Bitcode" to No
    - Plug in your iPhone
    - Press the "play" button on the top left of the xcode window
    - Wait until the project is build, it will open automatically
    - Enjoy!
  
## Testing the project on Android
- Go to "File" > "Build Settings", or press Ctrl + Shift + B
- Select Android and click on "Switch Platform"
- Click on "Add Open Scenes". You should see "Scenes/SampleScene" appear
- Click on "Build" and choose the phone you want to deploy to
- Wait until the project is build on your phone
- Enjoy!

## Making your own scene
> You can use the "Main" scene as a basis. The "AR Geospatial Creator Anchor" (also a prefab) can be used to place objects in the real world

## Credits
Special thanks Dilmer Valecillos' Youtube Channel for the awesome Geospatial tutorial! Check it out [here](https://www.youtube.com/watch?v=v2yQBDdw7jU)
> Note: You can also use this video to enable Android Build Support, or create your own project.

## License
[![License](https://img.shields.io/badge/License-Apache_2.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)
