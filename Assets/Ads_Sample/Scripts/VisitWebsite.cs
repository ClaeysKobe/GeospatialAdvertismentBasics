using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitWebsite : MonoBehaviour
{
    public void OpenWebsite(string _websiteURL)
    {
        Application.OpenURL(_websiteURL);
    }
}
