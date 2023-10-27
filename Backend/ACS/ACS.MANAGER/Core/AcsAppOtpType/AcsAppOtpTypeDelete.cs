using ACS.MANAGER.Core.AcsAppOtpType.Delete;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsAppOtpType
{
    partial class AcsAppOtpTypeDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsAppOtpTypeDelete(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.AcsAppOtpType.Contains(entity.GetType()))
                {
                    IAcsAppOtpTypeDelete behavior = AcsAppOtpTypeDeleteBehaviorFactory.MakeIAcsAppOtpTypeDelete(param, entity);
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
