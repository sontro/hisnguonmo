using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisLocationStore
{
    partial class HisLocationStoreUpdate : BusinessBase
    {
		private List<HIS_LOCATION_STORE> beforeUpdateHisLocationStores = new List<HIS_LOCATION_STORE>();
		
        internal HisLocationStoreUpdate()
            : base()
        {

        }

        internal HisLocationStoreUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_LOCATION_STORE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisLocationStoreCheck checker = new HisLocationStoreCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_LOCATION_STORE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.LOCATION_STORE_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisLocationStoreDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisLocationStore_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisLocationStore that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisLocationStores.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_LOCATION_STORE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisLocationStoreCheck checker = new HisLocationStoreCheck(param);
                List<HIS_LOCATION_STORE> listRaw = new List<HIS_LOCATION_STORE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.LOCATION_STORE_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisLocationStoreDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisLocationStore_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisLocationStore that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisLocationStores.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisLocationStores))
            {
                if (!DAOWorker.HisLocationStoreDAO.UpdateList(this.beforeUpdateHisLocationStores))
                {
                    LogSystem.Warn("Rollback du lieu HisLocationStore that bai, can kiem tra lai." + LogUtil.TraceData("HisLocationStores", this.beforeUpdateHisLocationStores));
                }
				this.beforeUpdateHisLocationStores = null;
            }
        }
    }
}
