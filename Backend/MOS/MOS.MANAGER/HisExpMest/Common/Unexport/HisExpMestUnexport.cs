using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMatyReq;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisExpMestMetyReq;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisExpMestBltyReq;
using MOS.MANAGER.HisExpMest.Common.Create;
using MOS.MANAGER.HisExpMest.Common.Update;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.EventLogUtil;
using MOS.LibraryEventLog;

namespace MOS.MANAGER.HisExpMest.Common.Unexport
{
    /// <summary>
    /// Hủy thực xuất phiếu lĩnh
    /// - Phải làm việc tại kho
    /// - Các đơn nội trú cần phải chưa khóa hồ sơ
    /// - Các phiếu con cần phải chưa bị thu hồi, chưa có phiếu nhập chuyển kho tương ứng
    /// - Thuốc vật tư phải chưa được thanh toán
    /// - Cập nhật trạng thái của các phiếu con/các service_req tương ứng ==> đang thực hiện
    /// - Hủy thông tin thực xuất trong exp_mest_medicine/material
    /// - Hủy thông tin sere_serv tương ứng
    /// - Tính toán lại tỉ lệ BHYT trong các sere_serv theo từng hồ sơ điều trị ==> cập nhật lại sere_serv của từng hồ sơ điều trị
    /// </summary>
    partial class HisExpMestUnexport : BusinessBase
    {
        private HisExpMestProcessor expMestProcessor;
        private MaterialProcessor materialProcessor;
        private MedicineProcessor medicineProcessor;
        private SereServProcessor sereServProcessor;
        private BloodProcessor bloodProcessor;
        private ExpBltyServiceProcessor expBltyServiceProcessor;
        private HisTransactionProcessor transactionProcessor;

        internal HisExpMestUnexport()
            : base()
        {
            this.Init();
        }

        internal HisExpMestUnexport(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.expMestProcessor = new HisExpMestProcessor(param);
            this.materialProcessor = new MaterialProcessor(param);
            this.medicineProcessor = new MedicineProcessor(param);
            this.sereServProcessor = new SereServProcessor(param);
            this.bloodProcessor = new BloodProcessor(param);
            this.expBltyServiceProcessor = new ExpBltyServiceProcessor(param);
            this.transactionProcessor = new HisTransactionProcessor(param);
        }

        internal bool Run(HisExpMestSDO data, ref HIS_EXP_MEST resultData)
        {
            return this.Run(data, false, ref resultData);
        }

        internal bool Run(HisExpMestSDO data, bool isAuto, ref HIS_EXP_MEST resultData)
        {
            bool result = false;
            try
            {
                List<HIS_SERE_SERV> existedSereServ = null;
                List<HIS_EXP_MEST_MEDICINE> medicines = null;
                List<HIS_EXP_MEST_MATERIAL> materials = null;
                List<HIS_EXP_MEST_BLOOD> bloods = null;
                HIS_TREATMENT treatment = null;
                HIS_EXP_MEST expMest = null;
                bool valid = true;
                HisExpMestUnexportCheck checker = new HisExpMestUnexportCheck(param);
                HisExpMestCheck commonChecker = new HisExpMestCheck(param);
                valid = valid && commonChecker.VerifyRequireField(data);
                valid = valid && checker.IsAllowed(data, isAuto, ref expMest);
                valid = valid && checker.IsUnlockTreatment(expMest, ref treatment);
                valid = valid && checker.HasNoMoba(expMest);
                valid = valid && checker.HasNoImpMest(expMest);
                valid = valid && checker.HasNoXbttExpMest(expMest);
                valid = valid && commonChecker.HasNotInExpMestAggr(expMest);
                valid = valid && commonChecker.HasNoNationalCode(expMest);
                valid = valid && commonChecker.IsUnNotTaken(expMest);
                valid = valid && checker.HasNoMediStockPeriod(expMest, ref medicines, ref materials, ref bloods);
                valid = valid && checker.HasNoTransfusionSum(expMest, bloods);
                valid = valid && checker.HasNoBill(expMest, ref existedSereServ);
                valid = valid && checker.HasNoBcsDetail(expMest, medicines, materials);
                valid = valid && checker.CheckVerifyBaseAmount(expMest, medicines, materials);
                valid = valid && checker.IsValidCancellationOfExport(materials, expMest);
                if (valid)
                {
                    HIS_SERVICE_REQ serviceReq = null;
                    //Xử lý phiếu xuat đầu tiên để, nếu có trường hợp 2 người cùng thực hiện hủy thực xuất
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
                    if (!this.bloodProcessor.Run(expMest, bloods))
                    {
                        throw new Exception("bloodProcessor: Ket thuc nghiep vu");
                    }
                    if (!this.transactionProcessor.Run(expMest))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_TuDongHuyGiaoDichThatBai);
                    }

                    //Xử lý sere_serv ở cuối cùng, vì nghiệp vụ này có xử lý xóa sere_serv và tính toán, cập nhật
                    //lại thông tin trong sere_serv được xử lý trong thread khác
                    if (!this.sereServProcessor.Run(expMest, treatment, existedSereServ))
                    {
                        throw new Exception("sereServProcessor: Ket thuc nghiep vu");
                    }

                    if (!expBltyServiceProcessor.Run(expMest))
                    {
                        throw new Exception("expBltyServiceProcessor: Ket thuc nghiep vu");
                    }

                    resultData = expMest;
                    result = true;
                    new EventLogGenerator(EventLog.Enum.HisExpMest_HuyThucXuatPhieuXuat).ExpMestCode(expMest.EXP_MEST_CODE).Run();
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
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
            this.sereServProcessor.Rollback();
            this.bloodProcessor.Rollback();
        }
    }
}
