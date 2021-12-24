Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.Editors

Namespace TwoWayConverterSample

    Public Class ValidationArgs

        Private _ErrorContent As String

        Public Property ErrorContent As String
            Get
                Return _ErrorContent
            End Get

            Private Set(ByVal value As String)
                _ErrorContent = value
            End Set
        End Property

        Public ReadOnly Property Value As Object

        Public Sub New(ByVal value As Object)
            CSharpImpl.__Assign(Me.Value, value)
        End Sub

        Public Sub SetError(ByVal isValid As Boolean, ByVal errorContent As String)
            CSharpImpl.__Assign(Me.ErrorContent, If(isValid, Nothing, errorContent))
        End Sub

        Private Class CSharpImpl

            <System.Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
            Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
                target = value
                Return value
            End Function
        End Class
    End Class

    Public Class ValidateEventArgsConverter
        Inherits EventArgsConverterBase(Of ValidationEventArgs)

        Protected Overrides Function Convert(ByVal sender As Object, ByVal e As ValidationEventArgs) As Object
            Return New ValidationArgs(e.Value)
        End Function

        Protected Overrides Sub ConvertBack(ByVal sender As Object, ByVal e As ValidationEventArgs, ByVal parameter As Object)
            Dim args = TryCast(parameter, ValidationArgs)
            e.IsValid = Equals(args.ErrorContent, Nothing)
            e.ErrorContent = args.ErrorContent
        End Sub
    End Class
End Namespace
