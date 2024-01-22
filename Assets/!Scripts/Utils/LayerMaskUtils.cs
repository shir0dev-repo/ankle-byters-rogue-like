using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LayerMaskUtils
{
    public static bool IsLayer(this LayerMask targetMask, int layer)
    {
        return ((1 << layer) & targetMask) != 0;
    }
}
