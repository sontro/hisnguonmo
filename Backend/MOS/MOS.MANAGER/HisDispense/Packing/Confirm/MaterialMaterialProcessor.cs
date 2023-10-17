using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisMaterialMaterial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisDispense.Packing.Confirm
{
    class MaterialMaterialProcessor : BusinessBase
    {
        private HisMaterialMaterialCreate hisMaterialMaterialCreate;

        internal MaterialMaterialProcessor()
            : base()
        {
            this.hisMaterialMaterialCreate = new HisMaterialMaterialCreate(param);
        }

        internal MaterialMaterialProcessor(CommonParam param)
            : base(param)
        {
            this.hisMaterialMaterialCreate = new HisMaterialMaterialCreate(param);
        }

        internal bool Run(HIS_MATERIAL material, List<HIS_EXP_MEST_MATERIAL> expMestMaterials)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(expMestMaterials))
                {
                    List<HIS_MATERIAL_MATERIAL> materialMaterials = new List<HIS_MATERIAL_MATERIAL>();
                    var Groups = expMestMaterials.GroupBy(g => g.MATERIAL_ID).ToList();
                    foreach (var group in Groups)
                    {
                        HIS_MATERIAL_MATERIAL mm = new HIS_MATERIAL_MATERIAL();
                        mm.MATERIAL_ID = material.ID;
                        mm.PREPARATION_MATERIAL_ID = group.Key.Value;
                        mm.PREPARATION_AMOUNT = group.Sum(s => s.AMOUNT);
                        materialMaterials.Add(mm);
                    }
                    if (!this.hisMaterialMaterialCreate.CreateList(materialMaterials))
                    {
                        throw new Exception("hisMaterialMaterialCreate. Ket thuc nghiep vu");
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
                this.hisMaterialMaterialCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
