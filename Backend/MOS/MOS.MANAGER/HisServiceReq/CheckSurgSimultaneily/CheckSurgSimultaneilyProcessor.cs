using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExecuteRole;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServExt;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisService;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.UTILITY;

namespace MOS.MANAGER.HisServiceReq.CheckSurgSimultaneily
{
    class CheckSurgSimultaneilyProcessor : BusinessBase
    {
        internal CheckSurgSimultaneilyProcessor()
            : base()
        {
            this.Init();
        }

        internal CheckSurgSimultaneilyProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
        }

        public bool Run(HisSurgServiceReqUpdateListSDO data)
        {
            bool result = false;

            try
            {
                bool valid = true;

                HIS_SERVICE_REQ serviceReq = null;
                HIS_TREATMENT treatment = null;
                List<HIS_SERE_SERV> sereServs = new List<HIS_SERE_SERV>();

                CheckSurgSimultaneilyCheck checker = new CheckSurgSimultaneilyCheck(param);
                HisSereServCheck ssChecker = new HisSereServCheck(param);
                HisServiceCheck svChecker = new HisServiceCheck(param);
                HisTreatmentCheck treatChecker = new HisTreatmentCheck(param);

                valid = valid && checker.VerifyRequireField(data);
                valid = valid && ssChecker.VerifyIds(data.SurgUpdateSDOs.Select(o => o.SereServId).Distinct().ToList(), sereServs);
                valid = valid && checker.IsValidServiceReq(sereServs, ref serviceReq);
                valid = valid && treatChecker.VerifyId(serviceReq.TREATMENT_ID, ref treatment);

                if (valid)
                {
                    foreach (SurgUpdateSDO s in data.SurgUpdateSDOs)
                    {
                        var ss = sereServs.FirstOrDefault(o => o.ID == s.SereServId);
                        result = this.CheckPatient(ss, treatment.ID, s.SereServExt.BEGIN_TIME, s.SereServExt.END_TIME, data.SurgUpdateSDOs.Select(o => o.SereServId).Distinct().ToList());
                        result = this.CheckDoctor(s.EkipUsers, ss, s.SereServExt.BEGIN_TIME, s.SereServExt.END_TIME, data.SurgUpdateSDOs.Select(o => o.SereServId).Distinct().ToList()) && result;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Rollback();
                LogSystem.Error(ex);
                result = false;
            }

            return result;
        }

        internal bool CheckPatient(HIS_SERE_SERV sereServ, long treatmentId, long? beginTime, long? endTime, List<long> mergeProcessIds)
        {
            bool result = true;

            try
            {
                List<object> listParam = new List<object>();
                List<long> types = new List<long>() { IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__PT, IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__TT };

                StringBuilder sbSql = new StringBuilder("SELECT * FROM HIS_SERE_SERV_EXT EXT ");
                sbSql.Append("WHERE EXISTS (SELECT 1 FROM HIS_SERE_SERV SS WHERE SS.ID = EXT.SERE_SERV_ID ");
                sbSql.AppendFormat("AND NVL(SS.IS_DELETE,0) <> {0} ", Constant.IS_TRUE);
                sbSql.Append("AND SS.TDL_TREATMENT_ID = :param1 ");
                listParam.Add(treatmentId);
                sbSql.AppendFormat("AND SS.TDL_SERVICE_TYPE_ID IN ({0}) ", string.Join(", ", types));
                sbSql.AppendFormat("AND NVL(SS.IS_NO_EXECUTE,0) <> {0} ", Constant.IS_TRUE);
                sbSql.Append("AND SS.ID <> :param2 ");
                listParam.Add(sereServ.ID);
                sbSql.Append("AND EXISTS(SELECT 1 FROM HIS_SERVICE SV WHERE SV.ID = SS.SERVICE_ID");
                sbSql.AppendFormat(" AND NVL(SV.ALLOW_SIMULTANEITY,0) <> {0})", Constant.IS_TRUE);
                sbSql.Append(") ");
                sbSql.Append("AND ((:param3 BETWEEN EXT.BEGIN_TIME AND EXT.END_TIME)");
                listParam.Add(beginTime ?? 0);
                sbSql.Append(" OR (:param4 BETWEEN EXT.BEGIN_TIME AND EXT.END_TIME))");
                listParam.Add(endTime ?? 0);
                LogSystem.Debug("CheckPatient: " + sbSql.ToString());
                List<HIS_SERE_SERV_EXT> ssExts = DAOWorker.SqlDAO.GetSql<HIS_SERE_SERV_EXT>(sbSql.ToString(), listParam.ToArray());
                if (IsNotNullOrEmpty(ssExts))
                {
                    foreach (var ext in ssExts)
                    {
                        List<string> serviceNames = new List<string>();
                        List<string> reqCodes = new List<string>();
                        List<HIS_SERE_SERV> ss = new HisSereServGet().GetByIds(ssExts.Select(o => o.SERE_SERV_ID).ToList());
                        List<HIS_SERVICE_REQ> reqs = new HisServiceReqGet().GetByIds(ss.Select(o => o.SERVICE_REQ_ID.Value).ToList());
                        foreach (var s in ss)
                        {
                            serviceNames.Add(s.TDL_SERVICE_NAME);
                            var req = reqs.FirstOrDefault(o => o.ID == s.SERVICE_REQ_ID.Value);
                            if (req != null)
                                reqCodes.Add(req.SERVICE_REQ_CODE);
                        }
                        string reqInfo = string.Format("{0} ({1}: {2})", string.Join(",", serviceNames.Distinct()), MessageUtil.GetMessage(LibraryMessage.Message.Enum.Common_MaYLenh, param.LanguageCode), string.Join(",", reqCodes.Distinct()));
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_BenhNhanCoThucHienDVTrongKhoangTGGiaoVoiKhoangTGThucHienDVKhac, reqInfo, sereServ.TDL_SERVICE_NAME);
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = false;
            }

            return result;
        }

        internal bool CheckDoctor(List<HIS_EKIP_USER> ekipUsers, HIS_SERE_SERV sereServ, long? beginTime, long? endTime, List<long> mergeProcessIds)
        {
            bool result = true;

            try
            {
                if (IsNotNullOrEmpty(ekipUsers))
                {
                    List<long> types = new List<long>() { IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__PT, IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__TT };
                    List<HIS_EXECUTE_ROLE> exeRoles = HisExecuteRoleCFG.DATA.Where(o => !o.ALLOW_SIMULTANEITY.HasValue).ToList();
                    List<long> roleIds = IsNotNullOrEmpty(exeRoles) ? exeRoles.Select(o => o.ID).ToList() : new List<long>();
                    List<HIS_EKIP_USER> neededCheckUsers = IsNotNullOrEmpty(roleIds) ? ekipUsers.Where(o => roleIds.Contains(o.EXECUTE_ROLE_ID)).ToList() : null;
                    if (IsNotNullOrEmpty(neededCheckUsers))
                    {
                        foreach (var user in neededCheckUsers)
                        {
                            List<object> listParam = new List<object>();
                            StringBuilder sbSql = new StringBuilder("SELECT * FROM HIS_SERE_SERV_EXT EXT ");
                            sbSql.Append("WHERE EXISTS (SELECT 1 FROM HIS_SERE_SERV SS WHERE SS.ID = EXT.SERE_SERV_ID ");
                            sbSql.AppendFormat("AND NVL(SS.IS_DELETE,0) <> {0} ", Constant.IS_TRUE);
                            sbSql.AppendFormat("AND SS.TDL_SERVICE_TYPE_ID IN ({0}) ", string.Join(", ", types));
                            sbSql.AppendFormat("AND NVL(SS.IS_NO_EXECUTE,0) <> {0} ", Constant.IS_TRUE);
                            if (IsNotNullOrEmpty(mergeProcessIds))
                            {
                                sbSql.Append("AND SS.ID NOT IN (:param1) ");
                                listParam.Add(string.Join(", ", mergeProcessIds));
                            }
                            else
                            {
                                sbSql.Append("AND SS.ID <> :param1 ");
                                listParam.Add(sereServ.ID);
                            }
                            sbSql.Append("AND EXISTS(SELECT 1 FROM HIS_SERVICE SV WHERE SV.ID = SS.SERVICE_ID");
                            sbSql.AppendFormat(" AND NVL(SV.ALLOW_SIMULTANEITY,0) <> {0}) ", Constant.IS_TRUE);
                            sbSql.Append("AND EXISTS(SELECT 1 FROM HIS_EKIP_USER EU WHERE EU.EKIP_ID = SS.EKIP_ID AND EU.LOGINNAME = :param2) ");
                            listParam.Add(user.LOGINNAME);
                            sbSql.Append(") ");
                            sbSql.Append("AND ((:param3 BETWEEN EXT.BEGIN_TIME AND EXT.END_TIME)");
                            listParam.Add(beginTime ?? 0);
                            sbSql.Append(" OR (:param4 BETWEEN EXT.BEGIN_TIME AND EXT.END_TIME))");
                            listParam.Add(endTime ?? 0);

                            LogSystem.Debug("CheckDoctor: " + sbSql.ToString());
                            List<HIS_SERE_SERV_EXT> ssExts = DAOWorker.SqlDAO.GetSql<HIS_SERE_SERV_EXT>(sbSql.ToString(), listParam.ToArray());

                            if (IsNotNullOrEmpty(ssExts))
                            {
                                foreach (var checkedUser in neededCheckUsers)
                                {
                                    List<string> serviceNames = new List<string>();
                                    List<string> reqCodes = new List<string>();
                                    List<HIS_SERE_SERV> ss = new HisSereServGet().GetByIds(ssExts.Select(o => o.SERE_SERV_ID).ToList());
                                    List<HIS_SERVICE_REQ> reqs = new HisServiceReqGet().GetByIds(ss.Select(o => o.SERVICE_REQ_ID.Value).ToList());
                                    foreach (var s in ss)
                                    {
                                        serviceNames.Add(s.TDL_SERVICE_NAME);
                                        var req = reqs.FirstOrDefault(o => o.ID == s.SERVICE_REQ_ID.Value);
                                        if (req != null)
                                            reqCodes.Add(req.SERVICE_REQ_CODE);
                                    }
                                    string reqInfo = string.Format("{0} ({1}: {2})", string.Join(",", serviceNames.Distinct()), MessageUtil.GetMessage(LibraryMessage.Message.Enum.Common_MaYLenh, param.LanguageCode), string.Join(",", reqCodes.Distinct()));
                                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_TKCoThucHienDVTrongKhoangTGGiaoVoiKhoangTGThucHienDVKhac, checkedUser.LOGINNAME, reqInfo, sereServ.TDL_SERVICE_NAME);
                                }

                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = false;
            }

            return result;
        }

        private void Rollback()
        {
        }
    }
}
