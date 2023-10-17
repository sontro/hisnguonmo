using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServBill
{
    partial class HisSereServBillGet : BusinessBase
    {
        internal HisSereServBillGet()
            : base()
        {

        }

        internal HisSereServBillGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_SERE_SERV_BILL> Get(HisSereServBillFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSereServBillDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SERE_SERV_BILL GetById(long id)
        {
            try
            {
                return GetById(id, new HisSereServBillFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SERE_SERV_BILL GetById(long id, HisSereServBillFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSereServBillDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_SERE_SERV_BILL> GetBySereServIds(List<long> sereServIds)
        {
            if (IsNotNullOrEmpty(sereServIds))
            {
                HisSereServBillFilterQuery filter = new HisSereServBillFilterQuery();
                filter.SERE_SERV_IDs = sereServIds;
                return this.Get(filter);
            }
            return null;
        }

        internal List<HIS_SERE_SERV_BILL> GetByServiceReqIds(List<long> serviceReqIds)
        {
            if (IsNotNullOrEmpty(serviceReqIds))
            {
                HisSereServBillFilterQuery filter = new HisSereServBillFilterQuery();
                filter.TDL_SERVICE_REQ_IDs = serviceReqIds;
                return this.Get(filter);
            }
            return null;
        }

        internal List<HIS_SERE_SERV_BILL> GetByServiceReqId(long serviceReqId)
        {
            if (serviceReqId != null)
            {
                HisSereServBillFilterQuery filter = new HisSereServBillFilterQuery();
                filter.TDL_SERVICE_REQ_ID = serviceReqId;
                return this.Get(filter);
            }
            return null;
        }

        internal List<HIS_SERE_SERV_BILL> GetNoCancelBySereServId(long sereServId)
        {
            HisSereServBillFilterQuery filter = new HisSereServBillFilterQuery();
            filter.SERE_SERV_ID = sereServId;
            filter.IS_NOT_CANCEL = true;
            return this.Get(filter);
        }

        internal List<HIS_SERE_SERV_BILL> GetNoCancelBySereServIds(List<long> sereServIds)
        {
            if (IsNotNullOrEmpty(sereServIds))
            {
                HisSereServBillFilterQuery filter = new HisSereServBillFilterQuery();
                filter.SERE_SERV_IDs = sereServIds;
                filter.IS_NOT_CANCEL = true;
                return this.Get(filter);
            }
            return null;
        }

        internal List<HIS_SERE_SERV_BILL> GetNoCancelByTreatmentId(long treatmentId)
        {
            HisSereServBillFilterQuery filter = new HisSereServBillFilterQuery();
            filter.TDL_TREATMENT_ID = treatmentId;
            filter.IS_NOT_CANCEL = true;
            return this.Get(filter);
        }

        internal List<HIS_SERE_SERV_BILL> GetByBillId(long billId)
        {
            if (billId != null)
            {
                HisSereServBillFilterQuery filter = new HisSereServBillFilterQuery();
                filter.BILL_ID = billId;
                return this.Get(filter);
            }
            return null;
        }


        internal List<HIS_SERE_SERV_BILL> GetByBillIds(List<long> billIds)
        {
            if (IsNotNullOrEmpty(billIds))
            {
                HisSereServBillFilterQuery filter = new HisSereServBillFilterQuery();
                filter.BILL_IDs = billIds;
                return this.Get(filter);
            }
            return null;
        }
    }
}
