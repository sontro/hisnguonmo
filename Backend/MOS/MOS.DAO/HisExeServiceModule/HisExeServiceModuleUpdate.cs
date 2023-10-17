using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisExeServiceModule
{
    partial class HisExeServiceModuleUpdate : EntityBase
    {
        public HisExeServiceModuleUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXE_SERVICE_MODULE>();
        }

        private BridgeDAO<HIS_EXE_SERVICE_MODULE> bridgeDAO;

        public bool Update(HIS_EXE_SERVICE_MODULE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_EXE_SERVICE_MODULE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
