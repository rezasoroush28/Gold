using Nop.Core;

using System;

namespace Tesla.Plugin.Widgets.B2CGold.Domain
{
    public class GoldCurrentPrice : BaseEntity
    {
        public DateTime ModifyDate { get; set; }
        //the time when a the goldCurrentPrice is added
        public decimal Price { get; set; }
    }
}
