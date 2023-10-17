using Inventec.Common.Logging;
using Inventec.Core;
using MOS.MANAGER.Base;
using MOS.MANAGER.SdaSql;
using MOS.MANAGER.SdaSqlParam;
using MOS.SDO;
using MOS.UTILITY;
using SDA.EFMODEL.DataModels;
using SDA.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.SqlExecute
{
    public class SqlExecuteCheck: BusinessBase
    {		
        internal SqlExecuteCheck()
            : base()
        {

        }

        internal SqlExecuteCheck(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool IsValid(ExecuteSqlSDO sdo, ref SDA_SQL sdaSql, ref List<SDA_SQL_PARAM> sdaSqlParams)
        {
            bool valid = true;
            try
            {
                SdaSqlFilter filter = new SdaSqlFilter();
                filter.ID = sdo.SqlId;
                filter.IS_ACTIVE = Constant.IS_TRUE;
                List<SDA_SQL> sqls = new SdaSqlGet().Get(filter);
                if (!IsNotNullOrEmpty(sqls))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.SdaSql_SqlKhongTonTaiHoacDaBiKhoa);
                    return false;
                }

                sdaSql = sqls.FirstOrDefault();

                if (sdaSql.SCHEMA_CODE != "HIS_RS")
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("script sql nay khong cho phep thuc hien tren HIS_RS");
                    return false;
                }

                SdaSqlParamFilter paramFilter = new SdaSqlParamFilter();
                paramFilter.SQL_ID = sdo.SqlId;
                paramFilter.IS_ACTIVE = Constant.IS_TRUE;
                sdaSqlParams = new SdaSqlParamGet().Get(paramFilter);
                
                List<long> sqlParamIds = sdaSqlParams != null ? sdaSqlParams.Select(o => o.ID).ToList() : null;

                if ((IsNotNullOrEmpty(sdo.SqlParams) && (sqlParamIds == null || sdo.SqlParams.Exists(t => !sqlParamIds.Contains(t.SqlParamId))))
                    || (!IsNotNullOrEmpty(sdo.SqlParams) && IsNotNullOrEmpty(sqlParamIds)))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("sql_param_id ko khop voi khai bao tren DB");
                    return false;
                }


            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }
    }
}
