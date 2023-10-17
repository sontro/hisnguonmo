using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisServiceReq;
using MOS.UTILITY;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace MOS.MANAGER.HisDrugIntervention
{
    partial class HisDrugInterventionCreate : BusinessBase
    {
        private List<HIS_DRUG_INTERVENTION> recentHisDrugInterventions = new List<HIS_DRUG_INTERVENTION>();

        internal HisDrugInterventionCreate()
            : base()
        {

        }

        internal HisDrugInterventionCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_DRUG_INTERVENTION data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDrugInterventionCheck checker = new HisDrugInterventionCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisDrugInterventionDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDrugIntervention_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisDrugIntervention that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisDrugInterventions.Add(data);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool CreateList(List<HIS_DRUG_INTERVENTION> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisDrugInterventionCheck checker = new HisDrugInterventionCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisDrugInterventionDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDrugIntervention_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisDrugIntervention that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisDrugInterventions.AddRange(listData);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }

            return result;
        }

        internal void RollbackData()
        {
            if (IsNotNullOrEmpty(this.recentHisDrugInterventions))
            {
                if (!DAOWorker.HisDrugInterventionDAO.TruncateList(this.recentHisDrugInterventions))
                {
                    LogSystem.Warn("Rollback du lieu HisDrugIntervention that bai, can kiem tra lai." + LogUtil.TraceData("recentHisDrugInterventions", this.recentHisDrugInterventions));
                }
                this.recentHisDrugInterventions = null;
            }
        }

        internal bool CreateInfo(TDO.DrugInterventionInfoTDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDrugInterventionCheck checker = new HisDrugInterventionCheck(param);
                valid = valid && checker.VerifyRequireFieldTdo(data);
                if (valid)
                {
                    HisServiceReqFilterQuery serviceReqFilter = new HisServiceReqFilterQuery();
                    serviceReqFilter.SERVICE_REQ_CODE__EXACT = data.Patient.prescriptionId;
                    serviceReqFilter.TDL_PATIENT_CODE__EXACT = data.Patient.patientID;

                    List<HIS_SERVICE_REQ> serviceReqs = new HisServiceReqGet().Get(serviceReqFilter);
                    if (IsNotNullOrEmpty(serviceReqs))
                    {
                        List<HIS_DRUG_INTERVENTION> createDatas = new List<HIS_DRUG_INTERVENTION>();

                        foreach (var serviceReq in serviceReqs)
                        {
                            HIS_DRUG_INTERVENTION drug = new HIS_DRUG_INTERVENTION();
                            drug.SESSION_CODE = data.SessionID;
                            drug.SERVICE_REQ_ID = serviceReq.ID;
                            drug.PHARMACIST_LOGINNAME = data.Pharmacist.id;
                            drug.PHARMACIST_USERNAME = data.Pharmacist.value;
                            if (data.IsUrgent)
                            {
                                drug.IS_URGENT = Constant.IS_TRUE;
                            }

                            if (data.PrescriptionTime.HasValue)
                            {
                                drug.INTERVENTION_TIME = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(data.PrescriptionTime.Value);
                            }

                            createDatas.Add(drug);
                        }

                        if (!DAOWorker.HisDrugInterventionDAO.CreateList(createDatas))
                        {
                            MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDrugIntervention_ThemMoiThatBai);
                            throw new Exception("Them moi thong tin HisDrugIntervention that bai." + LogUtil.TraceData("data", data));
                        }
                        this.recentHisDrugInterventions.AddRange(createDatas);
                        result = true;

                        ThreadCreateNotify(createDatas);
                    }
                    else
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisDrugIntervention_KhongTimThayDonThuoc);
                        Inventec.Common.Logging.LogSystem.Error(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => data), data));
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ThreadCreateNotify(List<HIS_DRUG_INTERVENTION> createDatas)
        {
            Thread createNotify = new Thread(ProcessCreateNotify);
            try
            {
                createNotify.Start(createDatas);
            }
            catch (Exception ex)
            {
                createNotify.Abort();
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void ProcessCreateNotify(object obj)
        {
            try
            {
                if (IsNotNull(obj) && obj.GetType() == typeof(List<HIS_DRUG_INTERVENTION>))
                {
                    List<SDA_NOTIFY> sdaNotifys = new List<SDA_NOTIFY>();

                    List<HIS_DRUG_INTERVENTION> datas = obj as List<HIS_DRUG_INTERVENTION>;

                    HisServiceReqFilterQuery serviceReqFilter = new HisServiceReqFilterQuery();
                    serviceReqFilter.IDs = datas.Select(s => s.SERVICE_REQ_ID).ToList();
                    List<HIS_SERVICE_REQ> serviceReqs = new HisServiceReqGet().Get(serviceReqFilter);

                    if (IsNotNullOrEmpty(serviceReqs))
                    {
                        string Pharmacist = string.Format("{0} - {1}", datas.First().PHARMACIST_LOGINNAME, datas.First().PHARMACIST_USERNAME);

                        var groupByReqLoginname = serviceReqs.GroupBy(o => new { o.REQUEST_LOGINNAME, o.TDL_PATIENT_ID }).ToList();
                        foreach (var item in groupByReqLoginname)
                        {
                            string reqCodes = string.Join(", ", item.Select(s => s.SERVICE_REQ_CODE).Distinct().OrderBy(o => o));

                            SDA_NOTIFY notify = new SDA_NOTIFY();
                            notify.CONTENT = String.Format(MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisDrugIntervention_NoiDungThongBaoCanThiepDuoc, param.LanguageCode), Pharmacist, reqCodes, item.First().TDL_PATIENT_NAME);
                            notify.FROM_TIME = Inventec.Common.DateTime.Get.Now().Value;
                            notify.RECEIVER_LOGINNAME = item.Key.REQUEST_LOGINNAME;
                            notify.TITLE = String.Format(MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisDrugIntervention_TieuDeThongBaoCanThiepDuoc, param.LanguageCode), reqCodes);
                            sdaNotifys.Add(notify);
                        }
                    }

                    var rs = ApiConsumerManager.ApiConsumerStore.SdaConsumerWrapper.Post<List<SDA_NOTIFY>>(true, "api/SdaNotify/CreateList", param, sdaNotifys);
                    if (rs == null || rs.Count <= 0)
                    {
                        LogSystem.Warn("HisTreatmentNotify. Send Create SdaNotify Failed.\n" + LogUtil.TraceData("Param", param) + "\n" + LogUtil.TraceData("obj", obj));
                    }
                    else
                    {
                        LogSystem.Debug("HisTreatmentNotify. Send Create SdaNotify Success.");
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
