using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryConfirmation.Common.Enums
{
    public enum IncidentalInformationEnum
    {
        None = 0,

        [Description("Customer was home")]
        CustomerWasHome = 11,

        [Description("Customer was not home")]
        CustomerWasNotHome = 12,

        [Description("Package left by door")]
        PackageLeftByDoor = 21,

        [Description("Relative took package")]
        RelativeTookPackage = 31,

        [Description("Custom")]
        Custom = 999,
    }
}
