using SDA.MANAGER.Core.SdaHideControl.Update;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaHideControl
{
    partial class SdaHideControlUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaHideControlUpdate(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaHideControl.Contains(entity.GetType()))
                {
                    ISdaHideControlUpdate behavior = SdaHideControlUpdateBehaviorFactory.MakeISdaHideControlUpdate(param, entity);
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
