﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyBindingMenu : MonoBehaviour
{
    public GameObject UIButtonUpFolder;
    public GameObject UIButtonDownFolder;
    public GameObject ErrorText;

    public Dictionary<KeyCode, string> PongableRepresentableKeycodes = new Dictionary<KeyCode, string>();
    public Dictionary<KeyCode, string> NotPongableRepresentableKeycodes = new Dictionary<KeyCode, string>();

    public bool CheckingForKey = false;
    public int ActionToSet = 0;

    void OnEnable()
    {
        ErrorText.GetComponent<TMP_Text>().text = "";

        GameObject UIButtonUp = UIButtonUpFolder.transform.Find("Input Button").gameObject;
        GameObject UIButtonDown = UIButtonDownFolder.transform.Find("Input Button").gameObject;

        GameObject ButtonLabelUp = UIButtonUp.transform.Find("ButtonText").gameObject;
        GameObject ButtonLabelDown = UIButtonDown.transform.Find("ButtonText").gameObject;

        //Debug.Log(GameInputManager.GetKeyMap("Up"));
        //Debug.Log(GameInputManager.GetKeyMap("Down"));

        DictKeyChecker("Up", GameInputManager.GetKeyMap("Up"), ButtonLabelUp);
        DictKeyChecker("Down", GameInputManager.GetKeyMap("Down"), ButtonLabelDown);

        SimularKeyChecker();

    }

    void OnGUI()
    {
        Event e = Event.current;

        if (e.isKey && (e.keyCode != KeyCode.None) && (CheckingForKey == true))
        {
            CheckingForKey = false;
            SetupNewKey(e.keyCode);
        }

    }

    void OnDisable()
    {
        GameObject Pointer;
        Pointer = UIButtonUpFolder.transform.Find("Pointer").gameObject;
        Pointer.SetActive(false);
        Pointer = UIButtonDownFolder.transform.Find("Pointer").gameObject;
        Pointer.SetActive(false);
        SaveSystem.SaveSettings(GameManager.GetManager().GetSettings());
    }

    //Action 1 is Up, Action 2 is down
    public void SetupLookingForKey(int Action)
    {
        ErrorText.GetComponent<TMP_Text>().text = "";
        CheckingForKey = true;
        ActionToSet = Action;
    }

    public void SetupNewKey(KeyCode key)
    {
        GameObject Pointer;
        GameObject Button;
        GameObject ButtonLabel;

        switch (ActionToSet)
        {

            case 1:
                Pointer = UIButtonUpFolder.transform.Find("Pointer").gameObject;
                Button = UIButtonUpFolder.transform.Find("Input Button").gameObject;
                ButtonLabel = Button.transform.Find("ButtonText").gameObject;

                Pointer.SetActive(false);

                DictKeyChecker("Up", key, ButtonLabel);
                SimularKeyChecker();
                break;

            case 2:
                Pointer = UIButtonDownFolder.transform.Find("Pointer").gameObject;
                Button = UIButtonDownFolder.transform.Find("Input Button").gameObject;
                ButtonLabel = Button.transform.Find("ButtonText").gameObject;

                Pointer.SetActive(false);

                DictKeyChecker("Down", key, ButtonLabel);
                SimularKeyChecker();
                break;

            default:
                Pointer = UIButtonUpFolder.transform.Find("Pointer").gameObject;
                Pointer.SetActive(false);
                Pointer = UIButtonDownFolder.transform.Find("Pointer").gameObject;
                Pointer.SetActive(false);

                ErrorText.GetComponent<TMP_Text>().text = "Something Went Wrong, Key not Changed";

                break;
        }

    }

    //Sets up the Dictonaries. Uses .count to check and see if they are not defined to make sure that we don't do the work more than we need to.
    void Awake()
    {

        if ((PongableRepresentableKeycodes.Count == 0) && (NotPongableRepresentableKeycodes.Count == 0))
        {
            //If all of the dictonaries are not defined, define all
            KeybindingDictonaryDefine();
        }
        else if ((PongableRepresentableKeycodes.Count == 0) || (NotPongableRepresentableKeycodes.Count == 0))
        {
            //If one of the dictonaries is not defined, reset and then define all.
            PongableRepresentableKeycodes.Clear();
            NotPongableRepresentableKeycodes.Clear();
            KeybindingDictonaryDefine();
        }

        //Otherwise do nothing

    }

    public void DictKeyChecker(string keyname, KeyCode key, GameObject buttonlabel)
    {
        if (PongableRepresentableKeycodes.ContainsKey(key))
        {
            buttonlabel.GetComponent<TMP_Text>().text = PongableRepresentableKeycodes[key];
            GameInputManager.SetKeyMap(keyname, key);
        }
        else if ((NotPongableRepresentableKeycodes.ContainsKey(key)) || (key == KeyCode.None))
        {
            GameInputManager.SetKeyMap(keyname, GameInputManager.keyDefaults[keyname]);
            buttonlabel.GetComponent<TMP_Text>().text = PongableRepresentableKeycodes[GameInputManager.keyDefaults[keyname]];
            ErrorText.GetComponent<TMP_Text>().text = "Set Key Not Displayable, Set to Default Key";
        }
        else
        {
            buttonlabel.GetComponent<TMP_Text>().text = key.ToString();
            GameInputManager.SetKeyMap(keyname, key);
        }

    }

    public void SimularKeyChecker()
    {
        if (GameInputManager.GetKeyMap("Up") == GameInputManager.GetKeyMap("Down"))
        {
            GameInputManager.SetKeyMap("Up", GameInputManager.keyDefaults["Up"]);
            GameInputManager.SetKeyMap("Down", GameInputManager.keyDefaults["Down"]);

            GameObject UIButtonUp = UIButtonUpFolder.transform.Find("Input Button").gameObject;
            GameObject UIButtonDown = UIButtonDownFolder.transform.Find("Input Button").gameObject;
            
            GameObject ButtonLabelUp = UIButtonUp.transform.Find("ButtonText").gameObject;
            GameObject ButtonLabelDown = UIButtonDown.transform.Find("ButtonText").gameObject;

            KeyCode Up = GameInputManager.keyDefaults["Up"];
            KeyCode Down = GameInputManager.keyDefaults["Down"];

            Debug.Log(Up);
            Debug.Log(Down);

            ButtonLabelUp.GetComponent<TMP_Text>().text = PongableRepresentableKeycodes[Up];
            ButtonLabelDown.GetComponent<TMP_Text>().text = PongableRepresentableKeycodes[Down];

            ErrorText.GetComponent<TMP_Text>().text = "Keys Set to Same Value, Setting both to Defaults";
        }

    }

    //This is formatted to group keys together for their actual keys, dictonaries are assigned to each keycode depending on what best suits it.
    //Pongable Simple keys are able to be represented with our Pongable font, Not Pongable are not, Complex use strings rather than chars as they require them.
    //Some edge cases here but nothing too badly breaks the 3 options.
    public void KeybindingDictonaryDefine()
    {
        //Typewriter Keys

        //Typewriter Letter Keys
        PongableRepresentableKeycodes.Add(KeyCode.A, "A");
        PongableRepresentableKeycodes.Add(KeyCode.B, "B");
        PongableRepresentableKeycodes.Add(KeyCode.C, "C");
        PongableRepresentableKeycodes.Add(KeyCode.D, "D");
        PongableRepresentableKeycodes.Add(KeyCode.E, "E");
        PongableRepresentableKeycodes.Add(KeyCode.F, "F");
        PongableRepresentableKeycodes.Add(KeyCode.G, "G");
        PongableRepresentableKeycodes.Add(KeyCode.H, "H");
        PongableRepresentableKeycodes.Add(KeyCode.I, "I");
        PongableRepresentableKeycodes.Add(KeyCode.J, "J");
        PongableRepresentableKeycodes.Add(KeyCode.K, "K");
        PongableRepresentableKeycodes.Add(KeyCode.L, "L");
        PongableRepresentableKeycodes.Add(KeyCode.M, "M");
        PongableRepresentableKeycodes.Add(KeyCode.N, "N");
        PongableRepresentableKeycodes.Add(KeyCode.O, "O");
        PongableRepresentableKeycodes.Add(KeyCode.P, "P");
        PongableRepresentableKeycodes.Add(KeyCode.Q, "Q");
        PongableRepresentableKeycodes.Add(KeyCode.R, "R");
        PongableRepresentableKeycodes.Add(KeyCode.S, "S");
        PongableRepresentableKeycodes.Add(KeyCode.T, "T");
        PongableRepresentableKeycodes.Add(KeyCode.U, "U");
        PongableRepresentableKeycodes.Add(KeyCode.V, "V");
        PongableRepresentableKeycodes.Add(KeyCode.W, "W");
        PongableRepresentableKeycodes.Add(KeyCode.X, "X");
        PongableRepresentableKeycodes.Add(KeyCode.Y, "Y");
        PongableRepresentableKeycodes.Add(KeyCode.Z, "Z");

        //Typewriter Number Keys
        PongableRepresentableKeycodes.Add(KeyCode.Alpha1, "1");
        PongableRepresentableKeycodes.Add(KeyCode.Alpha2, "2");
        PongableRepresentableKeycodes.Add(KeyCode.Alpha3, "3");
        PongableRepresentableKeycodes.Add(KeyCode.Alpha4, "4");
        PongableRepresentableKeycodes.Add(KeyCode.Alpha5, "5");
        PongableRepresentableKeycodes.Add(KeyCode.Alpha6, "6");
        PongableRepresentableKeycodes.Add(KeyCode.Alpha7, "7");
        PongableRepresentableKeycodes.Add(KeyCode.Alpha8, "8");
        PongableRepresentableKeycodes.Add(KeyCode.Alpha9, "9");
        PongableRepresentableKeycodes.Add(KeyCode.Alpha0, "0");

        //Typewriter Puncuation Keys
        PongableRepresentableKeycodes.Add(KeyCode.Exclaim, "!");
        PongableRepresentableKeycodes.Add(KeyCode.DoubleQuote, "\"");
        NotPongableRepresentableKeycodes.Add(KeyCode.Hash, "#");
        NotPongableRepresentableKeycodes.Add(KeyCode.Dollar, "$");
        NotPongableRepresentableKeycodes.Add(KeyCode.Percent, "%");
        NotPongableRepresentableKeycodes.Add(KeyCode.Ampersand, "&");
        PongableRepresentableKeycodes.Add(KeyCode.Quote, "\"");
        NotPongableRepresentableKeycodes.Add(KeyCode.LeftParen, "(");
        NotPongableRepresentableKeycodes.Add(KeyCode.RightParen, ")");
        NotPongableRepresentableKeycodes.Add(KeyCode.Asterisk, "*");
        PongableRepresentableKeycodes.Add(KeyCode.Plus, "+");
        PongableRepresentableKeycodes.Add(KeyCode.Comma, ",");
        PongableRepresentableKeycodes.Add(KeyCode.Minus, "-");
        PongableRepresentableKeycodes.Add(KeyCode.Period, ".");
        PongableRepresentableKeycodes.Add(KeyCode.Slash, "/");
        PongableRepresentableKeycodes.Add(KeyCode.Colon, ":");
        PongableRepresentableKeycodes.Add(KeyCode.Semicolon, ";");
        PongableRepresentableKeycodes.Add(KeyCode.Less, "<");
        PongableRepresentableKeycodes.Add(KeyCode.Equals, "=");
        PongableRepresentableKeycodes.Add(KeyCode.Greater, ">");
        PongableRepresentableKeycodes.Add(KeyCode.Question, "?");
        NotPongableRepresentableKeycodes.Add(KeyCode.At, "@");
        PongableRepresentableKeycodes.Add(KeyCode.LeftBracket, "[");
        PongableRepresentableKeycodes.Add(KeyCode.Backslash, "\\");
        PongableRepresentableKeycodes.Add(KeyCode.RightBracket, "]");
        PongableRepresentableKeycodes.Add(KeyCode.Caret, "^");
        PongableRepresentableKeycodes.Add(KeyCode.Underscore, "_");
        NotPongableRepresentableKeycodes.Add(KeyCode.BackQuote, "`");
        PongableRepresentableKeycodes.Add(KeyCode.LeftCurlyBracket, "{");
        NotPongableRepresentableKeycodes.Add(KeyCode.Pipe, "|");
        PongableRepresentableKeycodes.Add(KeyCode.RightCurlyBracket, "}");
        NotPongableRepresentableKeycodes.Add(KeyCode.Tilde, "~");

        //Typewriter Modifier Keys
        PongableRepresentableKeycodes.Add(KeyCode.LeftShift, "Left Shift");
        PongableRepresentableKeycodes.Add(KeyCode.RightShift, "Right Shift");
        PongableRepresentableKeycodes.Add(KeyCode.LeftControl, "Left Control");
        PongableRepresentableKeycodes.Add(KeyCode.RightControl, "Right Control");
        PongableRepresentableKeycodes.Add(KeyCode.LeftAlt, "Left Alt");
        PongableRepresentableKeycodes.Add(KeyCode.RightAlt, "Right Alt");
        PongableRepresentableKeycodes.Add(KeyCode.AltGr, "Alt Gr");


        //Typewriter Editing Keys
        PongableRepresentableKeycodes.Add(KeyCode.Return, "Enter");
        PongableRepresentableKeycodes.Add(KeyCode.Backspace, "Backspace");
        PongableRepresentableKeycodes.Add(KeyCode.Space, "Space");
        PongableRepresentableKeycodes.Add(KeyCode.Tab, "Tab");

        //Typewriter Lock Keys
        PongableRepresentableKeycodes.Add(KeyCode.CapsLock, "Capslock");


        //Top Row Keys

        //Escape
        PongableRepresentableKeycodes.Add(KeyCode.Escape, "Esc");

        //Function Keys
        PongableRepresentableKeycodes.Add(KeyCode.F1, "F1");
        PongableRepresentableKeycodes.Add(KeyCode.F2, "F2");
        PongableRepresentableKeycodes.Add(KeyCode.F3, "F3");
        PongableRepresentableKeycodes.Add(KeyCode.F4, "F4");
        PongableRepresentableKeycodes.Add(KeyCode.F5, "F5");
        PongableRepresentableKeycodes.Add(KeyCode.F6, "F6");
        PongableRepresentableKeycodes.Add(KeyCode.F7, "F7");
        PongableRepresentableKeycodes.Add(KeyCode.F8, "F8");
        PongableRepresentableKeycodes.Add(KeyCode.F9, "F9");
        PongableRepresentableKeycodes.Add(KeyCode.F10, "F10");
        PongableRepresentableKeycodes.Add(KeyCode.F11, "F11");
        PongableRepresentableKeycodes.Add(KeyCode.F12, "F12");


        //Arrow Keys Block 

        //Special Purpose Keys
        PongableRepresentableKeycodes.Add(KeyCode.Print, "Print");
        PongableRepresentableKeycodes.Add(KeyCode.ScrollLock, "Scroll Lock");
        PongableRepresentableKeycodes.Add(KeyCode.Break, "Break");

        PongableRepresentableKeycodes.Add(KeyCode.Insert, "Insert");
        PongableRepresentableKeycodes.Add(KeyCode.Home, "Home");
        PongableRepresentableKeycodes.Add(KeyCode.Delete, "Delete");
        PongableRepresentableKeycodes.Add(KeyCode.End, "End");
        PongableRepresentableKeycodes.Add(KeyCode.PageUp, "Page Up");
        PongableRepresentableKeycodes.Add(KeyCode.PageDown, "Page Down");

        //Arrow Keys
        PongableRepresentableKeycodes.Add(KeyCode.UpArrow, "Up");
        PongableRepresentableKeycodes.Add(KeyCode.DownArrow, "Down");
        PongableRepresentableKeycodes.Add(KeyCode.RightArrow, "Right");
        PongableRepresentableKeycodes.Add(KeyCode.LeftArrow, "Left");


        //Keypad

        //Numeric Keypad
        PongableRepresentableKeycodes.Add(KeyCode.Keypad1, "Numpad 1");
        PongableRepresentableKeycodes.Add(KeyCode.Keypad2, "Numpad 2");
        PongableRepresentableKeycodes.Add(KeyCode.Keypad3, "Numpad 3");
        PongableRepresentableKeycodes.Add(KeyCode.Keypad4, "Numpad 4");
        PongableRepresentableKeycodes.Add(KeyCode.Keypad5, "Numpad 5");
        PongableRepresentableKeycodes.Add(KeyCode.Keypad6, "Numpad 6");
        PongableRepresentableKeycodes.Add(KeyCode.Keypad7, "Numpad 7");
        PongableRepresentableKeycodes.Add(KeyCode.Keypad8, "Numpad 8");
        PongableRepresentableKeycodes.Add(KeyCode.Keypad9, "Numpad 9");
        PongableRepresentableKeycodes.Add(KeyCode.Keypad0, "Numpad 0");

        //Other Keypad Stuff
        PongableRepresentableKeycodes.Add(KeyCode.Numlock, "Numlock");
        PongableRepresentableKeycodes.Add(KeyCode.KeypadPeriod, "Numpad .");
        PongableRepresentableKeycodes.Add(KeyCode.KeypadDivide, "Numpad /");
        PongableRepresentableKeycodes.Add(KeyCode.KeypadMinus, "Numpad -");
        PongableRepresentableKeycodes.Add(KeyCode.KeypadPlus, "Numpad +");
        PongableRepresentableKeycodes.Add(KeyCode.KeypadEnter, "Numpad Enter");
        PongableRepresentableKeycodes.Add(KeyCode.KeypadEquals, "Numpad =");
        NotPongableRepresentableKeycodes.Add(KeyCode.KeypadMultiply, "Numpad *");


        //Other Keys (Some of the weird ones)
        PongableRepresentableKeycodes.Add(KeyCode.Clear, "Clear Key");
        PongableRepresentableKeycodes.Add(KeyCode.Pause, "Pause Key");

        PongableRepresentableKeycodes.Add(KeyCode.LeftCommand, "Left Cmmd");
        //PongableRepresentableKeycodes.Add(KeyCode.LeftApple, "Left Apple");
        PongableRepresentableKeycodes.Add(KeyCode.RightCommand, "Right Cmmd");
        //PongableRepresentableKeycodes.Add(KeyCode.RightApple, "Right Apple");
        PongableRepresentableKeycodes.Add(KeyCode.LeftWindows, "Left Windows");
        PongableRepresentableKeycodes.Add(KeyCode.RightWindows, "Right Windows");

        PongableRepresentableKeycodes.Add(KeyCode.Help, "Help Key");
        PongableRepresentableKeycodes.Add(KeyCode.SysReq, "SysReq Key");
        PongableRepresentableKeycodes.Add(KeyCode.Menu, "Menu Key");

        /*
        //These are mouse options, added for consistancy.
        //There are also joystick buttons, but those seem more than extranious.

        Dictonary.Add(KeyCode.Mouse0, "Mouse 0");
        Dictonary.Add(KeyCode.Mouse1, "Mouse 1");
        Dictonary.Add(KeyCode.Mouse2, "Mouse 2");
        Dictonary.Add(KeyCode.Mouse3, "Mouse 3");
        Dictonary.Add(KeyCode.Mouse4, "Mouse 4");
        Dictonary.Add(KeyCode.Mouse5, "Mouse 5");
        Dictonary.Add(KeyCode.Mouse6, "Mouse 6");
        Dictonary.Add(KeyCode.Mouse7, "Mouse 7");
        Dictonary.Add(KeyCode.Mouse0, "Mouse 0");
        */
    }

}
