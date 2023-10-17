using SDA.MANAGER.Core.SdaReligion.Create;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaReligion
{
    partial class SdaReligionCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaReligionCreate(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaReligion.Contains(entity.GetType()))
                {
                    ISdaReligionCreate behavior = SdaReligionCreateBehaviorFactory.MakeISdaReligionCreate(param, entity);
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
