using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisPtttGroupBest;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPtttGroup
{
    partial class HisPtttGroupUpdate : BusinessBase
    {
        private List<HIS_PTTT_GROUP> beforeUpdateHisPtttGroups = new List<HIS_PTTT_GROUP>();

        private HisPtttGroupBestCreate hisPtttGroupBestCreate;
        private HisPtttGroupBestTruncate hisPtttGroupBestTruncate;

        internal HisPtttGroupUpdate()
            : base()
        {
            this.hisPtttGroupBestCreate = new HisPtttGroupBestCreate(param);
            this.hisPtttGroupBestTruncate = new HisPtttGroupBestTruncate(param);
        }

        internal HisPtttGroupUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.hisPtttGroupBestCreate = new HisPtttGroupBestCreate(param);
            this.hisPtttGroupBestTruncate = new HisPtttGroupBestTruncate(param);
        }

        internal bool Update(HisPtttGroupSDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPtttGroupCheck checker = new HisPtttGroupCheck(param);
                valid = valid && IsNotNull(data);
                valid = valid && IsNotNull(data.HisPtttGroup);
                HIS_PTTT_GROUP raw = null;
                valid = valid && checker.VerifyId(data.HisPtttGroup.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.HisPtttGroup.PTTT_GROUP_CODE, data.HisPtttGroup.ID);
                valid = valid && checker.HasNoExistedNumOrder(data.HisPtttGroup.NUM_ORDER, true, raw.NUM_ORDER);
                if (valid)
                {
                    if (!DAOWorker.HisPtttGroupDAO.Update(data.HisPtttGroup))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPtttGroup_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPtttGroup that bai." + LogUtil.TraceData("data", data));
                    }
                    this.beforeUpdateHisPtttGroups.Add(raw);

                    this.ProcessGroupBest(data);

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

        private void ProcessGroupBest(HisPtttGroupSDO data)
        {
            List<HIS_PTTT_GROUP_BEST> olds = new HisPtttGroupBestGet().GetByPtttGroupId(data.HisPtttGroup.ID);
            if (IsNotNullOrEmpty(olds) || IsNotNullOrEmpty(data.HisPtttGroupBests))
            {
                List<HIS_PTTT_GROUP_BEST> createList = new List<HIS_PTTT_GROUP_BEST>();
                List<HIS_PTTT_GROUP_BEST> updateList = new List<HIS_PTTT_GROUP_BEST>();
                List<HIS_PTTT_GROUP_BEST> deleteList = new List<HIS_PTTT_GROUP_BEST>();

                if (IsNotNullOrEmpty(data.HisPtttGroupBests))
                {
                    foreach (HIS_PTTT_GROUP_BEST item in data.HisPtttGroupBests)
                    {
                        item.PTTT_GROUP_ID = data.HisPtttGroup.ID;
                        HIS_PTTT_GROUP_BEST old = olds != null ? olds.FirstOrDefault(o => o.BED_SERVICE_TYPE_ID == item.BED_SERVICE_TYPE_ID) : null;
                        if (old != null)
                        {
                            updateList.Add(old);
                            item.ID = old.ID;
                        }
                        else
                        {
                            createList.Add(item);
                        }
                    }
                }

                deleteList = olds != null ? olds.Where(o => !updateList.Exists(e => e.ID == o.ID)).ToList() : null;

                if (IsNotNullOrEmpty(createList))
                {
                    if (!this.hisPtttGroupBestCreate.CreateList(createList))
                    {
                        throw new Exception("hisPtttGroupBestCreate. Ket thuc nghiep vu");
                    }
                }

                if (IsNotNullOrEmpty(deleteList))
                {
                    if (!this.hisPtttGroupBestTruncate.TruncateList(deleteList))
                    {
                        throw new Exception("hisPtttGroupBestTruncate. Ket thuc nghiep vu");
                    }
                }
            }
        }

        internal bool UpdateList(List<HIS_PTTT_GROUP> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPtttGroupCheck checker = new HisPtttGroupCheck(param);
                List<HIS_PTTT_GROUP> listRaw = new List<HIS_PTTT_GROUP>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.PTTT_GROUP_CODE, data.ID);
                }
                if (valid)
                {
                    this.beforeUpdateHisPtttGroups.AddRange(listRaw);
                    if (!DAOWorker.HisPtttGroupDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPtttGroup_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPtttGroup that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisPtttGroups))
            {
                if (!DAOWorker.HisPtttGroupDAO.UpdateList(this.beforeUpdateHisPtttGroups))
                {
                    LogSystem.Warn("Rollback du lieu HisPtttGroup that bai, can kiem tra lai." + LogUtil.TraceData("HisPtttGroups", this.beforeUpdateHisPtttGroups));
                }
            }
        }
    }
}
