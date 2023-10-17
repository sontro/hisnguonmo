using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTransReq
{
    partial class HisTransReqCreate : BusinessBase
    {
        private List<HIS_TRANS_REQ> recentHisTransReqs = new List<HIS_TRANS_REQ>();

        internal HisTransReqCreate()
            : base()
        {

        }

        internal HisTransReqCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_TRANS_REQ data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTransReqCheck checker = new HisTransReqCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.TRANS_REQ_CODE, null);
                if (valid)
                {
                    if (!DAOWorker.HisTransReqDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTransReq_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisTransReq that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisTransReqs.Add(data);
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

        internal bool CreateList(List<HIS_TRANS_REQ> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTransReqCheck checker = new HisTransReqCheck(param);
                foreach (HIS_TRANS_REQ data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.TRANS_REQ_CODE, null);
                }
                if (valid)
                {
                    if (!DAOWorker.HisTransReqDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTransReq_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisTransReq that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisTransReqs.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisTransReqs))
            {
                if (!DAOWorker.HisTransReqDAO.TruncateList(this.recentHisTransReqs))
                {
                    LogSystem.Warn("Rollback du lieu HisTransReq that bai, can kiem tra lai." + LogUtil.TraceData("recentHisTransReqs", this.recentHisTransReqs));
                }
                this.recentHisTransReqs = null;
            }
        }
    }
}
