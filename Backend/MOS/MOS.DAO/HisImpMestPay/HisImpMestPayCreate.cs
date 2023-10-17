using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisImpMestPay
{
    partial class HisImpMestPayCreate : EntityBase
    {
        public HisImpMestPayCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_IMP_MEST_PAY>();
        }

        private BridgeDAO<HIS_IMP_MEST_PAY> bridgeDAO;

        public bool Create(HIS_IMP_MEST_PAY data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_IMP_MEST_PAY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
