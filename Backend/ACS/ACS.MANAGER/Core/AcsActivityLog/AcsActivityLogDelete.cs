using ACS.MANAGER.Core.AcsActivityLog.Delete;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsActivityLog
{
    partial class AcsActivityLogDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsActivityLogDelete(CommonParam param, object data)
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
                    IAcsActivityLogDelete behavior = AcsActivityLogDeleteBehaviorFactory.MakeIAcsActivityLogDelete(param, entity);
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
