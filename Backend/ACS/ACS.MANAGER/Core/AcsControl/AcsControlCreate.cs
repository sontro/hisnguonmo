using ACS.MANAGER.Core.AcsControl.Create;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsControl
{
    partial class AcsControlCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsControlCreate(CommonParam param, object data)
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
                    IAcsControlCreate behavior = AcsControlCreateBehaviorFactory.MakeIAcsControlCreate(param, entity);
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
