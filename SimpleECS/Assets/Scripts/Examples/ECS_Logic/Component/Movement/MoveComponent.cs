using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecs;
using UnityEngine;
namespace AGFrame
{
    [Serializable]
    public class MoveComponent : ComponentBase
    {
        [SerializeField]
        [HideInInspector]
        private float mLocalAngle;
        /// <summary>
        /// 自身角度
        /// </summary>
        public float LocalAngle
        {
            set
            {
                mLocalAngle = value;
                Vector3 vec = Vector3.zero;
                vec.y = -mLocalAngle;
                Entity.GetEcsComponent<GameObjectComponent>().transform.localEulerAngles = vec;
            }
            get
            {
                return mLocalAngle;
            }
        }
        /// <summary>
        /// 角度
        /// </summary>
        public float Angle
        {
            set
            {
                float mAngle = value;
                LocalAngle = mAngle;
            }
            get
            {
                float mAngle = mLocalAngle;
                return mAngle;
            }
        }


        public float hight = 1f;
        public float speed = 3f;
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
