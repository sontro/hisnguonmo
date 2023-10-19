using ACS.MANAGER.Core.AcsCredentialData.Delete;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsCredentialData
{
    partial class AcsCredentialDataDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsCredentialDataDelete(CommonParam param, object data)
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
                    IAcsCredentialDataDelete behavior = AcsCredentialDataDeleteBehaviorFactory.MakeIAcsCredentialDataDelete(param, entity);
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
