using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyNamespace
{
    public static class UnityUtilities
    {
        public static void ClearAllChildren(Transform parent)
        {
            foreach (Transform child in parent)
            {
                GameObject.DestroyImmediate(child.gameObject);
            }
        }
    }
}
