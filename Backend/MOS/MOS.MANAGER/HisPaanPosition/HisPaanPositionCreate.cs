using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPaanPosition
{
    partial class HisPaanPositionCreate : BusinessBase
    {
		private List<HIS_PAAN_POSITION> recentHisPaanPositions = new List<HIS_PAAN_POSITION>();
		
        internal HisPaanPositionCreate()
            : base()
        {

        }

        internal HisPaanPositionCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_PAAN_POSITION data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPaanPositionCheck checker = new HisPaanPositionCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisPaanPositionDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPaanPosition_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisPaanPosition that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisPaanPositions.Add(data);
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
		
		internal bool CreateList(List<HIS_PAAN_POSITION> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPaanPositionCheck checker = new HisPaanPositionCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisPaanPositionDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPaanPosition_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisPaanPosition that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisPaanPositions.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisPaanPositions))
            {
                if (!new HisPaanPositionTruncate(param).TruncateList(this.recentHisPaanPositions))
                {
                    LogSystem.Warn("Rollback du lieu HisPaanPosition that bai, can kiem tra lai." + LogUtil.TraceData("recentHisPaanPositions", this.recentHisPaanPositions));
                }
            }
        }
    }
}
