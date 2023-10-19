using ACS.MANAGER.Core.AcsControl.Delete;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsControl
{
    partial class AcsControlDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsControlDelete(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.AcsControl.Contains(entity.GetType()))
                {
                    IAcsControlDelete behavior = AcsControlDeleteBehaviorFactory.MakeIAcsControlDelete(param, entity);
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
