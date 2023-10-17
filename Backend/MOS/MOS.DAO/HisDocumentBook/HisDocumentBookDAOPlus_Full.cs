using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisDocumentBook
{
    public partial class HisDocumentBookDAO : EntityBase
    {
        public List<V_HIS_DOCUMENT_BOOK> GetView(HisDocumentBookSO search, CommonParam param)
        {
            List<V_HIS_DOCUMENT_BOOK> result = new List<V_HIS_DOCUMENT_BOOK>();

            try
            {
                result = GetWorker.GetView(search, param);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }

            return result;
        }

        public HIS_DOCUMENT_BOOK GetByCode(string code, HisDocumentBookSO search)
        {
            HIS_DOCUMENT_BOOK result = null;

            try
            {
                result = GetWorker.GetByCode(code, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }

            return result;
        }
        
        public V_HIS_DOCUMENT_BOOK GetViewById(long id, HisDocumentBookSO search)
        {
            V_HIS_DOCUMENT_BOOK result = null;

            try
            {
                result = GetWorker.GetViewById(id, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }

            return result;
        }

        public V_HIS_DOCUMENT_BOOK GetViewByCode(string code, HisDocumentBookSO search)
        {
            V_HIS_DOCUMENT_BOOK result = null;

            try
            {
                result = GetWorker.GetViewByCode(code, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

        public Dictionary<string, HIS_DOCUMENT_BOOK> GetDicByCode(HisDocumentBookSO search, CommonParam param)
        {
            Dictionary<string, HIS_DOCUMENT_BOOK> result = new Dictionary<string, HIS_DOCUMENT_BOOK>();
            try
            {
                result = GetWorker.GetDicByCode(search, param);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }

            return result;
        }

        public bool ExistsCode(string code, long? id)
        {
            try
            {
                return CheckWorker.ExistsCode(code, id);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                throw;
            }
        }
    }
}
