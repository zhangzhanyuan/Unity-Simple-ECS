﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Ecs
{
    public class MonoBehaviourEntity : MonoBehaviour, IEntity
    {
        /// <summary>
        /// 组件列表
        /// </summary>
        public List<ComponentBase> componentList = new List<ComponentBase>();

        /// <summary>
        /// 实体Id
        /// </summary>
        public int EntityId { get ; private set; }

        /// <summary>
        /// 组件列表属性
        /// </summary>
        public List<ComponentBase> ComponentList { get { return componentList; }}

        /// <summary>
        /// 增加组件
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <returns>返回该组件对象</returns>
        public T AddEcsComponent<T>(String componentName=null) where T : ComponentBase
        {
            RemoveEcsComponent<T>();
            T t= Activator.CreateInstance<T>();
            t.Entity = this;
            if (String.IsNullOrEmpty(componentName))
                t.ComponentName = typeof(T).Name;
            else
                t.ComponentName = componentName;
            componentList.Add(t);
            EcsWorld.Instance.AddComponentToCollector(this);
            return t;
        }
        /// <summary>
        /// 增加组件
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <returns>返回该组件对象</returns>
        public T AddEcsComponent<T>(T t) where T : ComponentBase
        {
            RemoveEcsComponent<T>(t);
            t.Entity = this;
            if(String.IsNullOrEmpty(t.ComponentName))
               t.ComponentName = typeof(T).Name;
            componentList.Add(t);

            EcsWorld.Instance.AddComponentToCollector(this);
            return t;
        }

        /// <summary>
        /// 移除组件
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        public void RemoveEcsComponent<T>() where T : ComponentBase
        {
            for (int i = 0; i < componentList.Count; i++)
            {
                if (componentList[i].GetType() == (typeof(T)))
                {
                    EcsWorld.Instance.RomveComponentToCollector<T>(this);
                }
            }
        }

        /// <summary>
        ///  移除组件
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <param name="t">组件对象</param>
        public void RemoveEcsComponent<T>(T t) where T : ComponentBase
        {
            for (int i = 0; i < componentList.Count; i++)
            {
                if (componentList[i]== t)
                {
                    EcsWorld.Instance.RomveComponentToCollector<T>(this);
                }
            }
        }


        /// <summary>
        /// 取得组件
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        public T GetEcsComponent<T>() where T : ComponentBase
        {
            for (int i = 0; i < componentList.Count; i++)
            {
                if (componentList[i].GetType() == typeof(T))
                    return componentList[i] as T;
            }
            Debug.LogError(this+"没有找到组件：" + typeof(T));
            return null;
        }

        /// <summary>
        /// 取得组件
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public ComponentBase GetEcsComponent(Type type)
        {
            for (int i = 0; i < componentList.Count; i++)
            {
                if (componentList[i].GetType() == type)
                   return componentList[i] ;
            }
            Debug.LogError(this + "没有找到组件：" + type);
            return null;
        }


        protected virtual void Awake()
        {
            EntityId = EcsWorld.Instance.entityIdQueue.Dequeue();
            EcsWorld.Instance.entityList.Add(this);
            GameObjectComponent objectComponent=AddEcsComponent<GameObjectComponent>();
            objectComponent.gameObject = gameObject;
            objectComponent.transform = transform;
        }

        protected virtual void OnDestroy()
        {
            EcsWorld.Instance.entityList.Remove(this);
            for (int i = 0; i < EcsWorld.Instance.ecsCollectorList.Count; i++)
            {
                if (EcsWorld.Instance.ecsCollectorList[i].EntityList.Contains(this))
                    EcsWorld.Instance.ecsCollectorList[i].EntityList.Remove(this);
            }
            componentList.Clear();
        }

        public List<Type> GetComponentTypeList()
        {
            List<Type> types = new List<Type>();
            for (int i = 0; i < componentList.Count; i++)
                types.Add(componentList[i].GetType());
            return types;
        }

        //清除
        public void Destroy()
        {
            Destroy(gameObject);
        }

        public override string ToString()
        {
            return gameObject.name;
        }
    }
}
