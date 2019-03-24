using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exiao.model.entity
{
    public class T_Agent
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }
        
        public int AdminId { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
