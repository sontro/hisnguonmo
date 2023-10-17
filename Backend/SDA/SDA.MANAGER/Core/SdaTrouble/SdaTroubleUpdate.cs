using SDA.MANAGER.Core.SdaTrouble.Update;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaTrouble
{
    partial class SdaTroubleUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaTroubleUpdate(CommonParam param, object data)
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
                    ISdaTroubleUpdate behavior = SdaTroubleUpdateBehaviorFactory.MakeISdaTroubleUpdate(param, entity);
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
