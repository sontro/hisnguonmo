using Inventec.Common.Logging;
using Inventec.Core;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisAccountBook.Authority
{
    class HisAccountBookAuthorityProcessor : BusinessBase
    {
        internal HisAccountBookAuthorityProcessor()
            : base()
        {

        }

        internal HisAccountBookAuthorityProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        private static Dictionary<string, AuthorityAccountBookSDO> AUTHORITY_ACCOUNT_BOOK = new Dictionary<string, AuthorityAccountBookSDO>();

        private static object LOCK = new object();

        public bool Request(AuthorityAccountBookSDO data, ref AuthorityAccountBookSDO resultData)
        {
            bool result = false;
            try
            {
                //Neu la MOS "slave" thi goi den MOS "master"
                if (SystemCFG.IS_SLAVE && !string.IsNullOrWhiteSpace(SystemCFG.MASTER_ADDRESS))
                {
                    return HisAccountBookAuthorityMasterService.Request(data, ref resultData, param);
                }

                string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                HisAccountBookAuthorityCheck checker = new HisAccountBookAuthorityCheck(param);

                bool valid = true;
                valid = valid && checker.IsAllowedRequestRoom(data.RequestRoomId);
                valid = valid && !string.IsNullOrWhiteSpace(data.CashierLoginName);
                valid = valid && checker.IsCashier(data.CashierLoginName);

                if (valid)
                {
                    lock (LOCK)
                    {
                        if (AUTHORITY_ACCOUNT_BOOK.ContainsKey(loginName))
                        {
                            resultData = AUTHORITY_ACCOUNT_BOOK[loginName];
                        }
                        else
                        {
                            data.RequestTime = Inventec.Common.DateTime.Get.Now().Value;
                            data.RequestLoginName = loginName;
                            data.RequestUserName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                            AUTHORITY_ACCOUNT_BOOK[loginName] = data;
                            resultData = data;
                        }
                    }
                    
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
        
        public bool Approve(ApprovalAccountBookSDO data)
        {
            bool result = false;
            try
            {
                //Neu la MOS "slave" thi goi den MOS "master"
                if (SystemCFG.IS_SLAVE && !string.IsNullOrWhiteSpace(SystemCFG.MASTER_ADDRESS))
                {
                    return HisAccountBookAuthorityMasterService.Approve(data, param);
                }

                string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                WorkPlaceSDO workPlace = null;
                HisAccountBookAuthorityCheck checker = new HisAccountBookAuthorityCheck(param);
                AuthorityAccountBookSDO request = null;
                bool valid = true;
                valid = valid && this.HasWorkPlaceInfo(data.WorkingRoomId, ref workPlace);
                valid = valid && checker.IsCashierRoom(workPlace);
                valid = valid && checker.IsAllowedAccountBook(data.AccountBookId, workPlace.CashierRoomId.Value);
                valid = valid && checker.HasRequest(data.RequestLoginName, loginName, AUTHORITY_ACCOUNT_BOOK, ref request);
                if (valid)
                {
                    lock (LOCK)
                    {
                        request.AccountBookId = data.AccountBookId;
                        request.CashierWorkingRoomId = workPlace.RoomId;
                        request.CashierUserName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                        request.ApprovalTime = Inventec.Common.DateTime.Get.Now().Value;
                    }
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

        public bool Unapprove(UnapprovalAccountBookSDO data)
        {
            bool result = false;
            try
            {
                //Neu la MOS "slave" thi goi den MOS "master"
                if (SystemCFG.IS_SLAVE && !string.IsNullOrWhiteSpace(SystemCFG.MASTER_ADDRESS))
                {
                    return HisAccountBookAuthorityMasterService.Unapprove(data, param);
                }

                string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                WorkPlaceSDO workPlace = null;
                HisAccountBookAuthorityCheck checker = new HisAccountBookAuthorityCheck(param);

                bool valid = true;
                valid = valid && this.HasWorkPlaceInfo(data.WorkingRoomId, ref workPlace);
                valid = valid && checker.IsCashierRoom(workPlace);
                if (valid)
                {
                    if (AUTHORITY_ACCOUNT_BOOK != null && AUTHORITY_ACCOUNT_BOOK.Values != null)
                    {
                        //Neu ko truyen len request_loginname ==> duoc hieu la unapprove toan bo
                        if (!string.IsNullOrWhiteSpace(data.RequestLoginName))
                        {
                            lock (LOCK)
                            {
                                AuthorityAccountBookSDO sdo = AUTHORITY_ACCOUNT_BOOK[data.RequestLoginName];
                                sdo.AccountBookId = null;
                                sdo.ApprovalTime = null;
                            }
                        }
                        else
                        {
                            List<AuthorityAccountBookSDO> authorities = this.RequestToMe(data.WorkingRoomId);
                            if (IsNotNullOrEmpty(authorities))
                            {
                                lock (LOCK)
                                {
                                    authorities.ForEach(o =>
                                    {
                                        o.ApprovalTime = null;
                                        o.AccountBookId = null;
                                    });
                                }
                            }
                        }
                    }
                    
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

        public bool Reject(RejectAccountBookSDO data)
        {
            bool result = false;
            try
            {
                //Neu la MOS "slave" thi goi den MOS "master"
                if (SystemCFG.IS_SLAVE && !string.IsNullOrWhiteSpace(SystemCFG.MASTER_ADDRESS))
                {
                    return HisAccountBookAuthorityMasterService.Reject(data, param);
                }
                
                string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                WorkPlaceSDO workPlace = null;
                HisAccountBookAuthorityCheck checker = new HisAccountBookAuthorityCheck(param);
                AuthorityAccountBookSDO request = null;

                
                bool valid = true;
                valid = valid && this.HasWorkPlaceInfo(data.WorkingRoomId, ref workPlace);
                valid = valid && checker.IsCashierRoom(workPlace);
                valid = valid && checker.HasRequest(data.RequestLoginName, loginName, AUTHORITY_ACCOUNT_BOOK, ref request);
                if (valid)
                {
                    lock (LOCK)
                    {
                        AUTHORITY_ACCOUNT_BOOK.Remove(data.RequestLoginName);
                    }
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

        public bool Cancel()
        {
            bool result = false;
            try
            {
                //Neu la MOS "slave" thi goi den MOS "master"
                if (SystemCFG.IS_SLAVE && !string.IsNullOrWhiteSpace(SystemCFG.MASTER_ADDRESS))
                {
                    return HisAccountBookAuthorityMasterService.Cancel(param);
                }

                string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                if (AUTHORITY_ACCOUNT_BOOK != null && AUTHORITY_ACCOUNT_BOOK.ContainsKey(loginName))
                {
                    lock (LOCK)
                    {
                        AUTHORITY_ACCOUNT_BOOK.Remove(loginName);
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

        public bool IsAuthorized(string cashierLoginname, string cashierUserName, long cashierWorkingRoomId, long accountBookId, long workingRoomId)
        {
            try
            {
                AuthorityAccountBookSDO myRequest = this.MyRequest(workingRoomId);
                if (myRequest == null 
                    || myRequest.AccountBookId != accountBookId
                    || myRequest.CashierLoginName != cashierLoginname
                    || myRequest.CashierUserName != cashierUserName
                    || myRequest.CashierWorkingRoomId != cashierWorkingRoomId
                    )
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisAccountBook_BanKhongCoQuyenSuDungSoHoaDonBienLai);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        public AuthorityAccountBookSDO MyRequest(long workingRoomId)
        {   
            AuthorityAccountBookSDO result = null;
            try
            {
                //Neu la MOS "slave" thi goi den MOS "master"
                if (SystemCFG.IS_SLAVE && !string.IsNullOrWhiteSpace(SystemCFG.MASTER_ADDRESS))
                {
                    return HisAccountBookAuthorityMasterService.MyRequest(workingRoomId, param);
                }

                string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                WorkPlaceSDO workPlace = null;

                bool valid = true;
                valid = valid && this.HasWorkPlaceInfo(workingRoomId, ref workPlace);
                if (valid)
                {
                    if (AUTHORITY_ACCOUNT_BOOK.ContainsKey(loginName))
                    {
                        result = AUTHORITY_ACCOUNT_BOOK[loginName];
                    }
                    else
                    {
                        result = null;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }

        public List<AuthorityAccountBookSDO> RequestToMe(long workingRoomId)
        {
            
            List<AuthorityAccountBookSDO> result = null;
            try
            {
                //Neu la MOS "slave" thi goi den MOS "master"
                if (SystemCFG.IS_SLAVE && !string.IsNullOrWhiteSpace(SystemCFG.MASTER_ADDRESS))
                {
                    return HisAccountBookAuthorityMasterService.RequestToMe(workingRoomId, param);
                }

                string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                WorkPlaceSDO workPlace = null;
                HisAccountBookAuthorityCheck checker = new HisAccountBookAuthorityCheck(param);

                bool valid = true;
                valid = valid && this.HasWorkPlaceInfo(workingRoomId, ref workPlace);
                valid = valid && checker.IsCashierRoom(workPlace);

                if (valid)
                {
                    if (AUTHORITY_ACCOUNT_BOOK != null && AUTHORITY_ACCOUNT_BOOK.Values != null)
                    {
                        result = AUTHORITY_ACCOUNT_BOOK.Values.Where(o => o.CashierLoginName == loginName).ToList();
                    }
                    else
                    {
                        result = null;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }
    }
}
