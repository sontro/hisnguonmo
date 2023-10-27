using ACS.MANAGER.Core.AcsAppOtpType.Update;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsAppOtpType
{
    partial class AcsAppOtpTypeUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsAppOtpTypeUpdate(CommonParam param, object data)
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
                    IAcsAppOtpTypeUpdate behavior = AcsAppOtpTypeUpdateBehaviorFactory.MakeIAcsAppOtpTypeUpdate(param, entity);
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
