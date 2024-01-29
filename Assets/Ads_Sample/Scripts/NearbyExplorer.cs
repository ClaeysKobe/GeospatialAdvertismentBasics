using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Collections;
using Google.XR.ARCoreExtensions.GeospatialCreator;
using UnityEngine.XR.ARFoundation;
using Google.XR.ARCoreExtensions;
using TMPro;

public class NearbyExplorer : MonoBehaviour
{
    
    [SerializeField]
    private string _apiKey = "YOUR_API_KEY"; 

    [SerializeField]
    private GameObject _nearbyPanel;

    [SerializeField]
    private TextMeshProUGUI _textPrefab;

    private bool _enabled = false;

    private bool _locationCoroutineRunning = false;

    public void ShowNearbyPanel()
    {
        _enabled = !_enabled;
        if (_enabled)
        {
            _nearbyPanel.SetActive(true);
            StartSearchCoroutine();
        }
        else
        {
            _nearbyPanel.SetActive(false);
        }
    }

    void StartSearchCoroutine() 
    {
        if (!_locationCoroutineRunning)
        {
            _locationCoroutineRunning = true;

            StartCoroutine(GetDeviceLocationAndSearch());
        }
    }

    void FillUI(List<Place> places)
    {
        Debug.Log("Number of POIs: " + places.Count);

        if (places.Count == 0)
        {
            Debug.Log("No POIs found");
            return;
        }

        string text = "\n";

        foreach (Place place in places)
        {
            // Set Variables
            string name = place.displayName.text;
            string address = place.formattedAddress;
            // string website = place.websiteUri;

            // Create Text
            // text += name + "\n-" + address + "\n-" + website + "\n\n";
            text += name + "\n-" + address + "\n\n";
        }

        text += "\n\n";

        Debug.Log("_textPrefab: " + _textPrefab); // Add this line
        _textPrefab.text = text;

    }

    IEnumerator GetDeviceLocationAndSearch()
    {
        _locationCoroutineRunning = true;

        // Start location service updates
        Input.location.Start();

        // Wait until the location service initializes
        int maxWaitTime = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWaitTime > 0)
        {
            yield return new WaitForSeconds(1);
            maxWaitTime--;
        }

        // Check if the location service has timed out or failed to initialize
        if (maxWaitTime <= 0 || Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.LogError("Unable to determine device location");
            _locationCoroutineRunning = false;
            yield break;
        }

        // Create a Google Places API request
        GooglePlacesRequest request = new GooglePlacesRequest
        {
            maxResultCount = 20,
            locationRestriction = new LocationRestriction
            {
                circle = new Circle
                {
                    center = new Center
                    {
                        latitude = Input.location.lastData.latitude,
                        longitude = Input.location.lastData.longitude

                        // Hardcode location for testing
                        // latitude = 50.82432f,
                        // longitude = 3.2495f
                    },
                    radius = 50.0f
                }
            }
        };

        // Convert the request object to JSON
        string jsonRequest = JsonUtility.ToJson(request);

        // Create a UnityWebRequest
        UnityWebRequest webRequest = new UnityWebRequest("https://places.googleapis.com/v1/places:searchNearby", "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonRequest);
        webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");
        webRequest.SetRequestHeader("X-Goog-Api-Key", _apiKey);
        webRequest.SetRequestHeader("X-Goog-FieldMask", "places.displayName,places.formattedAddress,places.websiteUri");

        // Send the request
        yield return webRequest.SendWebRequest();

        // Check for errors
        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            string jsonResponse = webRequest.downloadHandler.text;
            Debug.Log(jsonResponse);

            // Deserialize the JSON response into a list of POI objects
            Root data = JsonUtility.FromJson<Root>(jsonResponse);

            Debug.Log("Response: " + data);
            Debug.Log("Response: " + data.places);
            Debug.Log("Number of POIs: " + data.places.Count);
            Debug.Log("First POI: " + data.places[0].displayName.text);

            // Now that we have the POIs, spawn them
            FillUI(data.places);
        }
        else
        {
            // Request failed
            Debug.LogError("Error: " + webRequest.error);

            _textPrefab.text = "Oops, nothing around here...";
        }

        // Stop location service updates
        Input.location.Stop();

        _locationCoroutineRunning = false;

        yield break;
    }
}


// Classes 

[System.Serializable]
public class LocationRestriction
{
    public Circle circle;
}

[System.Serializable]
public class Circle
{
    public Center center;
    public float radius;
}

[System.Serializable]
public class Center
{
    public double latitude;
    public double longitude;
}

[System.Serializable]
public class GooglePlacesRequest
{
    public int maxResultCount;
    public LocationRestriction locationRestriction;
}

[System.Serializable]
public class DisplayName
{
    public string text;
    public string languageCode;

    public DisplayName(string text, string languageCode)
    {
        this.text = text;
        this.languageCode = languageCode;
    }
}

[System.Serializable]
public class Place
{
    public string formattedAddress;
    public string websiteUri;

    public DisplayName displayName;

    public Place(string formattedAddress, string websiteUri, DisplayName displayName)
    {
        this.formattedAddress = formattedAddress;
        this.websiteUri = websiteUri;
        this.displayName = displayName;
    }
}

[System.Serializable]
public class Root
{
    public List<Place> places;

    public Root(List<Place> places)
    {
        this.places = places;
    }
}