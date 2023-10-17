using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisBranch;
using MOS.MANAGER.HisTransReq;
using MOS.MANAGER.HisTransReq.CreateByService;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.License;
using MOS.SDO;
using SDA.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransReq.CreateByDepositReq
{
    class HisTransReqCreateByDepositReqProccesor: BusinessBase
    {
        private HisTransReqCreate hisTransReqCreate;

        internal HisTransReqCreateByDepositReqProccesor(CommonParam param)
            : base(param)
        {
            this.hisTransReqCreate = new HisTransReqCreate(param);
        }

        internal bool Run(long treatmentId, HIS_DEPOSIT_REQ depositReq, WorkPlaceSDO workPlace, ref HIS_TRANS_REQ hisTransReq)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_BRANCH branch = HisBranchCFG.DATA.FirstOrDefault(o => o.ID == workPlace.BranchId);
                VerifyLicenseCheck checkLiciense = new VerifyLicenseCheck(param);
                valid = valid && checkLiciense.VerifyLicense(branch.HEIN_MEDI_ORG_CODE, VerifyLicenseCheck.AppCode.QR_PAYMENT);
                if (valid)
                {
                    HIS_TREATMENT treatment = new HisTreatmentGet().GetById(treatmentId);

                    HIS_TRANS_REQ transReq = new HIS_TRANS_REQ();
                    transReq.TREATMENT_ID = treatmentId;
                    transReq.TRANS_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_STT.ID__REQUEST;
                    transReq.REQUEST_ROOM_ID = workPlace.RoomId;
                    if (treatment != null)
                    {
                        transReq.TDL_TREATMENT_CODE = treatment.TREATMENT_CODE;
                        transReq.TDL_PATIENT_CODE = treatment.TDL_PATIENT_CODE;
                        transReq.TDL_PATIENT_NAME = treatment.TDL_PATIENT_NAME;
                    }
                    decimal sqlTotalAmount = DAOWorker.SqlDAO.GetSqlSingle<decimal>("SELECT NVL(TOTAL_PATIENT_PRICE,0) - NVL(TOTAL_DEPOSIT_AMOUNT,0) - NVL(TOTAL_DEBT_AMOUNT,0) - NVL(TOTAL_BILL_AMOUNT,0) + NVL(TOTAL_BILL_TRANSFER_AMOUNT,0) + NVL(TOTAL_REPAY_AMOUNT,0) AS PATIENT_PRICE FROM V_HIS_TREATMENT_FEE WHERE ID = :param1", treatmentId);

                    if (depositReq == null && sqlTotalAmount > 0)
                    {
                        transReq.AMOUNT = HisTransReqUtil.RoundAmount(sqlTotalAmount);
                        transReq.TRANS_REQ_TYPE = IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_TYPE.ID__BY_TREATMENT;
                    }
                    if (depositReq != null)
                    {
                        transReq.AMOUNT = HisTransReqUtil.RoundAmount(depositReq.AMOUNT);
                        transReq.TRANS_REQ_TYPE = IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_TYPE.ID__BY_DEPOSIT;
                    }
                    if (!this.hisTransReqCreate.Create(transReq))
                    {
                        throw new Exception("Tự động tạo yêu cầu thanh toán HisTransReq that bai." + LogUtil.TraceData("transReq", transReq));
                    }

                    hisTransReq = transReq;
                    result = true;
                }
            }
            catch (Exception ex)
            {
                param.HasException = true;
                this.Rollback();
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        private void Rollback()
        {
            try
            {
                this.hisTransReqCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
