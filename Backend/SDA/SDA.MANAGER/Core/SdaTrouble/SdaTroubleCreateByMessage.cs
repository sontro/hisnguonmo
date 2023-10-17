using SDA.MANAGER.Core.SdaTrouble.CreateByMessege;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaTrouble
{
    partial class SdaTroubleCreateByMessage : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaTroubleCreateByMessage(CommonParam param, object data)
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
                    ISdaTroubleCreateByMessege behavior = SdaTroubleCreateByMessegeBehaviorFactory.MakeISdaTroubleCreateByMessege(param, entity);
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
