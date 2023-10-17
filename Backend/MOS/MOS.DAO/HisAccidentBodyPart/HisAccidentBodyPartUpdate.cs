using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.DAO.HisAccidentBodyPart
{
    partial class HisAccidentBodyPartUpdate : EntityBase
    {
        public HisAccidentBodyPartUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ACCIDENT_BODY_PART>();
        }

        private BridgeDAO<HIS_ACCIDENT_BODY_PART> bridgeDAO;

        public bool Update(HIS_ACCIDENT_BODY_PART data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_ACCIDENT_BODY_PART> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
