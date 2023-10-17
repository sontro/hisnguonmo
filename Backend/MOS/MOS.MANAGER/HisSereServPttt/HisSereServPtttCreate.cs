using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServPttt
{
    partial class HisSereServPtttCreate : BusinessBase
    {
		private List<HIS_SERE_SERV_PTTT> recentHisSereServPttts = new List<HIS_SERE_SERV_PTTT>();
		
        internal HisSereServPtttCreate()
            : base()
        {

        }

        internal HisSereServPtttCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_SERE_SERV_PTTT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSereServPtttCheck checker = new HisSereServPtttCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisSereServPtttDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServPttt_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisSereServPttt that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisSereServPttts.Add(data);
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

        internal bool CreateList(List<HIS_SERE_SERV_PTTT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSereServPtttCheck checker = new HisSereServPtttCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisSereServPtttDAO.CreateList(listData))
					{
						MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServPttt_ThemMoiThatBai);
							throw new Exception("Them moi thong tin HisSereServPttt that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisSereServPttts.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisSereServPttts))
            {
                if (!DAOWorker.HisSereServPtttDAO.TruncateList(this.recentHisSereServPttts))
                {
                    LogSystem.Warn("Rollback du lieu HisSereServPttt that bai, can kiem tra lai." + LogUtil.TraceData("recentHisSereServPttts", this.recentHisSereServPttts));
                }
            }
        }
    }
}
