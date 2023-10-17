using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisContactPoint.SetContactLevel;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisContactPoint.Save
{
    partial class HisContactPointSave : BusinessBase
    {
        private HisContactPointCreate hisContactPointCreate;
        private HisContactPointUpdate hisContactPointUpdate;

        internal HisContactPointSave()
            : base()
        {
            this.Init();
        }

        internal HisContactPointSave(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisContactPointCreate = new HisContactPointCreate(param);
            this.hisContactPointUpdate = new HisContactPointUpdate(param);
        }

        internal bool Run(HIS_CONTACT_POINT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisContactPointSaveCheck checker = new HisContactPointSaveCheck(param);
                valid = valid && checker.IsValidData(data);
                valid = valid && checker.IsNotExists(data);

                if (valid)
                {
                    //Neu truong hop them moi
                    if (data.ID <= 0)
                    {
                        result = this.hisContactPointCreate.Create(data);
                    }
                    else if (data.ID > 0)
                    {
                        result = this.hisContactPointUpdate.Update(data);
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
    }
}
