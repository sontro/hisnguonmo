using SDA.MANAGER.Core.SdaDeleteData.Lock;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaDeleteData
{
    partial class SdaDeleteDataChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaDeleteDataChangeLock(CommonParam param, object data)
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
                    ISdaDeleteDataChangeLock behavior = SdaDeleteDataChangeLockBehaviorFactory.MakeISdaDeleteDataChangeLock(param, entity);
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
