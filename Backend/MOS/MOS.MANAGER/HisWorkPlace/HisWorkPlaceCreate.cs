using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisWorkPlace
{
    partial class HisWorkPlaceCreate : BusinessBase
    {
		private List<HIS_WORK_PLACE> recentHisWorkPlaces = new List<HIS_WORK_PLACE>();
		
        internal HisWorkPlaceCreate()
            : base()
        {

        }

        internal HisWorkPlaceCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_WORK_PLACE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisWorkPlaceCheck checker = new HisWorkPlaceCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.WORK_PLACE_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisWorkPlaceDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisWorkPlace_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisWorkPlace that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisWorkPlaces.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisWorkPlaces))
            {
                if (!new HisWorkPlaceTruncate(param).TruncateList(this.recentHisWorkPlaces))
                {
                    LogSystem.Warn("Rollback du lieu HisWorkPlace that bai, can kiem tra lai." + LogUtil.TraceData("recentHisWorkPlaces", this.recentHisWorkPlaces));
                }
            }
        }
    }
}
