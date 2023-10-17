using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisMedicineMaterial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisDispense.Handler.Confirm
{
    class MedicineMaterialProcessor  : BusinessBase
    {
        private HisMedicineMaterialCreate hisMedicineMaterialCreate;

        internal MedicineMaterialProcessor()
            : base()
        {
            this.hisMedicineMaterialCreate = new HisMedicineMaterialCreate(param);
        }

        internal MedicineMaterialProcessor(CommonParam param)
            : base(param)
        {
            this.hisMedicineMaterialCreate = new HisMedicineMaterialCreate(param);
        }

        internal bool Run(HIS_MEDICINE medicine, List<HIS_EXP_MEST_MATERIAL> expMestMaterials)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(expMestMaterials))
                {
                    List<HIS_MEDICINE_MATERIAL> medicineMaterials = new List<HIS_MEDICINE_MATERIAL>();
                    var Groups = expMestMaterials.GroupBy(g => g.MATERIAL_ID).ToList();
                    foreach (var group in Groups)
                    {
                        HIS_MEDICINE_MATERIAL mm = new HIS_MEDICINE_MATERIAL();
                        mm.MEDICINE_ID = medicine.ID;
                        mm.MATERIAL_ID = group.Key.Value;
                        mm.MATERIAL_AMOUNT = group.Sum(s => s.AMOUNT);
                        medicineMaterials.Add(mm);
                    }
                    if (!this.hisMedicineMaterialCreate.CreateList(medicineMaterials))
                    {
                        throw new Exception("hisMedicineMaterialCreate. Ket thuc nghiep vu");
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
                this.hisMedicineMaterialCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
