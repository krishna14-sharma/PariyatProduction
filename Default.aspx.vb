
Partial Class _Default
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("user_name") Is Nothing Then

                Response.Redirect("~/loginforms/index.html")
            Else
                sql = "select location_name from location_master l inner join users u on l.location_code= u.AreaCode where UserName = '" & Convert.ToString(Session("user_name")) & "'"
                gmod.DataSetRet(sql, "location_name")
                Session("location_name") = gmod.ds.Tables("location_name").Rows(0)(0)

                lblString.Text = "Welcome " & Convert.ToString(Session("user_name")) & " you are in " & Session("location_name").ToString & " farm"
                'Response.Write("Welcome " & Convert.ToString(Session("user_name")))


            End If
        End If
    End Sub

    Public Sub fill()
        'sql = "select  egg_coll_date, flock_no,Age,hatch_crack,hatch_leaker,hatch_comm from [dbo].[hatch_pit_egg_collection] where isReceived =0 order by entry_date desc"
        'gmod.DataSetRet(sql, "hatpidata")
        'eggp.DataSource = gmod.ds.Tables("hatpidata")
    End Sub

End Class
