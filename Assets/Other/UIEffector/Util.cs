using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Framework
{
    public static class Utilities
    {
        public static bool IsBitSet(byte b, int pos)
        {
            return (b & (1 << pos)) != 0;
        }
    }
}
