using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Aggr.Unapprove
{
    class ImpMestProcessor : BusinessBase
    {
        internal ImpMestProcessor()
            : base()
        {
        }

        internal ImpMestProcessor(CommonParam param)
            : base(param)
        {
        }

        internal bool Run(HIS_IMP_MEST aggrImpMest, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                string sqlImpMestMedicine = string.Format("UPDATE HIS_IMP_MEST_MEDICINE IMM SET AMOUNT = REQ_AMOUNT WHERE EXISTS (SELECT 1 FROM HIS_IMP_MEST IMP WHERE IMP.AGGR_IMP_MEST_ID = {0} AND IMM.IMP_MEST_ID = IMP.ID) AND AMOUNT <> REQ_AMOUNT", aggrImpMest.ID);
                string sqlImpMestMaterial = string.Format("UPDATE HIS_IMP_MEST_MATERIAL IMM SET AMOUNT = REQ_AMOUNT WHERE EXISTS (SELECT 1 FROM HIS_IMP_MEST IMP WHERE IMP.AGGR_IMP_MEST_ID = {0} AND IMM.IMP_MEST_ID = IMP.ID) AND AMOUNT <> REQ_AMOUNT", aggrImpMest.ID);
                string sqlImpMestChild = string.Format("UPDATE HIS_IMP_MEST SET IMP_MEST_STT_ID = {0}, APPROVAL_LOGINNAME = NULL, APPROVAL_USERNAME = NULL, APPROVAL_TIME = NULL WHERE AGGR_IMP_MEST_ID = {1}", IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REQUEST, aggrImpMest.ID);
                string sqlImpMest = string.Format("UPDATE HIS_IMP_MEST SET IMP_MEST_STT_ID = {0}, APPROVAL_LOGINNAME = NULL, APPROVAL_USERNAME = NULL, APPROVAL_TIME = NULL WHERE ID = {1}", IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REQUEST, aggrImpMest.ID);

                sqls.Add(sqlImpMestMedicine);
                sqls.Add(sqlImpMestMaterial);
                sqls.Add(sqlImpMestChild);
                sqls.Add(sqlImpMest);

                //Update lai doi tuong de tra ve cho client
                aggrImpMest.IMP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REQUEST;
                aggrImpMest.APPROVAL_LOGINNAME = null;
                aggrImpMest.APPROVAL_USERNAME = null;
                aggrImpMest.APPROVAL_TIME = null;

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
