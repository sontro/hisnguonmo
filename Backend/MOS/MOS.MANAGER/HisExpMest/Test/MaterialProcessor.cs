using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.SDO;
using MOS.UTILITY;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Test
{
    class MaterialProcessor : BusinessBase
    {
        private HisExpMestMaterialMaker hisExpMestMaterialMaker;

        internal MaterialProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisExpMestMaterialMaker = new HisExpMestMaterialMaker(param);
        }

        internal bool Run(List<ExpMaterialTypeSDO> materials, HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_MATERIAL> expMestMaterials, ref List<string> sqls)
        {
            try
            {
                if (IsNotNullOrEmpty(materials) && expMest != null)
                {
                    string loginname = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    string username = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                    //Tao exp_mest_material
                    if (!this.hisExpMestMaterialMaker.Run(materials, expMest, null, loginname, username, null, false, ref expMestMaterials, ref sqls))
                    {
                        throw new Exception("exp_mest_material: Rollback du lieu. Ket thuc nghiep vu");
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                this.Rollback();
                LogSystem.Error(ex);
                return false;
            }
        }

        internal void Rollback()
        {
            this.hisExpMestMaterialMaker.Rollback();
        }
    }
}
