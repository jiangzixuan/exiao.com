using exiao.model.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exiao.model.dto
{
    public class Dto_User:T_User
    {
        public List<T_Agent> Agents { get; set; }
    }
}
