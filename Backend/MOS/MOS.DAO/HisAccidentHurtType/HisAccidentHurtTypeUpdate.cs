using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.DAO.HisAccidentHurtType
{
    partial class HisAccidentHurtTypeUpdate : EntityBase
    {
        public HisAccidentHurtTypeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ACCIDENT_HURT_TYPE>();
        }

        private BridgeDAO<HIS_ACCIDENT_HURT_TYPE> bridgeDAO;

        public bool Update(HIS_ACCIDENT_HURT_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_ACCIDENT_HURT_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
