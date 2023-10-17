using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskGeneral
{
    partial class HisKskGeneralCreate : BusinessBase
    {
		private List<HIS_KSK_GENERAL> recentHisKskGenerals = new List<HIS_KSK_GENERAL>();
		
        internal HisKskGeneralCreate()
            : base()
        {

        }

        internal HisKskGeneralCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_KSK_GENERAL data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisKskGeneralCheck checker = new HisKskGeneralCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisKskGeneralDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKskGeneral_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisKskGeneral that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisKskGenerals.Add(data);
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
		
		internal bool CreateList(List<HIS_KSK_GENERAL> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisKskGeneralCheck checker = new HisKskGeneralCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisKskGeneralDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKskGeneral_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisKskGeneral that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisKskGenerals.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisKskGenerals))
            {
                if (!DAOWorker.HisKskGeneralDAO.TruncateList(this.recentHisKskGenerals))
                {
                    LogSystem.Warn("Rollback du lieu HisKskGeneral that bai, can kiem tra lai." + LogUtil.TraceData("recentHisKskGenerals", this.recentHisKskGenerals));
                }
				this.recentHisKskGenerals = null;
            }
        }
    }
}
