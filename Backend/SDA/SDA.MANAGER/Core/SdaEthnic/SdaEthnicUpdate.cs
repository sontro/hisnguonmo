using SDA.MANAGER.Core.SdaEthnic.Update;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaEthnic
{
    partial class SdaEthnicUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaEthnicUpdate(CommonParam param, object data)
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
                    ISdaEthnicUpdate behavior = SdaEthnicUpdateBehaviorFactory.MakeISdaEthnicUpdate(param, entity);
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
