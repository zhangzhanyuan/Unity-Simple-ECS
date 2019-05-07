using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Ecs
{
    static class EcsInjection
    {
        public static void Inject(IEcsSystem system)
        {
            var systemType = system.GetType();
            FieldInfo[] fieldInfos = systemType.GetFields();
            for (int i = 0; i < fieldInfos.Length; i++)
            {
                FieldInfo fieldInfo = fieldInfos[i];
                if (typeof(IEcsCollector).IsAssignableFrom(fieldInfo.FieldType))
                {
                    for (int t = 0; t < fieldInfo.FieldType.GenericTypeArguments.Length; t++)
                    {
                        IEcsCollector collectorValue = EcsWorld.Instance.GetCollector(fieldInfo.FieldType);
                        fieldInfo.SetValue(system, collectorValue);
                        collectorValue.AddEntiySignal.AddListener(system.WhenCollectEntity);
                        collectorValue.RemoveEntiySignal.AddListener(system.WhenRemoveEntity);
                    }
                }
            }
        }
    }
}
