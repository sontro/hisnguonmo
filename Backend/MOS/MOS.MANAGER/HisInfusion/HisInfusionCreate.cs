using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.Token;
using MOS.MANAGER.HisMixedMedicine;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisInfusion
{
    partial class HisInfusionCreate : BusinessBase
    {
        private List<HIS_INFUSION> recentHisInfusions = new List<HIS_INFUSION>();
        private HisMixedMedicineCreate mixedMedicineCreate;

        internal HisInfusionCreate()
            : base()
        {
            this.Init();
        }

        internal HisInfusionCreate(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.mixedMedicineCreate = new HisMixedMedicineCreate(param);
        }

        internal bool Create(HisInfusionSDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisInfusionCheck checker = new HisInfusionCheck(param);
                valid = valid && checker.VerifyRequireField(data.HisInfusion);
                valid = valid && checker.ValidateData(data.HisInfusion);
                if (valid)
                {
                    if (!DAOWorker.HisInfusionDAO.Create(data.HisInfusion))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisInfusion_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisInfusion that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisInfusions.Add(data.HisInfusion);
                    this.ProcessMixedMedicine(data.HisInfusion.ID, data.HisMixedMedicines);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                this.RollbackData();
                result = false;
            }
            return result;
        }

        private void ProcessMixedMedicine(long infusionID, List<HIS_MIXED_MEDICINE> mixedMedicines)
        {
            if (IsNotNullOrEmpty(mixedMedicines))
            {
                mixedMedicines.ForEach(o => o.INFUSION_ID = infusionID);
                if (!this.mixedMedicineCreate.CreateList(mixedMedicines))
                {
                    throw new Exception("Tao danh sach mix medicines theo infusion that bai");
                }
            }

        }

        internal void RollbackData()
        {
            this.mixedMedicineCreate.RollbackData();
            if (IsNotNullOrEmpty(this.recentHisInfusions))
            {
                if (!new HisInfusionTruncate(param).TruncateList(this.recentHisInfusions))
                {
                    LogSystem.Warn("Rollback du lieu HisInfusion that bai, can kiem tra lai." + LogUtil.TraceData("recentHisInfusions", this.recentHisInfusions));
                }
            }
        }
    }
}
