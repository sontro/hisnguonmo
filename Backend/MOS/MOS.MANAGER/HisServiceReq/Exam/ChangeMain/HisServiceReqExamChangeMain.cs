using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisServiceReq;
using Inventec.Token.ResourceSystem;
using AutoMapper;
using MOS.UTILITY;
using MOS.LibraryEventLog;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisIcd;
using MOS.MANAGER.EventLogUtil;

namespace MOS.MANAGER.HisServiceReq.Exam.ChangeMain
{
    /// <summary>
    /// Xu ly nghiep vu chuyen kham chinh
    /// </summary>
    partial class HisServiceReqExamChangeMain : BusinessBase
    {
        private HisServiceReqUpdate hisServiceReqUpdate;
        private HisSereServUpdateHein hisSereServUpdateHein;
        private HisTreatmentUpdate hisTreatmentUpdate;

        internal HisServiceReqExamChangeMain()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqExamChangeMain(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
            this.hisTreatmentUpdate = new HisTreatmentUpdate(param);
        }

        internal bool Run(long serviceReqId, ref HIS_SERVICE_REQ resultData)
        {
            bool result = false;
            try
            {
                HIS_SERVICE_REQ serviceReq = null;
                List<HIS_SERVICE_REQ> currentMainExams = null;
                HIS_TREATMENT treatment = null;

                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                HisServiceReqExamCheck examChecker = new HisServiceReqExamCheck(param);
                bool valid = true;
                valid = valid && this.IsValidData(serviceReqId, ref serviceReq, ref currentMainExams);
                valid = valid && treatmentChecker.VerifyId(serviceReq.TREATMENT_ID, ref treatment);
                valid = valid && treatmentChecker.IsUnLock(treatment);
                valid = valid && treatmentChecker.IsUnTemporaryLock(treatment);
                valid = valid && treatmentChecker.IsUnpause(treatment);
                valid = valid && treatmentChecker.IsUnLockHein(treatment);

                if (valid)
                {
                    this.ProcessServiceReq(serviceReq, currentMainExams);
                    this.ProcessSereServ(treatment, serviceReq, currentMainExams);
                    this.ProcessTreatment(treatment, serviceReq, currentMainExams);
                    resultData = serviceReq;
                    result = true;

                    new EventLogGenerator(EventLog.Enum.HisServiceReq_CapNhatKhamChinh)
                            .TreatmentCode(resultData.TDL_TREATMENT_CODE)
                            .ServiceReqCode(resultData.SERVICE_REQ_CODE)
                            .Run();
                }
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

        private void ProcessTreatment(HIS_TREATMENT treatment, HIS_SERVICE_REQ data, List<HIS_SERVICE_REQ> currentMainExams)
        {
            if (data != null && treatment != null)
            {
                Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();
                HIS_TREATMENT before = Mapper.Map<HIS_TREATMENT>(treatment);

                //Set thong tin benh chinh theo y lenh moi
                treatment.ICD_CODE = data.ICD_CODE;
                treatment.ICD_NAME = data.ICD_NAME;
                //Bo thong tin benh phu theo y lenh moi
                treatment.ICD_SUB_CODE = HisIcdUtil.Remove(treatment.ICD_SUB_CODE, data.ICD_CODE);
                treatment.ICD_TEXT = HisIcdUtil.Remove(treatment.ICD_TEXT, data.ICD_NAME);

                if (IsNotNullOrEmpty(currentMainExams))
                {
                    foreach (HIS_SERVICE_REQ s in currentMainExams)
                    {
                        //Them thong tin benh phu theo y lenh kham chinh cu
                        HisTreatmentUpdate.AddIcd(treatment, s.ICD_CODE, s.ICD_NAME);
                    }
                }

                if (!this.hisTreatmentUpdate.Update(treatment, before))
                {
                    throw new Exception("Cap nhat lai thong tin ICD cua treatment");
                }
            }
        }

        //Cap nhat lai thong tin "kham chinh"
        private void ProcessServiceReq(HIS_SERVICE_REQ data, List<HIS_SERVICE_REQ> currentMainExams)
        {
            List<HIS_SERVICE_REQ> olds = new List<HIS_SERVICE_REQ>();
            List<HIS_SERVICE_REQ> news = new List<HIS_SERVICE_REQ>();

            Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();

            //gan kham chinh cho y lenh duoc chon
            if (data != null)
            {
                HIS_SERVICE_REQ beforeUpdate = Mapper.Map<HIS_SERVICE_REQ>(data);
                olds.Add(beforeUpdate);
                data.IS_MAIN_EXAM = Constant.IS_TRUE;
                news.Add(data);
            }

            //bo kham chinh doi voi cac y lenh kham chinh hien tai
            if (IsNotNullOrEmpty(currentMainExams))
            {
                List<HIS_SERVICE_REQ> beforeUpdates = Mapper.Map<List<HIS_SERVICE_REQ>>(currentMainExams);
                olds.AddRange(beforeUpdates);
                currentMainExams.ForEach(o => o.IS_MAIN_EXAM = null);
                news.AddRange(currentMainExams);
            }

            if (!this.hisServiceReqUpdate.UpdateList(news, olds))
            {
                throw new Exception("Cap nhat thong tin kham chinh cua y lenh that bai");
            }
        }

        private void ProcessSereServ(HIS_TREATMENT treatment, HIS_SERVICE_REQ serviceReq, List<HIS_SERVICE_REQ> currentMainExams)
        {
            //Viec cap nhat TDL_IS_MAIN_EXAM da duoc xu ly trong trigger (trong HIS_SERVICE_REQ), vi vay
            //chi can thuc hien cap nhat lai thong tin gia do thay doi "kham chinh", chu ko can cap nhat TDL_IS_MAIN_EXAM
            this.hisSereServUpdateHein = new HisSereServUpdateHein(param, treatment, false);
            if (!this.hisSereServUpdateHein.UpdateDb())
            {
                throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
            }
        }

        private bool IsValidData(long serviceReqId, ref HIS_SERVICE_REQ data, ref List<HIS_SERVICE_REQ> currentMainExams)
        {
            try
            {
                if (!HisEmployeeUtil.IsAdmin())
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.BanKhongPhaiLaQuanTri);
                    return false;
                }

                //Kiem tra du lieu hien tai co hop le khong
                var serviceReq = new HisServiceReqGet().GetById(serviceReqId);
                
                if (serviceReq == null || serviceReq.SERVICE_REQ_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("serviceReq null hoac ko phai la 'Kham'" + LogUtil.TraceData("serviceReq", serviceReq));
                    return false;
                }

                //Neu y lenh hien tai dang la "kham chinh" thi ko cho phep thuc hien
                if (serviceReq.IS_MAIN_EXAM == Constant.IS_TRUE)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_YLenhDangLaKhamChinh);
                    return false;
                }

                //Lay "cac" (de phong co do loi du lieu, co nhieu hon 1 kham chinh) kham chinh hien tai
                HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
                filter.TREATMENT_ID = serviceReq.TREATMENT_ID;
                filter.SERVICE_REQ_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH;
                filter.IS_MAIN_EXAM = true;
                List<HIS_SERVICE_REQ> mains = new HisServiceReqGet().Get(filter);

                data = serviceReq;
                currentMainExams = mains;

                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        //Rollback du lieu
        internal void RollbackData()
        {
            if (this.hisSereServUpdateHein != null)
            {
                this.hisSereServUpdateHein.RollbackData();
            }
            this.hisServiceReqUpdate.RollbackData();
            this.hisTreatmentUpdate.RollbackData();
        }
    }
}
