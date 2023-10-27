using ACS.MANAGER.Core.AcsApplication.Delete;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsApplication
{
    partial class AcsApplicationDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsApplicationDelete(CommonParam param, object data)
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
                    IAcsApplicationDelete behavior = AcsApplicationDeleteBehaviorFactory.MakeIAcsApplicationDelete(param, entity);
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
