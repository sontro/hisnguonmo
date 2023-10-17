using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisEmployee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.AggrExam.Export
{
    class ChildProcessor : BusinessBase
    {
        private List<long> recentExpMestIds;
        private List<long> recentServiceReqIds;

        internal ChildProcessor()
            : base()
        {
        }

        internal ChildProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
        }

        internal bool Run(List<HIS_EXP_MEST> children, long finishTime, string loginname, string username)
        {
            try
            {
                if (IsNotNullOrEmpty(children))
                {
                    this.FinishExpMest(children, finishTime, loginname, username);

                    this.FinishServiceReq(children, finishTime);
                }
                return true;
            }
            catch (Exception ex)
            {
                this.Rollback();
                LogSystem.Error(ex);
                return false;
            }
        }


        private void FinishExpMest(List<HIS_EXP_MEST> children, long finishTime, string loginname, string username)
        {
            if (IsNotNullOrEmpty(children))
            {
                List<long> expMestIds = children.Select(o => o.ID).ToList();
                string query = DAOWorker.SqlDAO.AddInClause(expMestIds, "UPDATE HIS_EXP_MEST SET EXP_MEST_STT_ID = :param1, FINISH_TIME = :param2, LAST_EXP_LOGINNAME = :param3, LAST_EXP_USERNAME = :param4, LAST_EXP_TIME = :param5 WHERE %IN_CLAUSE% ", "ID");

                if (!DAOWorker.SqlDAO.Execute(query, IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE, finishTime, loginname, username, finishTime))
                {
                    throw new Exception("Cap nhat trang thai exp_mest cac phieu con that bai. Rollback du lieu. Ket thuc nghiep vu");
                }
                this.recentExpMestIds = expMestIds; //phuc vu rollback
            }
        }

        private void FinishServiceReq(List<HIS_EXP_MEST> children, long finishTime)
        {
            List<long> serviceReqIds = children
                .Where(o => o.SERVICE_REQ_ID.HasValue)
                .Select(o => o.SERVICE_REQ_ID.Value).ToList();
            if (IsNotNullOrEmpty(serviceReqIds))
            {
                
                string query = DAOWorker.SqlDAO.AddInClause(serviceReqIds, "UPDATE HIS_SERVICE_REQ SET SERVICE_REQ_STT_ID = :param1, FINISH_TIME = :param2, EXECUTE_LOGINNAME = :param3, EXECUTE_USERNAME = :param4, EXECUTE_USER_TITLE = :param5 WHERE %IN_CLAUSE% ", "ID");

                long sttId = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT;
                string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                string userName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                string userTitle = HisEmployeeUtil.GetTitle(loginName);

                if (!DAOWorker.SqlDAO.Execute(query, sttId, finishTime, loginName, userName, userTitle))
                {
                    throw new Exception("Cap nhat trang thai service_req cac phieu con that bai. Rollback du lieu. Ket thuc nghiep vu");
                }
                this.recentServiceReqIds = serviceReqIds; //phuc vu rollback
            }
        }

        internal void Rollback()
        {
            try
            {
                this.UnfinishExpMest(this.recentExpMestIds);
                this.UnfinishServiceReq(this.recentServiceReqIds);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        private void UnfinishExpMest(List<long> expMestIds)
        {
            if (IsNotNullOrEmpty(expMestIds))
            {
                string query = DAOWorker.SqlDAO.AddInClause(expMestIds, "UPDATE HIS_EXP_MEST SET EXP_MEST_STT_ID = :param1, FINISH_TIME = NULL, LAST_EXP_LOGINNAME = NULL, LAST_EXP_USERNAME = NULL, LAST_EXP_TIME = NULL WHERE %IN_CLAUSE% ", "ID");
                if (!DAOWorker.SqlDAO.Execute(query, IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__EXECUTE))
                {
                    LogSystem.Warn("Rollback trang thai exp_mest cac phieu con that bai. Rollback du lieu. Ket thuc nghiep vu");
                }
                this.recentExpMestIds = null;
            }
        }

        private void UnfinishServiceReq(List<long> serviceReqIds)
        {
            if (IsNotNullOrEmpty(serviceReqIds))
            {
                string query = DAOWorker.SqlDAO.AddInClause(serviceReqIds, "UPDATE HIS_SERVICE_REQ SET SERVICE_REQ_STT_ID = :param1, FINISH_TIME = NULL, EXECUTE_LOGINNAME = NULL, EXECUTE_USERNAME = NULL WHERE %IN_CLAUSE% ", "ID");
                if (!DAOWorker.SqlDAO.Execute(query, IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__DXL))
                {
                    LogSystem.Warn("Rollback trang thai service_req cac phieu con that bai. Rollback du lieu. Ket thuc nghiep vu");
                }
                this.recentServiceReqIds = null;
            }
        }
    }
}
