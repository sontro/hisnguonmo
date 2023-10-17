using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccReactPlace
{
    partial class HisVaccReactPlaceCreate : BusinessBase
    {
		private List<HIS_VACC_REACT_PLACE> recentHisVaccReactPlaces = new List<HIS_VACC_REACT_PLACE>();
		
        internal HisVaccReactPlaceCreate()
            : base()
        {

        }

        internal HisVaccReactPlaceCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_VACC_REACT_PLACE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisVaccReactPlaceCheck checker = new HisVaccReactPlaceCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.VACC_REACT_PLACE_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisVaccReactPlaceDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaccReactPlace_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisVaccReactPlace that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisVaccReactPlaces.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisVaccReactPlaces))
            {
                if (!DAOWorker.HisVaccReactPlaceDAO.TruncateList(this.recentHisVaccReactPlaces))
                {
                    LogSystem.Warn("Rollback du lieu HisVaccReactPlace that bai, can kiem tra lai." + LogUtil.TraceData("recentHisVaccReactPlaces", this.recentHisVaccReactPlaces));
                }
				this.recentHisVaccReactPlaces = null;
            }
        }
    }
}
