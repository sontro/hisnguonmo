using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.DAO.HisAccidentCare
{
    partial class HisAccidentCareCreate : EntityBase
    {
        public HisAccidentCareCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ACCIDENT_CARE>();
        }

        private BridgeDAO<HIS_ACCIDENT_CARE> bridgeDAO;

        public bool Create(HIS_ACCIDENT_CARE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_ACCIDENT_CARE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
