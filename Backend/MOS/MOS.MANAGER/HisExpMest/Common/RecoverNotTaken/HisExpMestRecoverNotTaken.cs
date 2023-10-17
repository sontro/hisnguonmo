using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Common.RecoverNotTaken
{
    class HisExpMestRecoverNotTaken : BusinessBase
    {
        private ExpMestProcessor expMestProcessor = null;
        private MedicineProcessor medicineProcessor = null;
        private MaterialProcessor materialProcessor = null;
        private SereServProcessor sereServProcessor = null;

        internal HisExpMestRecoverNotTaken()
            : base()
        {
            this.Init();
        }

        internal HisExpMestRecoverNotTaken(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.expMestProcessor = new ExpMestProcessor(param);
            this.medicineProcessor = new MedicineProcessor(param);
            this.materialProcessor = new MaterialProcessor(param);
            this.sereServProcessor = new SereServProcessor(param);
        }

        internal bool Run(HisExpMestSDO data, ref HIS_EXP_MEST resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_EXP_MEST raw = null;
                HIS_TREATMENT treatment = null;
                List<HIS_EXP_MEST_MEDICINE> oldMedicines = null;
                List<HIS_EXP_MEST_MATERIAL> oldMaterials = null;
                List<HIS_SERE_SERV> oldSereServs = null;

                HisExpMestRecoverNotTakenCheck checker = new HisExpMestRecoverNotTakenCheck(param);
                HisExpMestCheck commonChecker = new HisExpMestCheck(param);
                valid = valid && commonChecker.VerifyRequireField(data);
                valid = valid && commonChecker.VerifyId(data.ExpMestId, ref raw);
                valid = valid && commonChecker.IsUnlock(raw);
                valid = valid && checker.IsPrescription(raw);
                valid = valid && checker.IsWorkingInStock(data.ReqRoomId, raw);
                valid = valid && checker.HasDetail(raw, ref oldMedicines, ref oldMaterials, ref oldSereServs, ref treatment);
                valid = valid && checker.VerifyUpdateSereServ(treatment, oldSereServs);
                if (valid)
                {
                    ResultMedicineData newMedicine = null;
                    ResultMaterialData newMaterial = null;
                    List<string> sqls = new List<string>();
                    if (!this.expMestProcessor.Run(raw))
                    {
                        throw new Exception("expMestProcessor");
                    }

                    if (!this.medicineProcessor.Run(raw, oldMedicines, ref newMedicine, ref sqls))
                    {
                        throw new Exception("medicineProcessor");
                    }

                    if (!this.materialProcessor.Run(raw, oldMaterials, ref newMaterial, ref sqls))
                    {
                        throw new Exception("materialProcessor");
                    }

                    if (!this.sereServProcessor.Run(raw, treatment, oldSereServs, newMedicine, newMaterial, ref sqls))
                    {
                        throw new Exception("sereServProcessor");
                    }

                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("Sql: " + sqls.ToString());
                    }

                    result = true;
                    resultData = raw;

                    if (raw.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DPK)
                    {
                        new EventLogUtil.EventLogGenerator(LibraryEventLog.EventLog.Enum.HisExpMest_PhucHoiDonKhamKhongLay)
                        .TreatmentCode(treatment.TREATMENT_CODE)
                        .ServiceReqCode(raw.TDL_SERVICE_REQ_CODE)
                        .ExpMestCode(raw.EXP_MEST_CODE).Run();
                    }
                    else if (raw.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DDT)
                    {
                        new EventLogUtil.EventLogGenerator(LibraryEventLog.EventLog.Enum.HisExpMest_PhucHoiDonDieuTriKhongLay)
                        .TreatmentCode(treatment.TREATMENT_CODE)
                        .ServiceReqCode(raw.TDL_SERVICE_REQ_CODE)
                        .ExpMestCode(raw.EXP_MEST_CODE).Run();
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
                this.Rollback();
            }
            return result;
        }

        private void Rollback()
        {
            try
            {
                this.sereServProcessor.Rollback();
                this.materialProcessor.Rollback();
                this.medicineProcessor.Rollback();
                this.expMestProcessor.Rollback();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
