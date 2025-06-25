using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TabNav : MonoBehaviour
{
    public GameObject[] navFields;
    private int selected;
    private int maxArray;
    private GameObject currentSelected;

   
    void Start()
    {
        selected = 0;
        maxArray = navFields.Length - 1;
        currentSelected = navFields[selected].gameObject;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Tab))
        {
            if (selected > 0)
            {
                selected--;
                currentSelected = navFields[selected].gameObject;

                if (EventSystem.current == null)
                    return;
                EventSystem.current.SetSelectedGameObject(currentSelected);
                if (GetComponent<InputField>() != null)
                {
                    GetComponent<InputField>().Select();
                    GetComponent<InputField>().ActivateInputField();
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (selected < maxArray)
            {
                selected++;
                currentSelected = navFields[selected].gameObject;

                if (EventSystem.current == null)
                    return;
                EventSystem.current.SetSelectedGameObject(currentSelected);
                if (GetComponent<InputField>() != null)
                {
                    GetComponent<InputField>().Select();
                    GetComponent<InputField>().ActivateInputField();
                }
            }
        }
    }
}
