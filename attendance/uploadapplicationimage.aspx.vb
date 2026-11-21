Imports System.Data.SqlClient
Imports System.IO
Imports System.Data

Partial Class attendance_uploadapplicationimage
    Inherits System.Web.UI.Page
    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Try
            If FileUpload1.HasFile Then
                ' Allow only JPEG files
                Dim extension As String = Path.GetExtension(FileUpload1.FileName).ToLower()
                If extension <> ".jpg" AndAlso extension <> ".jpeg" Then
                    lblMessage.ForeColor = Drawing.Color.Red
                    lblMessage.Text = "Only JPG/JPEG images are allowed."
                    Exit Sub
                End If

                ' Read the file into a byte array
                Dim imgBytes As Byte() = FileUpload1.FileBytes

                ' Save into SQL Server
                Dim connStr As String = "Data Source=YOUR_SERVER;Initial Catalog=YOUR_DATABASE;Integrated Security=True"
                Using con As New SqlConnection(connStr)
                    Dim query As String = "INSERT INTO EmployeePhotos (EmpCode, Photo) VALUES (@EmpCode, @Photo)"
                    Using cmd As New SqlCommand(query, con)
                        cmd.Parameters.AddWithValue("@EmpCode", txtEmpCode.Text.Trim())
                        cmd.Parameters.Add("@Photo", SqlDbType.VarBinary).Value = imgBytes
                        con.Open()
                        cmd.ExecuteNonQuery()
                    End Using
                End Using

                lblMessage.ForeColor = Drawing.Color.Green
                lblMessage.Text = "Photo uploaded successfully!"
            Else
                lblMessage.ForeColor = Drawing.Color.Red
                lblMessage.Text = "Please select an image file to upload."
            End If

        Catch ex As Exception
            lblMessage.ForeColor = Drawing.Color.Red
            lblMessage.Text = "Error: " & ex.Message
        End Try
    End Sub

End Class
