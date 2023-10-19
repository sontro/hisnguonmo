using ACS.MANAGER.Core.AcsOtpType.Delete;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsOtpType
{
    partial class AcsOtpTypeDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsOtpTypeDelete(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.AcsOtpType.Contains(entity.GetType()))
                {
                    IAcsOtpTypeDelete behavior = AcsOtpTypeDeleteBehaviorFactory.MakeIAcsOtpTypeDelete(param, entity);
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
