using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBillGoods
{
    partial class HisBillGoodsUpdate : BusinessBase
    {
		private List<HIS_BILL_GOODS> beforeUpdateHisBillGoodss = new List<HIS_BILL_GOODS>();
		
        internal HisBillGoodsUpdate()
            : base()
        {

        }

        internal HisBillGoodsUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_BILL_GOODS data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBillGoodsCheck checker = new HisBillGoodsCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_BILL_GOODS raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.BILL_GOODS_CODE, data.ID);
                if (valid)
                {
                    this.beforeUpdateHisBillGoodss.Add(raw);
					if (!DAOWorker.HisBillGoodsDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBillGoods_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBillGoods that bai." + LogUtil.TraceData("data", data));
                    }
                    
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

        internal bool UpdateList(List<HIS_BILL_GOODS> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBillGoodsCheck checker = new HisBillGoodsCheck(param);
                List<HIS_BILL_GOODS> listRaw = new List<HIS_BILL_GOODS>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.BILL_GOODS_CODE, data.ID);
                }
                if (valid)
                {
					this.beforeUpdateHisBillGoodss.AddRange(listRaw);
					if (!DAOWorker.HisBillGoodsDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBillGoods_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBillGoods that bai." + LogUtil.TraceData("listData", listData));
                    }
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisBillGoodss))
            {
                if (!new HisBillGoodsUpdate(param).UpdateList(this.beforeUpdateHisBillGoodss))
                {
                    LogSystem.Warn("Rollback du lieu HisBillGoods that bai, can kiem tra lai." + LogUtil.TraceData("HisBillGoodss", this.beforeUpdateHisBillGoodss));
                }
            }
        }
    }
}
