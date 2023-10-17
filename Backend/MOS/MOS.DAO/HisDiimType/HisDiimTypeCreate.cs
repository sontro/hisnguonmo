using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisDiimType
{
    partial class HisDiimTypeCreate : EntityBase
    {
        public HisDiimTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DIIM_TYPE>();
        }

        private BridgeDAO<HIS_DIIM_TYPE> bridgeDAO;

        public bool Create(HIS_DIIM_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_DIIM_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
