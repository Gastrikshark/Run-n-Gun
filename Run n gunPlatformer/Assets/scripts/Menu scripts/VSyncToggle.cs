using UnityEngine;

public class VSyncToggle : MonoBehaviour
{
    private bool vsyncEnabled = true;

    public void ToggleVSync()
    {
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

