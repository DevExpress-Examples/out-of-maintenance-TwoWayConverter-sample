using DevExpress.Mvvm;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Editors;
using System;

namespace TwoWayConverterSample {
    public class ViewModel : ViewModelBase {
        public decimal Age {
            get { return GetValue<decimal>(); }
            set { SetValue(value); }
        }
        public DateTime BirthDay {
            get { return GetValue<DateTime>(); }
            set { SetValue(value); }
        }
        public string UserName {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public void AgeValidation(ValidationArgs args) {
            var age = (decimal)args.Value;
            var isValid = age >= 21;
            args.SetError(isValid, "Sorry, you're too young!");
        }
        public void BirthDayValidation(ValidationArgs args) {
            var birthDay = (DateTime)args.Value;
            var isValid = birthDay.AddYears(21) <= DateTime.Today;
            args.SetError(isValid, "Sorry, you're too young!");
        }
        public void UserNameValidation(ValidationArgs args) {
            var name = (string)args.Value;
            var isValid = name != "UserName";
            args.SetError(isValid, "A user with this name is already registered!");
        }

        public ViewModel() {
            UserName = "UserName";
            Age = 20;
            BirthDay = DateTime.Today.AddYears(-21).AddDays(1);
        }
    }

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
