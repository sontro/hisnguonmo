using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisImpMestMaterial;
using MOS.MANAGER.HisImpMestMedicine;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Aggr.Unapprove
{
    /// <summary>
    /// Xu ly de huy cac phieu nhap luu thuoc/vat tu bi tu choi duyet khi thuc hien duyet
    /// </summary>
    public class RejectImpMestProcessor : BusinessBase
    {
        internal RejectImpMestProcessor()
            : base()
        {
        }

        internal RejectImpMestProcessor(CommonParam param)
            : base(param)
        {
        }

        internal bool Run(List<HIS_IMP_MEST> rejectedItemImpMests, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(rejectedItemImpMests))
                {
                    List<long> ids = rejectedItemImpMests.Select(o => o.ID).ToList();
                    
                    string idStr = string.Join(",", ids);

                    string sqlImpMestMedicine = string.Format("DELETE FROM HIS_IMP_MEST_MEDICINE WHERE IMP_MEST_ID IN ({0})", idStr);
                    string sqlImpMestMaterial = string.Format("DELETE FROM HIS_IMP_MEST_MATERIAL WHERE IMP_MEST_ID IN ({0})", idStr);
                    string sqlImpMest = string.Format("DELETE FROM HIS_IMP_MEST WHERE ID IN ({0})", idStr);

                    sqls.Add(sqlImpMestMedicine);
                    sqls.Add(sqlImpMestMaterial);
                    sqls.Add(sqlImpMest);
                }
                
                result = true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
