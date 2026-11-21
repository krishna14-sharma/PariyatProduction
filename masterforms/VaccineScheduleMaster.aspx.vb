Imports System
Imports System.Data
Imports System.Globalization
Imports System.Web.UI.WebControls

''' <summary>Maintains the standard male / female vaccination schedules (vacc_schedule_master).</summary>
Partial Class masterforms_VaccineScheduleMaster
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Session("user_name") Is Nothing Then
            Response.Redirect("~/loginforms/index.html", True)
        End If
        If Not IsPostBack Then
            BindItems()
            SuggestNextSeq()
        End If
    End Sub

    Protected Sub rblSex_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rblSex.SelectedIndexChanged
        ClearForm()
        BindItems()
        SuggestNextSeq()
    End Sub

    Protected Sub chkInactive_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkInactive.CheckedChanged
        BindItems()
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        ClearForm()
        SuggestNextSeq()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        Dim seq, dayNo As Integer
        If Not Integer.TryParse(txtSeq.Text, seq) OrElse seq < 1 Then
            ShowMessage("error", "Enter the serial number (1 or more).")
            Return
        End If
        If Not Integer.TryParse(txtDay.Text, dayNo) OrElse dayNo < 1 OrElse dayNo > 700 Then
            ShowMessage("error", "Enter the day number (1 = hatch day, up to 700).")
            Return
        End If
        If txtAge.Text.Trim().Length = 0 OrElse txtVaccine.Text.Trim().Length = 0 Then
            ShowMessage("error", "Age and vaccine / medicine are required.")
            Return
        End If
        Dim phase As String = txtPhase.Text.Trim()
        If phase.Length = 0 Then phase = "Rearing"

        Try
            Dim prms As New System.Collections.Generic.List(Of System.Data.SqlClient.SqlParameter)()
            prms.Add(VaccinationDb.P("@sex", rblSex.SelectedValue))
            prms.Add(VaccinationDb.P("@seq", seq))
            prms.Add(VaccinationDb.P("@phase", phase))
            prms.Add(VaccinationDb.P("@age", txtAge.Text.Trim()))
            prms.Add(VaccinationDb.P("@day", dayNo))
            prms.Add(VaccinationDb.P("@vaccine", txtVaccine.Text.Trim()))
            prms.Add(VaccinationDb.P("@dose", Blank(txtDose.Text)))
            prms.Add(VaccinationDb.P("@route", Blank(txtRoute.Text)))
            prms.Add(VaccinationDb.P("@company", Blank(txtCompany.Text)))
            prms.Add(VaccinationDb.P("@remark", Blank(txtRemark.Text)))
            prms.Add(VaccinationDb.P("@active", chkActive.Checked))
            prms.Add(VaccinationDb.P("@user", Convert.ToString(Session("user_name"))))

            If hfEditId.Value = "" Then
                VaccinationDb.Execute(
                    "insert into vacc_schedule_master (sex, seq_no, phase, age_label, day_no, vaccine, dose, route, company, remark, is_active, updated_by, updated_on) " &
                    "values (@sex, @seq, @phase, @age, @day, @vaccine, @dose, @route, @company, @remark, @active, @user, getdate())", prms.ToArray())
                ShowMessage("success", "Item added to the " & VaccinationData.SexName(rblSex.SelectedValue) & " schedule.")
            Else
                prms.Add(VaccinationDb.P("@id", CInt(hfEditId.Value)))
                VaccinationDb.Execute(
                    "update vacc_schedule_master set seq_no = @seq, phase = @phase, age_label = @age, day_no = @day, vaccine = @vaccine, " &
                    "dose = @dose, route = @route, company = @company, remark = @remark, is_active = @active, updated_by = @user, updated_on = getdate() " &
                    "where id = @id and sex = @sex", prms.ToArray())
                ' Keep stored due dates in line with a changed day number.
                VaccinationDb.Execute(
                    "update e set due_date = dateadd(day, @day - 1, f.hatch_date) from vacc_flock_entry e " &
                    "join vacc_flock f on f.flock_no = e.flock_no and f.sex = e.sex where e.schedule_id = @id",
                    VaccinationDb.P("@day", dayNo), VaccinationDb.P("@id", CInt(hfEditId.Value)))
                ShowMessage("success", "Item updated.")
            End If
            ClearForm()
            BindItems()
            SuggestNextSeq()
        Catch ex As Exception
            VaccinationDb.LogError(ex, "masterforms/VaccineScheduleMaster.aspx", "Save")
            ShowMessage("error", "Could not save: " & ex.Message)
        End Try
    End Sub

    Protected Sub gvItems_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles gvItems.RowCommand
        If e.CommandName <> "EditItem" AndAlso e.CommandName <> "DeleteItem" Then Return
        Dim index As Integer = Convert.ToInt32(e.CommandArgument, CultureInfo.InvariantCulture)
        Dim id As Integer = CInt(gvItems.DataKeys(index)("id"))
        Dim entries As Integer = CInt(gvItems.DataKeys(index)("entries"))
        Try
            If e.CommandName = "EditItem" Then
                LoadForEdit(id)
                Return
            End If
            If entries > 0 Then
                ' Used by flocks: keep the history, just switch it on / off.
                VaccinationDb.Execute(
                    "update vacc_schedule_master set is_active = 1 - is_active, updated_by = @u, updated_on = getdate() where id = @id",
                    VaccinationDb.P("@id", id), VaccinationDb.P("@u", Convert.ToString(Session("user_name"))))
                ShowMessage("success", "Item status changed.")
            Else
                VaccinationDb.Execute("delete from vacc_schedule_master where id = @id", VaccinationDb.P("@id", id))
                ShowMessage("success", "Item deleted.")
            End If
            BindItems()
        Catch ex As Exception
            VaccinationDb.LogError(ex, "masterforms/VaccineScheduleMaster.aspx", e.CommandName)
            ShowMessage("error", "Could not complete: " & ex.Message)
        End Try
    End Sub

    Private Sub BindItems()
        Try
            Dim sql As String =
                "select m.id, m.seq_no, m.phase, m.age_label, m.day_no, m.vaccine, m.dose, m.route, m.company, m.remark, m.is_active, " &
                "(select count(*) from vacc_flock_entry e where e.schedule_id = m.id) as entries " &
                "from vacc_schedule_master m where m.sex = @sex"
            If Not chkInactive.Checked Then sql &= " and m.is_active = 1"
            sql &= " order by m.day_no, m.seq_no"
            gvItems.DataSource = VaccinationDb.GetTable(sql, VaccinationDb.P("@sex", rblSex.SelectedValue))
            gvItems.DataBind()
        Catch ex As Exception
            VaccinationDb.LogError(ex, "masterforms/VaccineScheduleMaster.aspx", "Bind")
            ShowMessage("error", "Could not load the schedule: " & ex.Message & " (has Database/Vaccination_Schedule.sql been run?)")
        End Try
    End Sub

    Private Sub LoadForEdit(ByVal id As Integer)
        Dim dt As DataTable = VaccinationDb.GetTable("select * from vacc_schedule_master where id = @id", VaccinationDb.P("@id", id))
        If dt.Rows.Count = 0 Then Return
        Dim r As DataRow = dt.Rows(0)
        hfEditId.Value = id.ToString(CultureInfo.InvariantCulture)
        txtSeq.Text = Convert.ToString(r("seq_no"))
        txtAge.Text = Convert.ToString(r("age_label"))
        txtDay.Text = Convert.ToString(r("day_no"))
        txtPhase.Text = Convert.ToString(r("phase"))
        txtVaccine.Text = Convert.ToString(r("vaccine"))
        txtDose.Text = Convert.ToString(r("dose"))
        txtRoute.Text = Convert.ToString(r("route"))
        txtCompany.Text = Convert.ToString(r("company"))
        txtRemark.Text = Convert.ToString(r("remark"))
        chkActive.Checked = CBool(r("is_active"))
        litFormTitle.Text = "Edit item " & HttpUtility.HtmlEncode(Convert.ToString(r("seq_no")) & " – " & Convert.ToString(r("vaccine")))
        btnSave.Text = "Save changes"
        btnCancel.Visible = True
    End Sub

    Private Sub ClearForm()
        hfEditId.Value = ""
        For Each t As TextBox In New TextBox() {txtSeq, txtAge, txtDay, txtPhase, txtVaccine, txtDose, txtRoute, txtCompany, txtRemark}
            t.Text = ""
        Next
        chkActive.Checked = True
        litFormTitle.Text = "Add an item"
        btnSave.Text = "Add item"
        btnCancel.Visible = False
    End Sub

    Private Sub SuggestNextSeq()
        Try
            Dim value As Object = VaccinationDb.Scalar("select isnull(max(seq_no), 0) + 1 from vacc_schedule_master where sex = @sex",
                                               VaccinationDb.P("@sex", rblSex.SelectedValue))
            If value IsNot Nothing AndAlso txtSeq.Text = "" Then txtSeq.Text = Convert.ToString(value)
        Catch
        End Try
    End Sub

    Private Sub ShowMessage(ByVal kind As String, ByVal text As String)
        Dim css As String = If(kind = "success", "alert-success", "alert-danger")
        litMessage.Text = "<div class=""alert " & css & " vacc-message"" role=""alert"">" & HttpUtility.HtmlEncode(text) & "</div>"
    End Sub

    Private Shared Function Blank(ByVal text As String) As Object
        text = If(text, "").Trim()
        If text.Length = 0 Then Return Nothing
        Return text
    End Function

End Class
