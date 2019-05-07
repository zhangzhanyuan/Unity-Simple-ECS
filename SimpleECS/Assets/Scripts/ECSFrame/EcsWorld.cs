using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections;
using UniRx;

namespace Ecs
{
    internal class EcsWorld
    {
        #region 单例
        private static EcsWorld _instance = null;
        private static readonly object SynObject = new object();
        internal static EcsWorld Instance
        {
            get
            {
                // Syn operation.
                lock (SynObject)
                {
                    return _instance ?? (_instance = new EcsWorld());
                }
            }
        }

        private EcsWorld()
        {
            for (int i = 0; i < 100000; i++)
                entityIdQueue.Enqueue(i);
        }
        #endregion

        /// <summary>
        /// 实体Id队列
        /// </summary>
        public Queue<int> entityIdQueue = new Queue<int>();

        /// <summary>
        /// 所有的实体都存放在这个数组里面
        /// </summary>
        internal List<IEntity> entityList = new List<IEntity>();

        /// <summary>
        /// 所有的组件收集器
        /// </summary>

        internal List<IEcsCollector> ecsCollectorList = new List<IEcsCollector>();  

        // 取得实体List
        public List<IEntity> GetEntities(Type type)
        {
            List<IEntity> entities = new List<IEntity>();
            for (int i = 0; i < entityList.Count; i++)
            {
                IEntity entity = entityList[i];
                IComponent component = entity.GetEcsComponent(type);
                if (component != null)
                    entities.Add(entity);
            }
            return entities;
        }

        // 取得组件收集器
        public List<IEcsCollector> GetCollectorWithComponent(List<Type> componentTypeList)
        {
            List<IEcsCollector> list = new List<IEcsCollector>();
            for (int i = 0; i < ecsCollectorList.Count; i++)
            {
                IEcsCollector ecsCollector = ecsCollectorList[i];
                List<Type> genericTypeList = new List<Type>(ecsCollector.GetType().GenericTypeArguments);
                if (CollectorHasComponent(genericTypeList, componentTypeList))
                    list.Add(ecsCollector);
            }
            return list;
        }

        //把实体从收集器中添加
        public void AddComponentToCollector(IEntity entity)
        {
            Observable.TimerFrame(1).Subscribe(x =>{
                List<IEcsCollector> collectorList = GetCollectorWithComponent(entity.GetComponentTypeList());
                for (int i = 0; i < collectorList.Count; i++)
                    collectorList[i].AddEntiy(entity);
            });
        }

        //把实体从收集器中移除
        public void RomveComponentToCollector<T>(IEntity entity)where T:ComponentBase
        {
            for (int i = 0; i < ecsCollectorList.Count; i++)
            {
                IEcsCollector ecsCollector = ecsCollectorList[i];
                //判断收集器中是否包含该实体，以及判断收集器是否包含T泛型
                if (ecsCollector.EntityList.Contains(entity) 
                    &&Array.IndexOf(ecsCollector.GetType().GenericTypeArguments, typeof(T)) != -1)
                {
                    ecsCollector.RemoveEntiy<T>(entity);
                }
            }
        }

        //创建实体Mono
        public T CreatEntity<T>(GameObject obj) where T :MonoBehaviour,IEntity
        {
            T t= obj.AddComponent<T>();
            return t;
        }

        //清理所有实体
        public void ClearAll()
        {
            for (int i = 0; i < entityList.Count; i++)
            {
                IEntity entity = entityList[i];
                entity.Destroy();
            }
            entityList.Clear();

            for (int i = 0; i < ecsCollectorList.Count; i++)
                ecsCollectorList[i].EntityList.Clear();

        }

        //创建实体
        public IEntity CreatEntity()
        {
            IEntity entity = Activator.CreateInstance<Entity>();
            return entity;
        }

        // 取得组件收集器
        public IEcsCollector GetCollector(Type collectorType)
        {
            for (int i = 0; i < ecsCollectorList.Count; i++)
            {
                IEcsCollector ecsCollector = ecsCollectorList[i];
                if (collectorType == ecsCollector.GetType())
                    return ecsCollector;
            }
            IEcsCollector collector = (IEcsCollector)Activator.CreateInstance(collectorType);
            ecsCollectorList.Add(collector);
            return collector;
        }

        //判断收集器中是否存在该组件
        private bool CollectorHasComponent(List<Type> genericTypeList, List<Type> componentTypeList)
        {
            for (int i = 0; i < genericTypeList.Count; i++)
            {
                if (!componentTypeList.Contains(genericTypeList[i]))
                    return false;
            }
            return true;
        }
    }
}
