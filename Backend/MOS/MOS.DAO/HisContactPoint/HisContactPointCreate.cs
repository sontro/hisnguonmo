using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisContactPoint
{
    partial class HisContactPointCreate : EntityBase
    {
        public HisContactPointCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CONTACT_POINT>();
        }

        private BridgeDAO<HIS_CONTACT_POINT> bridgeDAO;

        public bool Create(HIS_CONTACT_POINT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_CONTACT_POINT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
