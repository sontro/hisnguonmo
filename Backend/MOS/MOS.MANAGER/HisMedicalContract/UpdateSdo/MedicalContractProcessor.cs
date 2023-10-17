using AutoMapper;
using Inventec.Common.ObjectChecker;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMedicalContract.UpdateSdo
{
    class MedicalContractProcessor : BusinessBase
    {
        private HisMedicalContractUpdate hisMedicalContractUpdate;

        internal MedicalContractProcessor(CommonParam param)
            : base(param)
        {
            this.hisMedicalContractUpdate = new HisMedicalContractUpdate(param);
        }

        internal bool Run(HisMedicalContractSDO data, HIS_MEDICAL_CONTRACT raw)
        {
            bool result = false;
            try
            {
                Mapper.CreateMap<HIS_MEDICAL_CONTRACT, HIS_MEDICAL_CONTRACT>();
                HIS_MEDICAL_CONTRACT before = Mapper.Map<HIS_MEDICAL_CONTRACT>(raw);

                raw.DOCUMENT_SUPPLIER_ID = data.DocumentSupplierId;
                raw.MEDICAL_CONTRACT_CODE = data.MedicalContractCode;
                raw.MEDICAL_CONTRACT_NAME = data.MedicalContractName;
                raw.NOTE = data.Note;
                raw.VALID_FROM_DATE = data.ValidFromDate;
                raw.VALID_TO_DATE = data.ValidToDate;
                raw.VENTURE_AGREENING = data.VentureAgreening;

                if (ValueChecker.IsPrimitiveDiff<HIS_MEDICAL_CONTRACT>(before, raw) && !this.hisMedicalContractUpdate.Update(raw, before))
                {
                    throw new Exception("hisMedicalContractCreate. Ket thuc nghiep vu");
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

        internal void Rollback()
        {
            try
            {
                this.hisMedicalContractUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
