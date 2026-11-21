Imports System.Data.SqlClient
Partial Class transforms_ShedEntryEdit
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            fillcombo()
            BindGrid()
        End If
    End Sub

    Sub BindGrid()
        sql = "SELECT * FROM dbo.Shed_Entry_Data  where shed_loc = '" & cmbarea.Text & "' and isAlive =1 and isEmpty =1 ORDER BY entry_date DESC"
        gmod.DataSetRet(sql, "Shed_Entry_DataEdit")
        GridView1.DataSource = gmod.ds.Tables("Shed_Entry_DataEdit")
        GridView1.DataBind()
    End Sub

    Sub fillcombo()
        sql = "select distinct shed_loc from [dbo].[Shed_Entry_Data] where isAlive =1 and isEmpty =1"
        gmod.DataSetRet(sql, "shed_loc_DataEdit")
        cmbarea.DataSource = gmod.ds.Tables("shed_loc_DataEdit")
        cmbarea.DataTextField = "shed_loc"
        cmbarea.DataValueField = "shed_loc"
        cmbarea.DataBind()
    End Sub

    Protected Sub GridView1_RowEditing(sender As Object,
                                      e As GridViewEditEventArgs) Handles GridView1.RowEditing
        GridView1.EditIndex = e.NewEditIndex
        BindGrid()   ' Re‑bind so the edit row shows its controls
    End Sub

    Protected Sub GridView1_RowCancelingEdit(sender As Object,
                                             e As GridViewCancelEditEventArgs) Handles GridView1.RowCancelingEdit
        GridView1.EditIndex = -1
        BindGrid()
    End Sub

    Protected Sub GridView1_RowUpdating(sender As Object,
                                        e As GridViewUpdateEventArgs) Handles GridView1.RowUpdating

        'Try
        '    Dim id As Integer = CInt(GridView1.DataKeys(e.RowIndex).Value)
        '    Dim row As GridViewRow = GridView1.Rows(e.RowIndex)

        '    Dim txtFQty As TextBox = CType(row.FindControl("txtFQty"), TextBox)
        '    Dim txtMQty As TextBox = CType(row.FindControl("txtMQty"), TextBox)
        '    Dim txtEntry As TextBox = CType(row.FindControl("txtEntryDate"), TextBox)
        '    Dim chkAlive As CheckBox = CType(row.FindControl("chkAliveEdit"), CheckBox)
        '    Dim chkEmpty As CheckBox = CType(row.FindControl("chkEmptyEdit"), CheckBox)


        '    sql = "update [Shed_Entry_Data] set fqty='" & Val(txtFQty.Text) & "',mqty = '" & Val(txtMQty.Text) & "' , IsAlive = '" & chkEmpty.Checked & "'   where id  ='" & id & "'"
        '    gmod.SqlExecuteNonQuery(sql)
        '    ' Persist to SQL
        '    GridView1.EditIndex = -1


        '    BindGrid()
        'Catch ex As Exception

        'End Try


        Try
            Dim id As Integer = CInt(GridView1.DataKeys(e.RowIndex).Value)
            Dim row As GridViewRow = GridView1.Rows(e.RowIndex)

            Dim txtFQty As TextBox = CType(row.FindControl("txtFQty"), TextBox)
            Dim txtMQty As TextBox = CType(row.FindControl("txtMQty"), TextBox)
            Dim txtEntry As TextBox = CType(row.FindControl("txtEntryDate"), TextBox)
            Dim chkAlive As CheckBox = CType(row.FindControl("chkAliveEdit"), CheckBox)
            Dim chkEmpty As CheckBox = CType(row.FindControl("chkEmptyEdit"), CheckBox)

            ' ✅ Use parameters instead of string concatenation
            sql = "UPDATE [Shed_Entry_Data]  SET fqty = @fqty, mqty = @mqty,IsAlive = @isAlive , isEmpty = @isEmpty  WHERE id = @id"
            Using conn As New SqlConnection(gmod.Connstr)
                conn.Open()
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@fqty", Val(txtFQty.Text))
                    cmd.Parameters.AddWithValue("@mqty", Val(txtMQty.Text))
                    ' If IsAlive is BIT in DB, store checkbox value as Boolean
                    cmd.Parameters.AddWithValue("@isAlive", chkAlive.Checked)
                    cmd.Parameters.AddWithValue("@isEmpty", chkEmpty.Checked)
                    cmd.Parameters.AddWithValue("@id", id)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            ' ✅ Reset editing mode and rebind grid
            GridView1.EditIndex = -1
            BindGrid()

        Catch ex As Exception
            ' At least log the error for debugging
            Console.WriteLine("Error updating record: " & ex.Message)
        End Try


       
    End Sub

    Protected Sub btnshw_Click(sender As Object, e As EventArgs) Handles btnshw.Click
        BindGrid()
    End Sub
End Class
