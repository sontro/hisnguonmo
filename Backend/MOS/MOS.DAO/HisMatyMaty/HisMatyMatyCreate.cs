using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMatyMaty
{
    partial class HisMatyMatyCreate : EntityBase
    {
        public HisMatyMatyCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MATY_MATY>();
        }

        private BridgeDAO<HIS_MATY_MATY> bridgeDAO;

        public bool Create(HIS_MATY_MATY data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MATY_MATY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
