using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StringUIData : MonoBehaviour
{
    public StringData stringData;
    public TMP_Text textBox;

    public void SetValue(string value)
    {
        stringData.value = value;
    }

    public void Start()
    {
        if (textBox == null) return;
        textBox.text = stringData.value;
    }
}
