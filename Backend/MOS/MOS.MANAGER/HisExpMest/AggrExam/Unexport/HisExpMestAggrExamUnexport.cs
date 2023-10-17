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
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisExpMestBltyReq;
using MOS.MANAGER.HisExpMest.Common.Create;
using MOS.MANAGER.HisExpMest.Common.Update;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.EventLogUtil;
using MOS.LibraryEventLog;

namespace MOS.MANAGER.HisExpMest.AggrExam.Unexport
{
    /// <summary>
    /// Hủy thực xuất phiếu khám tổng hợp
    /// - Phải làm việc tại kho
    /// - Các đơn nội trú cần phải chưa khóa hồ sơ
    /// - Các phiếu con cần phải chưa bị thu hồi
    /// - Thuốc vật tư phải chưa được thanh toán
    /// - Cập nhật trạng thái của các phiếu con/các service_req tương ứng ==> đang thực hiện
    /// - Hủy thông tin thực xuất trong exp_mest_medicine/material
    /// </summary>
    partial class HisExpMestAggrExamUnexport : BusinessBase
    {
        private ChildProcessor childProcessor;
        private ParentProcessor parentProcessor;
        private MaterialProcessor materialProcessor;
        private MedicineProcessor medicineProcessor;

        internal HisExpMestAggrExamUnexport()
            : base()
        {
            this.Init();
        }

        internal HisExpMestAggrExamUnexport(CommonParam paramCreate)
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
                List<HIS_EXP_MEST_MEDICINE> medicines = null;
                List<HIS_EXP_MEST_MATERIAL> materials = null;
                bool valid = true;
                HisExpMestAggrExamUnexportCheck checker = new HisExpMestAggrExamUnexportCheck(param);
                HisExpMestCheck commonChecker = new HisExpMestCheck(param);
                valid = valid && commonChecker.VerifyRequireField(data);
                valid = valid && checker.IsAllowed(data, ref aggrExpMest, ref children);
                valid = valid && commonChecker.HasNoNationalCode(children);
                valid = valid && commonChecker.IsUnNotTaken(children);
                valid = valid && checker.HasNoMoba(children);
                valid = valid && checker.HasNoMediStockPeriod(data.ExpMestId, ref medicines, ref materials);
                if (valid)
                {
                    //Xử lý phiếu lĩnh đầu tiên để, nếu có trường hợp 2 người cùng thực hiện hủy thực xuất
                    //đồng thời thì sẽ có 1 người bị thất bại và ko chạy tiếp các nghiệp vụ phía sau
                    if (!this.parentProcessor.Run(aggrExpMest))
                    {
                        throw new Exception("parentProcessor: Ket thuc nghiep vu");
                    }

                    List<HIS_SERVICE_REQ> serviceReqs = null;
                    if (!this.childProcessor.Run(children, ref serviceReqs))
                    {
                        throw new Exception("childProcessor: Ket thuc nghiep vu");
                    }

                    if (!this.medicineProcessor.Run(aggrExpMest, medicines))
                    {
                        throw new Exception("medicineProcessor: Ket thuc nghiep vu");
                    }
                    if (!this.materialProcessor.Run(aggrExpMest, materials))
                    {
                        throw new Exception("materialProcessor: Ket thuc nghiep vu");
                    }

                    resultData = new HisExpMestGet().GetById(aggrExpMest.ID);
                    result = true;
                    new EventLogGenerator(EventLog.Enum.HisExpMest_HuyThucXuatPhieuXuat).ExpMestCode(aggrExpMest.EXP_MEST_CODE).Run();
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
            this.medicineProcessor.Rollback();
            this.materialProcessor.Rollback();
        }
    }
}
