using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public interface InputManagment
    {
        InputDirection? DetectInputDirection();
    }

    public enum InputDirection
    {
        Left, Right, Top, Bottom
    }
}
