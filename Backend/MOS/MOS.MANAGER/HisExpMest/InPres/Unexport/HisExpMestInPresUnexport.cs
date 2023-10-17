using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisExpMest.Common;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.InPres.Unexport
{
    class HisExpMestInPresUnexport : BusinessBase
    {
        private ExpMestProcessor expMestProcessor;
        private MaterialProcessor materialProcessor;
        private MedicineProcessor medicineProcessor;

        internal HisExpMestInPresUnexport()
            : base()
        {
            this.Init();
        }

        internal HisExpMestInPresUnexport(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.expMestProcessor = new ExpMestProcessor(param);
            this.materialProcessor = new MaterialProcessor(param);
            this.medicineProcessor = new MedicineProcessor(param);
        }

        internal bool Run(HisExpMestSDO data, ref HIS_EXP_MEST resultData)
        {
            bool result = false;
            try
            {
                HIS_EXP_MEST expMest = null;
                HIS_SERVICE_REQ serviceReq = null;
                List<HIS_EXP_MEST_MEDICINE> medicines = null;
                List<HIS_EXP_MEST_MATERIAL> materials = null;
                HIS_TREATMENT treatment = null;

                bool valid = true;
                HisExpMestInPresUnexportCheck checker = new HisExpMestInPresUnexportCheck(param);
                HisExpMestCheck commonChecker = new HisExpMestCheck(param);
                valid = valid && commonChecker.VerifyRequireField(data);
                valid = valid && checker.IsAllowed(data, ref expMest);
                valid = valid && commonChecker.IsUnNotTaken(expMest);
                valid = valid && commonChecker.HasNotInExpMestAggr(expMest);
                valid = valid && commonChecker.HasNoNationalCode(expMest);
                valid = valid && commonChecker.IsUnfinishTreatmentInCaseOfInPatient(expMest, ref treatment);
                valid = valid && checker.HasNoBill(expMest);
                valid = valid && checker.HasNoMoba(expMest);
                valid = valid && checker.HasNoMediStockPeriod(expMest, ref medicines, ref materials);
                valid = valid && checker.IsValidCancellationOfExport(materials, expMest);
                if (valid)
                {
                    //Xử lý phiếu lĩnh đầu tiên để, nếu có trường hợp 2 người cùng thực hiện hủy thực xuất
                    //đồng thời thì sẽ có 1 người bị thất bại và ko chạy tiếp các nghiệp vụ phía sau
                    if (!this.expMestProcessor.Run(expMest, ref serviceReq))
                    {
                        throw new Exception("parentProcessor: Ket thuc nghiep vu");
                    }

                    if (!this.medicineProcessor.Run(expMest, medicines))
                    {
                        throw new Exception("medicineProcessor: Ket thuc nghiep vu");
                    }
                    if (!this.materialProcessor.Run(expMest, materials))
                    {
                        throw new Exception("materialProcessor: Ket thuc nghiep vu");
                    }
                    expMest.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__EXECUTE;
                    resultData = expMest;
                    result = true;

                    new EventLogGenerator(EventLog.Enum.HisExpMest_HuyThucXuatPhieuXuat).ExpMestCode(expMest.EXP_MEST_CODE).Run();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                resultData = null;
                this.RollBack();
                result = false;
            }
            return result;
        }

        private void RollBack()
        {
            this.expMestProcessor.Rollback();
            this.medicineProcessor.Rollback();
            this.materialProcessor.Rollback();
        }
    }
}
