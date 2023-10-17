using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisVaexVaer
{
    partial class HisVaexVaerCreate : EntityBase
    {
        public HisVaexVaerCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VAEX_VAER>();
        }

        private BridgeDAO<HIS_VAEX_VAER> bridgeDAO;

        public bool Create(HIS_VAEX_VAER data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_VAEX_VAER> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
