using ACS.MANAGER.Core.AcsApplication.Create;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsApplication
{
    partial class AcsApplicationCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsApplicationCreate(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.AcsApplication.Contains(entity.GetType()))
                {
                    IAcsApplicationCreate behavior = AcsApplicationCreateBehaviorFactory.MakeIAcsApplicationCreate(param, entity);
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
