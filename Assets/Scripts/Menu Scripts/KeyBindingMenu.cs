using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyBindingMenu : MonoBehaviour
{
    public GameObject UIButtonUp;
    public GameObject UIButtonDown;
    GameObject LabeledBinding;

    public Dictionary<KeyCode, char> PongableSimpleKeyDictonary = new Dictionary<KeyCode, char>();
    public Dictionary<KeyCode, char> NotPongableSimpleKeyDictonary = new Dictionary<KeyCode, char>();
    public Dictionary<KeyCode, string> ComplexKeyDictonary = new Dictionary<KeyCode, string>();

    bool DictPop = false;

    void OnEnable()
    {
        GameInputManager.GetKeyMap("Up");
        GameInputManager.GetKeyMap("Down");

        LabeledBinding = UIButtonUp.transform.Find("Label").gameObject;
        LabeledBinding.GetComponent<TMP_Text>().text = GameInputManager.GetKeyMap("Up");



    }

    //Sets up the Dictonaries for the first time, uses DictPop to make sure that it isn't doing this twice.
    //This is formatted to group keys together for their actual keys, dictonaries are assigned to each keycode depending on what best suits it.
    //Pongable Simple keys are able to be represented with our Pongable font, Not Pongable are not, Complex use strings rather than chars as they require them.
    void Awake()
    {
        if (DictPop == false)
        {
            //Typewriter Keys

            //Typewriter Letter Keys
            PongableSimpleKeyDictonary.Add(KeyCode.A, 'A');
            PongableSimpleKeyDictonary.Add(KeyCode.B, 'B');
            PongableSimpleKeyDictonary.Add(KeyCode.C, 'C');
            PongableSimpleKeyDictonary.Add(KeyCode.D, 'D');
            PongableSimpleKeyDictonary.Add(KeyCode.E, 'E');
            PongableSimpleKeyDictonary.Add(KeyCode.F, 'F');
            PongableSimpleKeyDictonary.Add(KeyCode.G, 'G');
            PongableSimpleKeyDictonary.Add(KeyCode.H, 'H');
            PongableSimpleKeyDictonary.Add(KeyCode.I, 'I');
            PongableSimpleKeyDictonary.Add(KeyCode.J, 'J');
            PongableSimpleKeyDictonary.Add(KeyCode.K, 'K');
            PongableSimpleKeyDictonary.Add(KeyCode.L, 'L');
            PongableSimpleKeyDictonary.Add(KeyCode.M, 'M');
            PongableSimpleKeyDictonary.Add(KeyCode.N, 'N');
            PongableSimpleKeyDictonary.Add(KeyCode.O, 'O');
            PongableSimpleKeyDictonary.Add(KeyCode.P, 'P');
            PongableSimpleKeyDictonary.Add(KeyCode.Q, 'Q');
            PongableSimpleKeyDictonary.Add(KeyCode.R, 'R');
            PongableSimpleKeyDictonary.Add(KeyCode.S, 'S');
            PongableSimpleKeyDictonary.Add(KeyCode.T, 'T');
            PongableSimpleKeyDictonary.Add(KeyCode.U, 'U');
            PongableSimpleKeyDictonary.Add(KeyCode.V, 'V');
            PongableSimpleKeyDictonary.Add(KeyCode.W, 'W');
            PongableSimpleKeyDictonary.Add(KeyCode.X, 'X');
            PongableSimpleKeyDictonary.Add(KeyCode.Y, 'Y');
            PongableSimpleKeyDictonary.Add(KeyCode.Z, 'Z');

            //Typewriter Number Keys
            PongableSimpleKeyDictonary.Add(KeyCode.Alpha1, '1');
            PongableSimpleKeyDictonary.Add(KeyCode.Alpha2, '2');
            PongableSimpleKeyDictonary.Add(KeyCode.Alpha3, '3');
            PongableSimpleKeyDictonary.Add(KeyCode.Alpha4, '4');
            PongableSimpleKeyDictonary.Add(KeyCode.Alpha5, '5');
            PongableSimpleKeyDictonary.Add(KeyCode.Alpha6, '6');
            PongableSimpleKeyDictonary.Add(KeyCode.Alpha7, '7');
            PongableSimpleKeyDictonary.Add(KeyCode.Alpha8, '8');
            PongableSimpleKeyDictonary.Add(KeyCode.Alpha9, '9');
            PongableSimpleKeyDictonary.Add(KeyCode.Alpha0, '0');

            //Typewriter Puncuation Keys
            PongableSimpleKeyDictonary.Add(KeyCode.Exclaim, '!');
            PongableSimpleKeyDictonary.Add(KeyCode.DoubleQuote, '"');
            NotPongableSimpleKeyDictonary.Add(KeyCode.Hash, '#');
            NotPongableSimpleKeyDictonary.Add(KeyCode.Dollar, '$');
            NotPongableSimpleKeyDictonary.Add(KeyCode.Percent, '%');
            NotPongableSimpleKeyDictonary.Add(KeyCode.Ampersand, '&');
            PongableSimpleKeyDictonary.Add(KeyCode.Quote, '\'');
            NotPongableSimpleKeyDictonary.Add(KeyCode.LeftParen, '(');
            NotPongableSimpleKeyDictonary.Add(KeyCode.RightParen, ')');
            NotPongableSimpleKeyDictonary.Add(KeyCode.Asterisk, '*');
            PongableSimpleKeyDictonary.Add(KeyCode.Plus, '+');
            PongableSimpleKeyDictonary.Add(KeyCode.Comma, ',');
            PongableSimpleKeyDictonary.Add(KeyCode.Minus, '-');
            PongableSimpleKeyDictonary.Add(KeyCode.Period, '.');
            PongableSimpleKeyDictonary.Add(KeyCode.Slash, '/');
            PongableSimpleKeyDictonary.Add(KeyCode.Colon, ':');
            PongableSimpleKeyDictonary.Add(KeyCode.Semicolon, ';');
            PongableSimpleKeyDictonary.Add(KeyCode.Less, '<');
            PongableSimpleKeyDictonary.Add(KeyCode.Equals, '=');
            PongableSimpleKeyDictonary.Add(KeyCode.Greater, '>');
            PongableSimpleKeyDictonary.Add(KeyCode.Question, '?');
            NotPongableSimpleKeyDictonary.Add(KeyCode.At, '@');
            PongableSimpleKeyDictonary.Add(KeyCode.LeftBracket, '[');
            PongableSimpleKeyDictonary.Add(KeyCode.Backslash, '\\');
            PongableSimpleKeyDictonary.Add(KeyCode.RightBracket, ']');
            PongableSimpleKeyDictonary.Add(KeyCode.Caret, '^');
            PongableSimpleKeyDictonary.Add(KeyCode.Underscore, '_');
            NotPongableSimpleKeyDictonary.Add(KeyCode.BackQuote, '`');
            PongableSimpleKeyDictonary.Add(KeyCode.LeftCurlyBracket, '{');
            NotPongableSimpleKeyDictonary.Add(KeyCode.Pipe, '|');
            PongableSimpleKeyDictonary.Add(KeyCode.RightCurlyBracket, '}');
            NotPongableSimpleKeyDictonary.Add(KeyCode.Tilde, '~');

            //Typewriter Modifier Keys

            //Typewriter Other Keys


            //Top Row Keys

            //Escape
            ComplexKeyDictonary.Add(KeyCode.Escape, "Esc");

            //Function Keys
            ComplexKeyDictonary.Add(KeyCode.F1, "F1");
            ComplexKeyDictonary.Add(KeyCode.F2, "F2");
            ComplexKeyDictonary.Add(KeyCode.F3, "F3");
            ComplexKeyDictonary.Add(KeyCode.F4, "F4");
            ComplexKeyDictonary.Add(KeyCode.F5, "F5");
            ComplexKeyDictonary.Add(KeyCode.F6, "F6");
            ComplexKeyDictonary.Add(KeyCode.F7, "F7");
            ComplexKeyDictonary.Add(KeyCode.F8, "F8");
            ComplexKeyDictonary.Add(KeyCode.F9, "F9");
            ComplexKeyDictonary.Add(KeyCode.F10, "F10");
            ComplexKeyDictonary.Add(KeyCode.F11, "F11");
            ComplexKeyDictonary.Add(KeyCode.F12, "F12");


            //Arrow Keys Columb 

            //Special Purpose Keys
            ComplexKeyDictonary.Add(KeyCode.Print, "Print");
            ComplexKeyDictonary.Add(KeyCode.ScrollLock, "Scroll Lock");
            ComplexKeyDictonary.Add(KeyCode.Break, "Break");

            ComplexKeyDictonary.Add(KeyCode.Insert, "Insert");
            ComplexKeyDictonary.Add(KeyCode.Home, "Home");
            ComplexKeyDictonary.Add(KeyCode.Delete, "Delete");
            ComplexKeyDictonary.Add(KeyCode.End, "End");
            ComplexKeyDictonary.Add(KeyCode.PageUp, "Page Up");
            ComplexKeyDictonary.Add(KeyCode.PageDown, "Page Down");

            //Arrow Keys
            ComplexKeyDictonary.Add(KeyCode.UpArrow, "Up");
            ComplexKeyDictonary.Add(KeyCode.DownArrow, "Down");
            ComplexKeyDictonary.Add(KeyCode.RightArrow, "Right");
            ComplexKeyDictonary.Add(KeyCode.LeftArrow, "Left");


            //Keypad

            //Numeric Keypad
            PongableSimpleKeyDictonary.Add(KeyCode.Keypad1, '1');
            PongableSimpleKeyDictonary.Add(KeyCode.Keypad2, '2');
            PongableSimpleKeyDictonary.Add(KeyCode.Keypad3, '3');
            PongableSimpleKeyDictonary.Add(KeyCode.Keypad4, '4');
            PongableSimpleKeyDictonary.Add(KeyCode.Keypad5, '5');
            PongableSimpleKeyDictonary.Add(KeyCode.Keypad6, '6');
            PongableSimpleKeyDictonary.Add(KeyCode.Keypad7, '7');
            PongableSimpleKeyDictonary.Add(KeyCode.Keypad8, '8');
            PongableSimpleKeyDictonary.Add(KeyCode.Keypad9, '9');
            PongableSimpleKeyDictonary.Add(KeyCode.Keypad0, '0');

            //Other Keypad Stuff
            ComplexKeyDictonary.Add(KeyCode.Numlock, "Numlock");
            PongableSimpleKeyDictonary.Add(KeyCode.KeypadPeriod, '.');
            PongableSimpleKeyDictonary.Add(KeyCode.KeypadDivide, '/');
            NotPongableSimpleKeyDictonary.Add(KeyCode.KeypadMultiply, '*');
            PongableSimpleKeyDictonary.Add(KeyCode.KeypadMinus, '-');
            PongableSimpleKeyDictonary.Add(KeyCode.KeypadPlus, '+');
            ComplexKeyDictonary.Add(KeyCode.KeypadEnter, "Enter");
            PongableSimpleKeyDictonary.Add(KeyCode.KeypadEquals, '=');

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

        bool DictPop = true;

    }

}
