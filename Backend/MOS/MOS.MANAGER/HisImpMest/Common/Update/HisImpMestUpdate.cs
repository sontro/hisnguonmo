using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisImpMest
{
    class HisImpMestUpdate : BusinessBase
    {
        private List<HIS_IMP_MEST> beforeUpdateHisImpMestDTOs = new List<HIS_IMP_MEST>();

        internal HisImpMestUpdate()
            : base()
        {

        }

        internal HisImpMestUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_IMP_MEST data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_IMP_MEST raw = null;
                HisImpMestCheck checker = new HisImpMestCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.IMP_MEST_CODE, data.ID);
                valid = valid && checker.HasNotMediStockPeriod(raw);
                valid = valid && checker.IsUnLockMediStock(raw);
                valid = valid && checker.VerifyStatusForUpdate(raw);
                valid = valid && checker.HasNoNationalCode(raw);
                valid = valid && checker.IsValidChangeType(data.IMP_MEST_TYPE_ID, raw.IMP_MEST_TYPE_ID);
                valid = valid && checker.IsValidChangeType(data.MEDI_STOCK_ID, raw.MEDI_STOCK_ID);
                valid = valid && checker.IsNotHasImpMestProposeId(raw, null);
                if (valid)
                {
                    data.MODIFY_TIME = Inventec.Common.DateTime.Get.Now();
                    if (!DAOWorker.HisImpMestDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpMest_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisImpMest that bai." + LogUtil.TraceData("data", data));
                    }
                    this.beforeUpdateHisImpMestDTOs.Add(raw);

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

        internal bool Update(HIS_IMP_MEST data, HIS_IMP_MEST before)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisImpMestCheck checker = new HisImpMestCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.IsUnLock(before);
                if (valid)
                {
                    if (!DAOWorker.HisImpMestDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpMest_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisImpMest that bai." + LogUtil.TraceData("data", data));
                    }

                    this.beforeUpdateHisImpMestDTOs.Add(before);
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

        internal bool UpdateList(List<HIS_IMP_MEST> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisImpMestCheck checker = new HisImpMestCheck(param);
                List<HIS_IMP_MEST> listRaw = new List<HIS_IMP_MEST>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.IMP_MEST_CODE, data.ID);
                }
                foreach (var raw in listRaw)
                {
                    valid = valid && checker.HasNotMediStockPeriod(raw);
                    valid = valid && checker.VerifyStatusForUpdate(raw);
                    valid = valid && checker.IsUnLockMediStock(raw);
                }
                if (valid)
                {
                    this.beforeUpdateHisImpMestDTOs.AddRange(listRaw);
                    if (!DAOWorker.HisImpMestDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpMest_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisImpMest that bai." + LogUtil.TraceData("listData", listData));
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

        internal bool UpdateList(List<HIS_IMP_MEST> listData, List<HIS_IMP_MEST> beforeUpdates)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(listData);
                valid = valid && IsNotNullOrEmpty(beforeUpdates);
                HisImpMestCheck checker = new HisImpMestCheck(param);
                valid = valid && checker.IsUnLock(beforeUpdates);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisImpMestDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpMest_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisImpMest that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.beforeUpdateHisImpMestDTOs.AddRange(beforeUpdates);
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

        /// <summary>
        /// Update phuc vu cho chot ky.
        /// - Chi cho phep cap nhat gia tri MEDI_STOCK_PERIOD_ID
        /// - Chi cho phep cap nhat voi cac exp_mest da thuc xuat, ko bi khoa, va chua co gia tri MEDI_STOCK_PERIOD_ID
        /// </summary>
        /// <param name="listData"></param>
        /// <returns></returns>
        internal bool UpdateListForMediStockPeriod(List<HIS_IMP_MEST> listData)
        {
            bool result = false;
            try
            {
                bool valid = IsNotNullOrEmpty(listData);

                HisImpMestCheck checker = new HisImpMestCheck(param);
                List<HIS_IMP_MEST> listRaw = new List<HIS_IMP_MEST>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);

                //luu lai truoc khi update de phuc vu rollback
                Mapper.CreateMap<HIS_IMP_MEST, HIS_IMP_MEST>();
                List<HIS_IMP_MEST> beforeUpdates = Mapper.Map<List<HIS_IMP_MEST>>(listRaw);
                this.beforeUpdateHisImpMestDTOs.AddRange(beforeUpdates);

                foreach (var raw in listRaw)
                {
                    valid = valid && checker.HasNotMediStockPeriod(raw);
                    valid = valid && checker.IsUnLockMediStock(raw);
                    valid = valid && checker.VerifyStatusForMediStockPeriodUpdate(raw);
                    //chi cho phep cap nhat MEDI_STOCK_PERIOD_ID
                    raw.MEDI_STOCK_PERIOD_ID = listData.Where(o => o.ID == raw.ID).FirstOrDefault().MEDI_STOCK_PERIOD_ID;
                }
                if (valid)
                {
                    if (!DAOWorker.HisImpMestDAO.UpdateList(listRaw))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpMest_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisImpMest that bai." + LogUtil.TraceData("listRaw", listRaw));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisImpMestDTOs))
            {
                if (!DAOWorker.HisImpMestDAO.UpdateList(this.beforeUpdateHisImpMestDTOs))
                {
                    LogSystem.Warn("Rollback du lieu HisImpMest that bai, can kiem tra lai." + LogUtil.TraceData("HisImpMestDTO", this.beforeUpdateHisImpMestDTOs));
                }
            }
        }
    }
}
