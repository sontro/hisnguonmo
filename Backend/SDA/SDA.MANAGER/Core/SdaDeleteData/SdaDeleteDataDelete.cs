using SDA.MANAGER.Core.SdaDeleteData.Delete;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaDeleteData
{
    partial class SdaDeleteDataDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaDeleteDataDelete(CommonParam param, object data)
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
                    ISdaDeleteDataDelete behavior = SdaDeleteDataDeleteBehaviorFactory.MakeISdaDeleteDataDelete(param, entity);
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
