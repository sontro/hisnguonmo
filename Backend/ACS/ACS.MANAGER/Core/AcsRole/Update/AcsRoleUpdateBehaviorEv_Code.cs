using ACS.EFMODEL.DataModels;
using ACS.LibraryEventLog;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using ACS.MANAGER.EventLogUtil;
using Inventec.Core;
using SDA.SDO;
using System;

namespace ACS.MANAGER.Core.AcsRole.Update
{
    class AcsRoleUpdateBehaviorEv : BeanObjectBase, IAcsRoleUpdate
    {
        ACS_ROLE entity;

        internal AcsRoleUpdateBehaviorEv(CommonParam param, ACS_ROLE data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsRoleUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsRoleDAO.Update(entity);
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
                result = result && AcsRoleCheckVerifyValidData.Verify(param, entity);
                result = result && AcsRoleCheckVerifyIsUnlock.Verify(param, entity.ID);
                result = result && AcsRoleCheckVerifyExistsCode.Verify(param, entity.ROLE_CODE, entity.ID);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        void CreateEventLog()
        {
            try
            {
                RoleData roleData = new RoleData();
                roleData.RoleCode = entity.ROLE_CODE;
                roleData.RoleName = entity.ROLE_NAME;

                new EventLogGenerator(EventLog.Enum.AcsRole_SuaVaiTro)
                          .RoleData(roleData).Run();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
