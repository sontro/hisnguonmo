using ACS.MANAGER.Core.AcsUser.UpdateListInfo;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsUser
{
    partial class AcsUserUpdateListInfo : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsUserUpdateListInfo(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                IAcsUserUpdateListInfo behavior = AcsUserUpdateListInfoBehaviorFactory.MakeIAcsUserUpdateListInfo(param, entity);
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
