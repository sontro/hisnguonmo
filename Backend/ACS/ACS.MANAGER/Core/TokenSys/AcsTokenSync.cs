using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Core.TokenSys.SyncToken;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.TokenSys
{
    partial class AcsTokenSync : BeanObjectBase, IAcsTokenSync
    {
        object entity;

        internal AcsTokenSync(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsTokenSync.Run()
        {
            bool result = false;
            try
            {
                if (TypeCollection.AcsToken.Contains(entity.GetType()))
                {
                    IAcsTokenSync behavior = AcsTokenSyncBehaviorFactory.MakeIAcsTokenLogin(param, entity);
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
