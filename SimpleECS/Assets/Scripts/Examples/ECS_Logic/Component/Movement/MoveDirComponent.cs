using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Ecs;

namespace AGFrame
{
    [Serializable]
    public class MoveDirComponent: ComponentBase
    {
        public float speed=2;
        public Vector3 dir;
        //是否停止移动
        public bool stopRun = true;
        public void StartRun()
        {
            stopRun = false;
        }

        public void StopRun()
        {
            stopRun = true;
        }
    }
}
