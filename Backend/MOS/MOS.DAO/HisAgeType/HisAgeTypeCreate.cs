using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisAgeType
{
    partial class HisAgeTypeCreate : EntityBase
    {
        public HisAgeTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_AGE_TYPE>();
        }

        private BridgeDAO<HIS_AGE_TYPE> bridgeDAO;

        public bool Create(HIS_AGE_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_AGE_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
