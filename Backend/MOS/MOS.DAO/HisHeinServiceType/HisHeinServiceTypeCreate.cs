using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisHeinServiceType
{
    partial class HisHeinServiceTypeCreate : EntityBase
    {
        public HisHeinServiceTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_HEIN_SERVICE_TYPE>();
        }

        private BridgeDAO<HIS_HEIN_SERVICE_TYPE> bridgeDAO;

        public bool Create(HIS_HEIN_SERVICE_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_HEIN_SERVICE_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
