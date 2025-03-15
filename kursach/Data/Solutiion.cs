using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kursach.Data
{
    public class Solution
    {
        public int SolutionId { get; set; }
        public string? Description { get; set; }
        public int IssueId { get; set; }
        public Issue? _Issue { get; set; }
    }
}
