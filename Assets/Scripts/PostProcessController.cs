using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessController : MonoBehaviour
{
    [SerializeField] private PostProcessVolume postProcessVolume;
    private Vignette vignette;
    private LensDistortion lensDistortion;

    [HideInInspector] public bool vignetteStatus;
    [HideInInspector] public bool lensDistortionStatus;
    
    
    
    void Start()
    {
        postProcessVolume = GetComponent<PostProcessVolume>();
        postProcessVolume.profile.TryGetSettings(out vignette);
        postProcessVolume.profile.TryGetSettings(out lensDistortion);
    }

    private void Update()
    {
        vignette.active = vignetteStatus;
        lensDistortion.active = lensDistortionStatus;
    }


}
