using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisImpMestPay
{
    partial class HisImpMestPayUpdate : EntityBase
    {
        public HisImpMestPayUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_IMP_MEST_PAY>();
        }

        private BridgeDAO<HIS_IMP_MEST_PAY> bridgeDAO;

        public bool Update(HIS_IMP_MEST_PAY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_IMP_MEST_PAY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
