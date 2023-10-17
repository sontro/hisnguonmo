using AutoMapper;
using Inventec.Core;
using Inventec.Token.ResourceSystem;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisExpMest.Common.Update;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.InPres.Approve
{
    class HisExpMestInPresApprove : BusinessBase
    {
        private HisExpMestUpdate hisExpMestUpdate;
        private MedicineProcessor medicineProcessor;
        private MaterialProcessor materialProcessor;
        private ServiceReqProcessor serviceReqProcessor;
        private AutoProcessor autoProcessor;

        internal HisExpMestInPresApprove()
            : base()
        {
            this.Init();
        }

        internal HisExpMestInPresApprove(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestUpdate = new HisExpMestUpdate(param);
            this.materialProcessor = new MaterialProcessor(param);
            this.medicineProcessor = new MedicineProcessor(param);
            this.serviceReqProcessor = new ServiceReqProcessor(param);
            this.autoProcessor = new AutoProcessor(param);
        }

        internal bool Run(HisExpMestSDO data, ref HIS_EXP_MEST resultData)
        {
            bool result = false;
            try
            {
                HIS_EXP_MEST expMest = null;
                bool valid = true;
                HisExpMestInPresApproveCheck checker = new HisExpMestInPresApproveCheck(param);
                HisExpMestCheck commonChecker = new HisExpMestCheck(param);
                valid = valid && commonChecker.VerifyRequireField(data);
                valid = valid && checker.IsAllowed(data, ref expMest);
                valid = valid && commonChecker.HasNotInExpMestAggr(expMest);
                valid = valid && commonChecker.IsUnNotTaken(expMest);
                valid = valid && commonChecker.IsNotBeingApproved(expMest);
                valid = valid && commonChecker.IsValidApproveAntibioticUse(new List<HIS_EXP_MEST>() { expMest });
                if (valid)
                {
                    string loginName = ResourceTokenManager.GetLoginName();
                    string userName = ResourceTokenManager.GetUserName();
                    long? time = Inventec.Common.DateTime.Get.Now();

                    List<string> sqls = new List<string>();

                    this.ProcessExpMest(expMest, loginName, userName, time);

                    if (!this.medicineProcessor.Run(expMest, loginName, userName, time, ref sqls))
                    {
                        throw new Exception("medicineProcessor. Rollback du lieu");
                    }
                    if (!this.materialProcessor.Run(expMest, loginName, userName, time, ref sqls))
                    {
                        throw new Exception("materialProcessor. Rollback du lieu");
                    }
                    if (!this.serviceReqProcessor.Run(expMest, loginName, userName, time.Value, ref sqls))
                    {
                        throw new Exception("serviceReqProcessor. Rollback du lieu");
                    }

                    sqls.Add(String.Format("UPDATE HIS_EXP_MEST SET IS_BEING_APPROVED = NULL WHERE ID = {0}", expMest.ID));

                    ///execute sql se thuc hien cuoi cung de de dang trong viec rollback
                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("Rollback du lieu");
                    }

                    expMest.IS_BEING_APPROVED = null;
                    result = true;
                    new EventLogGenerator(EventLog.Enum.HisExpMest_DuyetPhieuXuat).ExpMestCode(expMest.EXP_MEST_CODE).Run();

                    this.ProcessAuto(data, expMest);

                    resultData = new HisExpMestGet().GetById(expMest.ID);
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

        private void ProcessExpMest(HIS_EXP_MEST expMest, string loginname, string username, long? time)
        {
            //Cap nhat trang thai cua exp_mest
            Mapper.CreateMap<HIS_EXP_MEST, HIS_EXP_MEST>();
            HIS_EXP_MEST beforeUpdate = Mapper.Map<HIS_EXP_MEST>(expMest);//phuc vu rollback
            expMest.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__EXECUTE;
            expMest.IS_EXPORT_EQUAL_APPROVE = null;
            expMest.LAST_APPROVAL_TIME = time;
            expMest.LAST_APPROVAL_LOGINNAME = loginname;
            expMest.LAST_APPROVAL_USERNAME = username;
            expMest.LAST_APPROVAL_DATE = expMest.LAST_APPROVAL_TIME - expMest.LAST_APPROVAL_TIME % 1000000;
            expMest.IS_BEING_APPROVED = Constant.IS_TRUE;

            if (!this.hisExpMestUpdate.Update(expMest, beforeUpdate))
            {
                throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
            }
        }

        /// <summary>
        /// Tu dong phe duyet, thuc xuat trong truong hop kho co cau hinh tu dong
        /// </summary>
        private void ProcessAuto(HisExpMestSDO data, HIS_EXP_MEST expMest)
        {
            try
            {
                this.autoProcessor.Run(data, expMest);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void RollBack()
        {
            this.hisExpMestUpdate.RollbackData();
        }
    }
}
