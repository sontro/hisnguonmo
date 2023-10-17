using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediRecord
{
    partial class HisMediRecordCreate : BusinessBase
    {
        private List<HIS_MEDI_RECORD> recentHisMediRecords = new List<HIS_MEDI_RECORD>();

        internal HisMediRecordCreate()
            : base()
        {

        }

        internal HisMediRecordCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MEDI_RECORD data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMediRecordCheck checker = new HisMediRecordCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisMediRecordDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMediRecord_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMediRecord that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisMediRecords.Add(data);
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

        internal bool CreateList(List<HIS_MEDI_RECORD> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMediRecordCheck checker = new HisMediRecordCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMediRecordDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMediRecord_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMediRecord that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisMediRecords.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisMediRecords))
            {
                if (!DAOWorker.HisMediRecordDAO.TruncateList(this.recentHisMediRecords))
                {
                    LogSystem.Warn("Rollback du lieu HisMediRecord that bai, can kiem tra lai." + LogUtil.TraceData("recentHisMediRecords", this.recentHisMediRecords));
                }
                this.recentHisMediRecords = null;
            }
        }
    }
}
