using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MOS.MANAGER.EventLogUtil;
using MOS.LibraryEventLog;
using MOS.PubSub;

namespace MOS.MANAGER.HisAlert
{
    partial class HisAlertCreate : BusinessBase
    {
		private List<HIS_ALERT> recentHisAlerts = new List<HIS_ALERT>();
        private HIS_ALERT recentHisAlertDTO = new HIS_ALERT();
		
        internal HisAlertCreate()
            : base()
        {

        }

        internal HisAlertCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }
        /// <summary>
        /// tạo thông tin báo động
        /// </summary>
        /// <param name="sdo"></param>
        /// <param name="resultData"></param>
        /// <returns></returns>
        internal bool Create(HisAlertSDO sdo, ref HIS_ALERT resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                WorkPlaceSDO workPlace = null;
                
                HisAlertCheck checker = new HisAlertCheck(param);
                valid = valid && checker.VerifyRequireField1(sdo);
                valid = valid && this.HasWorkPlaceInfo(sdo.RequestRoomId, ref workPlace);
                valid = valid && checker.IsAllow(sdo, workPlace);
                if (valid)
                {
                    V_HIS_ROOM room = HisRoomCFG.DATA.FirstOrDefault(o => o.ID == sdo.RequestRoomId);
                    HIS_ALERT alert = new HIS_ALERT();
                    alert.ROOM_ID = sdo.RequestRoomId;
                    alert.DEPARTMENT_ID = room.DEPARTMENT_ID;
                    alert.TITLE = sdo.Title;
                    alert.CONTENT = sdo.Content;
                    alert.RECEIVE_DEPARTMENT_IDS = string.Join(",", sdo.ReceiveDepartmentIds);
                    alert.RECEIVER_LOGINNAME = null;
                    alert.RECEIVER_USERNAME = null;
                    alert.RECEIVE_TIME = null;

                    if (!DAOWorker.HisAlertDAO.Create(alert))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAlert_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisAlert that bai." + LogUtil.TraceData("data", alert));
                    }
                    this.recentHisAlertDTO = alert;

                    resultData = alert;

                    this.recentHisAlerts.Add(resultData);

                    PubSubProcessor.SendMessage(resultData);
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
		
		internal bool CreateList(List<HIS_ALERT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAlertCheck checker = new HisAlertCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisAlertDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAlert_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisAlert that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisAlerts.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisAlerts))
            {
                if (!DAOWorker.HisAlertDAO.TruncateList(this.recentHisAlerts))
                {
                    LogSystem.Warn("Rollback du lieu HisAlert that bai, can kiem tra lai." + LogUtil.TraceData("recentHisAlerts", this.recentHisAlerts));
                }
				this.recentHisAlerts = null;
            }
        }
    }
}
