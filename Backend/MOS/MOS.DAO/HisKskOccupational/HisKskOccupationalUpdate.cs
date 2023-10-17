using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisKskOccupational
{
    partial class HisKskOccupationalUpdate : EntityBase
    {
        public HisKskOccupationalUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_KSK_OCCUPATIONAL>();
        }

        private BridgeDAO<HIS_KSK_OCCUPATIONAL> bridgeDAO;

        public bool Update(HIS_KSK_OCCUPATIONAL data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_KSK_OCCUPATIONAL> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
