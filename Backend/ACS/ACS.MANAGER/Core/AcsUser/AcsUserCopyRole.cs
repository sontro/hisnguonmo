using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Core.AcsUser.CopyRole;
using ACS.MANAGER.Core.AcsUser.Create;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsUser
{
    partial class AcsUserCopyRole : BeanObjectBase, IAcsUserCopyRole
    {
        object entity;

        internal AcsUserCopyRole(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }
        
        public List<ACS_ROLE_USER> Run()
        {
            List<ACS_ROLE_USER> result = null;
            try
            {
                if (TypeCollection.AcsUser.Contains(entity.GetType()))
                {
                    IAcsUserCopyRole behavior = AcsUserCopyRoleBehaviorFactory.MakeIAcsUserCopyRole(param, entity);
                    result = behavior != null ? behavior.Run() : null;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
