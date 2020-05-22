using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using AmoaebaUtils;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SplashSystem : BootScriptableObject
{
   [SerializeField]
   private Transform splashPrefab;
   
   [SerializeField]
   private Transform diveSplashPrefab;

   [SerializeField]
   private Vector2Var submarinePosition;
   
   [SerializeField]
   private Rigidbody2DVar submarineBody;

   [SerializeField]
   private float heightThreshold;

   [SerializeField]
   private float waterHeight = 0;
   
   [SerializeField]
   private float timeBetweenSplashes = 0.2f;

   [SerializeField]
   private VoidEvent bounceEvent;

   private IEnumerator splashRoutine;
   private CoroutineRunner splashRunner;

   protected override void OnEnable()
   {
#if UNITY_EDITOR       
       if(!(Application.isPlaying || EditorApplication.isPlayingOrWillChangePlaymode))
       {
            return;
       }
#endif
       base.OnEnable();
       UnregisterEvents();
       RegisterEvents();
   }

   protected override void OnDisable()
   {
#if UNITY_EDITOR
       if(!(Application.isPlaying || EditorApplication.isPlayingOrWillChangePlaymode))
       {
            return;
       }
#endif
       base.OnDisable();
       
       UnregisterEvents();
       if(splashRunner != null)
       {
            Destroy(splashRunner.gameObject);
            splashRunner = null;
       }
   }

   private void OnDestroy()
   {
       UnregisterEvents();
   }

   private void RegisterEvents()
   {
       submarinePosition.OnChange += OnPositionChanged;
       bounceEvent.OnEvent += CreateSplash;
   }

   private void UnregisterEvents()
   {
       submarinePosition.OnChange -= OnPositionChanged;
       bounceEvent.OnEvent -= CreateSplash;
   }

   private void OnPositionChanged(Vector2 oldPos, Vector2 newPos)
   {
       if((oldPos.x != 0 && oldPos.y != 0) && Mathf.Sign(oldPos.y) != Mathf.Sign(newPos.y))
       {
           CreateSplash(oldPos.x +(newPos.x-oldPos.x)/2.0f, diveSplashPrefab);
       }
       else if(newPos.y < waterHeight || newPos.y > heightThreshold)
       {
            StopSplash();
       }
       else if(newPos.y < heightThreshold)
       {
           StartSplash();
       }
   }

   private void StartSplash()
   {
       if(splashRoutine != null)
       {
           return;
       }

       if(splashRunner == null)
       {
            splashRunner = CoroutineRunner.Instantiate(this.name);
       }
       
       splashRoutine = SplashRoutine();
       splashRunner.StartCoroutine(splashRoutine);
   }

   private void StopSplash()
   {
       if(splashRoutine == null)
       {
           return;
       }
       splashRunner.StopCoroutine(splashRoutine);
       splashRoutine = null;
   }

   private IEnumerator SplashRoutine()
   {
       while(true)
       {
           CreateSplash();
           yield return new WaitForSeconds(timeBetweenSplashes);
       }
   }
    
   private void CreateSplash()
   {
       CreateSplash(submarinePosition.Value.x, splashPrefab);
   }

   private void CreateSplash(float xPos)
   {
       CreateSplash(new Vector2(xPos, waterHeight), splashPrefab);
   }

      private void CreateSplash(float xPos, Transform prefab)
   {
       CreateSplash(new Vector2(xPos, waterHeight), prefab);
   }

   private void CreateSplash(Vector2 pos, Transform prefab)
   {
        Transform splash = GameObject.Instantiate<Transform>(prefab, pos, Quaternion.identity);
        splash.localScale = new Vector3(splash.localScale.x * ((submarineBody.Value.velocity.x < 0)? -1 : 1),
                                        splash.localScale.y,
                                        splash.localScale.z);
   }
}
