using UnityEngine;
// VisualEffectBehavior.cs
public abstract class ActionEffectBehaviour : MonoBehaviour
{
    // When an artist makes a visual effect, they generally make a GameObject Prefab.
    // You can extend this base class to support different kinds of visual effects
    // such as particle systems, post-processing screen effects, etc.
    public abstract void PlayEffect();
}

