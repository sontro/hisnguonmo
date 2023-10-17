using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisBid;
using MOS.MANAGER.HisSupplier;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMedicalContract.UpdateSdo
{
    class HisMedicalContractUpdateSdo : BusinessBase
    {
        private MedicalContractProcessor medicalContractProcessor;
        private MaterialProcessor materialProcessor;
        private MedicineProcessor medicineProcessor;

        internal HisMedicalContractUpdateSdo()
            : base()
        {
            this.Init();
        }

        internal HisMedicalContractUpdateSdo(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.medicalContractProcessor = new MedicalContractProcessor(param);
            this.materialProcessor = new MaterialProcessor(param);
            this.medicineProcessor = new MedicineProcessor(param);
        }

        internal bool Run(HisMedicalContractSDO data, ref HIS_MEDICAL_CONTRACT resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_MEDICAL_CONTRACT raw = null;
                List<HIS_BID_MATERIAL_TYPE> lstBidMaterialType = null;
                List<HIS_BID_MEDICINE_TYPE> lstBidMedicineType = null;
                List<HIS_MATERIAL_TYPE> lstMaterialType = null;
                List<HIS_MEDICINE_TYPE> lstMedicineType = null;
                List<HIS_MEDI_CONTRACT_MATY> lstOldMaty = null;
                List<HIS_MEDI_CONTRACT_METY> lstOldMety = null;
                List<HIS_MEDICINE> medicines = null;
                List<HIS_MATERIAL> materials = null;

                HisMedicalContractCheck checker = new HisMedicalContractCheck(param);
                valid = valid && checker.VerifyRequireField(data, true);
                valid = valid && checker.ValidData(data);
                valid = valid && checker.IsDuplicateData(data.MaterialTypes, data.MedicineTypes);
                valid = valid && checker.VerifyId(data.Id.Value, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.VerifyChangeInfo(data, raw, ref lstOldMaty, ref lstOldMety, ref medicines, ref materials);
                valid = valid && checker.VerifyBidUpdate(data, ref lstBidMaterialType, ref lstBidMedicineType, lstOldMaty, lstOldMety);
                valid = valid && checker.VerifyType(data, ref lstMaterialType, ref lstMedicineType);
                if (valid)
                {
                    List<string> sqls = new List<string>();
                    List<string> changePriceLogs = new List<string>();
                    List<HIS_SALE_PROFIT_CFG> saleProfitCfgs = null;

                    if (!this.medicalContractProcessor.Run(data, raw))
                    {
                        throw new Exception("medicalContractProcessor. Rollback du lieu");
                    }

                    if (!this.materialProcessor.Run(raw, data.MaterialTypes, lstMaterialType, materials, lstOldMaty, ref sqls, ref changePriceLogs, ref saleProfitCfgs))
                    {
                        throw new Exception("materialProcessor. Rollback du lieu");
                    }

                    if (!this.medicineProcessor.Run(raw, data.MedicineTypes, lstMedicineType, medicines, lstOldMety, ref sqls, ref changePriceLogs, ref saleProfitCfgs))
                    {
                        throw new Exception("medicineProcessor. Rollback du lieu");
                    }

                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("sqls: " + sqls.ToString());
                    }

                    if (!IsNotNullOrEmpty(changePriceLogs))
                    {
                        new EventLogGenerator(LibraryEventLog.EventLog.Enum.HisMedicalContract_Sua, raw.MEDICAL_CONTRACT_CODE, raw.MEDICAL_CONTRACT_NAME).Run();
                    }
                    else
                    {
                        string changePriceLogStr = string.Join("; ", changePriceLogs);
                        new EventLogGenerator(LibraryEventLog.EventLog.Enum.HisMedicalContract_SuaCoSuaGia, raw.MEDICAL_CONTRACT_CODE, raw.MEDICAL_CONTRACT_NAME, changePriceLogStr).Run();
                    }
                    
                    result = true;
                    resultData = raw;
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
                medicineProcessor.Rollback();
                materialProcessor.Rollback();
                medicalContractProcessor.Rollback();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
