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
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisExpMest.Common;
using Inventec.Token.ResourceSystem;
using MOS.MANAGER.EventLogUtil;
using MOS.LibraryEventLog;
using MOS.UTILITY;

namespace MOS.MANAGER.HisExpMest.Aggr.Approve
{
    /// <summary>
    /// Duyệt phiếu lĩnh
    /// - Phải làm việc tại kho
    /// - Phiếu con phải đang ở trạng thái yêu cầu
    /// - Phiếu lĩnh phải ở trạng thái yêu cầu
    /// - Cập nhật để bổ sung thông tin duyệt và tdl_aggr_exp_mest_id cho exp_mest_medicine của đơn nội trú
    /// - Kiểm tra thuốc lẻ
    /// Có 3 trường hợp:
    /// + Cho phép xuất lẻ: Không xử lý gì
    /// + Không cho phép xuất lẻ nhưng ko quản lý: tạo ra phần bù, gắn vào phiếu lĩnh
    /// + Không cho phép xuất lẻ nhưng có quản lý: tạo ra phần bù, tạo ra phiếu xuất bù lẻ. Gắn phiếu bù này vào phiếu lĩnh.
    /// - Cập nhật trạng thái cho các phiếu con
    /// - Cập nhật trạng thái cho các service_req tương ứng với các phiếu con
    /// </summary>
    partial class HisExpMestAggrApprove : BusinessBase
    {
        private HisExpMestUpdate hisExpMestUpdate;
        private MaterialProcessor materialProcessor;
        private MedicineProcessor medicineProcessor;
        private OddMedicineProcessor oddMedicineProcessor;
        private OddMaterialProcessor oddMaterialProcessor;
        private ChildrenProcessor childrenProcessor;
        private SereServProcessor sereServProcessor;

        internal HisExpMestAggrApprove()
            : base()
        {
            this.Init();
        }

        internal HisExpMestAggrApprove(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestUpdate = new HisExpMestUpdate(param);
            this.materialProcessor = new MaterialProcessor(param);
            this.medicineProcessor = new MedicineProcessor(param);
            this.childrenProcessor = new ChildrenProcessor(param);
            this.oddMedicineProcessor = new OddMedicineProcessor(param);
            this.oddMaterialProcessor = new OddMaterialProcessor(param);
            this.sereServProcessor = new SereServProcessor(param);
        }

        internal bool Run(HisExpMestSDO data, ref List<HIS_EXP_MEST> resultData)
        {
            bool result = false;
            try
            {
                HIS_EXP_MEST aggrExpMest = null;
                List<HIS_EXP_MEST_MATERIAL> expMestMaterials = null;
                List<HIS_EXP_MEST_MEDICINE> expMestMedicines = null;
                List<HIS_EXP_MEST> childs = null;
                List<HIS_EXP_MEST_REASON> reasons = null;
                bool valid = true;
                HisExpMestAggrApproveCheck checker = new HisExpMestAggrApproveCheck(param);
                HisExpMestCheck commonChecker = new HisExpMestCheck(param);
                valid = valid && commonChecker.VerifyRequireField(data);
                valid = valid && checker.IsAllowed(data, ref aggrExpMest, ref childs, ref reasons);
                valid = valid && commonChecker.IsValidApproveAntibioticUse(childs);
                if (valid)
                {
                    string loginName = ResourceTokenManager.GetLoginName();
                    string userName = ResourceTokenManager.GetUserName();
                    long? time = Inventec.Common.DateTime.Get.Now();

                    List<string> sqls = new List<string>();

                    this.ProcessAggrExpMest(aggrExpMest, loginName, userName, time);

                    if (!this.childrenProcessor.Run(data.ExpMestId, loginName, userName, time, ref sqls))
                    {
                        throw new Exception("Rollback du lieu");
                    }
                    if (!this.medicineProcessor.Run(aggrExpMest, loginName, userName, time, ref sqls))
                    {
                        throw new Exception("Rollback du lieu");
                    }
                    if (!this.materialProcessor.Run(aggrExpMest, loginName, userName, time, ref sqls))
                    {
                        throw new Exception("Rollback du lieu");
                    }
                    if (!this.oddMaterialProcessor.Run(aggrExpMest, childs, data.ReqRoomId, loginName, userName, time, reasons, ref expMestMaterials, ref sqls))
                    {
                        throw new Exception("Rollback du lieu");
                    }
                    if (!this.oddMedicineProcessor.Run(aggrExpMest, childs, data.ReqRoomId, loginName, userName, time, reasons, ref expMestMedicines, ref sqls))
                    {
                        throw new Exception("Rollback du lieu");
                    }
                    if (!this.sereServProcessor.Run(childs, expMestMedicines, expMestMaterials))
                    {
                        throw new Exception("sereServProcessor. Rollback du lieu");
                    }

                    if ((IsNotNullOrEmpty(expMestMaterials) && expMestMaterials.Exists(o => o.IS_NOT_PRES == Constant.IS_TRUE)) || (IsNotNullOrEmpty(expMestMedicines) && expMestMedicines.Exists(o => o.IS_NOT_PRES == Constant.IS_TRUE)))
                    {
                        sqls.Add(String.Format("UPDATE HIS_EXP_MEST SET IS_BEING_APPROVED = NULL, HAS_NOT_PRES = 1 WHERE ID = {0}", aggrExpMest.ID));
                    }
                    else
                    {
                        sqls.Add(String.Format("UPDATE HIS_EXP_MEST SET IS_BEING_APPROVED = NULL, HAS_NOT_PRES = NULL WHERE ID = {0}", aggrExpMest.ID));
                    }

                    ///execute sql se thuc hien cuoi cung de de dang trong viec rollback
                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("Rollback du lieu");
                    }

                    aggrExpMest.IS_BEING_APPROVED = null;
                    resultData = new HisExpMestGet().GetByAggrExpMestId(aggrExpMest.ID);
                    result = true;
                    new EventLogGenerator(EventLog.Enum.HisExpMest_DuyetPhieuXuat).ExpMestCode(aggrExpMest.EXP_MEST_CODE).Run();
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

        private void ProcessAggrExpMest(HIS_EXP_MEST aggrExpMest, string loginname, string username, long? time)
        {
            //Cap nhat trang thai cua exp_mest
            Mapper.CreateMap<HIS_EXP_MEST, HIS_EXP_MEST>();
            HIS_EXP_MEST beforeUpdate = Mapper.Map<HIS_EXP_MEST>(aggrExpMest);//phuc vu rollback

            //Neu phieu xuat chua o trang thai dang xu ly thi moi thuc hien cap nhat
            if (aggrExpMest.EXP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__EXECUTE)
            {
                aggrExpMest.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__EXECUTE;          
            }

            aggrExpMest.IS_EXPORT_EQUAL_APPROVE = null;      
            aggrExpMest.LAST_APPROVAL_TIME = time;
            aggrExpMest.LAST_APPROVAL_LOGINNAME = loginname;
            aggrExpMest.LAST_APPROVAL_USERNAME = username;
            aggrExpMest.LAST_APPROVAL_DATE = aggrExpMest.LAST_APPROVAL_TIME - aggrExpMest.LAST_APPROVAL_TIME % 1000000;
            aggrExpMest.IS_BEING_APPROVED = Constant.IS_TRUE;

            if (!this.hisExpMestUpdate.Update(aggrExpMest, beforeUpdate))
            {
                throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
            }
        }

        private void RollBack()
        {
            this.hisExpMestUpdate.RollbackData();
            this.sereServProcessor.Rollback();
            this.oddMaterialProcessor.Rollback();
            this.oddMedicineProcessor.Rollback();
        }
    }
}
