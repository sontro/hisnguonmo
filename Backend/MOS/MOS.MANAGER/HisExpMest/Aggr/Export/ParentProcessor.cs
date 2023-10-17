using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Aggr.Export
{
    class ParentProcessor : BusinessBase
    {
        private long? recentExpMestId;
        private long? old_last_time = null;
        string old_loginname = null;
        string old_username = null;

        internal ParentProcessor()
            : base()
        {
        }

        internal ParentProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
        }

        public bool Run(HIS_EXP_MEST expMest, long expTime, string loginname, string username)
        {
            try
            {
                string query = "UPDATE HIS_EXP_MEST PARENT SET PARENT.IS_EXPORT_EQUAL_APPROVE = 1, PARENT.EXP_MEST_STT_ID = :param1, PARENT.FINISH_TIME = :param2, PARENT.LAST_EXP_LOGINNAME = :param3, PARENT.LAST_EXP_USERNAME = :param4, PARENT.LAST_EXP_TIME = :param5 WHERE PARENT.ID = :param6";

                if (!DAOWorker.SqlDAO.Execute(query, IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE, expTime, loginname, username, expTime, expMest.ID))
                {
                    LogSystem.Warn("Cap nhat trang thai cua phieu linh sang hoan thanh bi that bai");
                    return false;
                }
                this.old_last_time = expMest.LAST_EXP_TIME;
                this.old_loginname = expMest.LAST_EXP_LOGINNAME;
                this.old_username = expMest.LAST_EXP_USERNAME;
                //cap nhat thong tin de tra ve cho ham su dung
                expMest.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE;
                expMest.FINISH_TIME = expTime;
                expMest.LAST_EXP_LOGINNAME = loginname;
                expMest.LAST_EXP_USERNAME = username;
                expMest.LAST_EXP_TIME = expTime;
                this.recentExpMestId = expMest.ID;
                return true;
            }
            catch (Exception ex)
            {
                this.Rollback();
                LogSystem.Error(ex);
                return false;
            }
        }

        internal void Rollback()
        {
            try
            {
                if (this.recentExpMestId.HasValue)
                {
                    string query = "UPDATE HIS_EXP_MEST PARENT SET PARENT.EXP_MEST_STT_ID = :param1, PARENT.FINISH_TIME = null, PARENT.LAST_EXP_LOGINNAME = :param2, PARENT.LAST_EXP_USERNAME = :param3, PARENT.LAST_EXP_TIME = :param4 WHERE PARENT.ID = :param5 ";

                    if (!DAOWorker.SqlDAO.Execute(query, IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__EXECUTE, this.old_loginname, this.old_username, this.old_last_time, this.recentExpMestId.Value))
                    {
                        LogSystem.Warn("Rollback trang thai cua phieu linh bi that bai");
                    }
                    this.recentExpMestId = null;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}
