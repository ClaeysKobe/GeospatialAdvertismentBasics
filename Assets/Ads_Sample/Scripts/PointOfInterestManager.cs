using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System;
using UnityEngine.UI;

public class PointOfInterestManager : MonoBehaviour
{
    [SerializeField]
    private float _detectionRange = 5f;

    [SerializeField]
    private Image _icon;

    [SerializeField]
    private TextMeshProUGUI _title;

    [SerializeField]
    private TextMeshProUGUI _openingHours;

    [SerializeField]
    private GameObject _infoPanel;

    [SerializeField]
    private List<Sprite> _venueSprites;

    private List<GameObject> _poiList;

    void Start()
    {
        // Initialize the list of POIs
        _poiList = new List<GameObject>();
    }

    void Update()
    {
        // Update the list of POIs within the detection range
        UpdatePOIList();

        // Find the closest POI in terms of camera angle
        GameObject closestPOI = FindClosestPOI();

        // Display information on UI panel for the closest POI
        DisplayPOIInformation(closestPOI);
    }

    void UpdatePOIList()
    {
        // Clear the existing list
        _poiList.Clear();

        // Find all GameObjects with the "POI" tag in the specified range
        Collider[] colliders = Physics.OverlapCapsule(transform.position, transform.position, _detectionRange);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("POI"))
            {
                _poiList.Add(collider.gameObject);
            }
        }
    }

    GameObject FindClosestPOI()
    {
        GameObject closestPOI = null;
        float minAngle = float.MaxValue;

        foreach (GameObject poi in _poiList)
        {
            // Get the direction from the camera to the POI
            Vector3 directionToPOI = poi.transform.position - Camera.main.transform.position;

            // Calculate the angle between the camera's forward vector and the direction to the POI
            float angle = Vector3.Angle(Camera.main.transform.forward, directionToPOI);

            // Check if this POI has a smaller angle
            if (angle < minAngle)
            {
                minAngle = angle;
                closestPOI = poi;
            }
        }

        return closestPOI;
    }

    void DisplayPOIInformation(GameObject poi)
    {
        if (poi != null)
        {
            // Enable InfoPanel
            _infoPanel.SetActive(true);

            // Get the POIMetaData script attached to the parent of the closest POI
            POIMetaData poiMetaData = poi.transform.parent.GetComponent<POIMetaData>();

            // Update the UI elements with information
            if (_icon != null && _title != null && _openingHours != null)
            {
                // Display the icon (You need to replace this with your logic to get the correct icon)
                string VenueType = poiMetaData._venueType.ToString();
                _icon.sprite = GetIconForVenueType(VenueType);

                // Display the name
                _title.text = poiMetaData._venueName;

                // Display the opening/closing time
                DateTime openingTime = DateTime.ParseExact(poiMetaData._openingTime, "HH:mm", null);
                DateTime closingTime = DateTime.ParseExact(poiMetaData._closingTime, "HH:mm", null);

                // Display the status
                bool opened = IsOpen(poiMetaData._openingTime, poiMetaData._closingTime);
                if (opened)
                {
                    _openingHours.text = "Open now - closes: " + closingTime.ToString("hh:mm");
                }
                else
                {
                    _openingHours.text = "Closed - opens: " + openingTime.ToString("hh:mm");
                }
            }
        }
        else
        {
            // Disable InfoPanel
            _infoPanel.SetActive(false);
        }
    }

    bool IsOpen(string openingTime, string closingTime)
    {
        DateTime currentTime = DateTime.Now;

        DateTime openTime = DateTime.ParseExact(openingTime, "HH:mm", null);
        DateTime closeTime = DateTime.ParseExact(closingTime, "HH:mm", null);

        return currentTime >= openTime && currentTime <= closeTime;
    }

    Sprite GetIconForVenueType(string venueType)
    {
        // Check if the venueSprites list is not null
        if (_venueSprites != null)
        {
            // Find the sprite by name in the venueSprites list
            Sprite sprite = _venueSprites.Find(x => x.name == venueType);

            // Return the sprite if found, or null if not found
            return sprite;
        }
        else
        {
            // Return null if the venueSprites list is not initialized
            return null;
        }
    }
}
