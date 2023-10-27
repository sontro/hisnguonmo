using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.AcsApplication;
using ACS.MANAGER.Core.AcsApplication.Get;
using ACS.MANAGER.Core.AcsApplicationRole;
using ACS.MANAGER.Core.AcsApplicationRole.Get;
using ACS.MANAGER.Core.AcsRole;
using ACS.MANAGER.Core.AcsRoleUser;
using ACS.MANAGER.Core.Check;
using Inventec.Core;
using Inventec.Common.Mail;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.MANAGER.Core.AcsUser.Create
{
    class AcsUserCreateBehaviorListSdo : BeanObjectBase, IAcsUserCreate
    {
        List<ACS.SDO.CreateAndGrantUserSDO> entitys;

        internal AcsUserCreateBehaviorListSdo(CommonParam param, List<ACS.SDO.CreateAndGrantUserSDO> data)
            : base(param)
        {
            entitys = data;
        }

        bool IAcsUserCreate.Run()
        {
            bool result = false;
            try
            {
                if (Check())
                {
                    foreach (var item in entitys)
                    {
                        AcsUserBO userBO = new AcsUserBO();
                        result = userBO.Create(item);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        bool Check()
        {
            bool result = true;
            try
            {
                result = result && AcsUserCheckVerifyValidData.Verify(param, entitys);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}
