using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ecs
{
    public interface IEntity
    {
        /// <summary>
        /// 实体Id
        /// </summary>
        int EntityId { get; }

        /// <summary>
        /// 组件列表
        /// </summary>
        List<ComponentBase> ComponentList{ get;  }

        /// <summary>
        /// 添加组件
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <returns>返回该组件</returns>
        T AddEcsComponent<T>(String componentName=null) where T : ComponentBase;

        /// <summary>
        /// 添加组件
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <returns>返回该组件</returns>
        T AddEcsComponent<T>(T t) where T : ComponentBase;

        /// <summary>
        /// 移除组件
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        void RemoveEcsComponent<T>() where T : ComponentBase;

        /// <summary>
        /// 移除组件
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        void RemoveEcsComponent<T>(T t) where T : ComponentBase;


        /// <summary>
        /// 取得组件
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        T GetEcsComponent<T>() where T : ComponentBase;

        /// <summary>
        /// 取得组件
        /// </summary>
        ComponentBase GetEcsComponent(Type type);

        /// <summary>
        /// 组件类型的列表
        /// </summary>
        List<Type> GetComponentTypeList();

        /// <summary>
        /// 清除
        /// </summary>
        void Destroy();
    }
}