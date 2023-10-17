using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisEkip
{
    partial class HisEkipCreate : EntityBase
    {
        public HisEkipCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EKIP>();
        }

        private BridgeDAO<HIS_EKIP> bridgeDAO;

        public bool Create(HIS_EKIP data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_EKIP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
