using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisCareTempDetail;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisCareTemp
{
    partial class HisCareTempUpdate : BusinessBase
    {
        private List<HIS_CARE_TEMP> beforeUpdateHisCareTemps = new List<HIS_CARE_TEMP>();

        internal HisCareTempUpdate()
            : base()
        {

        }

        internal HisCareTempUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_CARE_TEMP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_CARE_TEMP raw = null;
                HisCareTempCheck checker = new HisCareTempCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.CARE_TEMP_CODE, data.ID);
                valid = valid && checker.IsNotDuplicate(data);
                if (valid)
                {
                    if (new HisCareTempDetailTruncate(param).TruncateByCareTempId(data.ID))
                    {
                        List<HIS_CARE_TEMP_DETAIL> careTempDetails = data.HIS_CARE_TEMP_DETAIL != null ? data.HIS_CARE_TEMP_DETAIL.ToList() : null;

                        if (IsNotNullOrEmpty(careTempDetails))
                        {
                            careTempDetails.ForEach(t => t.CARE_TEMP_ID = raw.ID);
                            if (!new HisCareTempDetailCreate(param).CreateList(careTempDetails))
                            {
                                throw new Exception("Tao moi HIS_CARE_TEMP_DETAIL that bai");
                            }
                        }
                        data.HIS_CARE_TEMP_DETAIL = null;
                        if (!DAOWorker.HisCareTempDAO.Update(data))
                        {
                            MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCareTemp_CapNhatThatBai);
                            throw new Exception("Cap nhat thong tin HisCareTemp that bai." + LogUtil.TraceData("data", data));
                        }
                        this.beforeUpdateHisCareTemps.Add(raw);
                        result = true;
                    }
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

        internal bool UpdateList(List<HIS_CARE_TEMP> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisCareTempCheck checker = new HisCareTempCheck(param);
                List<HIS_CARE_TEMP> listRaw = new List<HIS_CARE_TEMP>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.CARE_TEMP_CODE, data.ID);
                }
                if (valid)
                {
                    if (!DAOWorker.HisCareTempDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCareTemp_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisCareTemp that bai." + LogUtil.TraceData("listData", listData));
                    }

                    this.beforeUpdateHisCareTemps.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisCareTemps))
            {
                if (!DAOWorker.HisCareTempDAO.UpdateList(this.beforeUpdateHisCareTemps))
                {
                    LogSystem.Warn("Rollback du lieu HisCareTemp that bai, can kiem tra lai." + LogUtil.TraceData("HisCareTemps", this.beforeUpdateHisCareTemps));
                }
                this.beforeUpdateHisCareTemps = null;
            }
        }
    }
}
