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

namespace MOS.MANAGER.HisExpMestMetyReq
{
    partial class HisExpMestMetyReqGet : BusinessBase
    {
        internal List<L_HIS_EXP_MEST_METY_REQ> GetLView(HisExpMestMetyReqLViewFilterQuery filter)
        {
            try
            {
                List<L_HIS_EXP_MEST_METY_REQ> result = DAOWorker.HisExpMestMetyReqDAO.GetLView(filter.Query(), param);
                return result;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal L_HIS_EXP_MEST_METY_REQ GetLViewById(long id)
        {
            try
            {
                return GetLViewById(id, new HisExpMestMetyReqLViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal L_HIS_EXP_MEST_METY_REQ GetLViewById(long id, HisExpMestMetyReqLViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExpMestMetyReqDAO.GetLViewById(id, filter.Query());
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
