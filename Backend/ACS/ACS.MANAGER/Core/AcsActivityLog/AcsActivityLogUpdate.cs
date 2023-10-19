using ACS.MANAGER.Core.AcsActivityLog.Update;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsActivityLog
{
    partial class AcsActivityLogUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsActivityLogUpdate(CommonParam param, object data)
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
                    IAcsActivityLogUpdate behavior = AcsActivityLogUpdateBehaviorFactory.MakeIAcsActivityLogUpdate(param, entity);
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
