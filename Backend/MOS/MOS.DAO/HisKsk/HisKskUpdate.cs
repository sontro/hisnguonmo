using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisKsk
{
    partial class HisKskUpdate : EntityBase
    {
        public HisKskUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_KSK>();
        }

        private BridgeDAO<HIS_KSK> bridgeDAO;

        public bool Update(HIS_KSK data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_KSK> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
