using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Moba.PresCabinet
{
    class ImpMestProcessor : BusinessBase
    {
        private HisImpMestCreate hisImpMestCreate;

        internal ImpMestProcessor()
            : base()
        {
            this.hisImpMestCreate = new HisImpMestCreate(param);
        }

        internal ImpMestProcessor(CommonParam param)
            : base(param)
        {
            this.hisImpMestCreate = new HisImpMestCreate(param);
        }

        internal bool Run(HisImpMestMobaPresCabinetSDO data, HIS_EXP_MEST expMest, Dictionary<long, MobaData> dicImpMest, WorkPlaceSDO workplace, ref List<HIS_IMP_MEST> resultData)
        {
            bool result = false;
            try
            {
                List<HIS_IMP_MEST> impMests = new List<HIS_IMP_MEST>();
                foreach (var dic in dicImpMest)
                {
                    HIS_IMP_MEST imp = new HIS_IMP_MEST();
                    imp.DESCRIPTION = data.Description;
                    imp.IMP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REQUEST;
                    imp.IMP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__DTTTL;
                    imp.MEDI_STOCK_ID = dic.Key;
                    imp.MOBA_EXP_MEST_ID = expMest.ID;
                    imp.SPECIAL_MEDICINE_TYPE = expMest.SPECIAL_MEDICINE_TYPE; //lay thong tin phan loai don thuoc dac biet dua vao phieu xuat
                    imp.TDL_MOBA_EXP_MEST_CODE = expMest.EXP_MEST_CODE;
                    imp.REQ_DEPARTMENT_ID = workplace.DepartmentId;
                    imp.REQ_ROOM_ID = workplace.RoomId;
                    imp.TRACKING_ID = data.TrackingId;

                    HisImpMestUtil.SetTdl(imp, expMest);
                    impMests.Add(imp);
                }
                if (!this.hisImpMestCreate.CreateList(impMests))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }
                resultData = impMests;
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }

        internal void Rollback()
        {
            try
            {
                this.hisImpMestCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
