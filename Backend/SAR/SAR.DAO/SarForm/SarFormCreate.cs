using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace SAR.DAO.SarForm
{
    partial class SarFormCreate : EntityBase
    {
        public SarFormCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_FORM>();
        }

        private BridgeDAO<SAR_FORM> bridgeDAO;

        public bool Create(SAR_FORM data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<SAR_FORM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
