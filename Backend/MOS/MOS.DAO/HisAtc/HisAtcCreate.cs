using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisAtc
{
    partial class HisAtcCreate : EntityBase
    {
        public HisAtcCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ATC>();
        }

        private BridgeDAO<HIS_ATC> bridgeDAO;

        public bool Create(HIS_ATC data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_ATC> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
