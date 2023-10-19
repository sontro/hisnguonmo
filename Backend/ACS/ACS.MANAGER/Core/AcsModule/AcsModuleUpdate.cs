using ACS.MANAGER.Core.AcsModule.Update;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsModule
{
    partial class AcsModuleUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsModuleUpdate(CommonParam param, object data)
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
                    IAcsModuleUpdate behavior = AcsModuleUpdateBehaviorFactory.MakeIAcsModuleUpdate(param, entity);
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
