using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisBid;
using MOS.MANAGER.HisSupplier;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMedicalContract.CreateSdo
{
    class HisMedicalContractCreateSdo : BusinessBase
    {
        private MedicalContractProcessor medicalContractProcessor;
        private MaterialProcessor materialProcessor;
        private MedicineProcessor medicineProcessor;

        private HIS_MEDICAL_CONTRACT recentContract;

        internal HisMedicalContractCreateSdo()
            : base()
        {
            this.Init();
        }

        internal HisMedicalContractCreateSdo(CommonParam param)
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
                HIS_BID bid = null;
                HIS_SUPPLIER supplier = null;
                List<HIS_BID_MATERIAL_TYPE> lstBidMaterialType = null;
                List<HIS_BID_MEDICINE_TYPE> lstBidMedicineType = null;
                List<HIS_MATERIAL_TYPE> lstMaterialType = null;
                List<HIS_MEDICINE_TYPE> lstMedicineType = null;

                HisSupplierCheck supplierChecker = new HisSupplierCheck(param);
                HisBidCheck bidChecker = new HisBidCheck(param);
                HisMedicalContractCheck checker = new HisMedicalContractCheck(param);
                valid = valid && checker.VerifyRequireField(data, false);
                valid = valid && checker.ValidData(data);
                valid = valid && checker.IsDuplicateData(data.MaterialTypes, data.MedicineTypes);
                valid = valid && supplierChecker.VerifyId(data.SupplierId, ref supplier);
                valid = valid && supplierChecker.IsUnLock(supplier);
                valid = valid && (!data.BidId.HasValue || bidChecker.VerifyId(data.BidId.Value, ref bid));
                valid = valid && (!data.BidId.HasValue || bidChecker.IsUnLock(bid));
                valid = valid && checker.VerifyBid(data, ref lstBidMaterialType, ref lstBidMedicineType);
                valid = valid && checker.VerifyType(data, ref lstMaterialType, ref lstMedicineType);
                if (valid)
                {
                    List<string> sqls = new List<string>();

                    if (!this.medicalContractProcessor.Run(data, ref this.recentContract))
                    {
                        throw new Exception("medicalContractProcessor. Rollback du lieu");
                    }

                    if (!this.materialProcessor.Run(this.recentContract, data.MaterialTypes, lstMaterialType, ref sqls))
                    {
                        throw new Exception("materialProcessor. Rollback du lieu");
                    }

                    if (!this.medicineProcessor.Run(this.recentContract, data.MedicineTypes, lstMedicineType, ref sqls))
                    {
                        throw new Exception("medicineProcessor. Rollback du lieu");
                    }

                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("sqls: " + sqls.ToString());
                    }
                    result = true;
                    resultData = this.recentContract;
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
