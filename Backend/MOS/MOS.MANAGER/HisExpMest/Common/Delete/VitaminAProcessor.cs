using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisVitaminA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Common.Delete
{
    class VitaminAProcessor : BusinessBase
    {
        private long expMestId;

        internal VitaminAProcessor(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HIS_EXP_MEST expMest, ref List<string> listSql)
        {
            bool result = false;
            try
            {
                if (expMest != null && expMest.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__VITAMINA)
                {
                    bool valid = true;
                    this.expMestId = expMest.ID;
                    List<HIS_VITAMIN_A> hisVitaminAs = new HisVitaminAGet().GetByExpMestId(expMest.ID);
                    if (IsNotNullOrEmpty(hisVitaminAs))
                    {
                        valid = valid && new HisVitaminACheck(param).IsUnLock(hisVitaminAs);
                        if (valid)
                        {

                            List<long> vitaminAIds = hisVitaminAs.Select(o => o.ID).ToList();

                            string updateVitaminA = DAOWorker.SqlDAO.AddInClause(vitaminAIds, "UPDATE HIS_VITAMIN_A SET EXP_MEST_ID = NULL WHERE %IN_CLAUSE% ", "ID");
                            listSql.Add(updateVitaminA);
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
