using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ecs
{
    public sealed class EcsSystemManager: MonoBehaviour
    {
        public List<IEcsSystem> ecsSystemList = new List<IEcsSystem>();
        private Dictionary<Type, IEcsSystem> systemDic = new Dictionary<Type, IEcsSystem>();
        /// <summary>
        /// 添加一个System
        /// </summary>
        /// <param name="system">系统</param>
        public EcsSystemManager Add(IEcsSystem system)
        {
            EcsInjection.Inject(system);
            ecsSystemList.Add(system);
            systemDic.Add(system.GetType(), system);
            return this;
        }

        /// <summary>
        /// 取得系统
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetSystem<T>() where T:class,IEcsSystem
        {
            if (systemDic.ContainsKey(typeof(T)))
                return systemDic[typeof(T)] as T;
            return null;
        }


        private static EcsSystemManager m_Instance = null;

        public static EcsSystemManager Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = GameObject.FindObjectOfType(typeof(EcsSystemManager)) as EcsSystemManager;
                    if (m_Instance == null)
                    {
                        m_Instance =new GameObject("Singleton of " + typeof(EcsSystemManager), typeof(EcsSystemManager)).GetComponent<EcsSystemManager>();
                    }

                }
                return m_Instance;
            }
        }

        private void Awake()
        {
            if (m_Instance == null)
                m_Instance = this as EcsSystemManager;

            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes()
                .Where(t => t.GetInterfaces().Contains(typeof(IEcsSystem)))).ToArray();
            foreach (var v in types)
            {
                IEcsSystem system = (IEcsSystem)Activator.CreateInstance(v);
                system.systemManager = this;
                Add(system);
            }
            for (int i = 0; i < ecsSystemList.Count; i++)
                ecsSystemList[i].Initialize();
        }
        public void Update()
        {
            for (int i = 0; i < ecsSystemList.Count; i++)
                ecsSystemList[i].Run();
        }

        public void OnDestroy()
        {
            m_Instance = null;
            EcsWorld.Instance.ClearAll();
        }
    }
}