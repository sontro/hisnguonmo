using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisDispense.Packing.Unconfirm
{
    class MaterialMaterialProcessor : BusinessBase
    {
        internal MaterialMaterialProcessor()
            : base()
        {

        }

        internal MaterialMaterialProcessor(CommonParam param)
            : base()
        {

        }

        internal bool Run(HIS_IMP_MEST_MATERIAL impMestMaterial, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                string sqlDelete = String.Format("DELETE HIS_MATERIAL_MATERIAL WHERE MATERIAL_ID = {0}", impMestMaterial.MATERIAL_ID);
                sqls.Add(sqlDelete);
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}
