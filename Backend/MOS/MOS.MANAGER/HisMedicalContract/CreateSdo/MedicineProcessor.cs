using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisMediContractMety;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMedicalContract.CreateSdo
{
    class MedicineProcessor : BusinessBase
    {
        private HisMediContractMetyCreate mediContractMetyCreate;

        internal MedicineProcessor(CommonParam param)
            : base(param)
        {
            this.mediContractMetyCreate = new HisMediContractMetyCreate(param);
        }

        internal bool Run(HIS_MEDICAL_CONTRACT contract, List<HisMediContractMetySDO> medicineSdos, List<HIS_MEDICINE_TYPE> medicineTypes, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(medicineSdos))
                {
                    List<HIS_MEDI_CONTRACT_METY> contractMetys = MakeMediContractMety(contract, medicineSdos, medicineTypes, ref sqls);

                    if (!this.mediContractMetyCreate.CreateList(contractMetys))
                    {
                        throw new Exception("mediContractMetyCreate. Ket thuc nghiep vu");
                    }

                }
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }

        private List<HIS_MEDI_CONTRACT_METY> MakeMediContractMety(HIS_MEDICAL_CONTRACT contract, List<HisMediContractMetySDO> medicineSdos, List<HIS_MEDICINE_TYPE> medicineTypes, ref List<string> sqls)
        {
            List<HIS_MEDI_CONTRACT_METY> contractMetys = new List<HIS_MEDI_CONTRACT_METY>();

            foreach (HisMediContractMetySDO sdo in medicineSdos)
            {
                HIS_MEDICINE_TYPE type = medicineTypes.FirstOrDefault(o => o.ID == sdo.MedicineTypeId);
                HIS_MEDI_CONTRACT_METY detail = new HIS_MEDI_CONTRACT_METY();
                detail.AMOUNT = sdo.Amount;
                detail.BID_MEDICINE_TYPE_ID = sdo.BidMedicineTypeId;
                detail.CONCENTRA = sdo.Concentra;
                detail.DAY_LIFESPAN = sdo.DayLifespan;
                detail.EXPIRED_DATE = sdo.ExpiredDate;
                detail.IMP_PRICE = sdo.ImpPrice;
                detail.IMP_VAT_RATIO = sdo.ImpVatRatio;
                detail.INTERNAL_PRICE = type.INTERNAL_PRICE;
                detail.MANUFACTURER_ID = sdo.ManufacturerId;
                detail.MEDICINE_TYPE_ID = sdo.MedicineTypeId;
                detail.MEDICAL_CONTRACT_ID = contract.ID;
                detail.MONTH_LIFESPAN = sdo.MonthLifespan;
                detail.NATIONAL_NAME = sdo.NationalName;
                detail.HOUR_LIFESPAN = sdo.HourLifespan;
                detail.CONTRACT_PRICE = sdo.ContractPrice;
                detail.MEDICINE_REGISTER_NUMBER = sdo.MedicineRegisterNumber;
                detail.IMP_EXPIRED_DATE = sdo.ImpExpiredDate;
                detail.NOTE = sdo.Note;
                detail.BID_NUMBER = sdo.BidNumber;
                detail.BID_GROUP_CODE = sdo.BidGroupCode;

                if (sdo.BidMedicineTypeId.HasValue)
                {
                    sqls.Add(String.Format("UPDATE HIS_BID_MEDICINE_TYPE SET TDL_CONTRACT_AMOUNT = (NVL(TDL_CONTRACT_AMOUNT,0)+{0}) WHERE ID = {1}", sdo.Amount, sdo.BidMedicineTypeId.Value));
                }

                contractMetys.Add(detail);
            }

            return contractMetys;
        }


        internal void Rollback()
        {
            try
            {
                this.mediContractMetyCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
