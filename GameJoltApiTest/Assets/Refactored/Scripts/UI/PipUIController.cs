using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LayoutGroup))]
public class PipUIController : MonoBehaviour
{
    [SerializeField]
    protected Image pipPrefab;

    [SerializeField]
    private bool increaseRight = true;

    protected List<Image> pips = new List<Image>();

    private LayoutGroup layout;
    private int visibleAmount = 0;

    private void Awake()
    {
        layout = GetComponent<LayoutGroup>();
    }
    
    private void CreatePip()
    {
        Image newPip = Instantiate<Image>(pipPrefab, this.transform);
        pips.Add(newPip);
    }

    protected void CreateNewPips(int amount)
    {
        foreach(Image image in pips)
        {
            DestroyImmediate(image.gameObject);
        }

        pips.Clear();

        for(int i = 0; i < amount; i++)
        {
            CreatePip();
        }

        if(!increaseRight)
        {
            pips.Reverse();
        }
        visibleAmount = amount;
    }

    protected void SetVisibleAmount(float amount)
    {
        int invisibleStartIndex = (int)Mathf.Ceil(Mathf.Clamp(amount * pips.Count, 0, pips.Count));
        
        for(int i = 0; i < pips.Count; i++)
        {
            Color c = pips[i].color;
            c.a = i < invisibleStartIndex? 1 : 0;
            pips[i].color = c;
        }
    }
}
