using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDocumentBook
{
    partial class HisDocumentBookUpdate : EntityBase
    {
        public HisDocumentBookUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DOCUMENT_BOOK>();
        }

        private BridgeDAO<HIS_DOCUMENT_BOOK> bridgeDAO;

        public bool Update(HIS_DOCUMENT_BOOK data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_DOCUMENT_BOOK> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
