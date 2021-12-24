using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Editors;

namespace TwoWayConverterSample {
    public class ValidationArgs {
        public string ErrorContent { get; private set; }
        public object Value { get; }

        public ValidationArgs(object value) => Value = value;
        public void SetError(bool isValid, string errorContent) => ErrorContent = isValid ? null : errorContent;
    }

    public class ValidateEventArgsConverter : EventArgsConverterBase<ValidationEventArgs> {
        protected override object Convert(object sender, ValidationEventArgs e) => new ValidationArgs(e.Value);
        protected override void ConvertBack(object sender, ValidationEventArgs e, object parameter) {
            var args = parameter as ValidationArgs;
            e.IsValid = args.ErrorContent == null;
            e.ErrorContent = args.ErrorContent;
        }
    }
}
