﻿using HIS.Desktop.Common;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisExamSchedule.HisExamSchedule
{
    class HisExamScheduleBehavior : BusinessBase, IHisExamSchedule
    { 
        object[] entity;
        internal HisExamScheduleBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IHisExamSchedule.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;

                if (entity.GetType() == typeof(object[]))
                {
                    if (entity != null && entity.Count() > 0)
                    {
                        for (int i = 0; i < entity.Count(); i++)
                        {
                            if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                            {
                                moduleData = (Inventec.Desktop.Common.Modules.Module)entity[i];
                            }
                        }
                    }
                }

                return new frmHisExamSchedule(moduleData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}