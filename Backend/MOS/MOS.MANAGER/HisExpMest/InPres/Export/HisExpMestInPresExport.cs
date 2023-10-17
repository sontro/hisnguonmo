using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.HisExpMest.Common.Export;
using MOS.MANAGER.HisExpMest.Common.Update;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.InPres.Export
{
    class HisExpMestInPresExport : BusinessBase
    {
        private MaterialProcessor materialProcessor;
        private MedicineProcessor medicineProcessor;
        private ServiceReqProcessor serviceReqProcessor;
        private HisExpMestUpdate hisExpMestUpdate;
        private ImportAutoMaterialProcessor importAutoMaterialProcessor;

        internal HisExpMestInPresExport()
            : base()
        {
            this.Init();
        }

        internal HisExpMestInPresExport(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.materialProcessor = new MaterialProcessor(param);
            this.medicineProcessor = new MedicineProcessor(param);
            this.serviceReqProcessor = new ServiceReqProcessor(param);
            this.hisExpMestUpdate = new HisExpMestUpdate(param);
            this.importAutoMaterialProcessor = new ImportAutoMaterialProcessor(param);
        }

        internal bool Run(HisExpMestSDO data, ref HIS_EXP_MEST resultData)
        {
            bool result = false;
            try
            {
                HIS_EXP_MEST expMest = null;
                bool valid = true;
                HisExpMestInPresExportCheck checker = new HisExpMestInPresExportCheck(param);
                HisExpMestExportCheck commonExportChecker = new HisExpMestExportCheck(param);
                HisExpMestCheck commonChecker = new HisExpMestCheck(param);
                valid = valid && commonChecker.VerifyRequireField(data);
                valid = valid && checker.IsAllowed(data, ref expMest);
                valid = valid && commonExportChecker.IsValidAntibioticRequest(new List<HIS_EXP_MEST>() { expMest });
                valid = valid && commonChecker.HasNotInExpMestAggr(expMest);
                valid = valid && commonChecker.IsUnNotTaken(expMest);
                valid = valid && commonChecker.IsUnNoExecute(expMest);
                if (valid)
                {
                    string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    string userName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                    long? time = Inventec.Common.DateTime.Get.Now();
                    List<string> sqls = new List<string>();

                    List<HIS_EXP_MEST_MEDICINE> medicines = null;
                    List<HIS_EXP_MEST_MATERIAL> materials = null;

                    this.ProcessHisExpMest(expMest, loginName, userName, time.Value);

                    if (!this.medicineProcessor.Run(expMest, ref medicines, loginName, userName, time, ref sqls))
                    {
                        throw new Exception("medicineProcessor: Ket thuc nghiep vu");
                    }
                    if (!this.materialProcessor.Run(expMest, ref materials, loginName, userName, time, ref sqls))
                    {
                        throw new Exception("materialProcessor: Ket thuc nghiep vu");
                    }
                    if (!this.serviceReqProcessor.Run(expMest, loginName, userName, time.Value, ref sqls))
                    {
                        throw new Exception("serviceReqProcessor: Ket thuc nghiep vu");
                    }

                    ///execute sql se thuc hien cuoi cung de de dang trong viec rollback
                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("Rollback du lieu");
                    }

                    Inventec.Common.Logging.LogSystem.Info("HisExpMestAggrExport process end");
                    expMest.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE;
                    expMest.FINISH_TIME = time.Value;
                    resultData = expMest;
                    result = true;
                    new EventLogGenerator(EventLog.Enum.HisExpMest_ThucXuatPhieuXuat).ExpMestCode(expMest.EXP_MEST_CODE).Run();

                    //Thực hiện tự động nhập tái sử dụng
                    this.importAutoMaterialProcessor.Run(materials, expMest, data.ReqRoomId);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                this.RollbackData();
                resultData = null;
                result = false;
            }
            return result;
        }

        private void ProcessHisExpMest(HIS_EXP_MEST expMest, string loginname, string username, long expTime)
        {
            Mapper.CreateMap<HIS_EXP_MEST, HIS_EXP_MEST>();
            HIS_EXP_MEST before = Mapper.Map<HIS_EXP_MEST>(expMest);
            expMest.IS_EXPORT_EQUAL_APPROVE = MOS.UTILITY.Constant.IS_TRUE;
            expMest.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE;
            expMest.FINISH_TIME = expTime;
            expMest.LAST_EXP_LOGINNAME = loginname;
            expMest.LAST_EXP_TIME = expTime;
            expMest.LAST_EXP_USERNAME = username;
            if (!this.hisExpMestUpdate.Update(expMest, before))
            {
                throw new Exception("Khong update duoc HIS_EXP_MEST. Ket thuc nghiep vu");
            }
        }

        private void RollbackData()
        {
            try
            {
                this.hisExpMestUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
