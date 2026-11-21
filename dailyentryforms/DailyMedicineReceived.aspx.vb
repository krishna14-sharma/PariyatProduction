
Partial Class dailyentryforms_DailyMedicineReceived
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True
    Dim rCount As Integer = 0

    Protected Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Try
            gmod.DataSetRet("exec MedicineRceFormStoreList '" & entrydate.Value & "' ,'" & Session("area_code") & "'", "medicine_received")
            materialrec_grid.DataSource = gmod.ds.Tables("medicine_received")
            materialrec_grid.DataBind()

        Catch ex As Exception
            AppUi.ShowError(Me, ex.Message)
        End Try
    End Sub

    Protected Sub btnReceve_Click(sender As Object, e As EventArgs) Handles btnReceve.Click
        Try
            gmod.DataSetRet("exec [MedicineReceived] '" & entrydate.Value & "' ,'" & Session("area_code") & "'", "medicine_received")
            'materialrec_grid.DataSource = gmod.ds.Tables("medicine_received")
            'materialrec_grid.DataBind()
            lbl.Text = "Medecine Received"

            gmod.DataSetRet("exec MedicineRceFormStoreList '" & entrydate.Value & "' ,'" & Session("area_code") & "'", "medicine_received")
            materialrec_grid.DataSource = gmod.ds.Tables("medicine_received")
            materialrec_grid.DataBind()
          
        Catch ex As Exception
            AppUi.ShowError(Me, ex.Message)
        End Try
    End Sub
End Class
