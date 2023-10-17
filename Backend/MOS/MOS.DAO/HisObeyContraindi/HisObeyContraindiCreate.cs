using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisObeyContraindi
{
    partial class HisObeyContraindiCreate : EntityBase
    {
        public HisObeyContraindiCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_OBEY_CONTRAINDI>();
        }

        private BridgeDAO<HIS_OBEY_CONTRAINDI> bridgeDAO;

        public bool Create(HIS_OBEY_CONTRAINDI data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_OBEY_CONTRAINDI> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
