using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.DAO.HisAccidentHelmet
{
    partial class HisAccidentHelmetUpdate : EntityBase
    {
        public HisAccidentHelmetUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ACCIDENT_HELMET>();
        }

        private BridgeDAO<HIS_ACCIDENT_HELMET> bridgeDAO;

        public bool Update(HIS_ACCIDENT_HELMET data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_ACCIDENT_HELMET> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
