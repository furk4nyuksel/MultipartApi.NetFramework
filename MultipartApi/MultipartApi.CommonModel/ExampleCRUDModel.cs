using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MultipartApi.CommonModel
{
    public class ExampleCRUDModel
    {
        public ByteArrayContent FileByteArray { get; set; }

        public string FilePath { get; set; }

        public string ExampleParameter { get; set; }
    }
}
