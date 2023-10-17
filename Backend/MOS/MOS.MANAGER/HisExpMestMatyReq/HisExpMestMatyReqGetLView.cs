using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest;
using MOS.MANAGER.HisMedicineBean;
using MOS.SDO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExpMestMatyReq
{
    partial class HisExpMestMatyReqGet : BusinessBase
    {
        internal List<L_HIS_EXP_MEST_MATY_REQ> GetLView(HisExpMestMatyReqLViewFilterQuery filter)
        {
            try
            {
                List<L_HIS_EXP_MEST_MATY_REQ> result = DAOWorker.HisExpMestMatyReqDAO.GetLView(filter.Query(), param);
                return result;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal L_HIS_EXP_MEST_MATY_REQ GetLViewById(long id)
        {
            try
            {
                return GetLViewById(id, new HisExpMestMatyReqLViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal L_HIS_EXP_MEST_MATY_REQ GetLViewById(long id, HisExpMestMatyReqLViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExpMestMatyReqDAO.GetLViewById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
