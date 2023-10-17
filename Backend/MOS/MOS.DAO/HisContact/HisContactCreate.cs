using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisContact
{
    partial class HisContactCreate : EntityBase
    {
        public HisContactCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CONTACT>();
        }

        private BridgeDAO<HIS_CONTACT> bridgeDAO;

        public bool Create(HIS_CONTACT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_CONTACT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
