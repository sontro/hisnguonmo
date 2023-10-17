using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.DAO.HisAccidentCare
{
    partial class HisAccidentCareUpdate : EntityBase
    {
        public HisAccidentCareUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ACCIDENT_CARE>();
        }

        private BridgeDAO<HIS_ACCIDENT_CARE> bridgeDAO;

        public bool Update(HIS_ACCIDENT_CARE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_ACCIDENT_CARE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
