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
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.EventLogUtil;
using MOS.LibraryEventLog;
using MOS.MANAGER.HisExpMest.Common.Export;
using System.Threading;

namespace MOS.MANAGER.HisExpMest.Aggr.Export
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
    partial class HisExpMestAggrExport : BusinessBase
    {
        private ChildProcessor childProcessor;
        private ParentProcessor parentProcessor;
        private MaterialProcessor materialProcessor;
        private MedicineProcessor medicineProcessor;
        private ImportAutoProcessor impAutoProcessor;
        private ImpMestReusableCreateProcessor impMestReusableCreateProcessor;

        internal HisExpMestAggrExport()
            : base()
        {
            this.Init();
        }

        internal HisExpMestAggrExport(CommonParam paramCreate)
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
            this.impAutoProcessor = new ImportAutoProcessor(param);
            this.impMestReusableCreateProcessor = new ImpMestReusableCreateProcessor(param);
        }

        internal bool Run(HisExpMestSDO data, ref HIS_EXP_MEST resultData)
        {
            bool result = false;
            try
            {
                List<HIS_EXP_MEST> children = null;
                HIS_EXP_MEST aggrExpMest = null;
                bool valid = true;
                HisExpMestAggrExportCheck checker = new HisExpMestAggrExportCheck(param);
                HisExpMestExportCheck exportChecker = new HisExpMestExportCheck(param);
                HisExpMestCheck commonChecker = new HisExpMestCheck(param);
                valid = valid && commonChecker.VerifyRequireField(data);
                valid = valid && checker.IsAllowed(data, ref aggrExpMest);
                valid = valid && checker.Children(data.ExpMestId, ref children);
                valid = valid && exportChecker.IsValidAntibioticRequest(children);
                valid = valid && commonChecker.IsUnNotTaken(children);
                valid = valid && commonChecker.IsUnNoExecute(children);
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

                    List<HIS_EXP_MEST_MEDICINE> medicines = null;
                    List<HIS_EXP_MEST_MATERIAL> materials = null;

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

                    this.ProcessAuto(aggrExpMest,children,materials, data);

                    LogSystem.Info("HisExpMestAggrExport process end");
                    resultData = aggrExpMest;
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

        private void ProcessAuto(HIS_EXP_MEST aggrExpMest, List<HIS_EXP_MEST> children, List<HIS_EXP_MEST_MATERIAL> materials, HisExpMestSDO data)
        {
            try
            {
                List<HIS_EXP_MEST> expMestBls = children != null ? children.Where(o => o.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BL).ToList() : null;
                if (IsNotNullOrEmpty(expMestBls))
                {
                    this.impAutoProcessor.Run(expMestBls, data.ReqRoomId);
                }
                this.impMestReusableCreateProcessor.Run(aggrExpMest, children, materials, data.ReqRoomId);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void RollBack()
        {
            this.childProcessor.Rollback();
            this.parentProcessor.Rollback();
        }
    }
}
