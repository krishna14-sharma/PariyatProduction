
Partial Class transforms_ShedEntry
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            fillLocation()
            fillBirdtype()
            fillShedmaster()
            fillFlockNo()
            fillGrid()
        Else

        End If
    End Sub
    Public Sub fillBirdtype()
        sql = "select [bird_type_code], [bird_type_name] from [dbo].[bird_type_master]"
        gmod.DataSetRet(sql, "shed_loc")
        birdtype.DataSource = gmod.ds.Tables("shed_loc")
        birdtype.DataTextField = "bird_type_code"
        birdtype.DataValueField = "bird_type_code"
        birdtype.DataBind()
    End Sub
    Public Sub fillShedmaster()
       
    End Sub

    Public Sub fillFlockNo()
        'sql = "select f.flock_no  flock_no from  flock_master f left outer join (select  isnull(sum(fqty),0) fqty,  isnull(sum(mqty),0) mqty,flock_no,[bird_type_code] from "
        'sql &= " [dbo].[Shed_Entry_Master] where isAlive = 1 group by flock_no,[bird_type_code]) s  on f.[flock_no] = s.flock_no and f.bird_type_code = s.bird_type_code"
        'sql &= " where ( isnull(f.fqty,0)- isnull(s.fqty,0)) > 0  or  ( isnull(f.mqty,0)- isnull(s.mqty,0)) > 0 "

        sql = "select * from flock_master  where isAlive = 1 order by LEFT(flock_no,3) desc"

        gmod.DataSetRet(sql, "getflock")
        flockno.DataSource = gmod.ds.Tables("getflock")
        flockno.DataTextField = "flock_no"
        flockno.DataValueField = "flock_no"
        flockno.DataBind()
    End Sub

    Protected Sub shedentrymaster_btnSave_Click(sender As Object, e As EventArgs) Handles shedentrymaster_btnSave.Click
        validation_check()
        If valiation = False Then
            lblprint.Text = "Please Fill All the Data and then Save"
            Exit Sub
        End If

        sql = "insert into [dbo].[Shed_Entry_Master]([bird_type_code], shed_loc, [shed_code], line_code,[flock_no], [fqty], [mqty], [entry_date], [isAlive], [isEmpty]) values ("
        sql &= "'" & birdtype.SelectedValue & "',"
        sql &= "'" & shedloc.SelectedValue & "',"
        sql &= "'" & shedname.SelectedValue & "',"
        sql &= "'" & lineno.SelectedValue & "',"
        sql &= "'" & flockno.SelectedValue & "',"
        sql &= "'" & fqty.Text & "',"
        sql &= "'" & mqty.Text & "',"
        sql &= "'" & shedentrydate.value & "',"
        sql &= "'1',"
        sql &= "'1')"

        qyeryresult = gmod.SqlExecuteNonQuery(sql)
        If qyeryresult = "SUCCESS" Then

            sql = "insert into [dbo].[Shed_Entry_Data]([bird_type_code],shed_loc, [shed_code],line_code, [flock_no], [fqty], [mqty], [entry_date], [isAlive], [isEmpty]) values ("
            sql &= "'" & birdtype.SelectedValue & "',"
            sql &= "'" & shedloc.SelectedValue & "',"
            sql &= "'" & shedname.SelectedValue & "',"
            sql &= "'" & lineno.SelectedValue & "',"
            sql &= "'" & flockno.SelectedValue & "',"
            sql &= "'" & fqty.Text & "',"
            sql &= "'" & mqty.Text & "',"
            sql &= "'" & shedentrydate.Value & "',"
            sql &= "'1',"
            sql &= "'1')"
            gmod.SqlExecuteNonQuery(sql)
            lblprint.Text = "Shed Entry Data Data Saved Successfully"
            fillGrid()
        Else
            lblprint.Text = qyeryresult
        End If
        ClearControls()

    End Sub

    Public Sub ClearControls()
       
        fqty.Text = ""
        mqty.Text = ""
       
    End Sub

    Public Sub validation_check()
        If shedentrydate.Value = "" Then
            valiation = False
        End If

        If fqty.Text = "" Then
            valiation = False
        End If

        If mqty.Text = "" Then
            valiation = False
        End If

       
    End Sub
    Public Sub fillLocation()
        sql = "select [location_code],[location_name] from [dbo].[location_master]  where org_code='10'"
        gmod.DataSetRet(sql, "shed_loc")
        shedloc.DataSource = gmod.ds.Tables("shed_loc")
        shedloc.DataTextField = "location_name"
        shedloc.DataValueField = "location_code"
        shedloc.DataBind()
    End Sub
    Public Sub fillGrid()
        sql = "select [bird_type_code],shed_code, line_code ,[flock_no], [fqty], [mqty],  convert(varchar,entry_date,103) [entry_date] from [Shed_Entry_Master] "
        gmod.DataSetRet(sql, "flockamster")
        shedentrymaster_grid.DataSource = gmod.ds.Tables("flockamster")
        shedentrymaster_grid.DataBind()
    End Sub

    Protected Sub shedname_SelectedIndexChanged(sender As Object, e As EventArgs) Handles shedname.SelectedIndexChanged
        sql = "select * from [dbo].[shed_line_master] where shed_code = '" & shedname.SelectedValue & "'"
        gmod.DataSetRet(sql, "shed_line_master")
        lineno.DataSource = gmod.ds.Tables("shed_line_master")
        lineno.DataTextField = "line_no"
        lineno.DataValueField = "line_code"
        lineno.DataBind()
    End Sub

    Protected Sub shedloc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles shedloc.SelectedIndexChanged
        sql = "select shed_code,shed_name from [dbo].[shed_master] where loc_code='" & shedloc.SelectedValue & "'"
        gmod.DataSetRet(sql, "shedname")
        shedname.DataSource = gmod.ds.Tables("shedname")
        shedname.DataTextField = "shed_name"
        shedname.DataValueField = "shed_code"
        shedname.DataBind()
    End Sub
End Class
