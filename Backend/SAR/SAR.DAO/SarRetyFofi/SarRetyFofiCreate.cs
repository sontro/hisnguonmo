using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace SAR.DAO.SarRetyFofi
{
    partial class SarRetyFofiCreate : EntityBase
    {
        public SarRetyFofiCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_RETY_FOFI>();
        }

        private BridgeDAO<SAR_RETY_FOFI> bridgeDAO;

        public bool Create(SAR_RETY_FOFI data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<SAR_RETY_FOFI> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
