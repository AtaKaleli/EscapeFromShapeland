using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessController : MonoBehaviour
{
    [SerializeField] private PostProcessVolume postProcessVolume;
    private Vignette vignette;

    [HideInInspector] public bool isActive;
    
    
    
    void Start()
    {
        postProcessVolume = GetComponent<PostProcessVolume>();
        postProcessVolume.profile.TryGetSettings(out vignette);
    }

    private void Update()
    {
        vignette.active = isActive;
    }


}
