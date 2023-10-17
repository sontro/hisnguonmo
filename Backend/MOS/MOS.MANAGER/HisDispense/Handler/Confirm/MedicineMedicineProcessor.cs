using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisMedicineMedicine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisDispense.Handler.Confirm
{
    class MedicineMedicineProcessor : BusinessBase
    {
        private HisMedicineMedicineCreate hisMedicineMedicineCreate;

        internal MedicineMedicineProcessor()
            : base()
        {
            this.hisMedicineMedicineCreate = new HisMedicineMedicineCreate(param);
        }

        internal MedicineMedicineProcessor(CommonParam param)
            : base(param)
        {
            this.hisMedicineMedicineCreate = new HisMedicineMedicineCreate(param);
        }

        internal bool Run(HIS_MEDICINE medicine, List<HIS_EXP_MEST_MEDICINE> expMestMedicines)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(expMestMedicines))
                {
                    List<HIS_MEDICINE_MEDICINE> medicineMedicines = new List<HIS_MEDICINE_MEDICINE>();
                    var Groups = expMestMedicines.GroupBy(g => g.MEDICINE_ID).ToList();
                    foreach (var group in Groups)
                    {
                        HIS_MEDICINE_MEDICINE mm = new HIS_MEDICINE_MEDICINE();
                        mm.MEDICINE_ID = medicine.ID;
                        mm.PREPARATION_MEDICINE_ID = group.Key.Value;
                        mm.PREPARATION_AMOUNT = group.Sum(s => s.AMOUNT);
                        medicineMedicines.Add(mm);
                    }
                    if (!this.hisMedicineMedicineCreate.CreateList(medicineMedicines))
                    {
                        throw new Exception("hisMedicineMedicineCreate. Ket thuc nghiep vu");
                    }
                }
                result = true;
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
                this.hisMedicineMedicineCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
