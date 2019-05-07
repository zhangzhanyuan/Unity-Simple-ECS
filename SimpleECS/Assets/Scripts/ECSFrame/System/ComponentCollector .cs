using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.signal.impl;
using UniRx;

namespace Ecs
{
    //组件收集器
    public class ComponentCollector<T> : IEcsCollector where T:ComponentBase
    {
        public Signal<IEcsCollector, IEntity> AddEntiySignal { get; private set; }
        public Signal<IEcsCollector, IEntity> RemoveEntiySignal { get; private set; }

        /// <summary>
        /// 实体List
        /// </summary>
        public List<IEntity> EntityList { get; protected set; }
        public ComponentCollector()
        {
            AddEntiySignal = new Signal<IEcsCollector, IEntity>();
            RemoveEntiySignal = new Signal<IEcsCollector, IEntity>();
            EntityList = new List<IEntity>();
        }
        public void AddEntiy(IEntity entity)
        {
            if (entity.GetEcsComponent<T>() == null)
                return;
            if (EntityList.Contains(entity))
                return;

            EntityList.Add(entity);
            AddEntiySignal.Dispatch(this, entity);
        }
        public void RemoveEntiy<TComponent>(IEntity entity) where TComponent : ComponentBase
        {
            if (EntityList.Contains(entity))
                return;
            EntityList.Remove(entity);
            RemoveEntiySignal.Dispatch(this, entity);
            Observable.TimerFrame(1).Subscribe(x =>
            {
                if (entity.ComponentList.Contains(entity.GetEcsComponent<TComponent>()))
                    entity.ComponentList.Remove(entity.GetEcsComponent<TComponent>());
            });
        }

        public List<TEntity> GetEntityWithType<TEntity>() where TEntity : IEntity
        {
            List<TEntity> entities = new List<TEntity>();
            for (int i = 0; i < EntityList.Count; i++)
            {
                if (EntityList[i].GetType() == typeof(TEntity))
                    entities.Add((TEntity)EntityList[i]);
            }
            return entities;
        }

        public IEntity GetEntity(int id)
        {
            for (int i = 0; i < EntityList.Count; i++)
            {
                if (EntityList[i].EntityId == id)
                    return EntityList[i];
            }
            return null;
        }
    }

    //组件收集器
    public class ComponentCollector<T,U> : IEcsCollector where T: ComponentBase where U: ComponentBase
    {
        public Signal<IEcsCollector, IEntity> AddEntiySignal { get; private set; }
        public Signal<IEcsCollector, IEntity> RemoveEntiySignal { get; private set; }

        /// <summary>
        /// 实体List
        /// </summary>
        public List<IEntity> EntityList { get; protected set; }
        public ComponentCollector()
        {
            AddEntiySignal = new Signal<IEcsCollector, IEntity>();
            RemoveEntiySignal = new Signal<IEcsCollector, IEntity>();
            EntityList = new List<IEntity>();
        }
        public void AddEntiy(IEntity entity)
        {
            if (entity.GetEcsComponent<T>() == null)
                return;
            if (entity.GetEcsComponent<U>() == null)
                return;
            if (EntityList.Contains(entity))
                return;

            EntityList.Add(entity);
            AddEntiySignal.Dispatch(this, entity);

        }
        public void RemoveEntiy<TComponent>(IEntity entity) where TComponent : ComponentBase
        {
            if (EntityList.Contains(entity))
                return;
            EntityList.Remove(entity);
            RemoveEntiySignal.Dispatch(this, entity);
            Observable.TimerFrame(1).Subscribe(x =>
            {
                if (entity.ComponentList.Contains(entity.GetEcsComponent<TComponent>()))
                    entity.ComponentList.Remove(entity.GetEcsComponent<TComponent>());
            });
        }
        public IEntity GetEntity(int id)
        {
            for (int i = 0; i < EntityList.Count; i++)
            {
                if (EntityList[i].EntityId == id)
                    return EntityList[i];
            }
            return null;
        }
        public List<TEntity> GetEntityWithType<TEntity>() where TEntity : IEntity
        {
            List<TEntity> entities = new List<TEntity>();
            for (int i = 0; i < EntityList.Count; i++)
            {
                if (EntityList[i].GetType() == typeof(TEntity))
                    entities.Add((TEntity)EntityList[i]);
            }
            return entities;
        }
    }


    //组件收集器
    public class ComponentCollector<T, U,G> : IEcsCollector where T : ComponentBase where U : ComponentBase where G : ComponentBase
    {
        public Signal<IEcsCollector, IEntity> AddEntiySignal { get;private set; }
        public Signal<IEcsCollector, IEntity> RemoveEntiySignal { get; private set; }

        /// <summary>
        /// 实体List
        /// </summary>
        public List<IEntity> EntityList { get; protected set; }
        public ComponentCollector()
        {
            AddEntiySignal = new Signal<IEcsCollector, IEntity>();
            RemoveEntiySignal = new Signal<IEcsCollector,IEntity>();
            EntityList = new List<IEntity>();
        }
        public void AddEntiy(IEntity entity)
        {
            if (entity.GetEcsComponent<T>() == null)
                return;
            if (entity.GetEcsComponent<U>() == null)
                return;
            if (entity.GetEcsComponent<G>() == null)
                return;
            if (EntityList.Contains(entity))
                return;

            EntityList.Add(entity);
            AddEntiySignal.Dispatch(this, entity);
        }
        public void RemoveEntiy<TComponent>(IEntity entity)where TComponent:ComponentBase
        {
            if (EntityList.Contains(entity))
                return;
            EntityList.Remove(entity);
            RemoveEntiySignal.Dispatch(this, entity);
            Observable.TimerFrame(1).Subscribe(x =>
            {
                if (entity.ComponentList.Contains(entity.GetEcsComponent<TComponent>()))
                    entity.ComponentList.Remove(entity.GetEcsComponent<TComponent>());
            });
        }
        public IEntity GetEntity(int id)
        {
            for (int i = 0; i < EntityList.Count; i++)
            {
                if (EntityList[i].EntityId == id)
                    return EntityList[i];
            }
            return null;
        }
        public List<TEntity> GetEntityWithType<TEntity>() where TEntity : IEntity
        {
            List<TEntity> entities = new List<TEntity>();
            for (int i = 0; i < EntityList.Count; i++)
            {
                if (EntityList[i].GetType() == typeof(TEntity))
                    entities.Add((TEntity)EntityList[i]);
            }
            return entities;
        }
    }
}
