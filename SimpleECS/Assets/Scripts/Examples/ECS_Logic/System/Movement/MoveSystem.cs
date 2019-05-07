using System;
using System.Collections.Generic;
using UnityEngine;
using Ecs;

namespace AGFrame
{
    public class MoveSystem : IEcsSystem
    {
        //该组件收集器包含了所有携带MoveComponent组件的实体
        public ComponentCollector<GameObjectComponent,MoveComponent> moveCollector;
        //该组件收集器包含了所有携带MoveTargetComponent组件的实体
        public ComponentCollector<GameObjectComponent,MoveTargetComponent> moveTargetCollector;
        //该组件收集器包含了所有携带MoveDirComponent组件的实体
        public ComponentCollector<GameObjectComponent,MoveDirComponent> moveDirCollector;
        #region 系统流程
        public void Initialize()
        {
           
        }
        public EcsSystemManager systemManager { get; set; }
        public void Run()
        {
            //直接移动
            for (int i = 0; i < moveCollector.EntityList.Count; i++)
            {
                MoveComponent moveComponent = moveCollector.EntityList[i].GetEcsComponent<MoveComponent>();
                if (moveComponent.speed <= 0||moveComponent.stopRun)
                    continue;
                Vector3 temp;
                float x = Mathf.Cos(moveComponent.Angle);
                float z = Mathf.Sin(moveComponent.Angle);

                temp.x = x * moveComponent.speed * Time.deltaTime;
                temp.y = 0;
                temp.z = z * moveComponent.speed * Time.deltaTime;
                moveComponent.Entity.GetEcsComponent<GameObjectComponent>().transform.position += temp;
                Vector3 customVector3 = moveComponent.Entity.GetEcsComponent<GameObjectComponent>().transform.position;
                moveComponent.Entity.GetEcsComponent<GameObjectComponent>().transform.position = new Vector3(customVector3.x, moveComponent.hight, customVector3.z);
            }

            //固定目标移动
            for (int i = 0; i < moveTargetCollector.EntityList.Count; i++)
            {
                MoveTargetComponent moveTargetComponent = moveTargetCollector.EntityList[i].GetEcsComponent<MoveTargetComponent>();
                GameObjectComponent gameObjectComponent = moveTargetCollector.EntityList[i].GetEcsComponent<GameObjectComponent>();
                if (moveTargetComponent.speed <= 0||moveTargetComponent.stopRun)
                    continue;
                if (Vector3.Distance(gameObjectComponent.transform.position, moveTargetComponent.targetPos)<0.05f)
                {
                    gameObjectComponent.transform.position = moveTargetComponent.targetPos;
                    continue;
                }
                gameObjectComponent.transform.position = Vector3.MoveTowards(gameObjectComponent.transform.position, moveTargetComponent.targetPos, moveTargetComponent.speed * Time.deltaTime);
            }

            //固定方向移动
            for (int i = 0; i < moveDirCollector.EntityList.Count; i++)
            {
                MoveDirComponent moveDirComponent = moveDirCollector.EntityList[i].GetEcsComponent<MoveDirComponent>();
                if (moveDirComponent.speed <= 0 || moveDirComponent.stopRun)
                    continue;
                IEntity entity = moveDirCollector.EntityList[i];
                entity.GetEcsComponent<GameObjectComponent>().transform.position = entity.GetEcsComponent<GameObjectComponent>().transform.position + moveDirComponent.dir * moveDirComponent.speed * Time.deltaTime;
            }
        }

        public void WhenCollectEntity(IEcsCollector collector, IEntity entity)
        {
            
        }

        public void WhenRemoveEntity(IEcsCollector collector, IEntity entity)
        {
            
        }
        #endregion
    }
}
