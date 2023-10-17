using Inventec.Common.Repository;
using SDA.EFMODEL.DataModels;
using System;
using System.Reflection;

namespace SDA.DTO.TransferObject
{
    public class VSdaGroupDTO : SDA_V_SDA_GROUP
    {
        public VSdaGroupDTO()
        {
        }

        public VSdaGroupDTO CreateDTO(SDA_V_SDA_GROUP raw)
        {
            try
            {
                if (raw != null)
                {
                    VSdaGroupDTO dto = new VSdaGroupDTO();
                    PropertyInfo[] pi = Properties.Get<SDA_V_SDA_GROUP>();
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
