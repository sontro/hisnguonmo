using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisHtu
{
    partial class HisHtuUpdate : EntityBase
    {
        public HisHtuUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_HTU>();
        }

        private BridgeDAO<HIS_HTU> bridgeDAO;

        public bool Update(HIS_HTU data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_HTU> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
