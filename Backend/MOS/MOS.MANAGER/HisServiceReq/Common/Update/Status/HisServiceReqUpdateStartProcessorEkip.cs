using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisEkip;
using MOS.MANAGER.HisEkipUser;
using MOS.MANAGER.HisSereServ;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Common.Update.Status
{
    partial class HisServiceReqUpdateStartProcessorEkip : BusinessBase
    {
        private HisEkipUserCreate hisEkipUserCreate;
        private HisEkipCreate hisEkipCreate;
        private HisSereServUpdate hisSereServUpdate;

        //Anh xa giua sere_serv va d/s ekip tuong ung
        private Dictionary<HIS_SERE_SERV, HIS_EKIP> SS_EKIP_MAPPING = new Dictionary<HIS_SERE_SERV, HIS_EKIP>();

        internal HisServiceReqUpdateStartProcessorEkip()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqUpdateStartProcessorEkip(CommonParam param)
            : base(param)
        {
            this.Init();
        }
        private void Init()
        {
            this.hisEkipUserCreate = new HisEkipUserCreate(param);
            this.hisEkipCreate = new HisEkipCreate(param);
            this.hisSereServUpdate = new HisSereServUpdate(param);
        }

       internal bool Run(HIS_SERVICE_REQ serviceReq)
        {
            bool result = true;
            try
            {
                bool valid = true;
                HisServiceReqStatusCheck statusChecker = new HisServiceReqStatusCheck(param);
                valid = valid && statusChecker.IsValidSrTypeCodeAndExeServiceModuleId(serviceReq);
                if (valid)
                {
                    string loginnameToken = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    HIS_EMPLOYEE employee = HisEmployeeCFG.DATA.FirstOrDefault(o => o.LOGINNAME == loginnameToken);
                    List<HIS_EXECUTE_ROLE_USER> exeRoleUsers = HisExecuteRoleUserCFG.DATA.Where(o => o.LOGINNAME == loginnameToken).ToList();
                    HIS_EXECUTE_ROLE role = IsNotNullOrEmpty(exeRoleUsers) ? HisExecuteRoleCFG.DATA.Where(o => exeRoleUsers.Select(p => p.EXECUTE_ROLE_ID).Contains(o.ID) && o.IS_SUBCLINICAL == Constant.IS_TRUE).OrderByDescending(a => a.ID).FirstOrDefault() : null;

                    if (role != null)
                    {
                        List<HIS_SERE_SERV> sereServs = new HisSereServGet().GetByServiceReqId(serviceReq.ID);
                        
                        if (IsNotNullOrEmpty(sereServs))
                        {
                            List<long> ekipIds = sereServs.Select(o => o.EKIP_ID ?? 0).ToList();
                            List<HIS_EKIP_USER> listEkipUsers = IsNotNullOrEmpty(ekipIds) ? new HisEkipUserGet().GetByEkipIds(ekipIds) : null;
                            listEkipUsers = IsNotNullOrEmpty(listEkipUsers) ? listEkipUsers.Where(o => o.LOGINNAME == loginnameToken).ToList() : null;

                            List<HIS_SERE_SERV> sereServUpdates = new List<HIS_SERE_SERV>();
                            List<HIS_SERE_SERV> SereServbefores = new List<HIS_SERE_SERV>();
                            List<HIS_EKIP> lstekipCreate = new List<HIS_EKIP>();
                            List<HIS_EKIP_USER> lstekUserNews = new List<HIS_EKIP_USER>();
                            foreach (var ss in sereServs)
                            {
                                List<HIS_EKIP_USER> ekUserCreates = new List<HIS_EKIP_USER>();
                                List<HIS_EKIP_USER> ekUserNews = new List<HIS_EKIP_USER>();
                                if (ss.EKIP_ID == null)
                                {
                                    HIS_EKIP_USER ekUser = new HIS_EKIP_USER();
                                    ekUser.EXECUTE_ROLE_ID = role.ID;
                                    ekUser.LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                                    ekUser.USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                                    ekUser.DEPARTMENT_ID = employee.DEPARTMENT_ID;
                                    ekUserCreates.Add(ekUser);

                                    HIS_EKIP ekipCreate = new HIS_EKIP();
                                    ekipCreate.HIS_EKIP_USER = ekUserCreates;
                                    SS_EKIP_MAPPING.Add(ss, ekipCreate);
                                }
                                else
                                {
                                    List<HIS_EKIP_USER> EkipUserChecks = IsNotNullOrEmpty(listEkipUsers) ? listEkipUsers.Where(o => o.EKIP_ID == ss.EKIP_ID).ToList() : null;
                                    if (!IsNotNullOrEmpty(EkipUserChecks))
                                    {
                                        HIS_EKIP_USER ekUser = new HIS_EKIP_USER();
                                        ekUser.EXECUTE_ROLE_ID = role.ID;
                                        ekUser.LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                                        ekUser.USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                                        ekUser.DEPARTMENT_ID = employee.DEPARTMENT_ID;
                                        ekUser.EKIP_ID = ss.EKIP_ID ?? 0;
                                        ekUserNews.Add(ekUser);
                                        if (IsNotNullOrEmpty(ekUserNews))
                                        {
                                            lstekUserNews.AddRange(ekUserNews);
                                        }
                                    }
                                }
                            }
                            if (IsNotNullOrEmpty(lstekUserNews))
                            {
                                if (!this.hisEkipUserCreate.CreateList(lstekUserNews))
                                {
                                    throw new Exception("tao List<HIS_EKIP_USER> lstekUserNews that bai.Ket thuc nghiep vu");
                                }
                            }
                            this.ProcessNewEkip();
                            foreach (var ssUpdate in sereServs)
                            {
                                HIS_SERE_SERV ssbefore = Mapper.Map<HIS_SERE_SERV>(ssUpdate);
                                SereServbefores.Add(ssbefore);

                                HIS_EKIP ekip = IsNotNullOrEmpty(SS_EKIP_MAPPING) && SS_EKIP_MAPPING.ContainsKey(ssUpdate) ? SS_EKIP_MAPPING[ssUpdate] : null;
                                if (ekip != null)
                                {
                                    ssUpdate.EKIP_ID = ekip.ID;
                                    sereServUpdates.Add(ssUpdate);
                                }
                            }

                            if (IsNotNullOrEmpty(sereServUpdates) && IsNotNullOrEmpty(SereServbefores))
                            {
                                if (!this.hisSereServUpdate.UpdateList(sereServUpdates, SereServbefores, false))
                                {
                                    throw new Exception("Cap nhat sere_serv that bai");
                                }
                            }
                        }
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ProcessNewEkip()
        {
            if (IsNotNullOrEmpty(SS_EKIP_MAPPING))
            {
                if (!this.hisEkipCreate.CreateList(SS_EKIP_MAPPING.Values.ToList()))
                {
                    throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                }
            }
        }

        public void RollbackData()
        {
            this.hisEkipUserCreate.RollbackData();
            this.hisEkipCreate.RollbackData();
            this.hisSereServUpdate.RollbackData();
        }
    }
}
