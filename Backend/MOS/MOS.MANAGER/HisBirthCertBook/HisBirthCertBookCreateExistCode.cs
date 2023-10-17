using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBirthCertBook
{
    partial class HisBirthCertBookCreate : BusinessBase
    {
		private List<HIS_BIRTH_CERT_BOOK> recentHisBirthCertBooks = new List<HIS_BIRTH_CERT_BOOK>();
		
        internal HisBirthCertBookCreate()
            : base()
        {

        }

        internal HisBirthCertBookCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_BIRTH_CERT_BOOK data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBirthCertBookCheck checker = new HisBirthCertBookCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.BIRTH_CERT_BOOK_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisBirthCertBookDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBirthCertBook_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBirthCertBook that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisBirthCertBooks.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisBirthCertBooks))
            {
                if (!DAOWorker.HisBirthCertBookDAO.TruncateList(this.recentHisBirthCertBooks))
                {
                    LogSystem.Warn("Rollback du lieu HisBirthCertBook that bai, can kiem tra lai." + LogUtil.TraceData("recentHisBirthCertBooks", this.recentHisBirthCertBooks));
                }
				this.recentHisBirthCertBooks = null;
            }
        }
    }
}
