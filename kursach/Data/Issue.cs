using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kursach.Data
{
    public class Issue
    {
        public int IssueId { get; set; }
        public string Description { get; set; }

        public Solution _solutions { get; set; }
    }
}
