using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecs;
using UnityEngine;

namespace AGFrame
{
    [Serializable]
    public class MoveTargetComponent : ComponentBase
    {
        //移动速度
        public float speed = 5f;
        //目标位置
        public Vector3 targetPos;

        //是否停止移动
        public bool stopRun = true;
        public void StartRun(Vector3 target)
        {
            targetPos = target;
            stopRun = false;
        }
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
