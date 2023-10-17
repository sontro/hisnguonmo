using ACS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.ApiConsumerManager;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEmployee
{
    partial class HisEmployeeCreate : BusinessBase
    {
		private List<HIS_EMPLOYEE> recentHisEmployees = new List<HIS_EMPLOYEE>();
		
        internal HisEmployeeCreate()
            : base()
        {

        }

        internal HisEmployeeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_EMPLOYEE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEmployeeCheck checker = new HisEmployeeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.IsNoExists(data);
                if (valid)
                {
					if (!DAOWorker.HisEmployeeDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEmployee_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisEmployee that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisEmployees.Add(data);
                    ACS_USER acs = new ACS_USER();
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
                    acs.IS_ACTIVE = data.IS_ACTIVE;

                    var ro = ApiConsumerStore.AcsConsumer.Post<Inventec.Core.ApiResultObject<ACS_USER>>("/api/AcsUser/Create", param, acs);
                    if (ro == null)
                    {
                        throw new Exception("Them moi thong tin AcsUser that bai." + LogUtil.TraceData("acs", acs));
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
		
		internal bool CreateList(List<HIS_EMPLOYEE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisEmployeeCheck checker = new HisEmployeeCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.IsNoExists(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisEmployeeDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEmployee_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisEmployee that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisEmployees.AddRange(listData);
                    List<ACS_USER> acsUsers = new List<ACS_USER>();
                    foreach (var item in listData)
                    {
                        ACS_USER acs = new ACS_USER();
                        acs.LOGINNAME = item.LOGINNAME;
                        acs.USERNAME = item.TDL_USERNAME;
                        acs.MOBILE = item.TDL_MOBILE;
                        acs.EMAIL = item.TDL_EMAIL;

                        acsUsers.Add(acs);
                    }

                    var ro = ApiConsumerStore.AcsConsumer.Post<Inventec.Core.ApiResultObject<List<ACS_USER>>>("/api/AcsUser/CreateList", param, acsUsers);
                    if (ro == null)
                    {
                        throw new Exception("Them moi thong tin AcsUser that bai." + LogUtil.TraceData("acs", acsUsers));
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
            if (IsNotNullOrEmpty(this.recentHisEmployees))
            {
                if (!new HisEmployeeTruncate(param).TruncateList(this.recentHisEmployees))
                {
                    LogSystem.Warn("Rollback du lieu HisEmployee that bai, can kiem tra lai." + LogUtil.TraceData("recentHisEmployees", this.recentHisEmployees));
                }
            }
        }
    }
}
