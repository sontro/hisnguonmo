using ACS.EFMODEL.DataModels;
using ACS.LibraryEventLog;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using ACS.MANAGER.EventLogUtil;
using Inventec.Core;
using SDA.SDO;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsRole.Create
{
    class AcsRoleCreateBehaviorListEv : BeanObjectBase, IAcsRoleCreate
    {
        List<ACS_ROLE> entities;

        internal AcsRoleCreateBehaviorListEv(CommonParam param, List<ACS_ROLE> datas)
            : base(param)
        {
            entities = datas;
        }

        bool IAcsRoleCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsRoleDAO.CreateList(entities);
                if (result)
                {
                    CreateEventLog();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        bool Check()
        {
            bool result = true;
            try
            {
                result = result && AcsRoleCheckVerifyValidData.Verify(param, entities);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        void CreateEventLog()
        {
            try
            {
                RoleListData roleListData = new RoleListData();
                roleListData.RoleDatas = entities;
                roleListData.ApplicationCode = Inventec.Token.ResourceSystem.ResourceTokenManager.GetApplicationCode();

                new EventLogGenerator(EventLog.Enum.AcsRole_ThemListVaiTro)                       
                          .RoleListData(roleListData).Run();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
