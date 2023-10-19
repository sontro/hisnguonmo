using ACS.MANAGER.Core.AcsModuleGroup.Create;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsModuleGroup
{
    partial class AcsModuleGroupCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsModuleGroupCreate(CommonParam param, object data)
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
                    IAcsModuleGroupCreate behavior = AcsModuleGroupCreateBehaviorFactory.MakeIAcsModuleGroupCreate(param, entity);
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
