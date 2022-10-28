using System;
using System.Collections.Generic;
using System.Text;

namespace App5.Models
{
    public class Objective
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }
        public string Priority { get; set; }
        public DateTime DateToFinish { get; set; }
        public List<Objective> LinksToOtherObjectives { get; set; }
    }
}
