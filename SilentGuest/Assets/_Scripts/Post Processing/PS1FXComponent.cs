using System;
using UnityEngine;
using UnityEngine.Rendering;

///-///////////////////////////////////////////////////////////////////////////////////////////
/// 
[Serializable, VolumeComponentMenu("Custom/PS1")]
public class PS1FXComponent : VolumeComponent, IPostProcessComponent
{
    public ClampedFloatParameter intensity = new(value: 0, min: 0, max: 1, overrideState: true);
    
    // Tells when the effect should be rendered
    public bool IsActive() => intensity.value > 0;
    
    public bool IsTileCompatible() => true;
}
