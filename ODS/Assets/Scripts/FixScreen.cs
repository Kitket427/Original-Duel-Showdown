using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixScreen : MonoBehaviour
{
    void Start()
    {
        if (Screen.width / 16 * 9 < Screen.height)
        {
            Screen.SetResolution(Screen.width, Screen.width / 16 * 9, true);
        }
        if (Screen.width / 16 * 9 > Screen.height)
        {
            Screen.SetResolution(Screen.height * 16 / 9, Screen.height,true);
        }
    }
}
