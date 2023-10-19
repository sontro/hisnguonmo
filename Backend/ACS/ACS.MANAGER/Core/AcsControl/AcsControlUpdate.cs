using ACS.MANAGER.Core.AcsControl.Update;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsControl
{
    partial class AcsControlUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsControlUpdate(CommonParam param, object data)
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
                    IAcsControlUpdate behavior = AcsControlUpdateBehaviorFactory.MakeIAcsControlUpdate(param, entity);
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
