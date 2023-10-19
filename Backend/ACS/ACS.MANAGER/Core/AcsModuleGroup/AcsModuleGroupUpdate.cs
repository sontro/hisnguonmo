using ACS.MANAGER.Core.AcsModuleGroup.Update;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsModuleGroup
{
    partial class AcsModuleGroupUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsModuleGroupUpdate(CommonParam param, object data)
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
                    IAcsModuleGroupUpdate behavior = AcsModuleGroupUpdateBehaviorFactory.MakeIAcsModuleGroupUpdate(param, entity);
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
