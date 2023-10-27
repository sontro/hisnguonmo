using ACS.MANAGER.Core.AcsOtpType.Create;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsOtpType
{
    partial class AcsOtpTypeCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsOtpTypeCreate(CommonParam param, object data)
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
                    IAcsOtpTypeCreate behavior = AcsOtpTypeCreateBehaviorFactory.MakeIAcsOtpTypeCreate(param, entity);
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
