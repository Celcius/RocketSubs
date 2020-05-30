using UnityEngine;
using System.Collections;
using AmoaebaUtils;

public class SeaController : MonoBehaviour {

    [SerializeField]
    private BoolEvent onSeaDetected;

    [SerializeField]
    private Vector2Var submarinePos;

    private void Awake() 
    {
        submarinePos.OnChange += OnPosChanged;
    }

    private void OnDestroy()
    {
        submarinePos.OnChange -= OnPosChanged;
    }

    private void OnPosChanged(Vector2 oldPos, Vector2 newPos)
    {
        if(oldPos.y >= transform.position.y && newPos.y < transform.position.y)
        {
            onSeaDetected.Invoke(true);
        }
        else if(oldPos.y < transform.position.y && newPos.y >= transform.position.y)
        {
            onSeaDetected.Invoke(false);
        }
    }
}
