//============================================================
// Project: DigitalFactory
// Author: Zoranner@ZORANNER
// Datetime: 2019-05-14 10:38:20
// Description: TODO >> This is a script Description.
//============================================================

using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace Zoranner.Engine.Extensions
{
    public enum RenderStyle
    {
        Opaque,
        Cutout,
        Fade,
        Transparent
    }

    public static class MaterialExtensions
    {
        /// <summary>
        /// Material渲染模式
        /// </summary>
        /// <param name="material"></param>
        /// <param name="renderMode"></param>
        public static void SetRenderStyle(this Material material, RenderStyle renderMode)
        {
            switch (renderMode)
            {
                case RenderStyle.Opaque:
                    material.SetInt("_SrcBlend", (int) BlendMode.One);
                    material.SetInt("_DstBlend", (int) BlendMode.Zero);
                    material.SetInt("_ZWrite", 1);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.DisableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = -1;
                    break;
                case RenderStyle.Cutout:
                    material.SetInt("_SrcBlend", (int) BlendMode.One);
                    material.SetInt("_DstBlend", (int) BlendMode.Zero);
                    material.SetInt("_ZWrite", 1);
                    material.EnableKeyword("_ALPHATEST_ON");
                    material.DisableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = 2450;
                    break;
                case RenderStyle.Fade:
                    material.SetInt("_SrcBlend", (int) BlendMode.SrcAlpha);
                    material.SetInt("_DstBlend", (int) BlendMode.OneMinusSrcAlpha);
                    material.SetInt("_ZWrite", 0);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.EnableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = 3000;
                    break;
                case RenderStyle.Transparent:
                    material.SetInt("_SrcBlend", (int) BlendMode.One);
                    material.SetInt("_DstBlend", (int) BlendMode.OneMinusSrcAlpha);
                    material.SetInt("_ZWrite", 0);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.DisableKeyword("_ALPHABLEND_ON");
                    material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = 3000;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(renderMode), renderMode, null);
            }
        }
    }
}