using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTouchKeyboard : MonoBehaviour
{
    public TouchScreenKeyboard keyboard;
    string keyboardText;


    public void OpenSystemKeyboard()
    {
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, false, false, "Enter your name");
    }
    void Update()
    {
        if (TouchScreenKeyboard.visible == false && keyboard != null)
        {
            if (keyboard.done == true)
            {
                keyboardText = keyboard.text;
                keyboard = null;
                Debug.Log(keyboardText);
            }
        }
    }
}
