using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIToastItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtContent;

    public void Set(string content)
    {
        txtContent.text = content;
    }
}
