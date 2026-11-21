Imports System.Data.SqlClient
Imports System.Data

Partial Class misreports_mis_flockdisesehistory
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True
    Private Function GetConnection() As SqlConnection
        Return New SqlConnection(gmod.Connstr)
    End Function
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'lblString.Text = "Welcome:" & Convert.ToString(Session("user_name"))
            fillLocation()
            fillDisease()
            'fillFeedType()
            'fillrid()
            shedloc_SelectedIndexChanged(sender, e)
            shedname_SelectedIndexChanged(sender, e)
            lineno_SelectedIndexChanged(sender, e)
            fillresaonMale()
            BindGrid()
            'heggs.Attributes.Add("onkeyup", "sum()")
            'commeggs.Attributes.Add("onkeyup", "sum()")
            'crackeggs.Attributes.Add("onkeyup", "sum()")
            'lekageeggs.Attributes.Add("onkeyup", "sum()")
        Else

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

    Protected Sub shedloc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles shedloc.SelectedIndexChanged
        sql = "select shed_code,shed_name from [dbo].[shed_master] where loc_code='" & shedloc.SelectedValue & "'"
        gmod.DataSetRet(sql, "shed_")
        shedname.DataSource = gmod.ds.Tables("shed_")
        shedname.DataTextField = "shed_name"
        shedname.DataValueField = "shed_code"
        shedname.DataBind()
        shedname_SelectedIndexChanged(sender, e)
    End Sub

    Protected Sub shedname_SelectedIndexChanged(sender As Object, e As EventArgs) Handles shedname.SelectedIndexChanged
        sql = "select * from [dbo].[shed_line_master] where shed_code = '" & shedname.SelectedValue & "'"
        gmod.DataSetRet(sql, "shed_line_master")
        lineno.DataSource = gmod.ds.Tables("shed_line_master")
        lineno.DataTextField = "line_no"
        lineno.DataValueField = "line_code"
        lineno.DataBind()
        lineno_SelectedIndexChanged(sender, e)
    End Sub

    Protected Sub lineno_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lineno.SelectedIndexChanged
        Try
            sql = "select [bird_type_code], [shed_code],[flock_no] from [dbo].[Shed_Entry_Data] where shed_code='" & shedname.SelectedValue & "' and line_code = '" & lineno.SelectedValue & "' and isAlive=1 and isEmpty=1 "
            gmod.DataSetRet(sql, "shed_data")

            sql = "select Sum(isnull([fqty],0)) fqty ,Sum(isnull([mqty],0))  mqty from [dbo].[Shed_Entry_Data] where shed_code='" & shedname.SelectedValue & "' and line_code = '" & lineno.SelectedValue & "' and isAlive=1 and isEmpty=1 and flock_no = '" & gmod.ds.Tables("shed_data").Rows(0)("flock_no").ToString & "' "
            gmod.DataSetRet(sql, "shed_qtydata")
            If gmod.ds.Tables("shed_data").Rows.Count > 0 Then
                lblbirdbreed.Text = gmod.ds.Tables("shed_data").Rows(0)("bird_type_code")
                lblflockno.Text = gmod.ds.Tables("shed_data").Rows(0)("flock_no")
                lblfqty.Text = gmod.ds.Tables("shed_qtydata").Rows(0)("fqty").ToString
                lblmqty.Text = gmod.ds.Tables("shed_qtydata").Rows(0)("mqty").ToString

                sql = "	select DATEDIFF(d,hatch_date,GETDATE())/7 Weeks , (DATEDIFF(d,hatch_date,GETDATE()) % 7)+1  Days, bird_type_code   from flock_master  where flock_no='" & lblflockno.Text & "'"
                gmod.DataSetRet(sql, "shed_data_bird_age")
                lblbirdbreed.Text = gmod.ds.Tables("shed_data_bird_age").Rows(0)("Weeks").ToString + "-" + gmod.ds.Tables("shed_data_bird_age").Rows(0)("Days").ToString
                lblbreed.Text = gmod.ds.Tables("shed_data_bird_age").Rows(0)("bird_type_code").ToString
            Else
                lblflockno.Text = "-"
                lblfqty.Text = "-"
                lblbirdbreed.Text = "-"
                lblmqty.Text = "-"
            End If

            fillresaonMale()
        Catch ex As Exception

        End Try
        ' fillrid()
    End Sub


    Public Sub fillDisease()
        sql = "select * from [dbo].[Disease_list]  where disease_name<>'' order by disease_name"
        gmod.DataSetRet(sql, "diseaselist")
        diseaselist.DataSource = gmod.ds.Tables("diseaselist")
        diseaselist.DataTextField = "disease_name"
        diseaselist.DataValueField = "disease_name"
        diseaselist.DataBind()

        sql = "select * from [dbo].[Disease_list]  where disease_liesons<>'' order by  disease_liesons"
        gmod.DataSetRet(sql, "diseaselist1")
        pmleasing.DataSource = gmod.ds.Tables("diseaselist1")
        pmleasing.DataTextField = "disease_liesons"
        pmleasing.DataValueField = "disease_liesons"
        pmleasing.DataBind()

    End Sub


    Public Sub fillresaonMale()
        sql = "select * from [dbo].[Sepration_master]"
        gmod.DataSetRet(sql, "Sepration_master_male")
        Symptoms.DataSource = gmod.ds.Tables("Sepration_master_male")
        Symptoms.DataTextField = "seperation_reason"
        Symptoms.DataValueField = "seperation_reason"
        Symptoms.DataBind()

        sql = "select * from vaccinelist where breed ='" & lblbreed.Text & "' ORDER BY ID"
        gmod.DataSetRet(sql, "Vaccination")
        Vaccination.DataSource = gmod.ds.Tables("Vaccination")
        Vaccination.DataTextField = "vaccname"
        Vaccination.DataValueField = "vaccname"
        Vaccination.DataBind()

    End Sub

    Public Sub BindGrid()
        ' sql = "select * from Medical_history_data order by id desc"
        sql = "SELECT " & _
      "id, " & _
      "CONVERT(varchar(9), entry_date, 6) AS entry_date, " & _
      "shed_loc, " & _
      "shed_name, " & _
      "line_pen, " & _
      "age, " & _
      "flock_no, " & _
      "disease_name, " & _
       "pmLesions, " & _
       "stockF, " & _
      "stockM, " & _
      "symptoms, " & _
      "noofbirds, " & _
      "noofbirdsm, " & _
      "medecine, " & _
      "vaccination, " & _
      "remarks, " & _
      "CONVERT(varchar(9), givenon, 6) AS givenon, " & _
      "CONVERT(varchar(9), dueon, 6) AS dueon " & _
      "FROM Medical_history_data " & _
      "WHERE entry_date = '" & entrydate.Value & "' " & _
      "ORDER BY id DESC"
        gmod.DataSetRet(sql, "med")

        gvMedical.DataSource = gmod.ds.Tables("med")
        gvMedical.DataBind()
    End Sub

    Public Sub loc_btnSave_Click(sender As Object, e As EventArgs) Handles loc_btnSave.Click
        sql = "insert into Medical_history_data"
        sql &= " ([entry_date],[shed_loc],[shed_name],[line_pen],[age],[flock_no],"
        sql &= " [disease_name],[pm_qtyf],[pm_qtym],[remarks],[isPosted],[user_name],"
        sql &= " [pmtime],[stockF],[stockM],[symptoms],[medecine],[vaccination],"
        sql &= "  [givenon],[dueon],[noofbirds],noofbirdsm,pmLesions) values ("
        sql &= "'" & entrydate.Value & "',"
        sql &= "'" & shedloc.Text & "',"
        sql &= "'" & shedname.Text & "',"
        sql &= "'" & lineno.Text & "',"
        sql &= "'" & lblbirdbreed.Text & "',"
        sql &= "'" & lblflockno.Text & "',"
        sql &= "'" & diseaselist.Text & "',"
        sql &= "'0',"
        sql &= "'0',"
        sql &= "'" & txtremarks.Text & "',"
        sql &= "'0',"
        sql &= "'" & Session("user_name") & "',"
        sql &= "'1/1/2000',"
        sql &= "'" & lblfqty.Text & "',"
        sql &= "'" & lblmqty.Text & "',"
        sql &= "'" & Symptoms.Text & "',"
        sql &= "'" & medication.Text & "',"
        sql &= "'" & Vaccination.Text & "',"
        sql &= "'" & txtgivenon.Value & "',"
        sql &= "'" & txtdueon.Value & "',"
        sql &= "'" & Val(noofinfectedbirds.Text) & "',"
        sql &= "'" & Val(noofinfectedbirds.Text) & "',"
        sql &= "'" & pmleasing.Text & "')"
        Try
            Using conn As New SqlConnection(gmod.Connstr)
                conn.Open()
                Using cmd As New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    lblprint.Text = "Medical History Saved Successfully"
                End Using
            End Using
            BindGrid()
        Catch ex As Exception
            lblprint.Text = ex.Message
        End Try

    End Sub

    Protected Sub gvMedical_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvMedical.RowEditing
        ' gvMedical.EditIndex = e.NewEditIndex
        ' BindGrid()

    End Sub

    Protected Sub gvMedical_RowUpdating(sender As Object, e As GridViewUpdateEventArgs)

        Dim id As String = gvMedical.DataKeys(e.RowIndex).Value.ToString()
        Dim row As GridViewRow = gvMedical.Rows(e.RowIndex)

        Dim disease As String = CType(row.Cells(2).Controls(0), TextBox).Text
        Dim fqty As String = CType(row.Cells(3).Controls(0), TextBox).Text
        Dim mqty As String = CType(row.Cells(4).Controls(0), TextBox).Text
        Dim remarks As String = CType(row.Cells(5).Controls(0), TextBox).Text

        sql = "update Medical_history_data set "
        sql &= "disease_name='" & disease & "',"
        sql &= "pm_qtyf='" & fqty & "',"
        sql &= "pm_qtym='" & mqty & "',"
        sql &= "remarks='" & remarks & "' "
        sql &= "where id='" & id & "'"

        gmod.SqlExecuteNonQuery(sql)

        gvMedical.EditIndex = -1
        BindGrid()

    End Sub

    Protected Sub gvMedical_RowDeleting(sender As Object, e As GridViewDeleteEventArgs)

        Dim id As String = gvMedical.DataKeys(e.RowIndex).Value.ToString()

        sql = "delete from Medical_history_data where id='" & id & "'"
        gmod.SqlExecuteNonQuery(sql)

        BindGrid()

    End Sub

    Protected Sub gvMedical_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs)

        gvMedical.EditIndex = -1
        BindGrid()

    End Sub

    Protected Sub btnBindGrid_Click(sender As Object, e As EventArgs)
        BindGrid()
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub
End Class
