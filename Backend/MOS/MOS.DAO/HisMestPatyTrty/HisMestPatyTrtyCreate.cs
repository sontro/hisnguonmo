using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMestPatyTrty
{
    partial class HisMestPatyTrtyCreate : EntityBase
    {
        public HisMestPatyTrtyCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_PATY_TRTY>();
        }

        private BridgeDAO<HIS_MEST_PATY_TRTY> bridgeDAO;

        public bool Create(HIS_MEST_PATY_TRTY data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MEST_PATY_TRTY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
