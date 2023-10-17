using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisOweType
{
    partial class HisOweTypeCreate : EntityBase
    {
        public HisOweTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_OWE_TYPE>();
        }

        private BridgeDAO<HIS_OWE_TYPE> bridgeDAO;

        public bool Create(HIS_OWE_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_OWE_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
