using SDA.MANAGER.Core.SdaModuleField.Update;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaModuleField
{
    partial class SdaModuleFieldUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaModuleFieldUpdate(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaModuleField.Contains(entity.GetType()))
                {
                    ISdaModuleFieldUpdate behavior = SdaModuleFieldUpdateBehaviorFactory.MakeISdaModuleFieldUpdate(param, entity);
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
