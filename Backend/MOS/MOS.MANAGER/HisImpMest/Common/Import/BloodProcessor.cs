using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisBlood;
using MOS.MANAGER.HisImpMestBlood;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Import
{
    class BloodProcessor : BusinessBase
    {
        private HisBloodUpdate hisBloodUpdate;

        internal BloodProcessor(CommonParam param)
            : base(param)
        {
            this.hisBloodUpdate = new HisBloodUpdate(param);
        }

        internal bool Run(HIS_IMP_MEST impMest)
        {
            bool result = false;
            try
            {
                //Lay thong tin chi tiet phieu nhap tuong ung voi phieu nhap
                List<V_HIS_IMP_MEST_BLOOD> hisImpMestBloods = new HisImpMestBloodGet(param).GetViewByImpMestId(impMest.ID);
                if (IsNotNullOrEmpty(hisImpMestBloods))
                {
                    List<long> bloodIds = hisImpMestBloods.Select(o => o.BLOOD_ID).ToList();
                    List<HIS_BLOOD> hisBloods = new HisBloodGet().GetByIds(bloodIds);

                    foreach (HIS_BLOOD blood in hisBloods)
                    {
                        //neu nhap tu nha cung cap thi bo sung xu ly imp_time cho blood
                        if (HisImpMestContanst.TYPE_MUST_CREATE_PACKAGE_IDS.Contains(impMest.IMP_MEST_TYPE_ID))
                        {
                            blood.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE;
                            blood.IMP_TIME = impMest.IMP_TIME;
                            blood.IS_PREGNANT = null;
                        }
                        blood.MEDI_STOCK_ID = impMest.MEDI_STOCK_ID;
                    }

                    if (!this.hisBloodUpdate.UpdateList(hisBloods))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal void Rollback()
        {
            try
            {
                this.hisBloodUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
