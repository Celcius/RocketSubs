using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HeightIndicator : MonoBehaviour
{
    [SerializeField]
    private FloatVar maxHeight;
    
    [SerializeField]
    private TextMeshProUGUI textLabel;

    [SerializeField]
    private string textFormat;

    // Start is called before the first frame update
    private void Awake()
    {
        maxHeight.OnChange += OnHeightChange;
    }

    private void Destroy()
    {
        maxHeight.OnChange -= OnHeightChange;
    }

    private void OnHeightChange(float oldVal, float newVal)
    {
        Vector3 newPos = transform.position;
        newPos.y = newVal;
        transform.position = newPos;
        textLabel.text = string.Format(textFormat, newVal.ToString("F1"));
    }
}
