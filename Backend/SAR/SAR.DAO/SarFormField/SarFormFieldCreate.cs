using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace SAR.DAO.SarFormField
{
    partial class SarFormFieldCreate : EntityBase
    {
        public SarFormFieldCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_FORM_FIELD>();
        }

        private BridgeDAO<SAR_FORM_FIELD> bridgeDAO;

        public bool Create(SAR_FORM_FIELD data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<SAR_FORM_FIELD> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
