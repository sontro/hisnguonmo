using SDA.MANAGER.Core.SdaSqlParam.Create;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaSqlParam
{
    partial class SdaSqlParamCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaSqlParamCreate(CommonParam param, object data)
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
                    ISdaSqlParamCreate behavior = SdaSqlParamCreateBehaviorFactory.MakeISdaSqlParamCreate(param, entity);
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
