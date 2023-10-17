using SDA.MANAGER.Core.SdaCommune.Create;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaCommune
{
    partial class SdaCommuneCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaCommuneCreate(CommonParam param, object data)
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
                    ISdaCommuneCreate behavior = SdaCommuneCreateBehaviorFactory.MakeISdaCommuneCreate(param, entity);
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
