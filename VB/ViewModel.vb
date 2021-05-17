Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.Editors
Imports System

Namespace TwoWayConverterSample
	Public Class ViewModel
		Inherits ViewModelBase

		Public Property Age() As Decimal
			Get
				Return GetValue(Of Decimal)()
			End Get
			Set(ByVal value As Decimal)
				SetValue(value)
			End Set
		End Property
		Public Property BirthDay() As DateTime
			Get
				Return GetValue(Of DateTime)()
			End Get
			Set(ByVal value As DateTime)
				SetValue(value)
			End Set
		End Property
		Public Property UserName() As String
			Get
				Return GetValue(Of String)()
			End Get
			Set(ByVal value As String)
				SetValue(value)
			End Set
		End Property

		Public Sub AgeValidation(ByVal args As ValidationArgs)
'INSTANT VB NOTE: The variable age was renamed since Visual Basic does not handle local variables named the same as class members well:
			Dim age_Conflict = DirectCast(args.Value, Decimal)
			Dim isValid = age_Conflict >= 21
			args.SetError(isValid, "Sorry, you're too young!")
		End Sub
		Public Sub BirthDayValidation(ByVal args As ValidationArgs)
'INSTANT VB NOTE: The variable birthDay was renamed since Visual Basic does not handle local variables named the same as class members well:
			Dim birthDay_Conflict = DirectCast(args.Value, DateTime)
			Dim isValid = birthDay_Conflict.AddYears(21) <= DateTime.Today
			args.SetError(isValid, "Sorry, you're too young!")
		End Sub
		Public Sub UserNameValidation(ByVal args As ValidationArgs)
			Dim name = DirectCast(args.Value, String)
			Dim isValid = name <> "UserName"
			args.SetError(isValid, "A user with this name is already registered!")
		End Sub

		Public Sub New()
			UserName = "UserName"
			Age = 20
			BirthDay = DateTime.Today.AddYears(-21).AddDays(1)
		End Sub
	End Class

	Public Class ValidationArgs
		Private privateErrorContent As String
		Public Property ErrorContent() As String
			Get
				Return privateErrorContent
			End Get
			Private Set(ByVal value As String)
				privateErrorContent = value
			End Set
		End Property
		Public ReadOnly Property Value() As Object

		Public Sub New(ByVal value As Object)
			Me.Value = value
		End Sub
		Public Sub SetError(ByVal isValid As Boolean, ByVal errorContent As String)
			Me.ErrorContent = If(isValid, Nothing, errorContent)
		End Sub
	End Class

	Public Class ValidateEventArgsConverter
		Inherits EventArgsConverterBase(Of ValidationEventArgs)

		Protected Overrides Function Convert(ByVal sender As Object, ByVal e As ValidationEventArgs) As Object
			Return New ValidationArgs(e.Value)
		End Function
		Protected Overrides Sub ConvertBack(ByVal sender As Object, ByVal e As ValidationEventArgs, ByVal parameter As Object)
			Dim args = TryCast(parameter, ValidationArgs)
			e.IsValid = args.ErrorContent Is Nothing
			e.ErrorContent = args.ErrorContent
		End Sub
	End Class
End Namespace
