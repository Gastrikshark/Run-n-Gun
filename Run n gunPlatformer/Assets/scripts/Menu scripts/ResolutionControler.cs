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
    // Start is called before the first frame update
    private void Start()
    {
        resolutions = Screen.resolutions;
        filteredresolutions = new List<Resolution>();

        dropdown.ClearOptions();
        currentRefreshRate = Screen.currentResolution.refreshRate;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].refreshRate == currentRefreshRate)
            {
                filteredresolutions.Add(resolutions[i]);
            }
        }

        List<string> options = new List<string>();
        for (int i = 0; i < filteredresolutions.Count; i++)
        {
            string ResolutionOption = filteredresolutions[i].width + "x" + filteredresolutions[i].height + " " + filteredresolutions[i].refreshRate + "hz";
            options.Add(ResolutionOption);
            if (filteredresolutions[i].width == Screen.width && filteredresolutions[i].height == Screen.height)
            {
                currentResolutionindex = i;
            }
        }



        dropdown.AddOptions(options);
        dropdown.value = currentResolutionindex;
        dropdown.RefreshShownValue();



    }
    public void SetResolution(int resolutionindex)
    {
        Resolution resolution = filteredresolutions[resolutionindex];
        Screen.SetResolution(resolution.width, resolution.height, true);
    }


}