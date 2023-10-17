using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisSereServPtttTemp
{
    partial class HisSereServPtttTempCreate : EntityBase
    {
        public HisSereServPtttTempCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV_PTTT_TEMP>();
        }

        private BridgeDAO<HIS_SERE_SERV_PTTT_TEMP> bridgeDAO;

        public bool Create(HIS_SERE_SERV_PTTT_TEMP data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SERE_SERV_PTTT_TEMP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
