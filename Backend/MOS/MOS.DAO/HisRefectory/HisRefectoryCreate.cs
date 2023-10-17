using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisRefectory
{
    partial class HisRefectoryCreate : EntityBase
    {
        public HisRefectoryCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_REFECTORY>();
        }

        private BridgeDAO<HIS_REFECTORY> bridgeDAO;

        public bool Create(HIS_REFECTORY data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_REFECTORY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
