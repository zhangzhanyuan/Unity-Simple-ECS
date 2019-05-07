using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using strange.extensions.signal.impl;

namespace Ecs
{
    public interface  IEcsCollector
    {
        Signal<IEcsCollector, IEntity> AddEntiySignal { get; }
        Signal<IEcsCollector, IEntity> RemoveEntiySignal { get; }

        /// <summary>
        /// 实体列表
        /// </summary>
        List<IEntity> EntityList { get;}

        /// <summary>
        /// 添加实体
        /// </summary>
        void AddEntiy(IEntity entity);

        /// <summary>
        /// 移除实体
        /// </summary>
        void RemoveEntiy<T>(IEntity entity) where T : ComponentBase;

        /// <summary>
        /// 取得实体
        /// </summary>
        /// <param name="id">Id</param>
        IEntity GetEntity(int id);

        /// <summary>
        /// 取得某个类型的实体列表
        /// </summary>
        /// <typeparam name="TEntity">类型</typeparam>
        /// <returns>实体列表</returns>
        List<TEntity> GetEntityWithType<TEntity>() where TEntity : IEntity;
    }
}
