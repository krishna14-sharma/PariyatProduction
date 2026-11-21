
Partial Class transforms_ChicksPurchase
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True

    Protected Sub chickspurchase_btnSave_Click(sender As Object, e As EventArgs) Handles chickspurchase_btnSave.Click
        lblprint.Text = inwdate.Value
        validation_check()
        If valiation = False Then
            lblprint.Text = "Please Fill All the Data and then Save"
            Exit Sub
        End If

        sql = "insert into [dbo].[chicks_purchase]([bird_type_code], [party_code], [inw_no], [inw_date], [inv_no], [inv_date], [fqty], [frate], [mqty], [mrate],session ) values ("
        sql &= "'" & birdtype.SelectedValue & "',"
        sql &= "'" & partyname.SelectedValue & "',"
        sql &= "'" & inwno.Text & "',"
        sql &= "'" & inwdate.Value & "',"

        sql &= "'" & invno.Text & "',"
        sql &= "'" & invdate.Value & "',"
        sql &= "'" & fqty.Text & "',"

        sql &= "'" & frate.Text & "',"
        sql &= "'" & mqty.Text & "',"
        sql &= "'" & mrate.Text & "',"
        sql &= "'" & gmod.Getsession(Now) & "')"
        qyeryresult = gmod.SqlExecuteNonQuery(sql)
        If qyeryresult = "SUCCESS" Then
            lblprint.Text = "Chicks Purchase Data Saved Successfully"
            fillGrid()

        Else
            lblprint.Text = qyeryresult
        End If
        ClearControls()
    End Sub
    Public Sub ClearControls()
        inwno.Text = ""
        invno.Text = ""
        fqty.Text = ""
        mqty.Text = ""
        frate.Text = ""
        mrate.Text = ""
    End Sub

    Public Sub validation_check()
        If inwdate.Value = "" Then
            valiation = False
        End If

        If fqty.Text = "" Then
            valiation = False
        End If

        If mqty.Text = "" Then
            valiation = False
        End If

        If inwno.Text = "" Then
            valiation = False
        End If

        If invno.Text = "" Then
            valiation = False
        End If
        If frate.Text = "" Then
            valiation = False
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

    Public Sub fillParty()
        sql = "select * from party_master"
        gmod.DataSetRet(sql, "party_master")
        partyname.DataSource = gmod.ds.Tables("party_master")
        partyname.DataTextField = "party_name"
        partyname.DataValueField = "party_code"
        partyname.DataBind()
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            fillBirdtype()
            fillParty()
            fillGrid()
        Else

        End If
    End Sub

    Protected Sub birdtype_SelectedIndexChanged(sender As Object, e As EventArgs) Handles birdtype.SelectedIndexChanged

    End Sub

    Public Sub fillGrid()
        sql = "select [bird_type_code], [party_code], [inw_no], convert(varchar,inw_date,103) inw_date , [inv_no], convert(varchar,inv_date,103) inv_date , [fqty], [frate], [mqty], [mrate], [session] from  [chicks_purchase] "
        gmod.DataSetRet(sql, "chicks_purchase")
        chikc_purchase_grid.DataSource = gmod.ds.Tables("chicks_purchase")
        chikc_purchase_grid.DataBind()
    End Sub
End Class
