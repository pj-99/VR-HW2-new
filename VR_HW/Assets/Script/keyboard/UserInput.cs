using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UserInput : MonoBehaviour
{
    public TMP_InputField username;
    public GameObject keyboard1;


    private int state;
    private Button[] buttons1;
//private string str;
// Start is called before the first frame update
    private void OnEnable()
    {
        Debug.Log("ONENABLE! " + gameObject.name);
        state = 0;
        buttons1 = keyboard1.GetComponentsInChildren<Button>();
        for (int i = 0; i < buttons1.Length; i++)
        {
            if (buttons1[i].name == "DEL")
            {
                    buttons1[i].onClick.AddListener(delegate { InputDeletion(username); });
            }
            else if (buttons1[i].name == "Shift")
            {
                buttons1[i].onClick.AddListener(delegate { InputShift(buttons1, 1); });
            }
            else if (buttons1[i].name == "<-")
            {

            }
            else if (buttons1[i].name == "->")
            {

            }
            else
            {
                string str1 = buttons1[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;

                    buttons1[i].onClick.AddListener(delegate { InputOnClick(str1, username); });
            }
        }
        //keyboard1.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void InputOnClick(string message, TMP_InputField field)
    {
        if (!gameObject.activeInHierarchy) {
            Debug.Log("not active return");
            return;
        } 
        if (state == 1)
            field.text = field.text + message.ToUpper();
        else
            field.text = field.text + message;
    }

    void InputDeletion(TMP_InputField field)
    {
        if (!gameObject.activeInHierarchy)
        {
            Debug.Log("not active return");
            return;
        }

        string str = field.text.Remove(field.text.Length - 1, 1);
        field.text = str;
    }

    void InputShift(Button[] buttons, int n)
    {
        if (!gameObject.activeInHierarchy)
        {
            Debug.Log("not active return");
            return;
        }
        for (int i = 0; i < buttons.Length; i++)
        {
            if (Regex.IsMatch(buttons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text, @"[a-z]") && buttons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Length == 1)
            {
                string str = buttons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.ToUpper();
                buttons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = str;
                state = 1;
            }
            else if (Regex.IsMatch(buttons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text, @"[A-Z]") && buttons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Length == 1)
            {
                string str = buttons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.ToLower();
                buttons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = str;
                state = 0;
            }
        }
    }
}
