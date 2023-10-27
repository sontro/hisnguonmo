using ACS.MANAGER.Core.AcsModuleGroup.Delete;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsModuleGroup
{
    partial class AcsModuleGroupDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsModuleGroupDelete(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.AcsModuleGroup.Contains(entity.GetType()))
                {
                    IAcsModuleGroupDelete behavior = AcsModuleGroupDeleteBehaviorFactory.MakeIAcsModuleGroupDelete(param, entity);
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
