using ACS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.ApiConsumerManager;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.AcsUser;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisEmployee
{
    partial class HisEmployeeUpdate : BusinessBase
    {
		private List<HIS_EMPLOYEE> beforeUpdateHisEmployees = new List<HIS_EMPLOYEE>();
        internal HisEmployeeUpdate()
            : base()
        {

        }

        internal HisEmployeeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_EMPLOYEE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEmployeeCheck checker = new HisEmployeeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_EMPLOYEE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.IsNoExists(data);
                ACS_USER acs = new AcsUserGet().GetByLoginName(raw.LOGINNAME);
                if (valid && acs != null)
                {
                    this.beforeUpdateHisEmployees.Add(raw);
					if (!DAOWorker.HisEmployeeDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEmployee_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisEmployee that bai." + LogUtil.TraceData("data", data));
                    }
                    acs.LOGINNAME = data.LOGINNAME;
                    acs.USERNAME = data.TDL_USERNAME;
                    acs.MOBILE = data.TDL_MOBILE;
                    acs.EMAIL = data.TDL_EMAIL;
                    acs.CREATE_TIME = data.CREATE_TIME;
                    acs.MODIFY_TIME = data.MODIFY_TIME;
                    acs.MODIFIER = data.MODIFIER;
                    acs.GROUP_CODE = data.GROUP_CODE;
                    acs.APP_CREATOR = data.APP_CREATOR;
                    acs.APP_MODIFIER = data.APP_MODIFIER;
                    acs.IS_DELETE = data.IS_DELETE;

                    var ro = ApiConsumerStore.AcsConsumer.Post<Inventec.Core.ApiResultObject<ACS_USER>>("/api/AcsUser/Update",param, acs);
                    if (ro == null)
                    {
                        throw new Exception("Cap nhat thong tin AcsUser that bai." + LogUtil.TraceData("acs", acs));
                    }

                    
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool UpdateList(List<HIS_EMPLOYEE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisEmployeeCheck checker = new HisEmployeeCheck(param);
                List<HIS_EMPLOYEE> listRaw = new List<HIS_EMPLOYEE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.IsNoExists(data);
                }
                if (valid)
                {
					this.beforeUpdateHisEmployees.AddRange(listRaw);
					if (!DAOWorker.HisEmployeeDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEmployee_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisEmployee that bai." + LogUtil.TraceData("listData", listData));
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
		
		internal void RollbackData()
        {
            if (IsNotNullOrEmpty(this.beforeUpdateHisEmployees))
            {
                if (!new HisEmployeeUpdate(param).UpdateList(this.beforeUpdateHisEmployees))
                {
                    LogSystem.Warn("Rollback du lieu HisEmployee that bai, can kiem tra lai." + LogUtil.TraceData("HisEmployees", this.beforeUpdateHisEmployees));
                }
            }
        }
    }
}
