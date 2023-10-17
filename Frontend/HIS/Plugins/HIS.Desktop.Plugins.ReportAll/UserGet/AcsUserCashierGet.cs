using ACS.EFMODEL.DataModels;
using ACS.Filter;
using HIS.Desktop.ApiConsumer;
using HIS.Desktop.LocalStorage.BackendData;
using Inventec.Common.Adapter;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HIS.Desktop.Plugins.ReportAll
{
    public static class AcsUserCashierGet
    {
        const int MAX_REQUEST_LENGTH_PARAM = 20;
        public static void Get()
        {
            try
            {
                CommonParam paramCommon = new CommonParam();
                HisCashierRoomFilter HisCashierRoomfilter = new HisCashierRoomFilter();
                HisCashierRoomfilter.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE;
                var cashierRoom = new BackendAdapter(paramCommon).Get<List<HIS_CASHIER_ROOM>>(
                    HisRequestUriStore.HIS_CASHIER_ROOM_GET, ApiConsumer.ApiConsumers.MosConsumer, HisCashierRoomfilter, paramCommon);
               
                HisUserRoomViewFilter UserRoomfilter = new HisUserRoomViewFilter();
                UserRoomfilter.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE;
                UserRoomfilter.ROOM_IDs = cashierRoom.Select(o => o.ROOM_ID).ToList();
                var userRoom =  new BackendAdapter(paramCommon).Get<List<V_HIS_USER_ROOM>>(
                    HisRequestUriStore.HIS_USER_ROOM_GETVIEW, ApiConsumer.ApiConsumers.MosConsumer, UserRoomfilter, paramCommon);
                // var BranchId = HIS.Desktop.LocalStorage.BackendData.BranchDataWorker.GetCurrentBranchId();
                //if (BranchId != null)
                //{
                //    userRoom = userRoom.Where(o => o.BRANCH_ID == BranchId).ToList();
                //}
                paramCommon = new CommonParam();
                
                var skip = 0;
                var loginnames = userRoom.Select(o => o.LOGINNAME).Distinct().ToList();
                //Inventec.Common.Logging.LogSystem.Info("userRoom:" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => userRoom), userRoom));
                List<ACS_USER> acsUserCashier = new List<ACS_USER>();
                while (loginnames.Count - skip > 0)
                {
                    var lists = loginnames.Skip(skip).Take(MAX_REQUEST_LENGTH_PARAM).ToList();
                    skip = skip + MAX_REQUEST_LENGTH_PARAM;
                    AcsUserFilter AcsUserfilter = new AcsUserFilter();
                    AcsUserfilter.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE;
                   
                    AcsUserfilter.LOGINNAMEs = lists;
                    acsUserCashier.AddRange(new BackendAdapter(paramCommon).Get<List<ACS.EFMODEL.DataModels.ACS_USER>>(AcsRequestUriStore.ACS_USER_GET, ApiConsumers.AcsConsumer, AcsUserfilter, paramCommon));
                }
                HIS.UC.FormType.Config.AcsFormTypeConfig.HisAcsUserCashier = acsUserCashier;
            }
            catch (Exception ex)
            {
                
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        
        }
    }
}
