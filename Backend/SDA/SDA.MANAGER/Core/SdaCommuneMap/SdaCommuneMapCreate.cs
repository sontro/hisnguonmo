using SDA.MANAGER.Core.SdaCommuneMap.Create;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaCommuneMap
{
    partial class SdaCommuneMapCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaCommuneMapCreate(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaCommuneMap.Contains(entity.GetType()))
                {
                    ISdaCommuneMapCreate behavior = SdaCommuneMapCreateBehaviorFactory.MakeISdaCommuneMapCreate(param, entity);
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
