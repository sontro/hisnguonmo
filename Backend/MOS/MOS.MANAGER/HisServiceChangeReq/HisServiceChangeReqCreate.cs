using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceChangeReq
{
    partial class HisServiceChangeReqCreate : BusinessBase
    {
		private List<HIS_SERVICE_CHANGE_REQ> recentHisServiceChangeReqs = new List<HIS_SERVICE_CHANGE_REQ>();
		
        internal HisServiceChangeReqCreate()
            : base()
        {

        }

        internal HisServiceChangeReqCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_SERVICE_CHANGE_REQ data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceChangeReqCheck checker = new HisServiceChangeReqCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisServiceChangeReqDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceChangeReq_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisServiceChangeReq that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisServiceChangeReqs.Add(data);
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
		
		internal bool CreateList(List<HIS_SERVICE_CHANGE_REQ> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServiceChangeReqCheck checker = new HisServiceChangeReqCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisServiceChangeReqDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceChangeReq_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisServiceChangeReq that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisServiceChangeReqs.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisServiceChangeReqs))
            {
                if (!DAOWorker.HisServiceChangeReqDAO.TruncateList(this.recentHisServiceChangeReqs))
                {
                    LogSystem.Warn("Rollback du lieu HisServiceChangeReq that bai, can kiem tra lai." + LogUtil.TraceData("recentHisServiceChangeReqs", this.recentHisServiceChangeReqs));
                }
				this.recentHisServiceChangeReqs = null;
            }
        }
    }
}
