namespace Timelogger.Shared.Models
{
    public class ValidationError
    {
        public ValidationError() { }
        public ValidationError(string propertyName, string reason)
        {
            PropertyName = propertyName != string.Empty ? propertyName : null;
            Reason = reason;
        }
        public ValidationError(string reason)
        {
            PropertyName = null;
            Reason = reason;
        }

        public string PropertyName { get; set; }
        public string Reason { get; set; }

        public override string ToString() => Reason;
    }
}
