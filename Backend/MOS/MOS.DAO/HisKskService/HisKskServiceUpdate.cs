using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisKskService
{
    partial class HisKskServiceUpdate : EntityBase
    {
        public HisKskServiceUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_KSK_SERVICE>();
        }

        private BridgeDAO<HIS_KSK_SERVICE> bridgeDAO;

        public bool Update(HIS_KSK_SERVICE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_KSK_SERVICE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
