using SDA.MANAGER.Core.SdaSqlParam.Update;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaSqlParam
{
    partial class SdaSqlParamUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaSqlParamUpdate(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaSqlParam.Contains(entity.GetType()))
                {
                    ISdaSqlParamUpdate behavior = SdaSqlParamUpdateBehaviorFactory.MakeISdaSqlParamUpdate(param, entity);
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
