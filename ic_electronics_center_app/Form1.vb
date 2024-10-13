Imports System.Data.SQLite
Imports System.IO

Public Class Form1

    Dim dbPath As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ic_electronics.db")
    Dim connectionString As String = $"Data Source={dbPath};Version=3;"

    Private Sub btn_login_Click(sender As Object, e As EventArgs) Handles btn_login.Click
        Using connection As New SQLiteConnection(connectionString)
            Try
                connection.Open()

                Dim query As String = "SELECT 'admin' AS user_type " &
                      "FROM user_admin " &
                      "WHERE username = @username AND password = @password " &
                      "UNION ALL " &
                      "SELECT 'staff' AS user_type " &
                      "FROM user_staff " &
                      "WHERE username = @username AND password = @password;"

                Using cmd As New SQLiteCommand(query, connection)
                    cmd.Parameters.AddWithValue("@username", tb_username.Text)
                    cmd.Parameters.AddWithValue("@password", tb_password.Text)

                    Using reader As SQLiteDataReader = cmd.ExecuteReader()
                        If reader.HasRows Then
                            reader.Read()
                            Dim userType As String = reader("user_type").ToString()

                            MessageBox.Show("Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                            If userType = "admin" Then
                                Dim adminForm As New Form3
                                adminForm.Show()
                                Me.Hide()
                            ElseIf userType = "staff" Then
                                Dim staffForm As New Form2
                                staffForm.Show()
                                Me.Hide()
                            End If
                        Else
                            MessageBox.Show("Invalid username or password. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show(dbPath)
                MessageBox.Show("An error occurred: " & ex.Message & vbCrLf & ex.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Environment.Exit(0)
            End Try
        End Using
    End Sub

    Private Sub btn_exit_Click(sender As Object, e As EventArgs) Handles btn_exit.Click
        Application.Exit()
    End Sub
End Class
