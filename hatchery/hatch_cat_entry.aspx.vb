Imports System.Data.SqlClient
Partial Class hatchery_hatch_cat_entry
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True
    Dim d As String

    Protected Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        ' Response.Write(the.Text)   ' debug output removed (Response.Write breaks AJAX postbacks)
        If Val(the.Text) = Val(fresh.Text) + Val(bigsize.Text) + Val(md.Text) + Val(mplus.Text) + Val(mminus.Text) + Val(hcomm.Text) + Val(hatchery_crack.Text) + +Val(hatchery_leaker.Text) + Val(rejhcomm.Text) Then

        Else
            lblString.Text = "Egg Miss Match..."
            Exit Sub
        End If

        Using connection As New SqlConnection(gmod.Connstr)
            connection.Open()

            ' Begin a transaction
            Dim transaction As SqlTransaction = connection.BeginTransaction()

            ' Create a command and assign the transaction and connection to it
            Dim command As SqlCommand = connection.CreateCommand()
            command.Transaction = transaction

            Try
                ' First SQL operation
                sql = " insert into [hatch_egg_grading_entry]([entry_date], [egg_coll_date], [bird_type], [flock_no], [Age], "
                sql &= " [total_eggs], [hatch_eggs], [fresh_eggs], [bigsize_eggs], [md_eggs], [mplus_eggs], [mminus_eggs], [hcomm_eggs],"
                sql &= " [hat_crack_eggs],[hat_leak_eggs], [loc], [cl_name], [cl_code], [user_name],hcomm_eggs_rej) values("

                sql &= "'" & entrydate.Text & "',"
                sql &= "'" & eggcolldate.Text & "',"
                sql &= "'" & ftype.Text & "',"
                sql &= "'" & flockno.Text & "',"
                sql &= "'" & LblAge.Text & "',"
                sql &= "'" & Val("") & "',"

                sql &= "'" & Val(the.Text) & "',"
                sql &= "'" & Val(fresh.Text) & "',"
                sql &= "'" & Val(bigsize.Text) & "',"
                sql &= "'" & Val(md.Text) & "',"
                sql &= "'" & Val(mplus.Text) & "',"
                sql &= "'" & Val(mminus.Text) & "',"
                sql &= "'" & Val(hcomm.Text) & "',"
                sql &= "'" & Val(hatchery_crack.Text) & "',"
                sql &= "'" & Val(hatchery_leaker.Text) & "',"
                sql &= "'" & cmbloc.Text & "',"
                sql &= "'" & coldroomname.SelectedValue & "',"
                sql &= "'" & coldroomname.SelectedItem.ToString & "',"
                sql &= "'" & Session("user_name") & "',"
                sql &= "'" & Val(rejhcomm.Text) & "')"

                command.CommandText = sql
                command.ExecuteNonQuery()

                ' Second SQL operation
                sql = " insert into [hatch_egg_grading]([entry_date], [egg_coll_date], [bird_type], [flock_no], [Age], "
                sql &= " [total_eggs], [hatch_eggs], [fresh_eggs], [bigsize_eggs], [md_eggs], [mplus_eggs], [mminus_eggs], [hcomm_eggs],"
                sql &= " [hat_crack_eggs],[hat_leak_eggs], [loc], [cl_name], [cl_code], [user_name],hcomm_eggs_rej) values("

                sql &= "'" & entrydate.Text & "',"
                sql &= "'" & eggcolldate.Text & "',"
                sql &= "'" & ftype.Text & "',"
                sql &= "'" & flockno.Text & "',"
                sql &= "'" & LblAge.Text & "',"
                sql &= "'" & Val("") & "',"

                sql &= "'" & Val(the.Text) & "',"
                sql &= "'" & Val(fresh.Text) & "',"
                sql &= "'" & Val(bigsize.Text) & "',"
                sql &= "'" & Val(md.Text) & "',"
                sql &= "'" & Val(mplus.Text) & "',"
                sql &= "'" & Val(mminus.Text) & "',"
                sql &= "'" & Val(hcomm.Text) & "',"
                sql &= "'" & Val(hatchery_crack.Text) & "',"
                sql &= "'" & Val(hatchery_leaker.Text) & "',"
                sql &= "'" & cmbloc.Text & "',"
                sql &= "'" & coldroomname.SelectedValue & "',"
                sql &= "'" & coldroomname.SelectedItem.ToString & "',"
                sql &= "'" & Session("user_name") & "',"
                sql &= "'" & Val(rejhcomm.Text) & "')"

                command.CommandText = sql
                command.ExecuteNonQuery()

                ' Commit the transaction
                transaction.Commit()
                lblString.Text = "Egg Grading Data Saved Successfully"
                fillrid()
                ClearControls()
            Catch ex As Exception
                ' Rollback the transaction on error
                Try
                    transaction.Rollback()
                    lblString.Text = "Transaction rolled back due to an error: " & ex.Message
                Catch rollEx As Exception
                    ClearControls()
                End Try
            End Try
        End Using

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            fillFlock()
            fillBirdtype()
            fillLocation()
            fillrid()
            fillArea()
        End If
    End Sub
    Public Sub fillArea()
        sql = "select distinct shed_loc from Shed_Entry_Data where isAlive=1"
        gmod.DataSetRet(sql, "shed_locdate")
        cmbAreaName.DataSource = gmod.ds.Tables("shed_locdate")
        cmbAreaName.DataTextField = "shed_loc"
        cmbAreaName.DataValueField = "shed_loc"
        cmbAreaName.DataBind()
    End Sub
    Public Sub ClearControls()
        the.Text = "0"
        md.Text = "0"
        bigsize.Text = "0"
        mplus.Text = "0"
        mminus.Text = "0"
        hatchery_crack.Text = "0"
        hatchery_leaker.Text = "0"
        hcomm.Text = "0"
        fresh.Text = "0"
        hcomm.Text = "0"
        rejhcomm.Text = "0"

    End Sub
    Public Sub fillrid()
        sql = "select convert(varchar, [egg_coll_date], 103) Date, [bird_type] Type, [flock_no] , [Age], [hatch_eggs] HE, [fresh_eggs] Fresh, [bigsize_eggs] BigSized, [md_eggs] MD, [mplus_eggs] [M+], [mminus_eggs] [M-], [hcomm_eggs] HCOMM, [hat_crack_eggs] Crack,[hat_leak_eggs] Leaker, [cl_code] CL, [user_name] [User]  from [dbo].[hatch_egg_grading_entry] where egg_coll_date='" & eggcolldate.Text & "' and user_name='" & Session("user_name") & "'"
        gmod.DataSetRet(sql, "tmp_daily_entry_show")
        egggrading.DataSource = gmod.ds.Tables("tmp_daily_entry_show")
        egggrading.DataBind()
        lblString.Text = ""
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
        'sql = "select distinct flock_no from [dbo].[flock_master] where isAlive=1 order by flock_no"
        sql = "select distinct flock_no  from Shed_Entry_Data where shed_loc='" & cmbAreaName.Text & "' and isAlive =1"
        gmod.DataSetRet(sql, "data_flock")
        flockno.DataSource = gmod.ds.Tables("data_flock")
        flockno.DataTextField = "flock_no"
        flockno.DataValueField = "flock_no"
        flockno.DataBind()
    End Sub
    Public Sub fillBirdtype()
        sql = "select [bird_type_code], [bird_type_name] from [dbo].[bird_type_master]"
        gmod.DataSetRet(sql, "shed_loc")
        ftype.DataSource = gmod.ds.Tables("shed_loc")
        ftype.DataTextField = "bird_type_code"
        ftype.DataValueField = "bird_type_code"
        ftype.DataBind()
    End Sub
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            sql = "select age,hatch_eggs from [dbo].[Hatchery_egg_collection] where bird_type='" & ftype.Text & "' and flock_no='" & flockno.Text & "' and entry_date='" & eggcolldate.Text & "'"
            gmod.DataSetRet(sql, "hegg_data")
            LblAge.Text = gmod.ds.Tables("hegg_data").Rows(0)("age")
            the.Text = gmod.ds.Tables("hegg_data").Rows(0)("hatch_eggs")

        Catch ex As Exception

        End Try

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

    Protected Sub cancel_Click(sender As Object, e As EventArgs) Handles cancel.Click
        fillrid()
    End Sub

    Protected Sub btnflockshow_Click(sender As Object, e As EventArgs) Handles btnflockshow.Click
        fillFlock()
    End Sub
End Class
