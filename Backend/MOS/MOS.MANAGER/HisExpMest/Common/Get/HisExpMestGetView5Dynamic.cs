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

namespace MOS.MANAGER.HisExpMest.Common.Get
{
    partial class HisExpMestGet : GetBase
    {
        internal List<HisExpMestView5DTO> GetView5Dynamic(HisExpMestView5FilterQuery filter)
        {
            try
            {
                if (!IsNotNullOrEmpty(filter.ColumnParams))
                {
                    throw new Exception("ColumnParams Is Empty");
                }
                return DAOWorker.HisExpMestDAO.GetView5Dynamic(filter.Query(), param);
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
