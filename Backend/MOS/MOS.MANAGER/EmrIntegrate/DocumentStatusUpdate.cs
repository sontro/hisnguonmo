using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MOS.MANAGER.EmrIntegrate
{
    class DocumentStatusChange : BusinessBase
    {
        private string TrackingKeyWord = "HIS_TRACKING:";
        private string InfusionKeyWord = "HIS_INFUSION:";

        internal DocumentStatusChange()
            : base()
        {
        }

        internal DocumentStatusChange(CommonParam param)
            : base(param)
        {
        }

        internal bool Run(EmrDocumentChangeStatusSDO data)
        {
            bool result = false;
            try
            {
                long? documentStatusId = null;
                DocumentStatusCheck checker = new DocumentStatusCheck(param);
                bool valid = checker.IsValidStatus(data, ref documentStatusId);
                if (valid)
                {
                    //Tam thoi chi xu ly voi to dieu tri
                    List<string> sqls = new List<string>();
                    List<string> trackingIds = this.GetIds(data.HisCode, TrackingKeyWord);
                    List<string> infusionIds = this.GetIds(data.HisCode, InfusionKeyWord);

                    if (IsNotNullOrEmpty(trackingIds))
                    {
                        string idStr = string.Join(",", trackingIds);
                        string sql = string.Format("UPDATE HIS_TRACKING T SET T.EMR_DOCUMENT_STT_ID = :param1, T.EMR_DOCUMENT_CODE = :param2, T.EMR_DOCUMENT_URL = :param3 WHERE ID IN ({0})", idStr);
                        sqls.Add(sql);
                    }

                    if (IsNotNullOrEmpty(infusionIds))
                    {
                        string idStr = string.Join(",", infusionIds);
                        string sql = string.Format("UPDATE HIS_INFUSION I SET I.EMR_DOCUMENT_STT_ID = :param1, I.EMR_DOCUMENT_CODE = :param2, I.EMR_DOCUMENT_URL = :param3 WHERE ID IN ({0})", idStr);
                        sqls.Add(sql);
                    }

                    if (IsNotNullOrEmpty(sqls))
                    {
                        if (DAOWorker.SqlDAO.Execute(sqls, documentStatusId.Value, data.DocumentCode, data.LastVersionUrl))
                        {
                            result = true;
                        }
                        else
                        {
                            LogSystem.Error(LogUtil.TraceData("EmrIntegrate sqls update his_tracking or his_infusion", sqls));
                        }
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
		
        private List<string> GetIds(string hisCode, string keyWord)
        {
            try
            {
                if (hisCode != null && hisCode.Contains(keyWord))
                {
                    int begin = hisCode.IndexOf(keyWord);
                    string substr = hisCode.Substring(begin, hisCode.Length - begin);
                    if (!string.IsNullOrWhiteSpace(substr))
                    {
                        int lastIndex = substr.IndexOf(" ") < 0 ? substr.Length : substr.IndexOf(" ");
                        string idStr = substr.Substring(0, lastIndex).Replace(keyWord, "");
                        List<string> ids = idStr.Split(',').Where(o => !string.IsNullOrWhiteSpace(o) && new Regex("^[0-9]+$").IsMatch(o)).ToList();
                        return ids;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return null;
        }
    }
}
