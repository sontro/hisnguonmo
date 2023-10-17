using SDA.MANAGER.Core.SdaModuleField.Create;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaModuleField
{
    partial class SdaModuleFieldCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaModuleFieldCreate(CommonParam param, object data)
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
                    ISdaModuleFieldCreate behavior = SdaModuleFieldCreateBehaviorFactory.MakeISdaModuleFieldCreate(param, entity);
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
