using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Common.NotTaken
{
    class MaterialProcessor : BusinessBase
    {
        internal MaterialProcessor(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MATERIAL> materials, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(materials))
                {
                    string query = DAOWorker.SqlDAO.AddInClause(materials.Select(s => s.ID).ToList(), "UPDATE HIS_MATERIAL_BEAN SET IS_ACTIVE = 1, EXP_MEST_MATERIAL_ID = NULL WHERE MEDI_STOCK_ID = {0} AND %IN_CLAUSE% ", "EXP_MEST_MATERIAL_ID");
                    string sql= string.Format(query, expMest.MEDI_STOCK_ID);
                    sqls.Add(sql);
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
    }
}
