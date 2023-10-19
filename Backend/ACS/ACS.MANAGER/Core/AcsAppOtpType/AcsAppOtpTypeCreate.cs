using ACS.MANAGER.Core.AcsAppOtpType.Create;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsAppOtpType
{
    partial class AcsAppOtpTypeCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsAppOtpTypeCreate(CommonParam param, object data)
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
                    IAcsAppOtpTypeCreate behavior = AcsAppOtpTypeCreateBehaviorFactory.MakeIAcsAppOtpTypeCreate(param, entity);
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
