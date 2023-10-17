using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisSereServSuin
{
    partial class HisSereServSuinCreate : EntityBase
    {
        public HisSereServSuinCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV_SUIN>();
        }

        private BridgeDAO<HIS_SERE_SERV_SUIN> bridgeDAO;

        public bool Create(HIS_SERE_SERV_SUIN data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SERE_SERV_SUIN> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
