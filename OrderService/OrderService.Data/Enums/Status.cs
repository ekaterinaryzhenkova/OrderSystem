using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Text.Json.Serialization;

namespace OrderService.Application.Enums
{
    [JsonConverter(typeof(StringEnumConverter<,,>))]
    public enum Status
    {
        Created,
        Confirmed,
        Rejected
    }
}