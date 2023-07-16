using APIDemo.Common.CommonEnum;
using APIDemo.Common.JsonConverter;
using System.Text.Json.Serialization;

namespace APIDemo.Model
{
    public class CreateDataModel
    {
        public int IntProperty { get; set; }

        public long LongProperty { get; set; }

        public float FloatProperty { get; set; }

        public double DoubleProperty { get; set; }

        public bool BoolProperty { get; set; }

        public string StringProperty { get; set; }

        public List<string> ListStringProperty { get; set; }

        [JsonConverter(typeof(DateOnlyConverter))]
        public DateOnly DateOnlyProperty { get; set; }

        public DateTime DateTimeProperty { get; set; }

        public TimeSpan TimeSpanProperty { get; set; }

        public Guid GuidProperty { get; set; }

        public DateOnly? NullableDateOnlyProperty { get; set; }

        public DateTime? NullableDateTimeProperty { get; set; }

        public TimeSpan? NullableTimeSpanProperty { get; set; }

        public Guid? NullableGuidProperty { get; set; }

        public IDictionary<string, SortTypeEnum> DictionarySortOrder { get; set; }

        public TestComplexModel ComplexModelProperty { get; set; }
    }
}
