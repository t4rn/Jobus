using System;

namespace Jobus.Core.Dto.Results
{
    public class DataResultDto<T> : ResultDto
    {
        public T Data { get; set; }
        public TimeSpan? ProcessTime { get; set; }
    }
}
