using SDA.MANAGER.Core.SdaTrouble.Create;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaTrouble
{
    partial class SdaTroubleCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaTroubleCreate(CommonParam param, object data)
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
                    ISdaTroubleCreate behavior = SdaTroubleCreateBehaviorFactory.MakeISdaTroubleCreate(param, entity);
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
