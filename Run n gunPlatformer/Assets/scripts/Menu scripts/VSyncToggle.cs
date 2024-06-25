using UnityEngine;

public class VSyncToggle : MonoBehaviour
{
    private bool vsyncEnabled = true;

    
    public void ToggleVSync()
    {// zet de vsyncEnabled bool op de vSync Optie in unity en zet hem aan of uit als de player op de optie clickt
        vsyncEnabled = !vsyncEnabled;
        QualitySettings.vSyncCount = vsyncEnabled ? 1 : 0;
        if (QualitySettings.vSyncCount == 1)
        {
            Debug.Log("VSync is currently enabled.");
        }
        else
        {
            Debug.Log("VSync is currently disabled.");
        }
    }



}

