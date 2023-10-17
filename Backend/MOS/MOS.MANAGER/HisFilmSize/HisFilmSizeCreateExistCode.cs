using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisFilmSize
{
    partial class HisFilmSizeCreate : BusinessBase
    {
		private List<HIS_FILM_SIZE> recentHisFilmSizes = new List<HIS_FILM_SIZE>();
		
        internal HisFilmSizeCreate()
            : base()
        {

        }

        internal HisFilmSizeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_FILM_SIZE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisFilmSizeCheck checker = new HisFilmSizeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.FILM_SIZE_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisFilmSizeDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisFilmSize_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisFilmSize that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisFilmSizes.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisFilmSizes))
            {
                if (!DAOWorker.HisFilmSizeDAO.TruncateList(this.recentHisFilmSizes))
                {
                    LogSystem.Warn("Rollback du lieu HisFilmSize that bai, can kiem tra lai." + LogUtil.TraceData("recentHisFilmSizes", this.recentHisFilmSizes));
                }
				this.recentHisFilmSizes = null;
            }
        }
    }
}
