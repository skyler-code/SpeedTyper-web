using System.ComponentModel.DataAnnotations;

namespace SpeedTyper.DataObjects
{
    public class TestData
    {
        public int TestID { get; set; }
        public string TestDataText { get; set; }
        [Display(Name="Source")]
        public string DataSource { get; set; }
    }
}
