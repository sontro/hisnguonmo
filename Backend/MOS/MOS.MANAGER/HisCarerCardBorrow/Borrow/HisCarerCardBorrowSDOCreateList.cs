using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisCarerCard;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisSereServ.Update.Package;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisCarerCardBorrow
{
    class HisCarerCardBorrowSDOCreateList : BusinessBase
    {
        private List<HisCarerCardBorrowSDOProcessor> processors = new List<HisCarerCardBorrowSDOProcessor>();

        internal HisCarerCardBorrowSDOCreateList()
            : base()
        {
        }

        internal HisCarerCardBorrowSDOCreateList(CommonParam param)
            : base(param)
        {
        }

        internal bool Run(HisCarerCardBorrowSDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_TREATMENT treatment = null;
                WorkPlaceSDO workPlace = null;
                List<HIS_CARER_CARD> carerCards = new List<HIS_CARER_CARD>();

                HisTreatmentCheck treatChecker = new HisTreatmentCheck(param);
                HisCarerCardCheck cardChecker = new HisCarerCardCheck(param);
                HisCarerCardBorrowSDOCheck checker = new HisCarerCardBorrowSDOCheck(param);

                valid = valid && checker.VerifyRequireField(data);
                valid = valid && treatChecker.VerifyId(data.TreatmentId, ref treatment);
                valid = valid && treatChecker.IsUnLock(treatment);
                valid = valid && checker.IsValidCarerCardInfo(data.CarerCardInfos, carerCards);
                valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlace);
                if (valid)
                {
                    if (IsNotNullOrEmpty(data.CarerCardInfos))
                    {
                        for (int i = 0; i < data.CarerCardInfos.Count; i++)
                        {
                            HisCarerCardSDOInfo s = data.CarerCardInfos[i];

                            HisCarerCardBorrowSDOProcessor processor = new HisCarerCardBorrowSDOProcessor(param);
                            this.processors.Add(processor);

                            if (!processor.Run(data, s, treatment, workPlace, carerCards))
                            {
                                throw new Exception("Rollback");
                            }
                        }
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
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
                if (IsNotNullOrEmpty(this.processors))
                {
                    foreach (HisCarerCardBorrowSDOProcessor p in this.processors)
                    {
                        p.RollbackData();
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }
        }
    }
}
