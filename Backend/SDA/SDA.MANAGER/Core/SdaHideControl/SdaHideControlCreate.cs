using SDA.MANAGER.Core.SdaHideControl.Create;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaHideControl
{
    partial class SdaHideControlCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaHideControlCreate(CommonParam param, object data)
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
                    ISdaHideControlCreate behavior = SdaHideControlCreateBehaviorFactory.MakeISdaHideControlCreate(param, entity);
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
