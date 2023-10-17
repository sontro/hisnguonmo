using SDA.MANAGER.Core.SdaEthnic.Delete;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaEthnic
{
    partial class SdaEthnicDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaEthnicDelete(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaEthnic.Contains(entity.GetType()))
                {
                    ISdaEthnicDelete behavior = SdaEthnicDeleteBehaviorFactory.MakeISdaEthnicDelete(param, entity);
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
