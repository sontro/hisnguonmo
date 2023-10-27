using ACS.MANAGER.Core.AcsUser.CheckActive;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsUser
{
    partial class AcsUserCheckActive : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsUserCheckActive(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                IAcsUserCheckActive behavior = AcsUserCheckActiveBehaviorFactory.MakeIAcsUserCheckActive(param, entity);
                result = behavior != null ? behavior.Run() : false;
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
