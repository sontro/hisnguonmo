using Inventec.Common.Repository;
using SDA.EFMODEL.DataModels;
using System;
using System.Reflection;

namespace SDA.DTO.TransferObject
{
    public class VSdaMessageBroadcastDTO : SDA_V_SDA_MESSAGE_BROADCAST
    {
        public VSdaMessageBroadcastDTO()
        {
        }

        public VSdaMessageBroadcastDTO CreateDTO(SDA_V_SDA_MESSAGE_BROADCAST raw)
        {
            try
            {
                if (raw != null)
                {
                    VSdaMessageBroadcastDTO dto = new VSdaMessageBroadcastDTO();
                    PropertyInfo[] pi = Properties.Get<SDA_V_SDA_MESSAGE_BROADCAST>();
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
