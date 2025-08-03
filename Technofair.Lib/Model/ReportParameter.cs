namespace Technofair.Lib.Model
{
    public class ReportParameter
    {
        private string name;
        private string value;
        private bool isInteger;
        private bool isDateTime;
        private bool isBoolean;

        public ReportParameter()
        {
            isInteger = false;
            isDateTime = false;
            isBoolean = false;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public bool IsInteger
        {
            get { return isInteger; }
            set { isInteger = value; }
        }

        public bool IsDateTime
        {
            get { return isDateTime; }
            set { isDateTime = value; }
        }

        public bool IsBoolean
        {
            get { return isBoolean; }
            set { isBoolean = value; }
        }
    }
}