using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisSubclinicalRsAdd
{
    partial class HisSubclinicalRsAddCreate : EntityBase
    {
        public HisSubclinicalRsAddCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SUBCLINICAL_RS_ADD>();
        }

        private BridgeDAO<HIS_SUBCLINICAL_RS_ADD> bridgeDAO;

        public bool Create(HIS_SUBCLINICAL_RS_ADD data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SUBCLINICAL_RS_ADD> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
