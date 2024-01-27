using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraColliderDetector : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _collisionParticle;

    [SerializeField]
    private PreferencesTracker _preferencesTracker;

    [SerializeField]
    private POIMetaData.VenueType _venueType;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        // Check if the entering collider is the main camera
        if (other.CompareTag("MainCamera"))
        {
            string venueType = _venueType.ToString();

            // Log the preference
            _preferencesTracker.LogPreference(venueType);
            
            // Play thearticle effect
            if (_collisionParticle != null)
            {
                _collisionParticle.Play();
            }
        }
    }
}
