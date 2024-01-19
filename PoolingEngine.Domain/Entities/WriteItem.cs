using PoolingEngine.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolingEngine.Domain.Entities
{
    public class WriteItem
    {
        public required int TagItemId { get; set; }
        public required object Value { get; set; }
        public EnumDataType DataType { get; set; }
        //public required int Req { get; set; }

    }
}
