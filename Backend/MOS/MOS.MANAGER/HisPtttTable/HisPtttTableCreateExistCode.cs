using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPtttTable
{
    partial class HisPtttTableCreate : BusinessBase
    {
		private List<HIS_PTTT_TABLE> recentHisPtttTables = new List<HIS_PTTT_TABLE>();
		
        internal HisPtttTableCreate()
            : base()
        {

        }

        internal HisPtttTableCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_PTTT_TABLE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPtttTableCheck checker = new HisPtttTableCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.PTTT_TABLE_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisPtttTableDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPtttTable_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisPtttTable that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisPtttTables.Add(data);
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

        internal bool CreateList(List<HIS_PTTT_TABLE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPtttTableCheck checker = new HisPtttTableCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.PTTT_TABLE_CODE, null);
                }
                if (valid)
                {
                    result = DAOWorker.HisPtttTableDAO.CreateList(listData);
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
            if (IsNotNullOrEmpty(this.recentHisPtttTables))
            {
                if (!DAOWorker.HisPtttTableDAO.TruncateList(this.recentHisPtttTables))
                {
                    LogSystem.Warn("Rollback du lieu HisPtttTable that bai, can kiem tra lai." + LogUtil.TraceData("recentHisPtttTables", this.recentHisPtttTables));
                }
				this.recentHisPtttTables = null;
            }
        }
    }
}
