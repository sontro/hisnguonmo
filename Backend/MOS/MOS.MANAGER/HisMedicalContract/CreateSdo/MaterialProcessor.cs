using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisMediContractMaty;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMedicalContract.CreateSdo
{
    class MaterialProcessor : BusinessBase
    {
        private HisMediContractMatyCreate mediContractMatyCreate;

        internal MaterialProcessor(CommonParam param)
            : base(param)
        {
            this.mediContractMatyCreate = new HisMediContractMatyCreate(param);
        }

        internal bool Run(HIS_MEDICAL_CONTRACT contract, List<HisMediContractMatySDO> materialSdos, List<HIS_MATERIAL_TYPE> materialTypes, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(materialSdos))
                {
                    List<HIS_MEDI_CONTRACT_MATY> contractMatys = MakeMediContractMaty(contract, materialSdos, materialTypes, ref sqls);

                    if (!this.mediContractMatyCreate.CreateList(contractMatys))
                    {
                        throw new Exception("mediContractMatyCreate. Ket thuc nghiep vu");
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

        private List<HIS_MEDI_CONTRACT_MATY> MakeMediContractMaty(HIS_MEDICAL_CONTRACT contract, List<HisMediContractMatySDO> materialSdos, List<HIS_MATERIAL_TYPE> materialTypes, ref List<string> sqls)
        {
            List<HIS_MEDI_CONTRACT_MATY> contractMatys = new List<HIS_MEDI_CONTRACT_MATY>();

            foreach (HisMediContractMatySDO sdo in materialSdos)
            {
                HIS_MATERIAL_TYPE type = materialTypes.FirstOrDefault(o => o.ID == sdo.MaterialTypeId);
                HIS_MEDI_CONTRACT_MATY detail = new HIS_MEDI_CONTRACT_MATY();
                detail.AMOUNT = sdo.Amount;
                detail.BID_MATERIAL_TYPE_ID = sdo.BidMaterialTypeId;
                detail.CONCENTRA = sdo.Concentra;
                detail.DAY_LIFESPAN = sdo.DayLifespan;
                detail.EXPIRED_DATE = sdo.ExpiredDate;
                detail.IMP_PRICE = sdo.ImpPrice;
                detail.IMP_VAT_RATIO = sdo.ImpVatRatio;
                detail.INTERNAL_PRICE = type.INTERNAL_PRICE;
                detail.MANUFACTURER_ID = sdo.ManufacturerId;
                detail.MATERIAL_TYPE_ID = sdo.MaterialTypeId;
                detail.MEDICAL_CONTRACT_ID = contract.ID;
                detail.MONTH_LIFESPAN = sdo.MonthLifespan;
                detail.NATIONAL_NAME = sdo.NationalName;
                detail.HOUR_LIFESPAN = sdo.HourLifespan;
                detail.CONTRACT_PRICE = sdo.ContractPrice;
                detail.IMP_EXPIRED_DATE = sdo.ImpExpiredDate;
                detail.NOTE = sdo.Note;
                detail.BID_NUMBER = sdo.BidNumber;
                detail.BID_GROUP_CODE = sdo.BidGroupCode;

                if (sdo.BidMaterialTypeId.HasValue)
                {
                    sqls.Add(String.Format("UPDATE HIS_BID_MATERIAL_TYPE SET TDL_CONTRACT_AMOUNT = (NVL(TDL_CONTRACT_AMOUNT,0)+{0}) WHERE ID = {1}", sdo.Amount, sdo.BidMaterialTypeId.Value));
                }

                contractMatys.Add(detail);
            }

            return contractMatys;
        }

        internal void Rollback()
        {
            try
            {
                this.mediContractMatyCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
