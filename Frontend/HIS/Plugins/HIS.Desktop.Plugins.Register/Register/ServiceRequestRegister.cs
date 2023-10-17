using HIS.Desktop.Common;
using Inventec.Core;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace HIS.Desktop.Plugins.Register.Register
{
    public class ServiceRequestRegister : BusinessBase, IAppDelegacyT
    {
        object entity;
        object patient;
        internal ServiceRequestRegister(CommonParam param, object data, object patient)
            : base(param)
        {
            this.entity = data;
            this.patient = patient;
        }

        T IAppDelegacyT.Execute<T>()
        {
            T result = default(T);
            try
            {
                if (typeof(T) == typeof(HisServiceReqExamRegisterResultSDO))
                {
                    IServiceRequestRegisterExam behavior = ServiceRequestRegisterExamBehaviorFactory.MakeIServiceRequestRegister(param, entity, patient);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(HisPatientProfileSDO))
                {
                    IServiceRequestRegisterPatientProfile behavior = ServiceRequestRegisterPatientProfileBehaviorFactory.MakeIServiceRequestRegister(param, entity, patient);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = default(T);
            }
            return result;
        }
    }
}
