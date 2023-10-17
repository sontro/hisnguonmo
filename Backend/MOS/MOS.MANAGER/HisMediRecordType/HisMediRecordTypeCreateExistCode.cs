using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediRecordType
{
    partial class HisMediRecordTypeCreate : BusinessBase
    {
		private List<HIS_MEDI_RECORD_TYPE> recentHisMediRecordTypes = new List<HIS_MEDI_RECORD_TYPE>();
		
        internal HisMediRecordTypeCreate()
            : base()
        {

        }

        internal HisMediRecordTypeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MEDI_RECORD_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMediRecordTypeCheck checker = new HisMediRecordTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.MEDI_RECORD_TYPE_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisMediRecordTypeDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMediRecordType_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMediRecordType that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisMediRecordTypes.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisMediRecordTypes))
            {
                if (!DAOWorker.HisMediRecordTypeDAO.TruncateList(this.recentHisMediRecordTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisMediRecordType that bai, can kiem tra lai." + LogUtil.TraceData("recentHisMediRecordTypes", this.recentHisMediRecordTypes));
                }
				this.recentHisMediRecordTypes = null;
            }
        }
    }
}
