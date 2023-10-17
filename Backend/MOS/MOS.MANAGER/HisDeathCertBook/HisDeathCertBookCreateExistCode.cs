using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDeathCertBook
{
    partial class HisDeathCertBookCreate : BusinessBase
    {
		private List<HIS_DEATH_CERT_BOOK> recentHisDeathCertBooks = new List<HIS_DEATH_CERT_BOOK>();
		
        internal HisDeathCertBookCreate()
            : base()
        {

        }

        internal HisDeathCertBookCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_DEATH_CERT_BOOK data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDeathCertBookCheck checker = new HisDeathCertBookCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.DEATH_CERT_BOOK_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisDeathCertBookDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDeathCertBook_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisDeathCertBook that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisDeathCertBooks.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisDeathCertBooks))
            {
                if (!DAOWorker.HisDeathCertBookDAO.TruncateList(this.recentHisDeathCertBooks))
                {
                    LogSystem.Warn("Rollback du lieu HisDeathCertBook that bai, can kiem tra lai." + LogUtil.TraceData("recentHisDeathCertBooks", this.recentHisDeathCertBooks));
                }
				this.recentHisDeathCertBooks = null;
            }
        }
    }
}
