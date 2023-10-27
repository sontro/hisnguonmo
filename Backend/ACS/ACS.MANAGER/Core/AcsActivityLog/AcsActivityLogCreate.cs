using ACS.MANAGER.Core.AcsActivityLog.Create;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsActivityLog
{
    partial class AcsActivityLogCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsActivityLogCreate(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.AcsActivityLog.Contains(entity.GetType()))
                {
                    IAcsActivityLogCreate behavior = AcsActivityLogCreateBehaviorFactory.MakeIAcsActivityLogCreate(param, entity);
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
