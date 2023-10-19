using ACS.MANAGER.Core.AcsAppOtpType.Lock;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsAppOtpType
{
    partial class AcsAppOtpTypeChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsAppOtpTypeChangeLock(CommonParam param, object data)
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
                    IAcsAppOtpTypeChangeLock behavior = AcsAppOtpTypeChangeLockBehaviorFactory.MakeIAcsAppOtpTypeChangeLock(param, entity);
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
