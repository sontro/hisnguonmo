using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace SAR.DAO.SarFormData
{
    partial class SarFormDataCreate : EntityBase
    {
        public SarFormDataCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_FORM_DATA>();
        }

        private BridgeDAO<SAR_FORM_DATA> bridgeDAO;

        public bool Create(SAR_FORM_DATA data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<SAR_FORM_DATA> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
