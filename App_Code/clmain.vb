Imports System.Data
Imports System.Web
Imports System.Web.Configuration
Imports Microsoft.VisualBasic

Public Class clmain
    Public Shared ConString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
End Class
