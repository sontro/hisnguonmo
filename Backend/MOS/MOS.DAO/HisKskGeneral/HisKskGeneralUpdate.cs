using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisKskGeneral
{
    partial class HisKskGeneralUpdate : EntityBase
    {
        public HisKskGeneralUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_KSK_GENERAL>();
        }

        private BridgeDAO<HIS_KSK_GENERAL> bridgeDAO;

        public bool Update(HIS_KSK_GENERAL data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_KSK_GENERAL> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
