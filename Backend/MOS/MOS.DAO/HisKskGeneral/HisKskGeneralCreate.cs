using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisKskGeneral
{
    partial class HisKskGeneralCreate : EntityBase
    {
        public HisKskGeneralCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_KSK_GENERAL>();
        }

        private BridgeDAO<HIS_KSK_GENERAL> bridgeDAO;

        public bool Create(HIS_KSK_GENERAL data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_KSK_GENERAL> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
