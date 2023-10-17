using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest;
using MOS.MANAGER.HisExpMest.Common.Update;
using MOS.MANAGER.HisExpMestBlood;
using MOS.MANAGER.HisExpMestBltyReq;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMatyReq;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisExpMestMetyReq;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.EventLogUtil;
using MOS.LibraryEventLog;

namespace MOS.MANAGER.HisExpMest.Aggr.Unapprove
{
    /// <summary>
    /// Huy duyet phieu linh
    /// - Chỉ ở kho mới cho phép thực hiện
    /// - Chỉ cho phép hủy duyệt nếu phiếu lĩnh đang thực hiện
    /// - Cập nhật trạng thái của exp_mest phiếu lĩnh
    /// - Cập nhật trạng thái exp_mest, service_req với phiếu xuất con là đơn nội trú
    /// - Xóa thông tin duyệt trong exp_mest_medicine/material của các đơn nội trú
    /// - Xóa phiếu lẻ, xóa thông tin duyệt tương ứng với phiếu lẻ. Unlock bean tương ứng với phiếu bù lẻ
    /// </summary>
    partial class HisExpMestAggrUnapprove : BusinessBase
    {
        private HisExpMestUpdate hisExpMestUpdate;
        private ChildrenProcessor childrenProcessor;
        private MedicineProcessor medicineProcessor;
        private MaterialProcessor materialProcessor;
        private ComplementProcessor complementProcessor;
        private SereServProcessor sereServProcessor;

        internal HisExpMestAggrUnapprove()
            : base()
        {
            this.Init();
        }

        internal HisExpMestAggrUnapprove(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestUpdate = new HisExpMestUpdate(param);
            this.childrenProcessor = new ChildrenProcessor(param);
            this.medicineProcessor = new MedicineProcessor(param);
            this.materialProcessor = new MaterialProcessor(param);
            this.complementProcessor = new ComplementProcessor(param);
            this.sereServProcessor = new SereServProcessor(param);
        }

        internal bool Run(HisExpMestSDO data, ref HIS_EXP_MEST resultData)
        {
            bool result = false;
            try
            {
                List<HIS_EXP_MEST_MEDICINE> medicines = null;
                List<HIS_EXP_MEST_MATERIAL> materials = null;
                List<HIS_EXP_MEST_MEDICINE> deleteMedicines = null;
                List<HIS_EXP_MEST_MATERIAL> deleteMaterials = null;
                bool valid = true;
                HIS_EXP_MEST aggrExpMest = null;
                HisExpMestAggrUnapproveCheck checker = new HisExpMestAggrUnapproveCheck(param);
                HisExpMestCheck commonChecker = new HisExpMestCheck(param);
                valid = valid && commonChecker.VerifyRequireField(data);
                valid = valid && checker.IsAllowed(data, ref aggrExpMest);
                valid = valid && checker.IsExists(data.ExpMestId, ref medicines, ref materials);
                valid = valid && commonChecker.IsNotBeingApproved(aggrExpMest);
                if (valid)
                {
                    List<string> sqls = new List<string>();

                    //Update parent
                    string sql = string.Format("UPDATE HIS_EXP_MEST SET EXP_MEST_STT_ID = {0}, LAST_APPROVAL_TIME = NULL, LAST_APPROVAL_LOGINNAME = NULL, LAST_APPROVAL_USERNAME = NULL, LAST_APPROVAL_DATE = NULL, HAS_NOT_PRES = NULL WHERE ID = {1}", IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST, aggrExpMest.ID);
                    sqls.Add(sql);

                    if (!this.childrenProcessor.Run(aggrExpMest.ID, ref sqls))
                    {
                        throw new Exception("Ket thuc nghiep vu");
                    }

                    if (!this.medicineProcessor.Run(aggrExpMest.ID, ref sqls))
                    {
                        throw new Exception("Rollback du lieu");
                    }

                    if (!this.materialProcessor.Run(aggrExpMest.ID, ref sqls))
                    {
                        throw new Exception("Rollback du lieu");
                    }

                    //Xu ly don bu le cuoi cung (de tranh rollback: do bu le thuc hien xoa du lieu)
                    if (!this.complementProcessor.Run(data.ExpMestId, medicines, materials, ref deleteMedicines, ref deleteMaterials, ref sqls))
                    {
                        throw new Exception("Rollback du lieu");
                    }

                    if (!this.sereServProcessor.Run(deleteMedicines, deleteMaterials))
                    {
                        throw new Exception("sereServProcessor. Rollback du lieu");
                    }

                    //Thuc hien xu ly xoa du lieu o cuoi de phuc vu rollback du lieu
                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("Rollback du lieu");
                    }

                    resultData = aggrExpMest;
                    result = true;
                    new EventLogGenerator(EventLog.Enum.HisExpMest_HuyDuyetPhieuXuat).ExpMestCode(aggrExpMest.EXP_MEST_CODE).Run();
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                resultData = null;
                result = false;
            }
            return result;
        }

        private void Rollback()
        {
            this.sereServProcessor.Rollback();
        }
    }
}
