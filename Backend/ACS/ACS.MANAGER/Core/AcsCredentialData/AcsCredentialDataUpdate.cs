using ACS.MANAGER.Core.AcsCredentialData.Update;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsCredentialData
{
    partial class AcsCredentialDataUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsCredentialDataUpdate(CommonParam param, object data)
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
                    IAcsCredentialDataUpdate behavior = AcsCredentialDataUpdateBehaviorFactory.MakeIAcsCredentialDataUpdate(param, entity);
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
