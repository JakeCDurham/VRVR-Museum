using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuTextUpdater : MonoBehaviour
{
    private TextMeshProUGUI myText;
    [SerializeField] private string baseText;
    [SerializeField] private string baseVal;
    private void Start()
    {
        myText = GetComponent<TextMeshProUGUI>();
        myText.text = baseText + " " + baseVal;
    }

    public void UpdateValue(string value)
    {
        myText.text = baseText + " " + value;
    }
}
