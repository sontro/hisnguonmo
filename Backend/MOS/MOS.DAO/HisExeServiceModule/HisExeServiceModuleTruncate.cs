using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisExeServiceModule
{
    partial class HisExeServiceModuleTruncate : EntityBase
    {
        public HisExeServiceModuleTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXE_SERVICE_MODULE>();
        }

        private BridgeDAO<HIS_EXE_SERVICE_MODULE> bridgeDAO;

        public bool Truncate(HIS_EXE_SERVICE_MODULE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_EXE_SERVICE_MODULE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
