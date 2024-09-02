using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIState : MonoBehaviour
{
    public GameObject pay;
    public GameObject bread;
    public GameObject cafe;

    public TextMeshProUGUI breadCountText;

    private void Start()
    {
    }
    private void showPayBox(bool isOn)
    {
        pay.SetActive(isOn);
        bread.SetActive(!isOn);
        cafe.SetActive(!isOn);
    }

    private void showBreadBox(bool isOn, int i)
    {
        bread.SetActive(isOn);
        breadCountText.text = $"{i}";
        pay.SetActive(!isOn);
        cafe.SetActive(!isOn);
    }

    private void showCafeBox(bool isOn)
    {
        cafe.SetActive(isOn);
        bread.SetActive(!isOn);
        pay.SetActive(!isOn);
    }

    private void Nothing(bool isOn)
    {
        cafe.SetActive(!isOn);
        bread.SetActive(!isOn);
        pay.SetActive(!isOn);
    }

    public void ShowCommentBox(string name, int i)
    {
        if (name == "P")
            showPayBox(true);

        else if (name == "B")
            showBreadBox(true,i);

        else if(name == "C")
            showCafeBox(true);
        else
            Nothing(true);
    }
}
