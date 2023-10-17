using MOS.EFMODEL.DataModels;
using System.Collections.Generic;
using System.Reflection;

namespace MOS.EFMODEL.Decorator
{
    public partial class DummyDecorator
    {
        private static void LoadHisVaccinationExam()
        {
            List<PropertyInfo> pies = new List<PropertyInfo>();
            pies.Add(typeof(HIS_VACCINATION_EXAM).GetProperty("VACCINATION_EXAM_CODE"));

            properties[typeof(HIS_VACCINATION_EXAM)] = pies;
        }
    }
}
