using System.ComponentModel;

namespace APIDemo.Common.CommonEnum
{
    public enum SortTypeEnum : int
    {
        [Description("None")]
        None = 0,

        [Description("asc")]
        ASC = 1,

        [Description("desc")]
        DESC = 2
    }
}
