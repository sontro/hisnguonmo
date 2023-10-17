using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.DynamicDTO;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisServiceReq
{
    partial class HisServiceReqGet : GetBase
    {
        internal List<HisServiceReqDTO> GetDynamic(HisServiceReqFilterQuery filter)
        {
            try
            {
                if (!IsNotNullOrEmpty(filter.ColumnParams))
                {
                    throw new Exception("ColumnParams Is Empty");
                }
                return DAOWorker.HisServiceReqDAO.GetDynamic(filter.Query(), param);
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
