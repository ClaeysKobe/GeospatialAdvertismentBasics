using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class POIMetaData : MonoBehaviour
{
    [SerializeField]
    public string _venueName;
    
    [SerializeField]
    public string _venueDescription;

    [SerializeField]
    public string _openingTime = "hh:mm";

    [SerializeField]
    public string _closingTime = "hh:mm";

    [SerializeField]
    public VenueType _venueType;

    [SerializeField]
    private Image _icon;

    [SerializeField]
    private TextMeshProUGUI _title;

    [SerializeField]
    private TextMeshProUGUI _openingHours;

    [SerializeField]
    private GameObject _infoPanel;

    [SerializeField]
    private Sprite _venueSprite;

    public enum VenueType{restaurant, bar, museumm, park, store, school, other};

    // colider enter maincamera
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MainCamera")
        {
            _infoPanel.SetActive(true);

            // Display the icon (You need to replace this with your logic to get the correct icon)
                string VenueType = _venueType.ToString();
                _icon.sprite = _venueSprite;

                // Display the name
                _title.text = _venueName;

                // Display the opening/closing time
                DateTime openingTime = DateTime.ParseExact(_openingTime, "HH:mm", null);
                DateTime closingTime = DateTime.ParseExact(_closingTime, "HH:mm", null);

                // Display the status
                bool opened = IsOpen(_openingTime, _closingTime);
                if (opened)
                {
                    _openingHours.text = "Open now - closes: " + closingTime.ToString("HH:mm");
                }
                else
                {
                    _openingHours.text = "Closed - opens: " + openingTime.ToString("HH:mm");
                }
        }
    }

    // colider exit maincamera
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "MainCamera")
        {
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
}
