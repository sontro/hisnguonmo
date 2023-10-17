using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisService;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceRati
{
    class HisServiceRatiCreateSDO : BusinessBase
    {
        private HisServiceRatiCreate hisServiceRatiCreate;
        private HisServiceRatiTruncate hisServiceRatiTruncate;

        internal HisServiceRatiCreateSDO()
            : base()
        {
            this.Init();
        }

        internal HisServiceRatiCreateSDO(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisServiceRatiCreate = new HisServiceRatiCreate(param);
            this.hisServiceRatiTruncate = new HisServiceRatiTruncate(param);
        }

        internal bool Run(HisServiceRatiSDO data, ref List<HIS_SERVICE_RATI> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_SERVICE raw = null;
                List<HIS_SERVICE_RATI> olds = null;
                HisServiceCheck serviceChecker = new HisServiceCheck(param);
                HisServiceRatiCheck checker = new HisServiceRatiCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && serviceChecker.VerifyId(data.ServiceId, ref raw);
                valid = valid && serviceChecker.IsUnLock(raw);
                valid = valid && checker.CheckServiceType(raw);
                valid = valid && checker.CheckDuplicate(data);
                if (valid)
                {
                    olds = new HisServiceRatiGet().GetByServiceId(raw.ID);
                    List<HIS_SERVICE_RATI> listCreate = new List<HIS_SERVICE_RATI>();
                    List<HIS_SERVICE_RATI> listUpdate = new List<HIS_SERVICE_RATI>();
                    List<HIS_SERVICE_RATI> listDelete = new List<HIS_SERVICE_RATI>();
                    foreach (long ratiId in data.RationTimeIds)
                    {
                        HIS_SERVICE_RATI sera = olds != null ? olds.FirstOrDefault(o => o.RATION_TIME_ID == ratiId) : null;
                        if (sera != null)
                        {
                            listUpdate.Add(sera);
                        }
                        else
                        {
                            sera = new HIS_SERVICE_RATI();
                            sera.SERVICE_ID = raw.ID;
                            sera.RATION_TIME_ID = ratiId;
                            listCreate.Add(sera);
                        }
                    }

                    listDelete = olds != null ? olds.Where(o => !listUpdate.Exists(e => e.ID == o.ID)).ToList() : null;

                    if (IsNotNullOrEmpty(listCreate))
                    {
                        if (!this.hisServiceRatiCreate.CreateList(listCreate))
                        {
                            throw new Exception("hisServiceRatiCreate. Ket thuc nghiep vu");
                        }
                    }

                    if (IsNotNullOrEmpty(listDelete))
                    {
                        if (!this.hisServiceRatiTruncate.TruncateList(listDelete))
                        {
                            throw new Exception("hisServiceRatiTruncate. Ket thuc nghiep vu. Rollback du lieu");
                        }
                    }

                    List<HIS_SERVICE_RATI> listData = new List<HIS_SERVICE_RATI>();
                    listData.AddRange(listUpdate);
                    listData.AddRange(listCreate);
                    resultData = listData;
                    result = true;
                }

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                this.RollbackData();
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void RollbackData()
        {
            try
            {
                this.hisServiceRatiCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
