using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using System;

namespace TwoWayConverterSample {
    public class MainViewModel : ViewModelBase {
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

        public MainViewModel() {
            UserName = "UserName";
            Age = 20;
            BirthDay = DateTime.Today.AddYears(-21).AddDays(1);
        }

        [Command]
        public void AgeValidation(ValidationArgs args) {
            var age = (decimal)args.Value;
            var isValid = age >= 21;
            args.SetError(isValid, "Sorry, you're too young!");
        }
        [Command]
        public void BirthDayValidation(ValidationArgs args) {
            var birthDay = (DateTime)args.Value;
            var isValid = birthDay.AddYears(21) <= DateTime.Today;
            args.SetError(isValid, "Sorry, you're too young!");
        }
        [Command]
        public void UserNameValidation(ValidationArgs args) {
            var name = (string)args.Value;
            var isValid = name != "UserName";
            args.SetError(isValid, "A user with this name is already registered!");
        }
    }
}
