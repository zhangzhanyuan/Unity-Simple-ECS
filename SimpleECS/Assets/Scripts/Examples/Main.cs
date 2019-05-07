using Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AGFrame;
public class Main : MonoBehaviour
{
    void Start()
    {

        //Examples 1  固定目标移动
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        IEntity cubeEntity = EcsWorld.Instance.CreatEntity<MonoBehaviourEntity>(cube);
        MoveTargetComponent moveTargetComponent= cubeEntity.AddEcsComponent<MoveTargetComponent>();
        moveTargetComponent.targetPos = new Vector3(0,0, 5);
        moveTargetComponent.StartRun();

        //Examples 2  固定方向移动
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        IEntity sphereEntity = EcsWorld.Instance.CreatEntity<MonoBehaviourEntity>(sphere);
        MoveDirComponent moveDirComponent = sphereEntity.AddEcsComponent<MoveDirComponent>();
        moveDirComponent.dir = new Vector3(0, 0, 1);
        moveDirComponent.StartRun();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
