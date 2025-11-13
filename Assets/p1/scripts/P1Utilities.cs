using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;

public class P1Utilities : MonoBehaviour
{

   
    void OnEnable()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    public void ExitApp()
    {
#if UNITY_EDITOR
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
#else
        {
            Application.Quit();
        }
#endif
    }

    
}
