
Partial Class pit_bird_recived_pit
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True
    Dim d As String

    Protected Sub showdata_Click(sender As Object, e As EventArgs) Handles showdata.Click
        Try
            sql = "select sum(pit_fem_ext) pit_fem_ext, sum(pit_male_ext) pit_male_ext from "
            sql &= " [dbo].[daily_morpit] where entry_date='" & eggcolldate.Text & "'  and bird_type_code = '" & cmbftype.Text & "' and flock_no='" & flockno.Text & "'"

            gmod.DataSetRet(sql, "birddata")

            fbirds.Text = gmod.ds.Tables("birddata").Rows(0)("pit_fem_ext")
            mbirds.Text = gmod.ds.Tables("birddata").Rows(0)("pit_male_ext")
        Catch ex As Exception
            lblString.Text = "No data found"
        End Try
       

    End Sub
    Public Sub fillLocation()
        sql = "select * from [dbo].[shed_master] where shed_type='CL' and  org_code='20'"
        gmod.DataSetRet(sql, "coldroom_loc")
        cmbloc.DataSource = gmod.ds.Tables("coldroom_loc")
        cmbloc.DataTextField = "loc_code"
        cmbloc.DataValueField = "loc_code"
        cmbloc.DataBind()
        'cmbloc_SelectedIndexChanged(sender, e)
    End Sub

    Public Sub fillFlock()
        'sql = "select flock_no from [dbo].[flock_master] where isAlive=1"
        sql = "select distinct flock_no from Shed_Entry_Data where isAlive = 1"
        gmod.DataSetRet(sql, "data_flock")
        flockno.DataSource = gmod.ds.Tables("data_flock")
        flockno.DataTextField = "flock_no"
        flockno.DataValueField = "flock_no"
        flockno.DataBind()
    End Sub
    Public Sub fillBirdtype()
        sql = "select [bird_type_code], [bird_type_name] from [dbo].[bird_type_master]"
        gmod.DataSetRet(sql, "shed_loc")
        cmbftype.DataSource = gmod.ds.Tables("shed_loc")
        cmbftype.DataTextField = "bird_type_code"
        cmbftype.DataValueField = "bird_type_code"
        cmbftype.DataBind()
    End Sub

    Protected Sub cmbloc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbloc.SelectedIndexChanged
        Try
            sql = "select * from [dbo].[shed_master] where loc_code='" & cmbloc.Text & "'  and shed_type='CL' and  org_code='20'"
            gmod.DataSetRet(sql, "coldroom_")
            coldroomname.DataSource = gmod.ds.Tables("coldroom_")
            coldroomname.DataTextField = "shed_code"
            coldroomname.DataTextField = "shed_name"
            coldroomname.DataValueField = "shed_code"
            coldroomname.DataBind()
            'cmbloc_SelectedIndexChanged(sender, e)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            fillFlock()
            fillBirdtype()
            fillLocation()
            cmbloc_SelectedIndexChanged(sender, e)
        End If
    End Sub

    Protected Sub save_Click(sender As Object, e As EventArgs) Handles save.Click
        Try
            sql = "insert into [dbo].[pit_birds_rec]([entry_date], [trf_date], [ftype], [flockno], [fbirds], [mbirds], [loc], [shed], [user_name]) values ("
            sql &= "'" & entrydate.Text & "',"
            sql &= "'" & eggcolldate.Text & "',"
            sql &= "'" & cmbftype.Text & "',"
            sql &= "'" & flockno.Text & "',"
            sql &= "'" & Val(fbirds.Text) & "',"
            sql &= "'" & Val(mbirds.Text) & "',"
            sql &= "'" & cmbloc.Text & "',"
            sql &= "'" & coldroomname.Text & "',"
            sql &= "'" & Session("user_name") & "')"

            qyeryresult = gmod.SqlExecuteNonQuery(sql)
            If qyeryresult = "SUCCESS" Then
                lblString.Text = "Data Saved Successfully"
                fillrid()
            Else
                lblString.Text = qyeryresult
            End If
            ClearControls()

        Catch ex As Exception

        End Try
    End Sub

    Sub fillrid()
        sql = "select   convert(varchar, [trf_date], 103) Coll_Date,  [ftype], [flockno], [fbirds], [mbirds], [loc], [shed], [user_name]  from [dbo].[pit_birds_rec] where trf_date='" & eggcolldate.Text & "' and ftype= '" & cmbftype.Text & "' and flockno = '" & flockno.Text & "' "
        gmod.DataSetRet(sql, "tmp_daily_entry_show1")
        pitbirds.DataSource = gmod.ds.Tables("tmp_daily_entry_show1")
        pitbirds.DataBind()
        lblString.Text = ""
    End Sub

    Sub ClearControls()
        fbirds.Text = 0
        mbirds.Text = 0

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        fillrid()
    End Sub
End Class
