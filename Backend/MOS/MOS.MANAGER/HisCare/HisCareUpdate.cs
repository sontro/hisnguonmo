using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisCareDetail;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisCare
{
    partial class HisCareUpdate : BusinessBase
    {
        private List<HIS_CARE> beforeUpdateHisCares = new List<HIS_CARE>();

        internal HisCareUpdate()
            : base()
        {

        }

        internal HisCareUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_CARE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCareCheck checker = new HisCareCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ValidateData(data);
                HIS_CARE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    this.beforeUpdateHisCares.Add(raw);
                    List<HIS_CARE_DETAIL> hisCareDetails = data.HIS_CARE_DETAIL != null ? data.HIS_CARE_DETAIL.ToList() : null;
                    //can set ve null truoc khi update, vi HIS_CARE_DETAIL duoc xu ly de tao moi, chu ko update. Neu ko set ve null se bi loi khi update
                    data.HIS_CARE_DETAIL = null;
                    data.HIS_DHST = null;
                    data.HIS_CARE_SUM = null;
                    data.HIS_DEPARTMENT = null;
                    data.HIS_TRACKING = null;
                    data.HIS_TREATMENT = null;
                    data.HIS_AWARENESS = null;
                    data.HIS_DHST1 = null;
                    if (!DAOWorker.HisCareDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCare_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisCare that bai." + LogUtil.TraceData("data", data));
                    }
                    this.ProcessCareDetail(hisCareDetails, data.ID);
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

        private void ProcessCareDetail(List<HIS_CARE_DETAIL> hisCareDetails, long careId)
        {
            List<HIS_CARE_DETAIL> oldCareDetails = new HisCareDetailGet().GetByCareId(careId);
            if (IsNotNullOrEmpty(oldCareDetails))
            {
                if (!new HisCareDetailTruncate(param).TruncateList(oldCareDetails))
                {
                    throw new Exception("Truncate du lieu HIS_CARE_DETAIL that bai. Ket thuc nghiep vu.");
                }
            }
            if (IsNotNullOrEmpty(hisCareDetails))
            {
                hisCareDetails.ForEach(o => o.CARE_ID = careId);
                if (!new HisCareDetailCreate(param).CreateList(hisCareDetails))
                {
                    throw new Exception("Tao du lieu HIS_CARE_DETAIL that bai. Ket thuc nghiep vu. ");
                }
            }
        }

        internal bool UpdateList(List<HIS_CARE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisCareCheck checker = new HisCareCheck(param);
                List<HIS_CARE> listRaw = new List<HIS_CARE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    this.beforeUpdateHisCares = listRaw;
                    if (!DAOWorker.HisCareDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCare_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisCare that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisCares))
            {
                if (!new HisCareUpdate(param).UpdateList(this.beforeUpdateHisCares))
                {
                    LogSystem.Warn("Rollback du lieu HisCare that bai, can kiem tra lai." + LogUtil.TraceData("HisCares", this.beforeUpdateHisCares));
                }
            }
        }
    }
}
