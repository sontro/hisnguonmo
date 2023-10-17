using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRegisterReq;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisRegisterGate
{
    class HisRegisterGateUpdateNumOrder : BusinessBase
    {
        internal HisRegisterGateUpdateNumOrder()
            : base()
        {

        }

        internal HisRegisterGateUpdateNumOrder(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(List<HisRegisterGateSDO> data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                List<HIS_REGISTER_GATE> listGate = new List<HIS_REGISTER_GATE>();
                valid = valid && this.VerifyRequireField(data);
                valid = valid && new HisRegisterGateCheck(param).VerifyIds(data.Select(s => s.Id).ToList(), listGate);
                if (valid)
                {
                    List<HIS_REGISTER_REQ> registerReqs = new List<HIS_REGISTER_REQ>();
                    foreach (HisRegisterGateSDO sdo in data)
                    {
                        HIS_REGISTER_REQ req = new HIS_REGISTER_REQ();
                        req.REGISTER_GATE_ID = sdo.Id;
                        req.NUM_ORDER = sdo.UpdateNumOrder.Value;
                        req.IS_UPDATE_FROM = Constant.IS_TRUE;
                        registerReqs.Add(req);
                    }

                    if (!new HisRegisterReqCreate(param).CreateList(registerReqs))
                    {
                        throw new Exception("HisRegisterReqCreate. Ket thuc nghiep vu");
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }

        private bool VerifyRequireField(List<HisRegisterGateSDO> datas)
        {
            bool valid = true;
            try
            {
                if (!IsNotNullOrEmpty(datas)) throw new ArgumentNullException("datas");
                foreach (HisRegisterGateSDO data in datas)
                {
                    valid = valid && this.VerifyRequireField(data);
                }
            }
            catch (ArgumentNullException ex)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                LogSystem.Warn(ex);
                valid = false;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        private bool VerifyRequireField(HisRegisterGateSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.Id <= 0) throw new ArgumentNullException("data.Id");
                if (!data.UpdateNumOrder.HasValue || data.UpdateNumOrder.Value < 0) throw new ArgumentNullException("data.UpdateNumOrder");
            }
            catch (ArgumentNullException ex)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                LogSystem.Warn(ex);
                valid = false;
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
