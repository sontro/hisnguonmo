using SDA.MANAGER.Core.SdaDeleteData.Create;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaDeleteData
{
    partial class SdaDeleteDataCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaDeleteDataCreate(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaDeleteData.Contains(entity.GetType()))
                {
                    ISdaDeleteDataCreate behavior = SdaDeleteDataCreateBehaviorFactory.MakeISdaDeleteDataCreate(param, entity);
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
