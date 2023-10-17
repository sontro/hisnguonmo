using SDA.MANAGER.Core.SdaSqlParam.Delete;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaSqlParam
{
    partial class SdaSqlParamDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaSqlParamDelete(CommonParam param, object data)
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
                    ISdaSqlParamDelete behavior = SdaSqlParamDeleteBehaviorFactory.MakeISdaSqlParamDelete(param, entity);
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
