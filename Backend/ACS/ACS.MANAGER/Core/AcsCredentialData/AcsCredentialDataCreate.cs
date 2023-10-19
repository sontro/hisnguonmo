using ACS.MANAGER.Core.AcsCredentialData.Create;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsCredentialData
{
    partial class AcsCredentialDataCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsCredentialDataCreate(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.AcsCredentialData.Contains(entity.GetType()))
                {
                    IAcsCredentialDataCreate behavior = AcsCredentialDataCreateBehaviorFactory.MakeIAcsCredentialDataCreate(param, entity);
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
