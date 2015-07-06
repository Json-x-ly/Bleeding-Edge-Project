using System;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
    [ExecuteInEditMode]
    [AddComponentMenu("Image Effects/Color Adjustments/Grayscale")]
    public class ScanlineEffects : ImageEffectBase {
        Bloom bloom;
        void OnEnabled() {
        }
        // Called by camera to apply image effect
        void OnRenderImage (RenderTexture source, RenderTexture destination) {
            Graphics.Blit (source, destination, material);
        }
    }
}
