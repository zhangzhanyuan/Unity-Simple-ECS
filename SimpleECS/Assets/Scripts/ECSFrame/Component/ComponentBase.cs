using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecs
{
    [Serializable]
    public class ComponentBase: IComponent
    {
        /// <summary>
        /// 组件名称
        /// </summary>
        public string ComponentName;

        /// <summary>
        /// 实体
        /// </summary>
        public IEntity Entity { get; set; }
    }
}
