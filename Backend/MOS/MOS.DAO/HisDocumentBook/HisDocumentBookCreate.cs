using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisDocumentBook
{
    partial class HisDocumentBookCreate : EntityBase
    {
        public HisDocumentBookCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DOCUMENT_BOOK>();
        }

        private BridgeDAO<HIS_DOCUMENT_BOOK> bridgeDAO;

        public bool Create(HIS_DOCUMENT_BOOK data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_DOCUMENT_BOOK> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
