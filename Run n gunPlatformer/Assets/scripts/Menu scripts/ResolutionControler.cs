using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResolutionController : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;

    private Resolution[] resolutions;
    private List<Resolution> filteredresolutions;

    private float currentRefreshRate;
    private int currentResolutionindex = 0;
   
    // creart en lijst met alle besicbare resoluties 
    private void Start()
    {
        resolutions = Screen.resolutions;
        filteredresolutions = new List<Resolution>();

      // maakt hem helemaal leeg
        dropdown.ClearOptions();
        currentRefreshRate = Screen.currentResolution.refreshRate;

        for (int i = 0; i < resolutions.Length; i++)
        {  // stopt de opties er weer in die passen bij refreshRate van je monitor
            if (resolutions[i].refreshRate == currentRefreshRate)
            {
                filteredresolutions.Add(resolutions[i]);
            }
        }

        // creart en lijst voor de drop down menu
        List<string> options = new List<string>();
        for (int i = 0; i < filteredresolutions.Count; i++)
        {// kijkt of de huide resoluties aan de eisen voldoen het gaat hier om brete, lengte, en refresh rate 
            string ResolutionOption = filteredresolutions[i].width + "x" + filteredresolutions[i].height + " " + filteredresolutions[i].refreshRate + "hz";
            options.Add(ResolutionOption);
            if (filteredresolutions[i].width == Screen.width && filteredresolutions[i].height == Screen.height)
            {
                currentResolutionindex = i;
            }
        }


        // selecteert de kezue
        dropdown.AddOptions(options);
        //stopt  jou keuze in de resolutie index
        dropdown.value = currentResolutionindex;
        // Refresh de resolutie menu zo dat de player kan zien wat er is gebeurt
        dropdown.RefreshShownValue();



    }
    public void SetResolution(int resolutionindex)
    {// laat de opties zien de de player kan kiezen gebasiert op de width van zijn beeld scherm en de refresh rate
        Resolution resolution = filteredresolutions[resolutionindex];
        Screen.SetResolution(resolution.width, resolution.height, true);
    }


}