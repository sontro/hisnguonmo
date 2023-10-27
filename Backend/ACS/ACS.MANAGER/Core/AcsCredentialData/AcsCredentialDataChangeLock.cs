using ACS.MANAGER.Core.AcsCredentialData.Lock;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsCredentialData
{
    partial class AcsCredentialDataChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsCredentialDataChangeLock(CommonParam param, object data)
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
                    IAcsCredentialDataChangeLock behavior = AcsCredentialDataChangeLockBehaviorFactory.MakeIAcsCredentialDataChangeLock(param, entity);
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
