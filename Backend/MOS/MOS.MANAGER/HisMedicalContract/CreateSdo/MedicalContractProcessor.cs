using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMedicalContract.CreateSdo
{
    class MedicalContractProcessor : BusinessBase
    {
        private HisMedicalContractCreate hisMedicalContractCreate;

        internal MedicalContractProcessor(CommonParam param)
            : base(param)
        {
            this.hisMedicalContractCreate = new HisMedicalContractCreate(param);
        }

        internal bool Run(HisMedicalContractSDO data, ref HIS_MEDICAL_CONTRACT resultData)
        {
            bool result = false;
            try
            {
                HIS_MEDICAL_CONTRACT contract = new HIS_MEDICAL_CONTRACT();
                contract.BID_ID = data.BidId;
                contract.DOCUMENT_SUPPLIER_ID = data.DocumentSupplierId;
                contract.MEDICAL_CONTRACT_CODE = data.MedicalContractCode;
                contract.MEDICAL_CONTRACT_NAME = data.MedicalContractName;
                contract.NOTE = data.Note;
                contract.SUPPLIER_ID = data.SupplierId;
                contract.VALID_FROM_DATE = data.ValidFromDate;
                contract.VALID_TO_DATE = data.ValidToDate;
                contract.VENTURE_AGREENING = data.VentureAgreening;

                if (!this.hisMedicalContractCreate.Create(contract))
                {
                    throw new Exception("hisMedicalContractCreate. Ket thuc nghiep vu");
                }
                resultData = contract;
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

        internal void Rollback()
        {
            try
            {
                this.hisMedicalContractCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
