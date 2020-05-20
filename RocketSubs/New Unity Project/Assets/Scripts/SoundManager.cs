using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : ScriptableObject
{
   public AudioClip ChargerDestroy;
   public AudioClip ChargeReady;
   public AudioClip Charging;
   public AudioClip Charge;
   public AudioClip EnemyHurt;
   public AudioClip GhostBubble;
   public AudioClip ghostPossess;
   public AudioClip ghostUnpossess;
   public AudioClip MeeleeAttacker;
   public AudioClip SelfHurt;
   public AudioClip ShooterRock;
   public AudioClip TotemDestroy;
   public AudioClip RockDestroy;

   public AudioClip MeleeWalk;
   public AudioClip BullWalk;
   public AudioClip GhostWalk;
   public AudioClip Death;

   private float defaultVolume = 0.1f;
   private float reduceVolume = 0.04f;
   private bool isReduced = false;

    public AudioSource PlayEnemyHurt()
    {
        return Play(EnemyHurt); 
    }
    
    public AudioSource PlayGhostBubble()
    {
        return Play(GhostBubble); 
    }
    
    public AudioSource PlayGhostPossess()
    {
        return Play(ghostPossess); 
    }

    public AudioSource PlayGhostUnpossess()
    {
        return Play(ghostUnpossess); 
    }
    
    public AudioSource PlayMeeleeAttacker()
    {
        return Play(MeeleeAttacker);  
    }
    
    public AudioSource PlaySelfHurt()
    {
        return Play(SelfHurt); 
    }
    
    public AudioSource PlayShooterRock()
    {
        return Play(ShooterRock);
    }

    public void StopAllSounds(float time)
    {
        ServiceLocator.Instance.StartCoroutine(ReduceAllSounds(time));
    }

    private IEnumerator ReduceAllSounds(float time)
    {
        AudioSource[] sources = ServiceLocator.Instance.GetComponents<AudioSource>();
        foreach(AudioSource source in sources)
        {
            source.volume = reduceVolume;
        }
        isReduced = true;

        yield return new WaitForSeconds(time);
        foreach(AudioSource source in sources)
        {
            if(source != null)
            {
                source.volume = defaultVolume;
            }
        }
        isReduced = false;
    }
    
    public AudioSource PlayTotemDestroy()
    {
        return Play(TotemDestroy);    
    }
    
    public AudioSource PlayRockDestroy()
    {
        return Play(RockDestroy);
    }

    public AudioSource PlayChargerDestroy()
    {
        return Play(ChargerDestroy);
    }
    

    public AudioSource Play(AudioClip clip)
    {
        AudioSource source = ServiceLocator.Instance.gameObject.AddComponent<AudioSource>();
        ServiceLocator.Instance.StartCoroutine(InstantiateAndPlayClip(clip,source));
        return source;
    }

    private IEnumerator InstantiateAndPlayClip(AudioClip clip, AudioSource source)
    { 
        source.clip = clip;
        source.volume =  isReduced? reduceVolume : defaultVolume;
        source.Play();
        yield return new WaitForSeconds(clip.length);
        while( source.isPlaying )
        {
            yield return null;    
        }
         
        if(source != null)
        {
            Destroy(source);
        }
        
    }
}
