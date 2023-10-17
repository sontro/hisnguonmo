using Inventec.Common.Repository;
using SDA.EFMODEL.DataModels;
using System;
using System.Reflection;

namespace SDA.DTO.TransferObject
{
    public class VSarReportTemplateDTO : SDA_V_SAR_REPORT_TEMPLATE
    {
        public VSarReportTemplateDTO()
        {
        }

        public VSarReportTemplateDTO CreateDTO(SDA_V_SAR_REPORT_TEMPLATE raw)
        {
            try
            {
                if (raw != null)
                {
                    VSarReportTemplateDTO dto = new VSarReportTemplateDTO();
                    PropertyInfo[] pi = Properties.Get<SDA_V_SAR_REPORT_TEMPLATE>();
                    foreach (var item in pi)
                    {
                        item.SetValue(dto, (item.GetValue(raw)));
                    }
                    return dto;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
