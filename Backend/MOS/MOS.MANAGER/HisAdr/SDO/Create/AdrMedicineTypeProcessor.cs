using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisAdrMedicineType;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisAdr.SDO.Create
{
    class AdrMedicineTypeProcessor : BusinessBase
    {
        private HisAdrMedicineTypeCreate hisAdrMedicineTypeCreate;

        internal AdrMedicineTypeProcessor()
            : base()
        {
            this.hisAdrMedicineTypeCreate = new HisAdrMedicineTypeCreate(param);
        }

        internal AdrMedicineTypeProcessor(CommonParam param)
            : base(param)
        {
            this.hisAdrMedicineTypeCreate = new HisAdrMedicineTypeCreate(param);
        }

        internal bool Run(HisAdrSDO data, HIS_ADR adr, ref List<HIS_ADR_MEDICINE_TYPE> adrMedicineTypes)
        {
            bool result = false;
            try
            {
                if (data != null && IsNotNullOrEmpty(data.AdrMedicineTypes))
                {
                    data.AdrMedicineTypes.ForEach(o => o.ADR_ID = adr.ID);
                    if (!this.hisAdrMedicineTypeCreate.CreateList(data.AdrMedicineTypes))
                    {
                        throw new Exception("Ket thuc nghiep vu");
                    }
                    adrMedicineTypes = data.AdrMedicineTypes;
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal void RollbackData()
        {
            try
            {
                this.hisAdrMedicineTypeCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
