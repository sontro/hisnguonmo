using SDA.MANAGER.Core.SdaDeleteData.Update;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaDeleteData
{
    partial class SdaDeleteDataUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaDeleteDataUpdate(CommonParam param, object data)
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
                    ISdaDeleteDataUpdate behavior = SdaDeleteDataUpdateBehaviorFactory.MakeISdaDeleteDataUpdate(param, entity);
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
