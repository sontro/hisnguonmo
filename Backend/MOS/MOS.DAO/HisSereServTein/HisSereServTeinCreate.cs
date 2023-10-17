using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisSereServTein
{
    partial class HisSereServTeinCreate : EntityBase
    {
        public HisSereServTeinCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV_TEIN>();
        }

        private BridgeDAO<HIS_SERE_SERV_TEIN> bridgeDAO;

        public bool Create(HIS_SERE_SERV_TEIN data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SERE_SERV_TEIN> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
