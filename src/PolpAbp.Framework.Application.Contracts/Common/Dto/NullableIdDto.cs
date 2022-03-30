using System;
namespace PolpAbp.Framework.Common.Dto
{
    public class NullableIdDto<T> where T : struct
    {
        public T? Id { get; set; }
    }
}
