using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class NetworkCheck : MonoBehaviour
{
    [SerializeField]
    private GameObject _alertPanel;

    [SerializeField]
    private GameObject _debugPanel;

    [SerializeField]
    private GameObject _infoPanel;

    private bool isInternetConnected = true;

    void Start()
    {
        // Check for network connectivity at the start
        CheckInternetConnection();
    }

    void Update()
    {
        // Check for network connectivity in the update loop (you can adjust the frequency)
        if (!isInternetConnected)
        {
            CheckInternetConnection();
        }
    }

    void CheckInternetConnection()
    {
        switch (Application.internetReachability)
        {
            case NetworkReachability.NotReachable:
                // No internet connection
                isInternetConnected = false;
                ShowAlert();
                break;
            case NetworkReachability.ReachableViaCarrierDataNetwork:
            case NetworkReachability.ReachableViaLocalAreaNetwork:
                // Internet connection is available
                if (!isInternetConnected)
                {
                    isInternetConnected = true;
                    HideAlert();
                    // You can add any additional actions you want to perform when the internet is back, such as refreshing the scene.
                    RefreshScene();
                }
                break;
        }
    }

    void ShowAlert()
    {
        _alertPanel.SetActive(true);
        _debugPanel.SetActive(false);
        _infoPanel.SetActive(false);
    }

    void HideAlert()
    {
        _alertPanel.SetActive(false);
        _infoPanel.SetActive(true);
    }

    void RefreshScene()
    {
        // You can modify this as needed, for example, by loading the current scene again
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
