using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisSereServMaty
{
    partial class HisSereServMatyCreate : EntityBase
    {
        public HisSereServMatyCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV_MATY>();
        }

        private BridgeDAO<HIS_SERE_SERV_MATY> bridgeDAO;

        public bool Create(HIS_SERE_SERV_MATY data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SERE_SERV_MATY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
