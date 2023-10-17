using SDA.MANAGER.Core.SdaReligion.Update;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaReligion
{
    partial class SdaReligionUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaReligionUpdate(CommonParam param, object data)
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
                    ISdaReligionUpdate behavior = SdaReligionUpdateBehaviorFactory.MakeISdaReligionUpdate(param, entity);
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
