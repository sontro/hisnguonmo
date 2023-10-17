using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.EventLogUtil
{
    class HisConfigLog
    {
        private static string FORMAT_EDIT = "{0}:{1}=>{2}";

        internal static void Run(HIS_CONFIG editData, HIS_CONFIG oldData, EventLog.Enum logEnum)
        {
            try
            {
                List<string> editFields = new List<string>();
                string co = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Co);
                string khong = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Khong);

                if (IsDiffLong(oldData.BRANCH_ID, editData.BRANCH_ID))
                {
                    HIS_BRANCH old = HisBranchCFG.DATA.FirstOrDefault(o => o.ID == oldData.BRANCH_ID);
                    HIS_BRANCH edit = HisBranchCFG.DATA.FirstOrDefault(o => o.ID == editData.BRANCH_ID);
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ChiNhanh);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, old != null ? old.BRANCH_NAME : "", edit != null ? edit.BRANCH_NAME : ""));
                }
                if (IsDiffString(oldData.CONFIG_CODE, editData.CONFIG_CODE))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Ma);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.CONFIG_CODE, editData.CONFIG_CODE));
                }
                if (IsDiffString(oldData.CONFIG_GROUP_CODES, editData.CONFIG_GROUP_CODES))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Nhom);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.CONFIG_GROUP_CODES, editData.CONFIG_GROUP_CODES));
                }
                if (IsDiffString(oldData.DEFAULT_VALUE, editData.DEFAULT_VALUE))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.GiaTriMacDinh);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.DEFAULT_VALUE, editData.DEFAULT_VALUE));
                }
                if (IsDiffString(oldData.DESCRIPTION, editData.DESCRIPTION))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.MoTa);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.DESCRIPTION, editData.DESCRIPTION));
                }
                if (IsDiffString(oldData.VALUE, editData.VALUE))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.GiaTri);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.VALUE, editData.VALUE));
                }

                new EventLogGenerator(logEnum, String.Join(". ", editFields))
                    .Key(oldData.KEY)
                    .Run();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private static bool IsDiffString(string oldValue, string newValue)
        {
            return (oldValue ?? "") != (newValue ?? "");
        }
        private static bool IsDiffLong(long? oldValue, long? newValue)
        {
            return (oldValue ?? -1) != (newValue ?? -1);
        }
    }
}
