
Partial Class dailyentryforms_Daily_mortality_pit
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblString.Text = "Welcome:" & Convert.ToString(Session("user_name"))
            fillLocation()
            fillFlock()
            ' fillFeedType()
            ' fillrid()
            flock_no_SelectedIndexChanged(sender, e)

        Else

        End If
    End Sub
    Public Sub fillLocation()
        sql = "select [location_code],[location_name] from [dbo].[location_master]  where org_code='10'"
        gmod.DataSetRet(sql, "shed_loc")
        flock_no.DataSource = gmod.ds.Tables("shed_loc")
        flock_no.DataTextField = "location_name"
        flock_no.DataValueField = "location_code"
        flock_no.DataBind()
    End Sub

   

    Protected Sub btnSAvemortality_Click(sender As Object, e As EventArgs) Handles btnSAvemortality.Click

        validation_check()
        If valiation = False Then
            lblprint.Text = "Please Fill All the Data and then Save"
            Exit Sub
        End If

        sql = "insert into [dbo].[flock_tmp_daily_morpit]([entry_date], [bird_type_code], [flock_no], [mor_fem], [mor_male], [pit_fem], [pit_male], [sale_fem], [sale_male], [user_name], [isPosted]) values("
        sql &= "'" & entrydate.Value & "',"
        sql &= "'" & Label2.Text & "',"

        sql &= "'" & lblflockfem.Text & "',"

        sql &= "'" & Val(Txtfemalemort.Text) & "',"
        sql &= "'" & Val(TxtMalemort.Text) & "',"

        sql &= "'" & Val(txtpitfem.Text) & "',"
        sql &= "'" & Val(txtpitmal.Text) & "',"

        sql &= "'" & Val(TxtFemalesale.Text) & "',"
        sql &= "'" & Val(TxtMaleSale.Text) & "',"

        sql &= "'" & Session("user_name") & "',"
        sql &= "'0')"

        qyeryresult = gmod.SqlExecuteNonQuery(sql)
        If qyeryresult = "SUCCESS" Then
            lblprint.Text = "Chicks Purchase Data Saved Successfully"
            'fillrid()
            ClearControls()
        Else
            lblprint.Text = qyeryresult
        End If
    End Sub

    Public Sub ClearControls()
        Label2.Text = ""
        lblflockfem.Text = ""
        Txtfemalemort.Text = ""
        TxtMalemort.Text = ""
        txtpitfem.Text = ""
        txtpitmal.Text = ""
        TxtFemalesale.Text = ""
        TxtMaleSale.Text = ""
    End Sub

    Public Sub validation_check()

        If Session("user_name") = "" Then
            valiation = False
        End If
        If entrydate.Value = "" Then
            valiation = False
        End If

        If Txtfemalemort.Text = "" Then
            valiation = False
        End If

        If TxtMalemort.Text = "" Then
            valiation = False
        End If

        If txtpitfem.Text = "" Then
            valiation = False
        End If

        If txtpitmal.Text = "" Then
            valiation = False
        End If
    End Sub

    Protected Sub flock_no_SelectedIndexChanged(sender As Object, e As EventArgs) Handles flock_no.SelectedIndexChanged
        Try
            sql = "select [bird_type_code], [flock_no],[fqty],[mqty] from flock_data where flock_no='" & flock_no.SelectedValue & "' and isAlive=1 "
            gmod.DataSetRet(sql, "flock_mor_pit")
            If gmod.ds.Tables("flock_mor_pit").Rows.Count > 0 Then
                Label2.Text = gmod.ds.Tables("flock_mor_pit").Rows(0)("bird_type_code")
                lblflockfem.Text = gmod.ds.Tables("flock_mor_pit").Rows(0)("flock_no")
                lblfem.Text = gmod.ds.Tables("flock_mor_pit").Rows(0)("fqty").ToString
                lblmale.Text = gmod.ds.Tables("flock_mor_pit").Rows(0)("mqty").ToString
            Else
                lblflockfem.Text = "-"
                lblfem.Text = "-"
                Label2.Text = "-"
                lblmale.Text = "-"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub fillFlock()
        sql = "select flock_no from [dbo].[flock_master] where isAlive=1"
        gmod.DataSetRet(sql, "data_flock")
        flock_no.DataSource = gmod.ds.Tables("data_flock")
        flock_no.DataTextField = "flock_no"
        flock_no.DataValueField = "flock_no"
        flock_no.DataBind()

    End Sub
End Class
