using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisVaccReactPlace
{
    partial class HisVaccReactPlaceUpdate : BusinessBase
    {
		private List<HIS_VACC_REACT_PLACE> beforeUpdateHisVaccReactPlaces = new List<HIS_VACC_REACT_PLACE>();
		
        internal HisVaccReactPlaceUpdate()
            : base()
        {

        }

        internal HisVaccReactPlaceUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_VACC_REACT_PLACE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisVaccReactPlaceCheck checker = new HisVaccReactPlaceCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_VACC_REACT_PLACE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.VACC_REACT_PLACE_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisVaccReactPlaceDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaccReactPlace_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisVaccReactPlace that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisVaccReactPlaces.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_VACC_REACT_PLACE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisVaccReactPlaceCheck checker = new HisVaccReactPlaceCheck(param);
                List<HIS_VACC_REACT_PLACE> listRaw = new List<HIS_VACC_REACT_PLACE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.VACC_REACT_PLACE_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisVaccReactPlaceDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaccReactPlace_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisVaccReactPlace that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisVaccReactPlaces.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisVaccReactPlaces))
            {
                if (!DAOWorker.HisVaccReactPlaceDAO.UpdateList(this.beforeUpdateHisVaccReactPlaces))
                {
                    LogSystem.Warn("Rollback du lieu HisVaccReactPlace that bai, can kiem tra lai." + LogUtil.TraceData("HisVaccReactPlaces", this.beforeUpdateHisVaccReactPlaces));
                }
				this.beforeUpdateHisVaccReactPlaces = null;
            }
        }
    }
}
