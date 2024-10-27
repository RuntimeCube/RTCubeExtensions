using RTCube;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RTCube.Extensions
{
    public class AssetLoader : MonoSingleton<AssetLoader>
    {
        internal T Load<T>(string assetPath) where T : UnityEngine.Object
        {
            return (T)Resources.Load(assetPath);
        }
    }
}