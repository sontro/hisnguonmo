using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisRestRetrType
{
    partial class HisRestRetrTypeCreate : EntityBase
    {
        public HisRestRetrTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_REST_RETR_TYPE>();
        }

        private BridgeDAO<HIS_REST_RETR_TYPE> bridgeDAO;

        public bool Create(HIS_REST_RETR_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_REST_RETR_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
