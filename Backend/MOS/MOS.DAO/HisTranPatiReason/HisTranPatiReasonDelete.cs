using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTranPatiReason
{
    partial class HisTranPatiReasonDelete : EntityBase
    {
        public HisTranPatiReasonDelete()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TRAN_PATI_REASON>();
        }

        private BridgeDAO<HIS_TRAN_PATI_REASON> bridgeDAO;

        public bool Delete(HIS_TRAN_PATI_REASON data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Delete(data.ID);
        }

        public bool DeleteList(List<HIS_TRAN_PATI_REASON> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(listData);
                if (valid)
                {
                    List<HIS_TRAN_PATI_REASON> listIdNotFound = new List<HIS_TRAN_PATI_REASON>();
                    List<long> listId = new List<long>();
                    foreach (var data in listData)
                    {
                        if (data.ID > 0)
                        {
                            listId.Add(data.ID);
                        }
                        else
                        {
                            listIdNotFound.Add(data);
                        }
                    }
                    if (listIdNotFound.Count > 0)
                    {
                        Logging("Danh sach du lieu can delete co ton tai id <= 0." + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => listIdNotFound), listIdNotFound), LogType.Warn);
                    }
                    else
                    {
                        using (var ctx = new AppContext())
                        {
                            List<HIS_TRAN_PATI_REASON> listRaw = ctx.HIS_TRAN_PATI_REASON.Where(o => listId.Contains(o.ID)).ToList();
                            if (listRaw != null && listRaw.Count == listData.Count)
                            {
                                result = bridgeDAO.DeleteListRaw(listRaw);
                            }
                            else
                            {
                                Logging("Danh sach du lieu truy van duoc de delete null hoac so luong khong bang danh sach dau vao (listRaw.Count <> listData.Count)." + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => listData), listData) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => listRaw), listRaw), LogType.Warn);
                            }
                        }
                    }
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                Logging(LogUtil.TraceDbException(ex), LogType.Error);
                Logging(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => listData), listData), LogType.Error);
                LogSystem.Error(ex);
                result = false;
            }
            catch (Exception ex)
            {
                Logging(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => listData), listData), LogType.Error);
                LogSystem.Error(ex);
                result = false;
            }
            
            return result;
        }
    }
}
