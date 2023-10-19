using ACS.MANAGER.Core.AcsModule.Create;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsModule
{
    partial class AcsModuleCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsModuleCreate(CommonParam param, object data)
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
                    IAcsModuleCreate behavior = AcsModuleCreateBehaviorFactory.MakeIAcsModuleCreate(param, entity);
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
