<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.Guna2HtmlLabel1 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.tb_username = New Guna.UI2.WinForms.Guna2TextBox()
        Me.tb_password = New Guna.UI2.WinForms.Guna2TextBox()
        Me.btn_login = New Guna.UI2.WinForms.Guna2Button()
        Me.GunaLabel1 = New Guna.UI.WinForms.GunaLabel()
        Me.GunaLabel2 = New Guna.UI.WinForms.GunaLabel()
        Me.btn_exit = New Guna.UI2.WinForms.Guna2Button()
        Me.SuspendLayout()
        '
        'Guna2HtmlLabel1
        '
        Me.Guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent
        Me.Guna2HtmlLabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2HtmlLabel1.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.Guna2HtmlLabel1.Location = New System.Drawing.Point(235, 90)
        Me.Guna2HtmlLabel1.Name = "Guna2HtmlLabel1"
        Me.Guna2HtmlLabel1.Size = New System.Drawing.Size(108, 39)
        Me.Guna2HtmlLabel1.TabIndex = 0
        Me.Guna2HtmlLabel1.Text = "LOGIN"
        '
        'tb_username
        '
        Me.tb_username.BackColor = System.Drawing.Color.Transparent
        Me.tb_username.BorderRadius = 8
        Me.tb_username.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.tb_username.DefaultText = ""
        Me.tb_username.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.tb_username.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.tb_username.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.tb_username.DisabledState.Parent = Me.tb_username
        Me.tb_username.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.tb_username.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tb_username.FocusedState.Parent = Me.tb_username
        Me.tb_username.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tb_username.ForeColor = System.Drawing.Color.Black
        Me.tb_username.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tb_username.HoverState.Parent = Me.tb_username
        Me.tb_username.Location = New System.Drawing.Point(136, 195)
        Me.tb_username.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.tb_username.Name = "tb_username"
        Me.tb_username.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.tb_username.PlaceholderText = ""
        Me.tb_username.SelectedText = ""
        Me.tb_username.ShadowDecoration.Parent = Me.tb_username
        Me.tb_username.Size = New System.Drawing.Size(300, 50)
        Me.tb_username.TabIndex = 3
        '
        'tb_password
        '
        Me.tb_password.BackColor = System.Drawing.Color.Transparent
        Me.tb_password.BorderRadius = 8
        Me.tb_password.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.tb_password.DefaultText = ""
        Me.tb_password.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.tb_password.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.tb_password.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.tb_password.DisabledState.Parent = Me.tb_password
        Me.tb_password.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.tb_password.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tb_password.FocusedState.Parent = Me.tb_password
        Me.tb_password.Font = New System.Drawing.Font("MS UI Gothic", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tb_password.ForeColor = System.Drawing.Color.Black
        Me.tb_password.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tb_password.HoverState.Parent = Me.tb_password
        Me.tb_password.Location = New System.Drawing.Point(135, 303)
        Me.tb_password.Margin = New System.Windows.Forms.Padding(7, 8, 7, 8)
        Me.tb_password.Name = "tb_password"
        Me.tb_password.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.tb_password.PlaceholderText = ""
        Me.tb_password.SelectedText = ""
        Me.tb_password.ShadowDecoration.Parent = Me.tb_password
        Me.tb_password.Size = New System.Drawing.Size(301, 50)
        Me.tb_password.TabIndex = 4
        Me.tb_password.UseSystemPasswordChar = True
        '
        'btn_login
        '
        Me.btn_login.BackColor = System.Drawing.Color.Transparent
        Me.btn_login.BorderRadius = 10
        Me.btn_login.CheckedState.Parent = Me.btn_login
        Me.btn_login.CustomImages.Parent = Me.btn_login
        Me.btn_login.FillColor = System.Drawing.Color.MediumSeaGreen
        Me.btn_login.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_login.ForeColor = System.Drawing.Color.White
        Me.btn_login.HoverState.Parent = Me.btn_login
        Me.btn_login.Location = New System.Drawing.Point(135, 390)
        Me.btn_login.Name = "btn_login"
        Me.btn_login.ShadowDecoration.Parent = Me.btn_login
        Me.btn_login.Size = New System.Drawing.Size(301, 45)
        Me.btn_login.TabIndex = 5
        Me.btn_login.Text = "LOGIN"
        '
        'GunaLabel1
        '
        Me.GunaLabel1.AutoSize = True
        Me.GunaLabel1.BackColor = System.Drawing.Color.Transparent
        Me.GunaLabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.GunaLabel1.Location = New System.Drawing.Point(135, 164)
        Me.GunaLabel1.Name = "GunaLabel1"
        Me.GunaLabel1.Size = New System.Drawing.Size(108, 25)
        Me.GunaLabel1.TabIndex = 6
        Me.GunaLabel1.Text = "Username:"
        '
        'GunaLabel2
        '
        Me.GunaLabel2.AutoSize = True
        Me.GunaLabel2.BackColor = System.Drawing.Color.Transparent
        Me.GunaLabel2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.GunaLabel2.Location = New System.Drawing.Point(135, 270)
        Me.GunaLabel2.Name = "GunaLabel2"
        Me.GunaLabel2.Size = New System.Drawing.Size(104, 25)
        Me.GunaLabel2.TabIndex = 7
        Me.GunaLabel2.Text = "Password:"
        '
        'btn_exit
        '
        Me.btn_exit.BackColor = System.Drawing.Color.Transparent
        Me.btn_exit.BorderRadius = 10
        Me.btn_exit.CheckedState.Parent = Me.btn_exit
        Me.btn_exit.CustomImages.Parent = Me.btn_exit
        Me.btn_exit.FillColor = System.Drawing.Color.DimGray
        Me.btn_exit.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btn_exit.ForeColor = System.Drawing.Color.White
        Me.btn_exit.HoverState.Parent = Me.btn_exit
        Me.btn_exit.Location = New System.Drawing.Point(512, 12)
        Me.btn_exit.Name = "btn_exit"
        Me.btn_exit.ShadowDecoration.Parent = Me.btn_exit
        Me.btn_exit.Size = New System.Drawing.Size(54, 45)
        Me.btn_exit.TabIndex = 8
        Me.btn_exit.Text = "Exit"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 25.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSlateGray
        Me.BackgroundImage = Global.ic_electronics_center_app.My.Resources.Resources.electronic_background_vector_52526152
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(578, 544)
        Me.ControlBox = False
        Me.Controls.Add(Me.btn_exit)
        Me.Controls.Add(Me.GunaLabel2)
        Me.Controls.Add(Me.GunaLabel1)
        Me.Controls.Add(Me.btn_login)
        Me.Controls.Add(Me.tb_password)
        Me.Controls.Add(Me.tb_username)
        Me.Controls.Add(Me.Guna2HtmlLabel1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "IC Electronics Center Login"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Guna2HtmlLabel1 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents tb_username As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents tb_password As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents btn_login As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents GunaLabel1 As Guna.UI.WinForms.GunaLabel
    Friend WithEvents GunaLabel2 As Guna.UI.WinForms.GunaLabel
    Friend WithEvents btn_exit As Guna2Button
End Class
