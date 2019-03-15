using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exiao.model.dto
{
    public class Dto_AjaxReturnData<T>
    {
        public AjaxResultCodeEnum code { get; set; }

        public string message { get; set; }

        public T data { get; set; }
    }

    public enum AjaxResultCodeEnum
    {
        Success,
        Error
    }
}
