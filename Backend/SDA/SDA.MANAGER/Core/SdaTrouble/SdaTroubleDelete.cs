using SDA.MANAGER.Core.SdaTrouble.Delete;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaTrouble
{
    partial class SdaTroubleDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaTroubleDelete(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaTrouble.Contains(entity.GetType()))
                {
                    ISdaTroubleDelete behavior = SdaTroubleDeleteBehaviorFactory.MakeISdaTroubleDelete(param, entity);
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
