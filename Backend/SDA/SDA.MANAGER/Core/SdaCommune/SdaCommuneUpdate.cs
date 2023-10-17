using SDA.MANAGER.Core.SdaCommune.Update;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaCommune
{
    partial class SdaCommuneUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaCommuneUpdate(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaCommune.Contains(entity.GetType()))
                {
                    ISdaCommuneUpdate behavior = SdaCommuneUpdateBehaviorFactory.MakeISdaCommuneUpdate(param, entity);
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
