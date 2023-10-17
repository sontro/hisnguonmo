using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisBedType
{
    partial class HisBedTypeCreate : EntityBase
    {
        public HisBedTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BED_TYPE>();
        }

        private BridgeDAO<HIS_BED_TYPE> bridgeDAO;

        public bool Create(HIS_BED_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_BED_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
