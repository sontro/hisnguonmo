using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Common.ApproveNotTaken
{
    class MedicineProcessor : BusinessBase
    {
        internal MedicineProcessor(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MEDICINE> medicines, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(medicines))
                {
                    string query = DAOWorker.SqlDAO.AddInClause(medicines.Select(s => s.ID).ToList(), "UPDATE HIS_MEDICINE_BEAN SET IS_ACTIVE = 1, EXP_MEST_MEDICINE_ID = NULL WHERE MEDI_STOCK_ID = {0} AND %IN_CLAUSE% ", "EXP_MEST_MEDICINE_ID");
                    string sql = string.Format(query, expMest.MEDI_STOCK_ID);
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
