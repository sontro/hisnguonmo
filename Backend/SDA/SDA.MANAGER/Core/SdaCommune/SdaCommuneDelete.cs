using SDA.MANAGER.Core.SdaCommune.Delete;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaCommune
{
    partial class SdaCommuneDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaCommuneDelete(CommonParam param, object data)
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
                    ISdaCommuneDelete behavior = SdaCommuneDeleteBehaviorFactory.MakeISdaCommuneDelete(param, entity);
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
