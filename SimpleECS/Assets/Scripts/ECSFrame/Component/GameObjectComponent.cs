using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Ecs
{
    [System.Serializable]
    public class GameObjectComponent:ComponentBase
    {
        //数据
        public GameObject gameObject;

        //三维
        public Transform transform;
    }
}
