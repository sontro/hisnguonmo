using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisMestInveUser;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestInventory
{
    partial class HisMestInventoryCreate : BusinessBase
    {
        private List<HIS_MEST_INVENTORY> recentHisMestInventorys = new List<HIS_MEST_INVENTORY>();
        private HIS_MEST_INVENTORY recentHisMestInventory;
        private List<HIS_MEST_INVE_USER> recentHisMestInveUsers = new List<HIS_MEST_INVE_USER>();

        private HisMestInveUserTruncate hisMestInveUserTruncate { get; set; }
        private HisMestInveUserCreate hisMestInveUserCreate { get; set; }

        internal HisMestInventoryCreate()
            : base()
        {

        }

        internal HisMestInventoryCreate(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        void Init()
        {
            this.hisMestInveUserTruncate = new HisMestInveUserTruncate(param);
            this.hisMestInveUserCreate = new HisMestInveUserCreate();
        }

        internal bool Create(HIS_MEST_INVENTORY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMestInventoryCheck checker = new HisMestInventoryCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisMestInventoryDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMestInventory_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMestInventory that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisMestInventorys.Add(data);
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

        internal bool Create(HisMestInventorySDO data, ref HisMestInventoryResultSDO resultData)
        {
            bool result = true;
            try
            {
                this.ProcessMestInventory(data);
                this.ProcessMestInveUser(data);
                this.PassResult(ref resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                this.RollbackData();
                param.HasException = true;
                result = false;
            }
            return result;
        }

        void ProcessMestInventory(HisMestInventorySDO data)
        {
            bool valid = true;
            HIS_MEST_INVENTORY raw = new HisMestInventoryGet().GetByMediStockPeriodId(data.MediStockPeriodId);
            if (raw == null)
            {
                raw = new HIS_MEST_INVENTORY();
                raw.MEDI_STOCK_PERIOD_ID = data.MediStockPeriodId;
                HisMestInventoryCheck checkInvetory = new HisMestInventoryCheck(param);
                valid = valid && checkInvetory.VerifyRequireField(raw);
                if (!valid)
                {
                    throw new Exception("Du lieu bien ban kiem ke khong chinh xac.");
                }
                if (!DAOWorker.HisMestInventoryDAO.Create(raw))
                {
                    throw new Exception("Khong tao duoc bien ban kiem ke");
                }
                this.recentHisMestInventorys.Add(raw);
            }
            this.recentHisMestInventory = raw;
        }

        void ProcessMestInveUser(HisMestInventorySDO data)
        {
            if (recentHisMestInventory != null && data.MestInveUsers != null && data.MestInveUsers.Count > 0)
            {
                data.MestInveUsers.ForEach(o => o.MEST_INVENTORY_ID = recentHisMestInventory.ID);
                List<HIS_MEST_INVE_USER> oldInveUsers = new HisMestInveUserGet().GetByMestInventoryId(recentHisMestInventory.ID);
                if (IsNotNullOrEmpty(oldInveUsers))
                {
                    if (!this.hisMestInveUserTruncate.TruncateList(oldInveUsers))
                    {
                        throw new Exception("Khong xoa duoc du lieu MestInveUser cu.");
                    }
                }
                if (!hisMestInveUserCreate.CreateList(data.MestInveUsers))
                {
                    throw new Exception("Khong tao duoc du lieu MestInveUser.");
                }
                this.recentHisMestInveUsers = data.MestInveUsers;
            }
        }

        void PassResult(ref HisMestInventoryResultSDO resultData)
        {
            resultData = new HisMestInventoryResultSDO();
            resultData.MestInventory = this.recentHisMestInventory;
            resultData.MestInveUsers = this.recentHisMestInveUsers;
        }

        internal bool CreateList(List<HIS_MEST_INVENTORY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMestInventoryCheck checker = new HisMestInventoryCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMestInventoryDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMestInventory_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMestInventory that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisMestInventorys.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisMestInventorys))
            {
                if (!new HisMestInventoryTruncate(param).TruncateList(this.recentHisMestInventorys))
                {
                    LogSystem.Warn("Rollback du lieu HisMestInventory that bai, can kiem tra lai." + LogUtil.TraceData("recentHisMestInventorys", this.recentHisMestInventorys));
                }
            }
        }
    }
}
