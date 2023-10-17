using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Fss.Client;
using Inventec.Fss.Utility;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisExpMest.Common.Update;
using MOS.MANAGER.HisImpMest;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisTransaction;
using MOS.MANAGER.HisTreatment.Util;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MOS.MANAGER.HisTreatment.UpdatePatientInfo
{
    partial class HisTreatmentUpdatePatientInfo : BusinessBase
    {
        private HIS_TREATMENT beforeUpdate;

        private HisServiceReqUpdate hisServiceReqUpdate;
        private HisTransactionUpdate hisTransactionUpdate;
        private HisExpMestUpdate hisExpMestUpdate;
        private HisImpMestUpdate hisImpMestUpdate;

        internal HisTreatmentUpdatePatientInfo()
            : base()
        {
            this.Init();
        }

        internal HisTreatmentUpdatePatientInfo(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
            this.hisTransactionUpdate = new HisTransactionUpdate(param);
            this.hisExpMestUpdate = new HisExpMestUpdate(param);
            this.hisImpMestUpdate = new HisImpMestUpdate(param);
        }

        internal bool Run(HIS_TREATMENT data, HIS_PATIENT patient, bool isUpdateEmr)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTreatmentCheck checker = new HisTreatmentCheck(param);
                valid = valid && IsNotNull(data);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.IsUnLock(data);
                valid = valid && checker.IsUnTemporaryLock(data);
                if (valid)
                {
                    Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();
                    this.beforeUpdate = Mapper.Map<HIS_TREATMENT>(data);

                    HisTreatmentUtil.SetTdl(data, patient);

                    if (!DAOWorker.HisTreatmentDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatment_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTreatment that bai." + LogUtil.TraceData("data", data));
                    }

                    this.UpdatePatientInfoForServiceReq(data);
                    this.UpdatePatientInfoForTransaction(data);
                    this.UpdatePatientInfoForExpMest(data);
                    this.UpdatePatientInfoForImpMest(data);
                    this.UpdatePatientInfoForEmr(data, isUpdateEmr);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                this.Rollback();
                result = false;
            }
            return result;
        }

        private void UpdatePatientInfoForServiceReq(HIS_TREATMENT data)
        {
            List<HIS_SERVICE_REQ> listServiceReq = new HisServiceReqGet().GetByTreatmentId(data.ID);
            if (IsNotNullOrEmpty(listServiceReq))
            {
                foreach (var serviceReq in listServiceReq)
                {
                    HisServiceReqUtil.SetTdl(serviceReq, data);
                }
                if (!this.hisServiceReqUpdate.UpdateList(listServiceReq))
                {
                    throw new Exception("Khong cap nhat duoc thong tin benh nhan cho Yeu cau dich vu");
                }
            }
        }

        private void UpdatePatientInfoForTransaction(HIS_TREATMENT data)
        {
            List<HIS_TRANSACTION> listTransaction = new HisTransactionGet().GetByTreatmentId(data.ID);
            if (IsNotNullOrEmpty(listTransaction))
            {
                Mapper.CreateMap<HIS_TRANSACTION, HIS_TRANSACTION>();
                List<HIS_TRANSACTION> befores = Mapper.Map<List<HIS_TRANSACTION>>(listTransaction);
                foreach (var transaction in listTransaction)
                {
                    HisTransactionUtil.SetTdl(transaction, data);
                }
                if (!this.hisTransactionUpdate.UpdateListWithoutCheckLock(listTransaction, befores))
                {
                    throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                }
            }
        }

        private void UpdatePatientInfoForExpMest(HIS_TREATMENT data)
        {
            List<HIS_EXP_MEST> expMests = new HisExpMestGet().GetByTreatmentId(data.ID);
            if (IsNotNullOrEmpty(expMests))
            {
                Mapper.CreateMap<HIS_EXP_MEST, HIS_EXP_MEST>();
                List<HIS_EXP_MEST> befores = Mapper.Map<List<HIS_EXP_MEST>>(expMests);//phuc vu rollback

                expMests.ForEach(o =>
                {
                    o.TDL_PATIENT_ID = data.PATIENT_ID;
                    o.TDL_PATIENT_CODE = data.TDL_PATIENT_CODE;
                    HisExpMestUtil.SetTdl(o, data);
                });
                if (!this.hisExpMestUpdate.UpdateList(expMests, befores))
                {
                    throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                }
            }
        }

        private void UpdatePatientInfoForImpMest(HIS_TREATMENT data)
        {
            List<HIS_IMP_MEST> impMests = new HisImpMestGet().GetByTreatmentId(data.ID);
            if (IsNotNullOrEmpty(impMests))
            {
                Mapper.CreateMap<HIS_IMP_MEST, HIS_IMP_MEST>();
                List<HIS_IMP_MEST> befores = Mapper.Map<List<HIS_IMP_MEST>>(impMests);//phuc vu rollback

                impMests.ForEach(o =>
                {
                    o.TDL_PATIENT_ID = data.PATIENT_ID;
                    o.TDL_PATIENT_CODE = data.TDL_PATIENT_CODE;
                    HisImpMestUtil.SetTdl(o, data);
                });
                if (!this.hisImpMestUpdate.UpdateList(impMests, befores))
                {
                    throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                }
            }
        }

        private void UpdatePatientInfoForEmr(HIS_TREATMENT raw, bool isUpdateEmr)
        {
            try
            {
                if (isUpdateEmr)
                {
                    HisTreatmentUploadEmr uploadEmr = new HisTreatmentUploadEmr();
                    uploadEmr.Run(raw);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        public void Rollback()
        {
            if (this.beforeUpdate != null)
            {
                if (!DAOWorker.HisTreatmentDAO.Update(this.beforeUpdate))
                {
                    LogSystem.Warn("Rollback du lieu HisTreatment that bai, can kiem tra lai.");
                }
            }

            this.hisImpMestUpdate.RollbackData();
            this.hisExpMestUpdate.RollbackData();
            this.hisServiceReqUpdate.RollbackData();
            this.hisTransactionUpdate.RollbackData();
        }

    }
}
