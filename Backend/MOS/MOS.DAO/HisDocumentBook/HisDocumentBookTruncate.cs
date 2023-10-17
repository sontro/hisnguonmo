using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDocumentBook
{
    partial class HisDocumentBookTruncate : EntityBase
    {
        public HisDocumentBookTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DOCUMENT_BOOK>();
        }

        private BridgeDAO<HIS_DOCUMENT_BOOK> bridgeDAO;

        public bool Truncate(HIS_DOCUMENT_BOOK data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_DOCUMENT_BOOK> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
