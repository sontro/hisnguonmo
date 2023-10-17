using SDA.MANAGER.Core.SdaModuleField.Delete;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaModuleField
{
    partial class SdaModuleFieldDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaModuleFieldDelete(CommonParam param, object data)
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
                    ISdaModuleFieldDelete behavior = SdaModuleFieldDeleteBehaviorFactory.MakeISdaModuleFieldDelete(param, entity);
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
