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
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.EventLogUtil;
using MOS.LibraryEventLog;
using MOS.MANAGER.HisTreatment;

namespace MOS.MANAGER.HisExpMest.AggrExam.Export
{
    /// <summary>
    /// Thực xuất phiếu lĩnh
    /// - Phải làm việc tại kho
    /// - Kiểm tra các đơn nội trú đã khóa hồ sơ chưa
    /// - Phải tồn tại thông tin duyệt chưa thực xuất
    /// - Thực xuất thuốc theo exp_mest_medicine/material. 
    /// Lưu ý: cần xử lý cả các exp_mest_medicine/material của phiếu bù lẻ
    /// - Cập nhật trạng thái của các phiếu con
    /// - Kiểm tra nếu tất cả các phiếu con đều đã thực xuất hết thì cập nhật phiếu lĩnh sang trạng thái đã hoàn thành
    /// - Tạo sere_serv tương ứng
    /// </summary>
    partial class HisExpMestAggrExamExport : BusinessBase
    {
        private ChildProcessor childProcessor;
        private ParentProcessor parentProcessor;
        private MaterialProcessor materialProcessor;
        private MedicineProcessor medicineProcessor;

        internal HisExpMestAggrExamExport()
            : base()
        {
            this.Init();
        }

        internal HisExpMestAggrExamExport(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.childProcessor = new ChildProcessor(param);
            this.parentProcessor = new ParentProcessor(param);
            this.materialProcessor = new MaterialProcessor(param);
            this.medicineProcessor = new MedicineProcessor(param);
        }

        internal bool Run(HisExpMestSDO data, ref HIS_EXP_MEST resultData)
        {
            bool result = false;
            try
            {
                List<HIS_EXP_MEST> children = null;
                HIS_EXP_MEST aggrExpMest = null;
                HIS_TREATMENT treatment = null;
                List<HIS_EXP_MEST_MEDICINE> medicines = null;
                List<HIS_EXP_MEST_MATERIAL> materials = null;
                bool valid = true;
                HisExpMestAggrExamExportCheck checker = new HisExpMestAggrExamExportCheck(param);
                HisExpMestCheck commonChecker = new HisExpMestCheck(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);

                valid = valid && commonChecker.VerifyRequireField(data);
                valid = valid && checker.IsAllowed(data, ref aggrExpMest);
                valid = valid && (!aggrExpMest.TDL_TREATMENT_ID.HasValue || treatmentChecker.VerifyId(aggrExpMest.TDL_TREATMENT_ID.Value, ref treatment));
                valid = valid && checker.Children(data.ExpMestId, ref children, ref medicines, ref materials);
                valid = valid && checker.CheckUnpaidOutPatientPrescription(treatment, aggrExpMest, children);
                valid = valid && commonChecker.IsUnNotTaken(children);
                valid = valid && commonChecker.IsUnNoExecute(children);
                valid = valid && checker.CheckValidData(aggrExpMest, medicines, materials);
                if (valid)
                {
                    string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    string userName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                    long? time = Inventec.Common.DateTime.Get.Now();
                    List<string> sqls = new List<string>();

                    if (!this.childProcessor.Run(children, time.Value, loginName, userName))
                    {
                        throw new Exception("childProcessor: Ket thuc nghiep vu");
                    }
                    if (!this.parentProcessor.Run(aggrExpMest, time.Value, loginName, userName))
                    {
                        throw new Exception("parentProcessor: Ket thuc nghiep vu");
                    }

                    if (!this.medicineProcessor.Run(aggrExpMest, ref medicines, loginName, userName, time, ref sqls))
                    {
                        throw new Exception("medicineProcessor: Ket thuc nghiep vu");
                    }
                    if (!this.materialProcessor.Run(aggrExpMest, ref materials, loginName, userName, time, ref sqls))
                    {
                        throw new Exception("materialProcessor: Ket thuc nghiep vu");
                    }

                    ///execute sql se thuc hien cuoi cung de de dang trong viec rollback
                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("Rollback du lieu");
                    }

                    LogSystem.Info("HisExpMestAggrExport process end");
                    // Tra lai trang thai phieu sau khi da huy phieu
                    resultData = new HisExpMestGet().GetById(aggrExpMest.ID);
                    result = true;

                    new EventLogGenerator(EventLog.Enum.HisExpMest_ThucXuatPhieuXuat).ExpMestCode(aggrExpMest.EXP_MEST_CODE).Run();
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
            this.childProcessor.Rollback();
            this.parentProcessor.Rollback();
        }
    }
}
