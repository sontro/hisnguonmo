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

namespace MOS.MANAGER.HisExpMest.InPres.Unapprove
{
    class HisExpMestInPresUnapprove : BusinessBase
    {
        private MaterialProcessor materialProcessor;
        private MedicineProcessor medicineProcessor;
        private ServiceReqProcessor serviceReqProcessor;

        internal HisExpMestInPresUnapprove()
            : base()
        {
            this.Init();
        }

        internal HisExpMestInPresUnapprove(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.materialProcessor = new MaterialProcessor(param);
            this.medicineProcessor = new MedicineProcessor(param);
            this.serviceReqProcessor = new ServiceReqProcessor(param);
        }

        internal bool Run(HisExpMestSDO data, ref HIS_EXP_MEST resultData)
        {
            bool result = false;
            try
            {
                List<HIS_EXP_MEST_MEDICINE> medicines = null;
                List<HIS_EXP_MEST_MATERIAL> materials = null;
                bool valid = true;
                HIS_EXP_MEST expMest = null;
                HisExpMestInPresUnapproveCheck checker = new HisExpMestInPresUnapproveCheck(param);
                HisExpMestCheck commonChecker = new HisExpMestCheck(param);
                valid = valid && commonChecker.VerifyRequireField(data);
                valid = valid && checker.IsAllowed(data, ref expMest);
                valid = valid && commonChecker.IsUnNotTaken(expMest);
                valid = valid && commonChecker.HasNotInExpMestAggr(expMest);
                valid = valid && commonChecker.IsNotBeingApproved(expMest);
                valid = valid && checker.IsExists(data.ExpMestId, ref medicines, ref materials);
                if (valid)
                {
                    List<string> sqls = new List<string>();

                    //Update parent
                    string sql = string.Format("UPDATE HIS_EXP_MEST SET EXP_MEST_STT_ID = {0} WHERE ID = {1}", IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST, expMest.ID);
                    sqls.Add(sql);

                    if (!this.medicineProcessor.Run(expMest.ID, ref sqls))
                    {
                        throw new Exception("medicineProcessor. Rollback du lieu");
                    }

                    if (!this.materialProcessor.Run(expMest.ID, ref sqls))
                    {
                        throw new Exception("materialProcessor. Rollback du lieu");
                    }

                    if (!this.serviceReqProcessor.Run(expMest, ref sqls))
                    {
                        throw new Exception("serviceReqProcessor. Rollback du lieu");
                    }

                    //Thuc hien xu ly xoa du lieu o cuoi de phuc vu rollback du lieu
                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("Rollback du lieu");
                    }
                    expMest.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST;
                    resultData = expMest;
                    result = true;
                    new EventLogGenerator(EventLog.Enum.HisExpMest_HuyDuyetPhieuXuat).ExpMestCode(expMest.EXP_MEST_CODE).Run();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }
    }
}
