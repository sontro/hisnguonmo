using AutoMapper;
using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisEkip;
using MOS.MANAGER.HisEkipUser;
using MOS.MANAGER.HisEyeSurgryDesc;
using MOS.MANAGER.EventLogUtil;
using MOS.LibraryEventLog;
using MOS.MANAGER.HisSkinSurgeryDesc;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServExt;
using MOS.MANAGER.HisSereServFile;
using MOS.MANAGER.HisSereServPttt;
using MOS.MANAGER.HisSesePtttMethod;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.MANAGER.HisStentConclude;

namespace MOS.MANAGER.HisServiceReq.Surg.SurgAssignAndCopy
{
    class HisSereServ
    {
        public HIS_SERE_SERV SereServ { get; set; }
        public HIS_SERE_SERV_EXT SereServExt { get; set; }

    }

    class SurgAssignAndCopyData
    {
        public HIS_SERVICE_REQ ServiceReq { get; set; }
        public List<HisSereServ> SereServs { get; set; }
    }

    class SurgAssignAndCopyProcessor : BusinessBase
    {
        private HisServiceReqCreate reqCreateProcessor;
        private HisSereServCreate ssCreateProcessor;
        private HisSereServExtCreate extCreateProcessor;
        private HisSesePtttMethodCreate methodCreateProcessor;
        private HisStentConcludeCreate stentConcludeCreateProcessor;

        internal SurgAssignAndCopyProcessor()
            : base()
        {
            this.Init();
        }

        internal SurgAssignAndCopyProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.reqCreateProcessor = new HisServiceReqCreate(param);
            this.ssCreateProcessor = new HisSereServCreate(param);
            this.extCreateProcessor = new HisSereServExtCreate(param);
            this.methodCreateProcessor = new HisSesePtttMethodCreate(param);
            this.stentConcludeCreateProcessor = new HisStentConcludeCreate(param);
        }

        internal bool Run(SurgAssignAndCopySDO data)
        {
            bool result = false;
            try
            {
                HIS_TREATMENT treatment = null;
                HIS_SERVICE_REQ serviceReq = null;

                SurgAssignAndCopyProcessorCheck checker = new SurgAssignAndCopyProcessorCheck(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                HisServiceReqCheck reqChecker = new HisServiceReqCheck(param);

                bool valid = true;
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && reqChecker.VerifyId(data.ServiceReqId, ref serviceReq);
                valid = valid && reqChecker.IsFinished(serviceReq);
                valid = valid && treatmentChecker.VerifyId(serviceReq.TREATMENT_ID, ref treatment);
                valid = valid && treatmentChecker.HasNotFinished(treatment);

                if (valid)
                {
                    List<HIS_SERE_SERV> sereServs = new HisSereServGet().GetByServiceReqId(serviceReq.ID);
                    List<HIS_SERE_SERV_EXT> ssExts = IsNotNullOrEmpty(sereServs) ? new HisSereServExtGet().GetBySereServIds(sereServs.Select(s => s.ID).ToList()) : null;
                    List<HIS_SERE_SERV_FILE> ssFiles = IsNotNullOrEmpty(sereServs) ? new HisSereServFileGet().GetBySereServIds(sereServs.Select(s => s.ID).ToList()) : null;
                    List<HIS_EKIP_USER> ekipUsers = IsNotNullOrEmpty(sereServs) ? new HisEkipUserGet().GetByEkipIds(sereServs.Where(o => o.EKIP_ID.HasValue).Select(s => s.EKIP_ID.Value).Distinct().ToList()) : null;
                    List<HIS_SERE_SERV_PTTT> ssPttts = IsNotNullOrEmpty(sereServs) ? new HisSereServPtttGet().GetBySereServIds(sereServs.Select(s => s.ID).ToList()) : null;
                    List<HIS_SESE_PTTT_METHOD> ssPtttMethods = IsNotNullOrEmpty(ssPttts) ? new HisSesePtttMethodGet().GetBySereServPtttIds(ssPttts.Select(s => s.ID).ToList()) : null;
                    List<HIS_EYE_SURGRY_DESC> eyeSurgryDescs = IsNotNullOrEmpty(ssPttts) ? new HisEyeSurgryDescGet().GetByIds(ssPttts.Where(o => o.EYE_SURGRY_DESC_ID.HasValue).Select(s => s.ID).Distinct().ToList()) : null;
                    List<HIS_SKIN_SURGERY_DESC> skinSurgeryDescs = IsNotNullOrEmpty(ssPttts) ? new HisSkinSurgeryDescGet().GetByIds(ssPttts.Where(o => o.EYE_SURGRY_DESC_ID.HasValue).Select(s => s.ID).Distinct().ToList()) : null;
                    List<HIS_STENT_CONCLUDE> stentConludes = IsNotNullOrEmpty(sereServs) ? new HisStentConcludeGet().GetBySereServIds(sereServs.Select(s => s.ID).ToList()) : null;

                    Dictionary<HIS_SERVICE_REQ, List<HIS_SERE_SERV>> dicCreateSRSS = new Dictionary<HIS_SERVICE_REQ, List<HIS_SERE_SERV>>();
                    Dictionary<HIS_SERE_SERV, SereServChildrenData> dicSSChildren = new Dictionary<HIS_SERE_SERV, SereServChildrenData>();

                    foreach (var intructionTime in data.InstructionTimes)
                    {
                        Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                        HIS_SERVICE_REQ newServiceReq = Mapper.Map<HIS_SERVICE_REQ>(serviceReq);
                        newServiceReq.INTRUCTION_TIME = intructionTime;
                        newServiceReq.INTRUCTION_DATE = Inventec.Common.DateTime.Get.StartDay(intructionTime).Value;
                        newServiceReq.TRACKING_ID = null;
                        DateTime? reqStart = serviceReq.START_TIME.HasValue ? Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(serviceReq.START_TIME.Value) : null;
                        DateTime? reqFinish = serviceReq.FINISH_TIME.HasValue ? Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(serviceReq.FINISH_TIME.Value) : null;
                        if (reqStart != null)
                            newServiceReq.START_TIME = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(newServiceReq.INTRUCTION_DATE).Value.AddHours(reqStart.Value.Hour).AddMinutes(reqStart.Value.Minute).AddSeconds(reqStart.Value.Second));
                        if (reqFinish != null)
                            newServiceReq.FINISH_TIME = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(newServiceReq.INTRUCTION_DATE).Value.AddHours(reqFinish.Value.Hour).AddMinutes(reqFinish.Value.Minute).AddSeconds(reqFinish.Value.Second));

                        if (!IsNotNullOrEmpty(sereServs))
                            continue;

                        List<HIS_SERE_SERV> newSereServs = new List<HIS_SERE_SERV>();
                        List<SereServChildrenData> childrenData = new List<SereServChildrenData>();

                        foreach (var sereServ in sereServs)
                        {
                            SereServChildrenData newdata = new SereServChildrenData();

                            Mapper.CreateMap<HIS_SERE_SERV, HIS_SERE_SERV>();
                            HIS_SERE_SERV newSereServ = Mapper.Map<HIS_SERE_SERV>(sereServ);

                            HIS_SERE_SERV_EXT ext = IsNotNullOrEmpty(ssExts) ? ssExts.FirstOrDefault(o => o.SERE_SERV_ID == sereServ.ID) : null;
                            Mapper.CreateMap<HIS_SERE_SERV_EXT, HIS_SERE_SERV_EXT>();
                            HIS_SERE_SERV_EXT newExt = Mapper.Map<HIS_SERE_SERV_EXT>(ext);

                            HIS_SERE_SERV_PTTT pttt = IsNotNullOrEmpty(ssPttts) ? ssPttts.FirstOrDefault(o => o.SERE_SERV_ID == sereServ.ID) : null;
                            Mapper.CreateMap<HIS_SERE_SERV_PTTT, HIS_SERE_SERV_PTTT>();
                            HIS_SERE_SERV_PTTT newPttt = Mapper.Map<HIS_SERE_SERV_PTTT>(pttt);

                            List<HIS_SERE_SERV_FILE> files = IsNotNullOrEmpty(ssFiles) ? ssFiles.Where(o => o.SERE_SERV_ID == sereServ.ID).ToList() : null;
                            Mapper.CreateMap<HIS_SERE_SERV_FILE, HIS_SERE_SERV_FILE>();
                            List<HIS_SERE_SERV_FILE> newFiles = Mapper.Map<List<HIS_SERE_SERV_FILE>>(files);

                            List<HIS_SESE_PTTT_METHOD> ptttMethods = IsNotNull(pttt) && IsNotNullOrEmpty(ssPtttMethods) ? ssPtttMethods.Where(o => o.SERE_SERV_PTTT_ID == pttt.ID).ToList() : null;
                            Mapper.CreateMap<HIS_SESE_PTTT_METHOD, HIS_SESE_PTTT_METHOD>();
                            List<HIS_SESE_PTTT_METHOD> newPtttMethods = Mapper.Map<List<HIS_SESE_PTTT_METHOD>>(ptttMethods);

                            List<HIS_EKIP_USER> ekUsers = sereServ.EKIP_ID.HasValue && IsNotNullOrEmpty(ekipUsers) ? ekipUsers.Where(o => o.EKIP_ID == sereServ.EKIP_ID).ToList() : null;
                            Mapper.CreateMap<HIS_EKIP_USER, HIS_EKIP_USER>();
                            List<HIS_EKIP_USER> newEkUsers = Mapper.Map<List<HIS_EKIP_USER>>(ekUsers);

                            List<HIS_STENT_CONCLUDE> stConludes = IsNotNullOrEmpty(stentConludes) ? stentConludes.Where(o => o.SERE_SERV_ID == sereServ.ID).ToList() : null;
                            Mapper.CreateMap<HIS_STENT_CONCLUDE, HIS_STENT_CONCLUDE>();
                            List<HIS_STENT_CONCLUDE> newStConludes = Mapper.Map<List<HIS_STENT_CONCLUDE>>(stConludes);

                            // Thong tin "Mat"
                            HIS_EYE_SURGRY_DESC eyeSurgryDesc = pttt.EYE_SURGRY_DESC_ID.HasValue && IsNotNullOrEmpty(eyeSurgryDescs) ? eyeSurgryDescs.FirstOrDefault(o => o.ID == pttt.EYE_SURGRY_DESC_ID.Value) : null;
                            Mapper.CreateMap<HIS_EYE_SURGRY_DESC, HIS_EYE_SURGRY_DESC>();
                            HIS_EYE_SURGRY_DESC newEyeSurgryDesc = Mapper.Map<HIS_EYE_SURGRY_DESC>(eyeSurgryDesc);

                            // Thong tin "Da lieu"
                            HIS_SKIN_SURGERY_DESC skinSurgeryDesc = pttt.SKIN_SURGERY_DESC_ID.HasValue && IsNotNullOrEmpty(skinSurgeryDescs) ? skinSurgeryDescs.FirstOrDefault(o => o.ID == pttt.SKIN_SURGERY_DESC_ID.Value) : null;
                            Mapper.CreateMap<HIS_SKIN_SURGERY_DESC, HIS_SKIN_SURGERY_DESC>();
                            HIS_SKIN_SURGERY_DESC newSkinSurgeryDesc = Mapper.Map<HIS_SKIN_SURGERY_DESC>(skinSurgeryDesc);

                            string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                            if (newPttt != null)
                            {
                                if (newEyeSurgryDesc != null)
                                {
                                    newEyeSurgryDesc.CREATOR = loginName;
                                    newEyeSurgryDesc.MODIFIER = loginName;
                                }
                                if (newSkinSurgeryDesc != null)
                                {
                                    newSkinSurgeryDesc.CREATOR = loginName;
                                    newSkinSurgeryDesc.MODIFIER = loginName;
                                }

                                newPttt.CREATOR = loginName;
                                newPttt.MODIFIER = loginName;
                                newPttt.HIS_EYE_SURGRY_DESC = newEyeSurgryDesc;
                                newPttt.HIS_SKIN_SURGERY_DESC = newSkinSurgeryDesc;
                            }

                            newSereServ.HIS_EKIP = new HIS_EKIP();
                            newSereServ.HIS_EKIP.CREATOR = loginName;
                            newSereServ.HIS_EKIP.MODIFIER = loginName;
                            if (IsNotNullOrEmpty(newEkUsers))
                            {
                                newEkUsers.ForEach(o => { o.CREATOR = loginName; o.MODIFIER = loginName; });
                                newSereServ.HIS_EKIP.HIS_EKIP_USER = newEkUsers;
                            }

                            newSereServ.HIS_SERE_SERV_PTTT = new List<HIS_SERE_SERV_PTTT>() { newPttt };
                            if (IsNotNullOrEmpty(newFiles))
                            {
                                newFiles.ForEach(o => { o.CREATOR = loginName; o.MODIFIER = loginName; });
                            }
                            newSereServ.HIS_SERE_SERV_FILE = newFiles;
                            newSereServs.Add(newSereServ);

                            newdata.SereServExt = newExt;
                            newdata.SereServPttt = newPttt;
                            //newdata.SereServFiles = newFiles;
                            newdata.SereServPtttMethods = newPtttMethods;
                            newdata.StentConcludes = newStConludes;
                            //newdata.EyeSurgryDesc = newEyeSurgryDesc;
                            //newdata.SkinSurgeryDesc = newSkinSurgeryDesc;
                            //dicSSChildren[newSereServ]
                            dicSSChildren[newSereServ] = newdata;
                        }
                        dicCreateSRSS[newServiceReq] = newSereServs;
                    }

                    //Clone
                    if (dicCreateSRSS != null && dicCreateSRSS.Count > 0)
                    {
                        if (!this.reqCreateProcessor.CreateListWithoutCheckAnyThings(dicCreateSRSS.Keys.ToList()))
                        {
                            throw new Exception("Tao moi cac thong tin y lenh khi clone that bai");
                        }

                        List<HIS_SERE_SERV> ssToCreate = new List<HIS_SERE_SERV>();
                        foreach (var dic in dicCreateSRSS)
                        {

                            List<HIS_SERE_SERV> list = dic.Value;
                            foreach (HIS_SERE_SERV ss in list)
                            {
                                ss.ID = 0;
                                ss.SERVICE_REQ_ID = dic.Key.ID;
                                HisSereServUtil.SetTdl(ss, dic.Key);
                            }
                            ssToCreate.AddRange(list);
                        }

                        if (!this.ssCreateProcessor.CreateList(ssToCreate, dicCreateSRSS.Keys.ToList(), false))
                        {
                            throw new Exception("Rollback du lieu");
                        }


                        if (IsNotNullOrEmpty(ssToCreate) && dicSSChildren != null)
                        {
                            List<HIS_SERE_SERV_EXT> extsToCreate = new List<HIS_SERE_SERV_EXT>();
                            List<HIS_SESE_PTTT_METHOD> methodsToCreate = new List<HIS_SESE_PTTT_METHOD>();
                            List<HIS_STENT_CONCLUDE> stentConludesToCreate = new List<HIS_STENT_CONCLUDE>();
                            foreach (HIS_SERE_SERV ss in dicSSChildren.Keys)
                            {
                                SereServChildrenData chil = dicSSChildren[ss];
                                if (chil != null)
                                {
                                    HIS_SERE_SERV_EXT ext = chil.SereServExt;
                                    HIS_SERE_SERV_PTTT pt = chil.SereServPttt;
                                    HIS_SERVICE_REQ req = dicCreateSRSS.Keys.FirstOrDefault(o => o.ID == ss.SERVICE_REQ_ID);
                                    List<HIS_SESE_PTTT_METHOD> methods = chil.SereServPtttMethods;
                                    List<HIS_STENT_CONCLUDE> stents = chil.StentConcludes;

                                    if (ext != null)
                                    {
                                        ext.SERE_SERV_ID = ss.ID;
                                        ext.TDL_SERVICE_REQ_ID = req.ID;
                                        DateTime? dtstart = ext.BEGIN_TIME.HasValue ? Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(ext.BEGIN_TIME.Value) : null;
                                        DateTime? dtend = ext.END_TIME.HasValue ? Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(ext.END_TIME.Value) : null;
                                        if (dtstart != null)
                                            ext.BEGIN_TIME = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(req.INTRUCTION_DATE).Value.AddHours(dtstart.Value.Hour).AddMinutes(dtstart.Value.Minute).AddSeconds(dtstart.Value.Second));
                                        if (dtend != null)
                                            ext.END_TIME = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(req.INTRUCTION_DATE).Value.AddHours(dtend.Value.Hour).AddMinutes(dtend.Value.Minute).AddSeconds(dtend.Value.Second));
                                        HisSereServExtUtil.SetTdl(ext, ss);
                                        extsToCreate.Add(ext);
                                    }
                                    if (methods != null && pt != null)
                                    {
                                        methods.ForEach(o => { o.TDL_SERVICE_REQ_ID = req.ID; o.TDL_SERE_SERV_ID = ss.ID; o.SERE_SERV_PTTT_ID = pt.ID; });
                                        methodsToCreate.AddRange(methods);
                                    }
                                    if (IsNotNullOrEmpty(stents))
                                    {
                                        stents.ForEach(o => { o.SERE_SERV_ID = ss.ID;});
                                        stentConludesToCreate.AddRange(stents);
                                    }
                                }
                            }

                            if (IsNotNullOrEmpty(extsToCreate) && !this.extCreateProcessor.CreateList(extsToCreate))
                            {
                                throw new Exception("Rollback du lieu");
                            }

                            if (IsNotNullOrEmpty(methodsToCreate) && !this.methodCreateProcessor.CreateList(methodsToCreate))
                            {
                                throw new Exception("Rollback du lieu");
                            }
                            if (IsNotNullOrEmpty(stentConludesToCreate) && !this.stentConcludeCreateProcessor.CreateList(stentConludesToCreate))
                            {
                                throw new Exception("tao moi List<HIS_STENT_CONCLUDE> that bai. Rollback du lieu");
                            }
                        }
                    }

                    result = true;
                    new EventLogGenerator(EventLog.Enum.HisServiceReq_ChiDinhVaSaoChepNoiDungXuLy, serviceReq.SERVICE_REQ_CODE, string.Join(", ", dicCreateSRSS.Keys.Select(o => o.SERVICE_REQ_CODE))).Run();
                }
            }
            catch (Exception ex)
            {
                this.Rollback();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void Rollback()
        {
            this.methodCreateProcessor.RollbackData();
            this.extCreateProcessor.RollbackData();
            this.ssCreateProcessor.RollbackData();
            this.reqCreateProcessor.RollbackData();
            this.stentConcludeCreateProcessor.RollbackData();
        }
    }
}
