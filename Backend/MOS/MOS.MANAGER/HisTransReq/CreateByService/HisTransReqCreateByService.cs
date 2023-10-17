using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisSeseTransReq;
using MOS.MANAGER.License;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransReq.CreateByService
{
    class HisTransReqCreateByService : BusinessBase
    {
        private HisTransReqCreate hisTransReqCreate;
        private HisSeseTransReqCreate hisSeseTransReqCreate;

        internal HisTransReqCreateByService(CommonParam param)
            : base(param)
        {
            this.hisTransReqCreate = new HisTransReqCreate(param);
            this.hisSeseTransReqCreate = new HisSeseTransReqCreate(param);
        }

        internal bool Run(HIS_TREATMENT treatment, List<HIS_SERVICE_REQ> serviceReqs, WorkPlaceSDO workPlace)
        {
            bool result = true;
            try
            {
                List<HIS_SERE_SERV> executeSereServ = null;
                bool valid = true;
                HIS_BRANCH branch = HisBranchCFG.DATA.FirstOrDefault(o => o.ID == workPlace.BranchId);
                HisTransReqCreateByServiceCheck checker = new HisTransReqCreateByServiceCheck(param);
                VerifyLicenseCheck checkLiciense = new VerifyLicenseCheck(param);
                valid = valid && checker.CheckConfig(treatment, ref executeSereServ);
                valid = valid && checkLiciense.VerifyLicense(branch.HEIN_MEDI_ORG_CODE, VerifyLicenseCheck.AppCode.QR_PAYMENT);
                if (valid)
                {
                    Dictionary<long, HIS_TRANS_REQ> dicServiceReq = new Dictionary<long, HIS_TRANS_REQ>();
                    Dictionary<HIS_TRANS_REQ, List<HIS_SESE_TRANS_REQ>> dicTranReq = new Dictionary<HIS_TRANS_REQ, List<HIS_SESE_TRANS_REQ>>();

                    this.ProgressTranReq(treatment, serviceReqs, executeSereServ, dicServiceReq, dicTranReq, workPlace);

                    this.ProcessSeseTransReq(dicTranReq);

                    this.ProcessServiceReq(serviceReqs, dicServiceReq);

                    this.WriteEventLog(treatment, serviceReqs, dicServiceReq);
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

        private void WriteEventLog(HIS_TREATMENT treatment, List<HIS_SERVICE_REQ> serviceReqs, Dictionary<long, HIS_TRANS_REQ> dicServiceReq)
        {
            foreach (var item in dicServiceReq)
            {
                HIS_SERVICE_REQ serviceReq = serviceReqs != null ? serviceReqs.FirstOrDefault(o => o.ID == item.Key) : null;
                HIS_TRANS_REQ transReq = item.Value;
                if (transReq == null) continue;
                new EventLogGenerator(EventLog.Enum.HisTransReq_TaoYeuCauThanhToan,
                                                transReq.TRANS_REQ_CODE,
                                                transReq.TRANS_REQ_TYPE == IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_TYPE.ID__BY_SERVICE_REQ ? MessageUtil.GetMessage(LibraryMessage.Message.Enum.TransReqType_1, param.LanguageCode)
                                                                            : (transReq.TRANS_REQ_TYPE == IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_TYPE.ID__BY_SERVICE ? MessageUtil.GetMessage(LibraryMessage.Message.Enum.TransReqType_2, param.LanguageCode) : ""),
                                                transReq.AMOUNT,
                                                String.Format("SERVICE_REQ_CODE: {0}.", serviceReq != null ? serviceReq.SERVICE_REQ_CODE : "")).TreatmentCode(treatment.TREATMENT_CODE).Run();
            }
        }

        private void ProcessServiceReq(List<HIS_SERVICE_REQ> serviceReqs, Dictionary<long, HIS_TRANS_REQ> dicServiceReq)
        {
            if (IsNotNullOrEmpty(serviceReqs))
            {
                List<string> querys = new List<string>();
                string sql = "UPDATE HIS_SERVICE_REQ SET TRANS_REQ_ID = {0} WHERE ID = {1}";
                foreach (var item in serviceReqs)
                {
                    if (dicServiceReq.ContainsKey(item.ID))
                    {
                        querys.Add(string.Format(sql, dicServiceReq[item.ID].ID, item.ID));
                    }
                    else
                    {
                        querys.Add(string.Format(sql, "NULL", item.ID));
                    }
                }

                if (IsNotNullOrEmpty(querys) && !DAOWorker.SqlDAO.Execute(querys))
                {
                    throw new Exception("Cap nhat y lenh that bai. Rollback du lieu. Ket thuc nghiep vu");
                }
            }
        }

        private void ProcessSeseTransReq(Dictionary<HIS_TRANS_REQ, List<HIS_SESE_TRANS_REQ>> dicTranReq)
        {
            List<HIS_SESE_TRANS_REQ> seseTransReq = new List<HIS_SESE_TRANS_REQ>();

            foreach (var item in dicTranReq)
            {
                item.Value.ForEach(o => o.TRANS_REQ_ID = item.Key.ID);
                seseTransReq.AddRange(item.Value);
            }

            if (!this.hisSeseTransReqCreate.CreateList(seseTransReq))
            {
                throw new Exception("Tao du lieu seseTransReq that bai. Rollback du lieu. Ket thuc nghiep vu");
            }
        }

        private void ProgressTranReq(HIS_TREATMENT treatment, List<HIS_SERVICE_REQ> serviceReqs, List<HIS_SERE_SERV> executeSereServ, Dictionary<long, HIS_TRANS_REQ> dicServiceReq, Dictionary<HIS_TRANS_REQ, List<HIS_SESE_TRANS_REQ>> dicTranReq, WorkPlaceSDO workPlace)
        {
            List<HIS_TRANS_REQ> tranReqs = new List<HIS_TRANS_REQ>();

            if (IsNotNullOrEmpty(serviceReqs))
            {
                List<long> serviceReqIds = serviceReqs.Select(s => s.ID).ToList();
                List<HIS_SERE_SERV> sereServHasReq = executeSereServ.Where(o => serviceReqIds.Contains(o.SERVICE_REQ_ID ?? 0)).ToList();
                if (IsNotNullOrEmpty(sereServHasReq))
                {
                    var groupByReq = sereServHasReq.GroupBy(o => o.SERVICE_REQ_ID ?? 0).ToList();
                    foreach (var ssGroup in groupByReq)
                    {
                        HIS_TRANS_REQ tran = new HIS_TRANS_REQ();
                        tran.TREATMENT_ID = treatment.ID;
                        tran.TRANS_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_STT.ID__REQUEST;
                        tran.REQUEST_ROOM_ID = workPlace.RoomId;
                        tran.AMOUNT = HisTransReqUtil.RoundAmount(ssGroup.Sum(s => s.VIR_TOTAL_PATIENT_PRICE.Value));
                        tran.TRANS_REQ_TYPE = IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_TYPE.ID__BY_SERVICE_REQ;
                        tran.TDL_TREATMENT_CODE = treatment.TREATMENT_CODE;
                        tran.TDL_PATIENT_CODE = treatment.TDL_PATIENT_CODE;
                        tran.TDL_PATIENT_NAME = treatment.TDL_PATIENT_NAME;

                        List<HIS_SESE_TRANS_REQ> seseTransReq = new List<HIS_SESE_TRANS_REQ>();
                        foreach (var ss in ssGroup)
                        {
                            HIS_SESE_TRANS_REQ sstr = new HIS_SESE_TRANS_REQ();
                            sstr.SERE_SERV_ID = ss.ID;
                            sstr.PRICE = ss.VIR_TOTAL_PATIENT_PRICE.Value;
                            seseTransReq.Add(sstr);
                        }

                        tranReqs.Add(tran);
                        dicServiceReq.Add(ssGroup.Key, tran);
                        dicTranReq.Add(tran, seseTransReq);
                    }
                }
            }

            HIS_TRANS_REQ tranTotal = new HIS_TRANS_REQ();
            tranTotal.TREATMENT_ID = treatment.ID;
            tranTotal.TRANS_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_STT.ID__REQUEST;
            tranTotal.REQUEST_ROOM_ID = workPlace.RoomId;
            tranTotal.AMOUNT = HisTransReqUtil.RoundAmount(executeSereServ.Sum(s => s.VIR_TOTAL_PATIENT_PRICE.Value));
            tranTotal.TRANS_REQ_TYPE = IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_TYPE.ID__BY_SERVICE;
            tranTotal.TDL_TREATMENT_CODE = treatment.TREATMENT_CODE;
            tranTotal.TDL_PATIENT_CODE = treatment.TDL_PATIENT_CODE;
            tranTotal.TDL_PATIENT_NAME = treatment.TDL_PATIENT_NAME;
            List<HIS_SESE_TRANS_REQ> allSeseTransReq = new List<HIS_SESE_TRANS_REQ>();
            foreach (var ss in executeSereServ)
            {
                HIS_SESE_TRANS_REQ sstr = new HIS_SESE_TRANS_REQ();
                sstr.SERE_SERV_ID = ss.ID;
                sstr.PRICE = ss.VIR_TOTAL_PATIENT_PRICE.Value;
                allSeseTransReq.Add(sstr);
            }

            tranReqs.Add(tranTotal);
            dicServiceReq.Add(-1, tranTotal);
            dicTranReq.Add(tranTotal, allSeseTransReq);

            if (!this.hisTransReqCreate.CreateList(tranReqs))
            {
                throw new Exception("Tao du lieu tranReqs that bai. Rollback du lieu. Ket thuc nghiep vu");
            }
        }

        private void Rollback()
        {
            try
            {
                this.hisSeseTransReqCreate.RollbackData();
                this.hisTransReqCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
