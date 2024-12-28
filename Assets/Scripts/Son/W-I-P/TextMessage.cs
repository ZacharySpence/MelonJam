using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextMessage : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textPart;
    public void Setup(string message)
    {
        textPart.text = message;


    }
}
