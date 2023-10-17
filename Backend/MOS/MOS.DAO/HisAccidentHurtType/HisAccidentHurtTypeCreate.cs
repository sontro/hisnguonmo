using Inventec.Core;
using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.DAO.HisAccidentHurtType
{
    partial class HisAccidentHurtTypeCreate : EntityBase
    {
        public HisAccidentHurtTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ACCIDENT_HURT_TYPE>();
        }

        private BridgeDAO<HIS_ACCIDENT_HURT_TYPE> bridgeDAO;

        public bool Create(HIS_ACCIDENT_HURT_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_ACCIDENT_HURT_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
