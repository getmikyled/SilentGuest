using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PostProcessManager : MonoBehaviour
{
    public static PostProcessManager instance { get; private set; }
    
    [SerializeField] private Material postProcessMaterial;
    
    [Space]
    [SerializeField] private float deathEffectDuration = 2f;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        SetIntensity(0);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    public void PlayDeathEffect()
    {
        StartCoroutine(CoPlayDeathEffect());
    }
    
    private IEnumerator CoPlayDeathEffect()
    {
        float deathEffectTimer = 0f;
        
        postProcessMaterial.SetFloat("_ScreenEffectIntensity", 0);

        while (deathEffectTimer < deathEffectDuration)
        {
            postProcessMaterial.SetFloat("_ScreenEffectIntensity", deathEffectTimer / deathEffectDuration);
            
            deathEffectTimer += Time.deltaTime;
            
            yield return null;
        }
        
        postProcessMaterial.SetFloat("_ScreenEffectIntensity", 1);
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetIntensity(0);
    }

    public void SetIntensity(float intensity)
    {
        postProcessMaterial.SetFloat("_ScreenEffectIntensity", intensity);
    }
}
