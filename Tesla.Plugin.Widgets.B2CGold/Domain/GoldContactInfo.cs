using Nop.Core;
using System;

namespace Tesla.Plugin.Widgets.B2CGold.Domain
{
    public class GoldContactInfo : BaseEntity
    {
        public string Value { get; set; }

        public int GoldContactInfoTypeId { get; set; }

        public GoldContactInfoType GoldContactInfoType
        {
            get
            {
                return (GoldContactInfoType)GoldContactInfoTypeId;
            }
            set
            {
                GoldContactInfoTypeId = (int)value;
            }
        }

        public DayOfWeek StartDayofWeekEnum
        {
            get
            {
                return (DayOfWeek)StartDayofWeek;
            }
            set
            {
                StartDayofWeek = (int)value;
            }
        }

        public DayOfWeek EndDayofWeekEnum
        {
            get
            {
                return (DayOfWeek)EndDayofWeek;
            }
            set
            {
                EndDayofWeek = (int)value;
            }
        }

        public bool AllDaysOfWeek { get; set; }

        public int StartDayofWeek { get; set; }

        public int EndDayofWeek { get; set; }

        public int FromHour { get; set; }

        public int ToHour { get; set; }

        public bool Enabled { get; set; }
    }
}
