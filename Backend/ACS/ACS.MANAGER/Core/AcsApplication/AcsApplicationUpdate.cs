using ACS.MANAGER.Core.AcsApplication.Update;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsApplication
{
    partial class AcsApplicationUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsApplicationUpdate(CommonParam param, object data)
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
                    IAcsApplicationUpdate behavior = AcsApplicationUpdateBehaviorFactory.MakeIAcsApplicationUpdate(param, entity);
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
