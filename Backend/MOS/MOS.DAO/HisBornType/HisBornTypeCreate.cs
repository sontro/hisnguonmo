using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisBornType
{
    partial class HisBornTypeCreate : EntityBase
    {
        public HisBornTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BORN_TYPE>();
        }

        private BridgeDAO<HIS_BORN_TYPE> bridgeDAO;

        public bool Create(HIS_BORN_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_BORN_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
