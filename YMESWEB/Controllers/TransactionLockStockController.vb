Imports System.Web.Mvc
Imports MES_WEB.My.Resources

Namespace Controllers
    Public Class TransactionLockStockController
        Inherits Controller

        ' GET: TransactionLockStock
        Function Index(ByVal ParamExceptionCode As String) As ActionResult

            If ParamExceptionCode = "99901" Then
                '倉庫棚卸中のため、登録できません。
                TempData("ErrExceptionMsg") = LangResources.MSG_Comm_Ex_99901_TransactionLocked
                '工程棚卸中のため、登録できません。
            ElseIf ParamExceptionCode = "99902" Then
                TempData("ErrExceptionMsg") = LangResources.MSG_Comm_Ex_99902_TransactionLocked
                '完成・不良倉庫棚卸中のため、登録できません。
            ElseIf ParamExceptionCode = "99903" Then
                TempData("ErrExceptionMsg") = LangResources.MSG_Comm_Ex_99903_TransactionLocked
                '棚卸中のため、登録できません。
            ElseIf ParamExceptionCode = "99904" Then
                TempData("ErrExceptionMsg") = LangResources.MSG_Comm_Ex_99904_TransactionLocked
            End If
            ViewData!ID = LangResources.MSG_Comm_Error
            Return View()
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="ex"></param>
        ''' <returns></returns>
        Function Check_Transaction_lock_Exception(ex As Exception) As ActionResult

            If ex.InnerException IsNot Nothing AndAlso
               ex.InnerException.InnerException IsNot Nothing AndAlso
               (DirectCast(ex.InnerException.InnerException, Npgsql.PostgresException).Code = "99901" OrElse
               DirectCast(ex.InnerException.InnerException, Npgsql.PostgresException).Code = "99902" OrElse
               DirectCast(ex.InnerException.InnerException, Npgsql.PostgresException).Code = "99903" OrElse
               DirectCast(ex.InnerException.InnerException, Npgsql.PostgresException).Code = "99904") Then
                Dim ExceptionCode As String = DirectCast(ex.InnerException.InnerException, Npgsql.PostgresException).Code.ToString
                Return RedirectToAction("Index", "TransactionLockStock", New RouteValueDictionary(New With {.ParamExceptionCode = ExceptionCode}))
            Else
                Return Nothing
            End If
        End Function

    End Class
End Namespace