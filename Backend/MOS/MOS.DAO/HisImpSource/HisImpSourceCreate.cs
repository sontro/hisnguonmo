using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisImpSource
{
    partial class HisImpSourceCreate : EntityBase
    {
        public HisImpSourceCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_IMP_SOURCE>();
        }

        private BridgeDAO<HIS_IMP_SOURCE> bridgeDAO;

        public bool Create(HIS_IMP_SOURCE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_IMP_SOURCE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
