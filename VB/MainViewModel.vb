Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations

Namespace TwoWayConverterSample

    Public Class MainViewModel
        Inherits ViewModelBase

        Public Property Age As Decimal
            Get
                Return GetValue(Of Decimal)()
            End Get

            Set(ByVal value As Decimal)
                SetValue(value)
            End Set
        End Property

        Public Property BirthDay As Date
            Get
                Return GetValue(Of Date)()
            End Get

            Set(ByVal value As Date)
                SetValue(value)
            End Set
        End Property

        Public Property UserName As String
            Get
                Return GetValue(Of String)()
            End Get

            Set(ByVal value As String)
                SetValue(value)
            End Set
        End Property

        Public Sub New()
            UserName = "UserName"
            Age = 20
            BirthDay = Date.Today.AddYears(-21).AddDays(1)
        End Sub

        <Command>
        Public Sub AgeValidation(ByVal args As ValidationArgs)
            Dim age = CDec(args.Value)
            Dim isValid = age >= 21
            args.SetError(isValid, "Sorry, you're too young!")
        End Sub

        <Command>
        Public Sub BirthDayValidation(ByVal args As ValidationArgs)
            Dim birthDay = CDate(args.Value)
            Dim isValid = birthDay.AddYears(21) <= Date.Today
            args.SetError(isValid, "Sorry, you're too young!")
        End Sub

        <Command>
        Public Sub UserNameValidation(ByVal args As ValidationArgs)
            Dim name = CStr(args.Value)
            Dim isValid = Not Equals(name, "UserName")
            args.SetError(isValid, "A user with this name is already registered!")
        End Sub
    End Class
End Namespace
