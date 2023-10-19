using ACS.MANAGER.Core.AcsOtpType.Update;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsOtpType
{
    partial class AcsOtpTypeUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsOtpTypeUpdate(CommonParam param, object data)
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
                    IAcsOtpTypeUpdate behavior = AcsOtpTypeUpdateBehaviorFactory.MakeIAcsOtpTypeUpdate(param, entity);
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
