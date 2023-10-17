using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisServiceReq;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisBedLog.Update
{
    class UsingBedTempProcessor : BusinessBase
    {
        private List<HIS_SERE_SERV> beforeUpdateSS = new List<HIS_SERE_SERV>();

        private HisServiceReqUpdate reqUpdateProcessor;
        private HisSereServUpdate ssUpdateProcessor;

        private HisServiceReqCreate reqCreateProcessor;
        private HisSereServCreate ssCreateProcessor;

        private UsingBedTemChangeInfoProcessor bedTempChangeInfo;
        private HisSereServUpdateHein hisSereServUpdateHein;

        internal UsingBedTempProcessor()
            : base()
        {
            this.Init();
        }

        internal UsingBedTempProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.reqUpdateProcessor = new HisServiceReqUpdate(param); ;
            this.ssUpdateProcessor = new HisSereServUpdate(param);

            this.reqCreateProcessor = new HisServiceReqCreate(param);
            this.ssCreateProcessor = new HisSereServCreate(param);

            this.bedTempChangeInfo = new UsingBedTemChangeInfoProcessor(param);
        }

        internal bool Run(HisBedLogSDO data, HIS_BED_LOG newBedLog, HIS_BED_LOG oldBedLog, HIS_TREATMENT treatment, WorkPlaceSDO workPlace, List<HIS_SERVICE_REQ> serviceReqs, ref List<HIS_SERE_SERV> sereServs)
        {
            bool result = false;
            try
            {
                if (HisSereServCFG.IS_USING_BED_TEMP && IsNotNullOrEmpty(serviceReqs))
                {
                    List<HIS_PATIENT_TYPE_ALTER> ptas = null;
                    List<string> sqls = new List<string>();
                    Dictionary<HIS_SERVICE_REQ, HIS_SERE_SERV> dicCreateSRSS = new Dictionary<HIS_SERVICE_REQ, HIS_SERE_SERV>();
                    Dictionary<HIS_SERVICE_REQ, HIS_SERE_SERV> dicUpdateSRSS = new Dictionary<HIS_SERVICE_REQ, HIS_SERE_SERV>();

                    sereServs = new HisSereServGet().GetByServiceReqIds(serviceReqs.Select(o => o.ID).ToList());
                    // Khi lich su giuong thay doi thoi gian bat dau
                    if (newBedLog.START_TIME != oldBedLog.START_TIME)
                    {
                        // Lay ra dich vu dau tien (dich vu co thoi gian y lenh nho nhat)
                        HIS_SERE_SERV sereServ = IsNotNullOrEmpty(sereServs) ? sereServs.OrderBy(o => o.TDL_INTRUCTION_TIME).ThenBy(o => o.ID).FirstOrDefault() : null;
                        this.ProcessChangeStartTime(sereServ, newBedLog, treatment, serviceReqs, sereServs, ref sqls, ref dicCreateSRSS, ref dicUpdateSRSS);
                    }

                    // Khi lich su giuong thay doi thoi gian ket thuc
                    if (newBedLog.FINISH_TIME != oldBedLog.FINISH_TIME)
                    {
                        // Lay ra ban ghi dich vu cuoi cung (dich vu co thoi gian y lenh lon nhat)
                        HIS_SERE_SERV sereServ = IsNotNullOrEmpty(sereServs) ? sereServs.OrderByDescending(o => o.TDL_INTRUCTION_TIME).ThenByDescending(o => o.ID).FirstOrDefault() : null;
                        this.ProcessChangeFinishTime(sereServ, newBedLog, treatment, serviceReqs, sereServs, ref sqls, ref dicCreateSRSS, ref dicUpdateSRSS);
                    }

                    // Cap nhat khi thay doi thong tin doi tuong thanh toan, doi tuong phu thu, nam ghep
                    if (newBedLog.BED_SERVICE_TYPE_ID != oldBedLog.BED_SERVICE_TYPE_ID  || newBedLog.PATIENT_TYPE_ID != oldBedLog.PATIENT_TYPE_ID || newBedLog.PRIMARY_PATIENT_TYPE_ID != oldBedLog.PRIMARY_PATIENT_TYPE_ID || newBedLog.SHARE_COUNT != oldBedLog.SHARE_COUNT)
                    {
                        if (!this.bedTempChangeInfo.Run(treatment, newBedLog, workPlace, ref dicUpdateSRSS, ref this.beforeUpdateSS, ref ptas))
                        {
                            throw new Exception("Cap nhat thong tin gia dich vu giuong that bai");
                        }
                    }

                    this.ExecuteCreateOrUpdateData(dicCreateSRSS, dicUpdateSRSS, sqls, treatment, ptas);
                }
                result = true;
            }
            catch (Exception ex)
            {
                this.RollbackData();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }

            return result;
        }

        private void ExecuteCreateOrUpdateData(Dictionary<HIS_SERVICE_REQ, HIS_SERE_SERV> dicCreateSRSS, Dictionary<HIS_SERVICE_REQ, HIS_SERE_SERV> dicUpdateSRSS, List<string> sqls, HIS_TREATMENT treatment, List<HIS_PATIENT_TYPE_ALTER> ptas)
        {
            // Cap nhat
            if (dicUpdateSRSS != null && dicUpdateSRSS.Count > 0)
            {
                if (!this.reqUpdateProcessor.UpdateList(dicUpdateSRSS.Keys.ToList()))
                {
                    throw new Exception("Cap nhat cac thong tin y lenh khi thay doi thoi gian bat dau va ket thuc that bai");
                }

                List<HIS_SERE_SERV> ssToUpdate = new List<HIS_SERE_SERV>();
                foreach (var dic in dicUpdateSRSS)
                {
                    ssToUpdate.Add(dic.Value);
                }
                if (!this.ssUpdateProcessor.UpdateList(ssToUpdate, this.beforeUpdateSS, false))
                {
                    throw new Exception("Cap nhat cac thong tin dich vu khi thay doi thoi gian bat dau va ket thuc that bai");
                }
            }

            //Tao moi
            if (dicCreateSRSS != null && dicCreateSRSS.Count > 0)
            {
                if (!this.reqCreateProcessor.CreateList(dicCreateSRSS.Keys.ToList()))
                {
                    throw new Exception("Tao moi cac thong tin y lenh khi thay doi thoi gian bat dau va ket thuc that bai");
                }

                List<HIS_SERE_SERV> ssToCreate = new List<HIS_SERE_SERV>();
                foreach (var dic in dicCreateSRSS)
                {
                    dic.Value.SERVICE_REQ_ID = dic.Key.ID;
                    HisSereServUtil.SetTdl(dic.Value, dic.Key);
                    ssToCreate.Add(dic.Value);
                }
                if (!this.ssCreateProcessor.CreateList(ssToCreate, dicCreateSRSS.Keys.ToList(), false))
                {
                    throw new Exception("Cap nhat cac thong tin dich vu khi thay doi thoi gian bat dau va ket thuc that bai");
                }
            }

            // Xoa bo
            if (IsNotNullOrEmpty(sqls))
            {
                if (!DAOWorker.SqlDAO.Execute(sqls))
                {
                    LogSystem.Warn("Xoa HIS_SERE_SERV_EXT, HIS_SERE_SERV, HIS_SERVICE_REQ that bai");
                }
            }

            if (IsNotNullOrEmpty(ptas)
                && ((dicCreateSRSS != null && dicCreateSRSS.Values.ToList().Exists(o => o.PATIENT_TYPE_ID ==  HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT))
                || (dicUpdateSRSS != null && dicUpdateSRSS.Values.ToList().Exists(o => o.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT)))
            )
            {
                this.hisSereServUpdateHein = new HisSereServUpdateHein(param, treatment, ptas, false);
                //Cap nhat ti le BHYT cho sere_serv: chi thuc hien khi co y/c, tranh thuc hien nhieu lan, giam hieu nang
                if (!this.hisSereServUpdateHein.UpdateDb())
                {
                    throw new Exception("Du lieu se bi rollback. Ket thuc nghiep vu");
                }
            }
        }

        private void ProcessChangeFinishTime(HIS_SERE_SERV sereServ, HIS_BED_LOG newBedLog, HIS_TREATMENT treatment, List<HIS_SERVICE_REQ> serviceReqs, List<HIS_SERE_SERV> sereServs, ref List<string> sqls, ref Dictionary<HIS_SERVICE_REQ, HIS_SERE_SERV> dicCreateSRSS, ref Dictionary<HIS_SERVICE_REQ, HIS_SERE_SERV> dicUpdateSRSS)
        {
            if (sereServ != null)
            {
                if (!newBedLog.FINISH_TIME.HasValue)
                    newBedLog.FINISH_TIME = Inventec.Common.DateTime.Get.Now();
                DateTime dtFinish = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(newBedLog.FINISH_TIME.Value).Value;
                DateTime dtIntruc = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(sereServ.TDL_INTRUCTION_TIME).Value;
                DateTime dtIncome = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(treatment.CLINICAL_IN_TIME.Value).Value;
                if (dtIntruc.Date == dtFinish.Date)
                {
                    HIS_SERVICE_REQ serviceReq = serviceReqs.FirstOrDefault(o => o.ID == sereServ.SERVICE_REQ_ID);

                    if (serviceReq != null)
                    {
                        this.UpdateSRAndSSWithFinishTime(sereServ, newBedLog, serviceReq, serviceReqs, dtFinish, dtIncome, false, ref dicUpdateSRSS);
                    }
                }
                else if (dtIntruc.Date > dtFinish.Date)
                {
                    bool isDeleted = false;
                    List<HIS_SERVICE_REQ> deleteReqs = serviceReqs.Where(o => Inventec.Common.DateTime.Get.StartDay(o.INTRUCTION_TIME) > Inventec.Common.DateTime.Get.StartDay(newBedLog.FINISH_TIME.Value)).ToList();
                    HIS_SERVICE_REQ updateReq = serviceReqs.FirstOrDefault(o => Inventec.Common.DateTime.Get.StartDay(o.INTRUCTION_TIME) == Inventec.Common.DateTime.Get.StartDay(newBedLog.FINISH_TIME.Value));
                    if (IsNotNullOrEmpty(deleteReqs))
                    {
                        this.DeleteData(treatment.ID, deleteReqs, sereServs, ref sqls);
                        isDeleted = true;
                    }
                    if (IsNotNull(updateReq))
                    {
                        var ss = sereServs.FirstOrDefault(o => o.SERVICE_REQ_ID.HasValue && o.SERVICE_REQ_ID.Value == updateReq.ID);
                        this.UpdateSRAndSSWithFinishTime(ss, newBedLog, updateReq, serviceReqs, dtFinish, dtIncome, isDeleted, ref dicUpdateSRSS);
                    }
                }
                else
                {
                    // Cap nhat y lenh
                    HIS_SERVICE_REQ serviceReq = serviceReqs.FirstOrDefault(o => o.ID == sereServ.SERVICE_REQ_ID);
                    if (serviceReq.INTRUCTION_TIME != newBedLog.START_TIME) // Trường hợp sửa thời gian kết thúc và có 1 y lệnh
                    {
                        // Lay du lieu cu
                        this.beforeUpdateSS.Add(sereServ);

                        serviceReq.INTRUCTION_TIME = Inventec.Common.DateTime.Get.StartDay(serviceReq.INTRUCTION_TIME).Value;

                        //Cap nhat dich vu
                        if (sereServ.AMOUNT_TEMP.HasValue)
                        {
                            sereServ.AMOUNT_TEMP = 1;
                        }
                        else
                        {
                            sereServ.AMOUNT = 1;
                        }
                        HisSereServUtil.SetTdl(sereServ, serviceReq);
                        dicUpdateSRSS[serviceReq] = sereServ;
                    }
                    else
                    {
                        DateTime dtStart = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(newBedLog.START_TIME).Value;
                        this.UpdateOnlyAmountSS(sereServ, serviceReq, newBedLog, serviceReqs, dtStart, dtIncome, ref dicUpdateSRSS);
                    }
                    // Tinh khoang cach giua 2 thoi diem
                    var distancDates = new List<DateTime>();

                    DateTime dtnewIntruc = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(Inventec.Common.DateTime.Get.EndDay(serviceReq.INTRUCTION_TIME).Value).Value;

                    for (var dtcurrent = dtnewIntruc.AddDays(1); dtcurrent <= dtFinish.AddDays(1); dtcurrent = dtcurrent.AddDays(1))
                    {
                        distancDates.Add(dtcurrent);
                    }
                    if (IsNotNullOrEmpty(distancDates))
                    {
                        foreach (var d in distancDates)
                        {
                            Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                            HIS_SERVICE_REQ newSR = Mapper.Map<HIS_SERVICE_REQ>(serviceReq);

                            Mapper.CreateMap<HIS_SERE_SERV, HIS_SERE_SERV>();
                            HIS_SERE_SERV newSS = Mapper.Map<HIS_SERE_SERV>(sereServ);

                            if (d <= dtFinish)
                            {
                                // Gan thong tin y lenh
                                newSR.ID = 0;
                                newSR.INTRUCTION_TIME = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(d.Date).Value;

                                // Gan thong tin dich vu
                                newSS.ID = 0;
                                newSS.TDL_INTRUCTION_TIME = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(d.Date).Value;
                                if (newSS.AMOUNT_TEMP.HasValue)
                                {
                                    newSS.AMOUNT_TEMP = 1;
                                }
                                else
                                {
                                    newSS.AMOUNT = 1;
                                }
                                newSS.IS_TEMP_BED_PROCESSED = Constant.IS_TRUE;
                            }
                            else
                            {
                                // Gan thong tin y lenh
                                newSR.ID = 0;
                                newSR.INTRUCTION_TIME = Inventec.Common.DateTime.Get.StartDay(newBedLog.FINISH_TIME.Value).Value;

                                // Gan thong tin dich vu
                                newSS.ID = 0;
                                newSS.TDL_INTRUCTION_TIME = newSR.INTRUCTION_TIME;
                                DateTime dtIntructionTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(newSS.TDL_INTRUCTION_TIME).Value;
                                decimal? amount = HisBedLogUtil.CalculateBedAmount(serviceReqs, serviceReq, newBedLog, sereServ.TDL_REQUEST_DEPARTMENT_ID, dtFinish, dtIntructionTime, dtIncome);
                                if (newSS.AMOUNT_TEMP.HasValue && amount.HasValue)
                                {
                                    newSS.AMOUNT_TEMP = amount.Value;
                                }
                                else
                                {
                                    newSS.AMOUNT = amount.Value;
                                }
                                newSS.IS_TEMP_BED_PROCESSED = Constant.IS_TRUE;
                            }
                            dicCreateSRSS[newSR] = newSS;
                        }
                    }
                }
            }
        }

        private void UpdateOnlyAmountSS(HIS_SERE_SERV sereServ, HIS_SERVICE_REQ serviceReq, HIS_BED_LOG newBedLog, List<HIS_SERVICE_REQ> serviceReqs, DateTime dtStart, DateTime dtIncome, ref Dictionary<HIS_SERVICE_REQ, HIS_SERE_SERV> dicUpdateSRSS)
        {
            // Lay du lieu cu
            this.beforeUpdateSS.Add(sereServ);

            //Cap nhat dich vu
            HisSereServUtil.SetTdl(sereServ, serviceReq);
            DateTime dtIntruc = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(sereServ.TDL_INTRUCTION_TIME).Value;
            decimal? amount = HisBedLogUtil.CalculateBedAmount(serviceReqs, serviceReq, newBedLog, sereServ.TDL_REQUEST_DEPARTMENT_ID, dtStart, dtIntruc, dtIncome);
            if (sereServ.AMOUNT_TEMP.HasValue && amount.HasValue)
            {
                sereServ.AMOUNT_TEMP = amount.Value;
            }
            else
            {
                sereServ.AMOUNT = amount.Value;
            }
            dicUpdateSRSS[serviceReq] = sereServ;
        }

        private void ProcessChangeStartTime(HIS_SERE_SERV sereServ, HIS_BED_LOG newBedLog, HIS_TREATMENT treatment, List<HIS_SERVICE_REQ> serviceReqs, List<HIS_SERE_SERV> sereServs, ref List<string> sqls, ref Dictionary<HIS_SERVICE_REQ, HIS_SERE_SERV> dicCreateSRSS, ref Dictionary<HIS_SERVICE_REQ, HIS_SERE_SERV> dicUpdateSRSS)
        {
            if (sereServ != null)
            {
                DateTime dtStart = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(newBedLog.START_TIME).Value;
                DateTime dtIntruc = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(sereServ.TDL_INTRUCTION_TIME).Value;
                DateTime dtIncome = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(treatment.CLINICAL_IN_TIME.Value).Value;

                if (dtIntruc.Date == dtStart.Date)
                {
                    HIS_SERVICE_REQ serviceReq = serviceReqs.FirstOrDefault(o => o.ID == sereServ.SERVICE_REQ_ID);

                    if (serviceReq != null)
                    {
                        this.UpdateSRAndSSWithStartTime(sereServ, newBedLog, serviceReq, serviceReqs, dtStart, dtIncome, ref dicUpdateSRSS);
                    }
                }
                else if (dtIntruc.Date < dtStart.Date)
                {
                    List<HIS_SERVICE_REQ> deleteReqs = serviceReqs.Where(o => Inventec.Common.DateTime.Get.StartDay(o.INTRUCTION_TIME) < Inventec.Common.DateTime.Get.StartDay(newBedLog.START_TIME)).ToList();
                    HIS_SERVICE_REQ updateReq = serviceReqs.FirstOrDefault(o => Inventec.Common.DateTime.Get.StartDay(o.INTRUCTION_TIME) == Inventec.Common.DateTime.Get.StartDay(newBedLog.START_TIME));
                    if (IsNotNullOrEmpty(deleteReqs))
                    {
                        this.DeleteData(treatment.ID, deleteReqs, sereServs, ref sqls);
                    }
                    if (IsNotNull(updateReq))
                    {
                        var ss = sereServs.FirstOrDefault(o => o.SERVICE_REQ_ID.HasValue && o.SERVICE_REQ_ID.Value == updateReq.ID);
                        this.UpdateSRAndSSWithStartTime(ss, newBedLog, updateReq, serviceReqs, dtStart, dtIncome, ref dicUpdateSRSS);
                    }
                }
                else
                {
                    // Lay du lieu cu
                    this.beforeUpdateSS.Add(sereServ);

                    // Cap nhat y lenh
                    HIS_SERVICE_REQ serviceReq = serviceReqs.FirstOrDefault(o => o.ID == sereServ.SERVICE_REQ_ID);
                    serviceReq.INTRUCTION_TIME = Inventec.Common.DateTime.Get.StartDay(serviceReq.INTRUCTION_TIME).Value;

                    // Cap nhat dich vu
                    HisSereServUtil.SetTdl(sereServ, serviceReq);
                    if (sereServ.AMOUNT_TEMP.HasValue)
                    {
                        sereServ.AMOUNT_TEMP = 1;
                    }
                    else
                    {
                        sereServ.AMOUNT = 1;
                    }
                    dicUpdateSRSS[serviceReq] = sereServ;

                    // Tinh khoang cach giua 2 thoi diem
                    var distancDates = new List<DateTime>();

                    DateTime dtnewIntruc = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(serviceReq.INTRUCTION_TIME).Value.Date;

                    for (var dtcurrent = dtStart; dtcurrent <= dtnewIntruc; dtcurrent = dtcurrent.AddDays(1))
                    {
                        distancDates.Add(dtcurrent);
                    }
                    if (IsNotNullOrEmpty(distancDates))
                    {

                        // Them dach sach y lenh se duoc tao moi ma chua duoc update vao db
                        Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                        List<HIS_SERVICE_REQ> allAndAdditionReqs = Mapper.Map<List<HIS_SERVICE_REQ>>(serviceReqs);
                        distancDates = distancDates.OrderByDescending(o => o.Date).ToList();
                        foreach (var d in distancDates)
                        {
                            Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                            HIS_SERVICE_REQ newSR = Mapper.Map<HIS_SERVICE_REQ>(serviceReq);

                            Mapper.CreateMap<HIS_SERE_SERV, HIS_SERE_SERV>();
                            HIS_SERE_SERV newSS = Mapper.Map<HIS_SERE_SERV>(sereServ);

                            if (d.Date >= dtStart)
                            {
                                // Gan thong tin y lenh
                                newSR.ID = 0;
                                newSR.INTRUCTION_TIME = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(d.Date).Value;
                                allAndAdditionReqs.Add(newSR);

                                // Gan thong tin dich vu
                                newSS.ID = 0;
                                newSS.TDL_INTRUCTION_TIME = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(d.Date).Value;
                                if (newSS.AMOUNT_TEMP.HasValue)
                                {
                                    newSS.AMOUNT_TEMP = 1;
                                }
                                else
                                {
                                    newSS.AMOUNT = 1;
                                }
                                newSS.IS_TEMP_BED_PROCESSED = Constant.IS_TRUE;
                            }
                            else
                            {
                                // Gan thong tin y lenh
                                newSR.ID = 0;
                                newSR.INTRUCTION_TIME = newBedLog.START_TIME;

                                // Gan thong tin dich vu
                                newSS.ID = 0;
                                newSS.TDL_INTRUCTION_TIME = newSR.INTRUCTION_TIME;
                                DateTime dtIntructionTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(newSS.TDL_INTRUCTION_TIME).Value;
                                decimal? amount = HisBedLogUtil.CalculateBedAmount(allAndAdditionReqs, newSR, newBedLog, sereServ.TDL_REQUEST_DEPARTMENT_ID, dtStart, dtIntructionTime, dtIncome);
                                if (newSS.AMOUNT_TEMP.HasValue && amount.HasValue)
                                {
                                    newSS.AMOUNT_TEMP = amount.Value;
                                }
                                else
                                {
                                    newSS.AMOUNT = amount.Value;
                                }
                                newSS.IS_TEMP_BED_PROCESSED = Constant.IS_TRUE;
                            }
                            dicCreateSRSS[newSR] = newSS;
                        }
                    }
                }
            }
        }

        private void DeleteData(long treatmentId, List<HIS_SERVICE_REQ> deleteReqs, List<HIS_SERE_SERV> allSS, ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(deleteReqs))
            {
                List<long> delReqIds = deleteReqs.Select(o => o.ID).ToList();
                List<HIS_SERE_SERV> delSS = allSS.Where(o => o.SERVICE_REQ_ID.HasValue && delReqIds.Contains(o.SERVICE_REQ_ID.Value)).ToList();

                if (IsNotNullOrEmpty(delSS))
                {
                    List<long> delSSIds = delSS.Select(o => o.ID).ToList();
                    sqls.Add(string.Format("UPDATE HIS_SERE_SERV_EXT EXT SET EXT.IS_DELETE = 1, EXT.TDL_SERVICE_REQ_ID = NULL, EXT.TDL_TREATMENT_ID = NULL, BED_LOG_ID = NULL WHERE SERE_SERV_ID IN ({0})", string.Join(", ", delSSIds)));
                    sqls.Add(string.Format("UPDATE HIS_SERE_SERV SS SET SS.IS_DELETE = 1, SS.SERVICE_REQ_ID = NULL, SS.TDL_TREATMENT_ID = NULL WHERE ID IN ({0})", string.Join(", ", delSSIds)));
                    sqls.Add(string.Format("UPDATE HIS_SERVICE_REQ SR SET SR.IS_DELETE = 1 WHERE ID IN ({0})", string.Join(", ", delReqIds)));
                }
            }
        }


        private void UpdateSRAndSSWithFinishTime(HIS_SERE_SERV sereServ, HIS_BED_LOG newBedLog, HIS_SERVICE_REQ serviceReq, List<HIS_SERVICE_REQ> serviceReqs, DateTime dtFinish, DateTime dtIncome, bool isDeleted, ref Dictionary<HIS_SERVICE_REQ, HIS_SERE_SERV> dicUpdateSRSS)
        {
            if (serviceReq.INTRUCTION_TIME != newBedLog.START_TIME) // Trường hợp sửa thời gian kết thúc và có 1 y lệnh
            {
                // Lay du lieu cu
                this.beforeUpdateSS.Add(sereServ);

                //Cap nhat y lenh
                serviceReq.INTRUCTION_TIME = Inventec.Common.DateTime.Get.StartDay(newBedLog.FINISH_TIME.Value).Value;

                //Cap nhat dich vu
                HisSereServUtil.SetTdl(sereServ, serviceReq);
                DateTime dtIntruc = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(sereServ.TDL_INTRUCTION_TIME).Value;
                decimal? amount = HisBedLogUtil.CalculateBedAmount(serviceReqs, serviceReq, newBedLog, sereServ.TDL_REQUEST_DEPARTMENT_ID, dtFinish, dtIntruc, dtIncome, isDeleted);
                if (sereServ.AMOUNT_TEMP.HasValue && amount.HasValue)
                {
                    sereServ.AMOUNT_TEMP = amount.Value;
                }
                else
                {
                    sereServ.AMOUNT = amount.Value;
                }
                dicUpdateSRSS[serviceReq] = sereServ;
            }
            DateTime dtStart = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(newBedLog.START_TIME).Value;
            this.UpdateOnlyAmountSS(sereServ, serviceReq, newBedLog, serviceReqs, dtStart, dtIncome, ref dicUpdateSRSS);
        }

        private void UpdateSRAndSSWithStartTime(HIS_SERE_SERV sereServ, HIS_BED_LOG newBedLog, HIS_SERVICE_REQ serviceReq, List<HIS_SERVICE_REQ> serviceReqs, DateTime dtStart, DateTime dtIncome, ref Dictionary<HIS_SERVICE_REQ, HIS_SERE_SERV> dicUpdateSRSS)
        {
            // Lay du lieu cu
            this.beforeUpdateSS.Add(sereServ);

            //Cap nhat y lenh
            serviceReq.INTRUCTION_TIME = newBedLog.START_TIME;

            //Cap nhat dich vu
            HisSereServUtil.SetTdl(sereServ, serviceReq);
            DateTime dtIntruc = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(sereServ.TDL_INTRUCTION_TIME).Value;
            decimal? amount = HisBedLogUtil.CalculateBedAmount(serviceReqs, serviceReq, newBedLog, sereServ.TDL_REQUEST_DEPARTMENT_ID, dtStart, dtIntruc, dtIncome);
            if (sereServ.AMOUNT_TEMP.HasValue && amount.HasValue)
            {
                sereServ.AMOUNT_TEMP = amount.Value;
            }
            else
            {
                sereServ.AMOUNT = amount.Value;
            }
            dicUpdateSRSS[serviceReq] = sereServ;
        }

        internal void RollbackData()
        {
            if (this.hisSereServUpdateHein != null)
            {
                this.hisSereServUpdateHein.RollbackData();
            }
            this.reqCreateProcessor.RollbackData();
            this.ssCreateProcessor.RollbackData();
            this.reqUpdateProcessor.RollbackData();
            this.ssUpdateProcessor.RollbackData();
        }
    }
}
