using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecs
{
    public interface IEcsSystem
    {
        /// <summary>
        /// 系统管理者
        /// </summary>
        EcsSystemManager systemManager { get; set; }

        /// <summary>
        /// Initializes system inside EcsWorld instance.
        /// </summary>
        void Initialize();

        /// <summary>
        /// 实时刷新逻辑
        /// </summary>
        void Run();

        //当组件收集器被加入实体的时候回调事件
        void WhenCollectEntity(IEcsCollector collector, IEntity entity);
        //当组件收集器被移除实体的时候回调事件
        void WhenRemoveEntity(IEcsCollector collector, IEntity entity);
    }
}
