using SDA.MANAGER.Core.SdaCommuneMap.Delete;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaCommuneMap
{
    partial class SdaCommuneMapDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaCommuneMapDelete(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaCommuneMap.Contains(entity.GetType()))
                {
                    ISdaCommuneMapDelete behavior = SdaCommuneMapDeleteBehaviorFactory.MakeISdaCommuneMapDelete(param, entity);
                    result = behavior != null ? behavior.Run() : false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}
