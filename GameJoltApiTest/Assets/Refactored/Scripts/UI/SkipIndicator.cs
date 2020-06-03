using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using AmoaebaUtils;

public class SkipIndicator : MonoBehaviour
{
    [SerializeField]
    private IntVar skipCount;

    [SerializeField]
    private IntVar maxSkipCount;

    [SerializeField]
    private TextMeshProUGUI label;

    [SerializeField]
    private Color maxColor;

    [SerializeField]
    private Color normalColor;
    
    [SerializeField]
    private PhysicsAnimationCurve moveCurve;

    [SerializeField]
    private AnimationCurve fadeCurve;

    [SerializeField]
    private string stringFormat;

    private Color selectedColor;

    private float initialPosY;
    private void Awake()
    {
        bool isRecord = skipCount.Value == maxSkipCount.Value;
        selectedColor = isRecord? maxColor : normalColor;
        label.color = selectedColor;
        label.text = string.Format(stringFormat, skipCount.Value);
        
        initialPosY = transform.position.y;

        StartCoroutine(MoveAndFadeRoutine());
    }

    private IEnumerator MoveAndFadeRoutine()
    {
        float elapsed = 0;
        while(elapsed < moveCurve.Duration)
        {
            transform.position = new Vector3(transform.position.x,
                                            moveCurve.Evaluate(initialPosY, elapsed),
                                            transform.position.z);
            
            selectedColor.a = fadeCurve.Evaluate(elapsed/(moveCurve.Duration));
            label.color = selectedColor;

            yield return new WaitForEndOfFrame();
            elapsed += Time.deltaTime;
        }
        selectedColor.a = fadeCurve.Evaluate(1.0f);
        label.color = selectedColor;
        Destroy(gameObject);
    }
}
