using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest;
using MOS.MANAGER.HisImpMest;
using MOS.MANAGER.HisMediStock;
using MOS.MANAGER.HisServiceReq.Exam;
using MOS.SDO;
using MOS.UTILITY;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MOS.MANAGER.HisServiceReq
{
    partial class HisServiceReqCheck : BusinessBase
    {
        internal bool IsNotAprovedSurgeryRemuneration(HIS_SERVICE_REQ serviceReq)
        {
            try
            {
                //Chi check voi cac loai cdha, ns, sieu am, pt, tt, gpbl để đảm bảo hiệu năng
                //(Thuc ra ban chat la cac dv được khai báo dv BHYT là PTTT, nhưng do đặc thù, viện sẽ khai báo loại dv là ns, sa, ..., nhưng loại dv BHYT là PTTT)
                if (serviceReq != null &&
                    (serviceReq.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__GPBL
                    || serviceReq.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__CDHA
                    || serviceReq.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__TDCN
                    || serviceReq.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__NS
                    || serviceReq.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__SA
                    || serviceReq.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__PT
                    || serviceReq.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__TT))
                {
                    string sql = "SELECT COUNT(1) FROM HIS_SERE_SERV_EXT EXT WHERE EXT.TDL_SERVICE_REQ_ID = :param1 AND (EXT.IS_FEE = 1 OR EXT.IS_GATHER_DATA = 1)";
                    long count = DAOWorker.SqlDAO.GetSqlSingle<long>(sql, serviceReq.ID);
                    if (count > 0)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_SoLieuCongThucHienPtttDaDuocChot, serviceReq.SERVICE_REQ_CODE);
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return false;
        }

        internal bool IsNotAprovedSurgeryRemuneration(HIS_SERE_SERV sereServ)
        {
            try
            {
                //Chi check voi cac loai cdha, ns, sieu am, pt, tt, gpbl để đảm bảo hiệu năng
                //(Thuc ra ban chat la cac dv được khai báo dv BHYT là PTTT, nhưng do đặc thù, viện sẽ khai báo loại dv là ns, sa, ..., nhưng loại dv BHYT là PTTT)
                if (sereServ != null && 
                    (sereServ.TDL_HEIN_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__PTTT
                    || sereServ.TDL_HEIN_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__TT))
                {
                    string sql = "SELECT COUNT(1) FROM HIS_SERE_SERV_EXT EXT WHERE EXT.SERE_SERV_ID = :param1 AND (EXT.IS_FEE = 1 OR EXT.IS_GATHER_DATA = 1)";
                    long count = DAOWorker.SqlDAO.GetSqlSingle<long>(sql, sereServ.ID);
                    if (count > 0)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_SoLieuCongThucHienPtttDaDuocChot, sereServ.TDL_SERVICE_NAME);
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return false;
        }

        internal bool IsAllowedForStart(HIS_SERVICE_REQ data)
        {
            try
            {
                //Da bat dau roi ko cho thuc hien bat dau tiep
                if (data.SERVICE_REQ_STT_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_ChiChoPhepThucHienKhiChuaBatDau);
                    return false;
                }

                if (data.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH && !new HisServiceReqExamCheck(param).IsAllowedUser())
                {
                    return false;
                }

                if (HisServiceReqCFG.DO_NOT_ALLOW_TO_PROCESS_EXECUTE_ASSIGNED_SERVICE_REQ_BY_ANOTHER && !string.IsNullOrWhiteSpace(data.ASSIGNED_EXECUTE_LOGINNAME))
                {
                    string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    if (loginName != data.ASSIGNED_EXECUTE_LOGINNAME)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_KhongPhaiTaiKhoanDuocChiDinhXuLyYeuCau, data.SERVICE_REQ_CODE, data.ASSIGNED_EXECUTE_LOGINNAME, data.ASSIGNED_EXECUTE_USERNAME);
                        return false;
                    }
                }

                string storedSql = "PKG_START_SERVICE_REQ.PRO_START";

                OracleParameter idPar = new OracleParameter("P_ID", OracleDbType.Int32, data.ID, ParameterDirection.Input);
                OracleParameter dontCheckBhytPar = new OracleParameter("P_DONT_CHECK_BHYT", OracleDbType.Int32, (int)HisServiceReqCFG.NOT_REQUIRE_FEE_FOR_BHYT, ParameterDirection.Input);
                OracleParameter bhytIdPar = new OracleParameter("P_BHYT_ID", OracleDbType.Int32, HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT, ParameterDirection.Input);
                OracleParameter resultPar = new OracleParameter("P_RESULT", OracleDbType.Int32, ParameterDirection.Output);

                object resultHolder = null;

                if (DAOWorker.SqlDAO.ExecuteStored(OutputHandler, ref resultHolder, storedSql, resultPar, idPar, dontCheckBhytPar, bhytIdPar))
                {
                    if (resultHolder != null)
                    {
                        if (!(bool)resultHolder)
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_KhongChoPhepBatDauKhiThieuVienPhi);
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }
            return false;
        }

        //Xu ly ket qua tra ve khi goi procedure
        private void OutputHandler(ref object resultHolder, params OracleParameter[] parameters)
        {
            try
            {
                //Tham so thu 1 chua output
                if (parameters[0] != null && parameters[0].Value != null)
                {
                    short isTrue = short.Parse(parameters[0].Value.ToString());
                    resultHolder = isTrue == Constant.IS_TRUE;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}
