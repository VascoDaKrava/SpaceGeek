﻿using System;
using UnityEngine;

namespace GeekSpace
{
    public sealed class PCInputHorizontal : IUserInputProxy
    {
        public event Action<float> AxisOnChange = delegate (float f) { };

        public void GetAxis()
        {
            AxisOnChange.Invoke(Input.GetAxis(InputManager.HORIZONTAL));
        }
    }
}