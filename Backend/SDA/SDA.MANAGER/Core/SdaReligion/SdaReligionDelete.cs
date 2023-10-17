using SDA.MANAGER.Core.SdaReligion.Delete;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaReligion
{
    partial class SdaReligionDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaReligionDelete(CommonParam param, object data)
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
                    ISdaReligionDelete behavior = SdaReligionDeleteBehaviorFactory.MakeISdaReligionDelete(param, entity);
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
