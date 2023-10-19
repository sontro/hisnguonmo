using ACS.EFMODEL.DataModels;
using ACS.LibraryEventLog;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.AcsRole;
using ACS.MANAGER.Core.AcsRole.Get;
using ACS.MANAGER.Core.AcsRoleUser.Get;
using ACS.MANAGER.Core.Check;
using ACS.MANAGER.EventLogUtil;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.MANAGER.Core.AcsRoleUser.Delete
{
    class AcsRoleUserDeleteBehaviorListId : BeanObjectBase, IAcsRoleUserDelete
    {
        List<ACS_ROLE_USER> listDeletes;
        private List<string> RoleUsers;
        private ACS_ROLE recentRole;
        List<long> entity;

        internal AcsRoleUserDeleteBehaviorListId(CommonParam param, List<long> data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsRoleUserDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsRoleUserDAO.TruncateList(listDeletes);
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
                foreach (var item in entity)
                {
                    result = result && AcsRoleUserCheckVerifyIsUnlock.Verify(param, item);
                }

                AcsRoleUserFilterQuery moduleRoleFilterQuery = new AcsRoleUserFilterQuery();
                moduleRoleFilterQuery.IDs = entity;
                this.listDeletes = DAOWorker.AcsRoleUserDAO.Get(moduleRoleFilterQuery.Query(), param);
                result = result && (this.listDeletes != null && this.listDeletes.Count > 0);
                if (!result || this.listDeletes == null || this.listDeletes.Count == 0)
                {
                    Inventec.Common.Logging.LogSystem.Info("Kiem tra dieu kien truoc khi xoa RoleUser tra ve khong hop le.____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity) + "____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => listDeletes), listDeletes));
                }

                if (result)
                {
                    //var RoleUserIds = this.listDeletes.Select(o => o.ROLE_BASE_ID).ToList();
                    //var roleId = this.listDeletes.Select(o => o.ROLE_ID).First();
                    //var rs = new AcsRoleBO().Get<List<ACS_ROLE>>(new AcsRoleFilterQuery() { IDs = RoleUserIds });
                    //if (rs != null && rs.Count > 0)
                    //{
                    //    RoleUsers = rs.Select(o => o.ROLE_CODE + " - " + o.ROLE_NAME).ToList();
                    //}

                    //recentRole = new AcsRoleBO().Get<ACS_ROLE>(roleId) ?? new ACS_ROLE();
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

        void CreateEventLog()
        {
            try
            {
                //RoleUserData roleListData = new RoleUserData();
                //roleListData.RoleUserCodes = RoleUsers;
                //roleListData.RoleCode = recentRole.ROLE_CODE;
                //roleListData.RoleName = recentRole.ROLE_NAME;
                //new EventLogGenerator(EventLog.Enum.AcsRole_XoaVaiTroKeThua)
                //          .RoleUserData(roleListData).Run();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
