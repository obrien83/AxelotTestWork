using System;
using System.Collections.Generic;
using System.Text;

namespace AxelotTestWork.Application.Models
{
    public class TableSettings
    {
        public bool DeleteAfter { get; set; }
        public string TableName { get; set; }
        public List<string> Columns { get; set; }

        public TableSettings()
        {
            Columns = new List<string>();
        }
    }
}
