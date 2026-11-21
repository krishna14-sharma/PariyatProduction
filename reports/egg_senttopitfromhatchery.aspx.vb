
Partial Class reports_egg_senttopitfromhatchery
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True
    Dim d As String

    Protected Sub eggsentotpit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles eggsentotpit.SelectedIndexChanged

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            sql = "select   convert(varchar, [egg_coll_date], 103) Coll_Date, [bird_type] Type, [flock_no] Flock, [Age], [comm_eggs] Comm , [crack_eggs] Crack, [leakge_eggs] Leaker,  [hatch_Comm] HComm, [hatch_crack] HCrack, [hatch_leaker] HLeaker,[cl_code] CL, [user_name] [User] from [dbo].[hatch_pit_egg_collection]  where isReceived = 0 order by  egg_coll_date desc"
            gmod.DataSetRet(sql, "tmp_daily_entry_show1")
            eggsentotpit.DataSource = gmod.ds.Tables("tmp_daily_entry_show1")
            eggsentotpit.DataBind()
        End If
    End Sub
End Class
