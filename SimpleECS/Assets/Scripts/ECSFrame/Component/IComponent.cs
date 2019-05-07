using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ecs
{
    internal interface IComponent
    {
        IEntity Entity { get; }
    }
}