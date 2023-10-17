using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpMestMedicine
{
    class HisImpMestMedicineCreate : BusinessBase
    {
        private List<HIS_IMP_MEST_MEDICINE> recentHisImpMestMedicines = new List<HIS_IMP_MEST_MEDICINE>();
        internal HisImpMestMedicineCreate()
            : base()
        {

        }

        internal HisImpMestMedicineCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_IMP_MEST_MEDICINE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisImpMestMedicineCheck checker = new HisImpMestMedicineCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    data.REQ_AMOUNT = data.AMOUNT; //Mac dinh luc tao thi so luong yeu cau = so luong duyet
                    if (!DAOWorker.HisImpMestMedicineDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpMestMedicine_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisImpMestMedicine that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisImpMestMedicines.Add(data);
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

        internal bool CreateList(List<HIS_IMP_MEST_MEDICINE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisImpMestMedicineCheck checker = new HisImpMestMedicineCheck(param);
                foreach (var data in listData)
                {
                    data.REQ_AMOUNT = data.AMOUNT; //Mac dinh luc tao thi so luong yeu cau = so luong duyet
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisImpMestMedicineDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpMestMedicine_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisImpMestMedicine that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisImpMestMedicines.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisImpMestMedicines))
            {
                if (!DAOWorker.HisImpMestMedicineDAO.TruncateList(this.recentHisImpMestMedicines))
                {
                    LogSystem.Warn("Rollback du lieu HisImpMestMedicine that bai, can kiem tra lai." + LogUtil.TraceData("HisImpMestMedicines", this.recentHisImpMestMedicines));
                }
            }
        }
    }
}
