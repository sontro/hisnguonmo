using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMestBltyReq
{
    partial class HisExpMestBltyReqCreate : BusinessBase
    {
        private List<HIS_EXP_MEST_BLTY_REQ> recentHisExpMestBltyReqs = new List<HIS_EXP_MEST_BLTY_REQ>();

        internal HisExpMestBltyReqCreate()
            : base()
        {

        }

        internal HisExpMestBltyReqCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_EXP_MEST_BLTY_REQ data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExpMestBltyReqCheck checker = new HisExpMestBltyReqCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisExpMestBltyReqDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpMestBltyReq_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisExpMestBltyReq that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisExpMestBltyReqs.Add(data);
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

        internal bool CreateList(List<HIS_EXP_MEST_BLTY_REQ> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExpMestBltyReqCheck checker = new HisExpMestBltyReqCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    //ko thuoc goi nao thi set truong IS_OUT_PARENT_FEE = null
                    if (!data.SERE_SERV_PARENT_ID.HasValue)
                    {
                        data.IS_OUT_PARENT_FEE = null;
                    }
                }
                if (valid)
                {
                    if (!DAOWorker.HisExpMestBltyReqDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpMestBltyReq_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisExpMestBltyReq that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisExpMestBltyReqs.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisExpMestBltyReqs))
            {
                if (!new HisExpMestBltyReqTruncate(param).TruncateList(this.recentHisExpMestBltyReqs))
                {
                    LogSystem.Warn("Rollback du lieu HisExpMestBltyReq that bai, can kiem tra lai." + LogUtil.TraceData("recentHisExpMestBltyReqs", this.recentHisExpMestBltyReqs));
                }
            }
        }
    }
}
