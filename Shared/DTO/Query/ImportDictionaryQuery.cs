using Shared.Enums;
using Shared.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shared.DTO.Query
{
    public class ImportDictionaryQuery
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 10;
        [EnumDataType(typeof(ImportTypeEnum))]
        public ImportTypeEnum ImportType { get; set; }
    }
}
