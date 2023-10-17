using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisObeyContraindi
{
    partial class HisObeyContraindiUpdate : EntityBase
    {
        public HisObeyContraindiUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_OBEY_CONTRAINDI>();
        }

        private BridgeDAO<HIS_OBEY_CONTRAINDI> bridgeDAO;

        public bool Update(HIS_OBEY_CONTRAINDI data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_OBEY_CONTRAINDI> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
