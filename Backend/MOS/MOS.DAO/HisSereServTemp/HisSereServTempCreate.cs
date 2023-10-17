using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisSereServTemp
{
    partial class HisSereServTempCreate : EntityBase
    {
        public HisSereServTempCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV_TEMP>();
        }

        private BridgeDAO<HIS_SERE_SERV_TEMP> bridgeDAO;

        public bool Create(HIS_SERE_SERV_TEMP data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SERE_SERV_TEMP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
