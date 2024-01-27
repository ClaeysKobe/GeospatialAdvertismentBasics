using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class TargetColliderScript : MonoBehaviour
{
    [SerializeField]
    private List<string> _colliderTypes;

    [SerializeField]
    private PreferencesTracker _preferencesTracker;

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("MainCamera"))
        {

            if (_colliderTypes.Count > 0)
            {
                foreach (string colliderType in _colliderTypes)
                {
                    // Log the preference
                    _preferencesTracker.LogPreference(colliderType);
                }
            }
        }
    }
}
