using ACS.MANAGER.Core.AcsModule.Delete;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsModule
{
    partial class AcsModuleDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsModuleDelete(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.AcsModule.Contains(entity.GetType()))
                {
                    IAcsModuleDelete behavior = AcsModuleDeleteBehaviorFactory.MakeIAcsModuleDelete(param, entity);
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
