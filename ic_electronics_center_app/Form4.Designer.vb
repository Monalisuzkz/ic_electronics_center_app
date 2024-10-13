<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form4
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.pnl_cashier = New Guna.UI.WinForms.GunaPanel()
        Me.GunaLabel6 = New Guna.UI.WinForms.GunaLabel()
        Me.datatable_itemlist = New Guna.UI2.WinForms.Guna2DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GunaLabel1 = New Guna.UI.WinForms.GunaLabel()
        Me.Guna2DataGridView1 = New Guna.UI2.WinForms.Guna2DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GunaLabel2 = New Guna.UI.WinForms.GunaLabel()
        Me.tb_productname = New Guna.UI2.WinForms.Guna2TextBox()
        Me.cmb_servicename = New Guna.UI.WinForms.GunaComboBox()
        Me.GunaLabel3 = New Guna.UI.WinForms.GunaLabel()
        Me.tb_quantity = New Guna.UI2.WinForms.Guna2TextBox()
        Me.GunaLabel4 = New Guna.UI.WinForms.GunaLabel()
        Me.GunaLabel5 = New Guna.UI.WinForms.GunaLabel()
        Me.tb_servicecost = New Guna.UI2.WinForms.Guna2TextBox()
        Me.GunaLabel8 = New Guna.UI.WinForms.GunaLabel()
        Me.tb_producttotalcost = New Guna.UI2.WinForms.Guna2TextBox()
        Me.GunaLabel7 = New Guna.UI.WinForms.GunaLabel()
        Me.Guna2TextBox1 = New Guna.UI2.WinForms.Guna2TextBox()
        Me.btn_addtolist = New Guna.UI2.WinForms.Guna2Button()
        Me.btn_completetransactproducts = New Guna.UI2.WinForms.Guna2Button()
        Me.GunaLabel13 = New Guna.UI.WinForms.GunaLabel()
        Me.tb_change = New Guna.UI2.WinForms.Guna2TextBox()
        Me.tb_moneyamount = New Guna.UI2.WinForms.Guna2TextBox()
        Me.GunaLabel12 = New Guna.UI.WinForms.GunaLabel()
        Me.tb_productamounttopaid = New Guna.UI2.WinForms.Guna2TextBox()
        Me.GunaLabel11 = New Guna.UI.WinForms.GunaLabel()
        Me.pnl_cashier.SuspendLayout()
        CType(Me.datatable_itemlist, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Guna2DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnl_cashier
        '
        Me.pnl_cashier.Controls.Add(Me.btn_completetransactproducts)
        Me.pnl_cashier.Controls.Add(Me.btn_addtolist)
        Me.pnl_cashier.Controls.Add(Me.GunaLabel13)
        Me.pnl_cashier.Controls.Add(Me.Guna2TextBox1)
        Me.pnl_cashier.Controls.Add(Me.tb_change)
        Me.pnl_cashier.Controls.Add(Me.tb_producttotalcost)
        Me.pnl_cashier.Controls.Add(Me.tb_moneyamount)
        Me.pnl_cashier.Controls.Add(Me.GunaLabel7)
        Me.pnl_cashier.Controls.Add(Me.GunaLabel12)
        Me.pnl_cashier.Controls.Add(Me.tb_servicecost)
        Me.pnl_cashier.Controls.Add(Me.tb_productamounttopaid)
        Me.pnl_cashier.Controls.Add(Me.GunaLabel8)
        Me.pnl_cashier.Controls.Add(Me.GunaLabel11)
        Me.pnl_cashier.Controls.Add(Me.GunaLabel5)
        Me.pnl_cashier.Controls.Add(Me.tb_quantity)
        Me.pnl_cashier.Controls.Add(Me.GunaLabel4)
        Me.pnl_cashier.Controls.Add(Me.GunaLabel3)
        Me.pnl_cashier.Controls.Add(Me.cmb_servicename)
        Me.pnl_cashier.Controls.Add(Me.tb_productname)
        Me.pnl_cashier.Controls.Add(Me.GunaLabel2)
        Me.pnl_cashier.Controls.Add(Me.Guna2DataGridView1)
        Me.pnl_cashier.Controls.Add(Me.GunaLabel6)
        Me.pnl_cashier.Controls.Add(Me.GunaLabel1)
        Me.pnl_cashier.Controls.Add(Me.datatable_itemlist)
        Me.pnl_cashier.Location = New System.Drawing.Point(200, 44)
        Me.pnl_cashier.Name = "pnl_cashier"
        Me.pnl_cashier.Size = New System.Drawing.Size(1324, 1001)
        Me.pnl_cashier.TabIndex = 3
        Me.pnl_cashier.Visible = False
        '
        'GunaLabel6
        '
        Me.GunaLabel6.AutoSize = True
        Me.GunaLabel6.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GunaLabel6.Location = New System.Drawing.Point(34, 68)
        Me.GunaLabel6.Name = "GunaLabel6"
        Me.GunaLabel6.Size = New System.Drawing.Size(140, 32)
        Me.GunaLabel6.TabIndex = 5
        Me.GunaLabel6.Text = "PRODUCTS"
        '
        'datatable_itemlist
        '
        Me.datatable_itemlist.AllowUserToAddRows = False
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.White
        Me.datatable_itemlist.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle4
        Me.datatable_itemlist.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.datatable_itemlist.BackgroundColor = System.Drawing.Color.White
        Me.datatable_itemlist.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.datatable_itemlist.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.datatable_itemlist.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(237, Byte), Integer))
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Segoe UI", 10.5!)
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.datatable_itemlist.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.datatable_itemlist.ColumnHeadersHeight = 62
        Me.datatable_itemlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.datatable_itemlist.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3, Me.Column4})
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Segoe UI", 10.5!)
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(243, Byte), Integer))
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.datatable_itemlist.DefaultCellStyle = DataGridViewCellStyle6
        Me.datatable_itemlist.EnableHeadersVisualStyles = False
        Me.datatable_itemlist.GridColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.datatable_itemlist.Location = New System.Drawing.Point(40, 103)
        Me.datatable_itemlist.Name = "datatable_itemlist"
        Me.datatable_itemlist.RowHeadersVisible = False
        Me.datatable_itemlist.RowHeadersWidth = 62
        Me.datatable_itemlist.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.datatable_itemlist.RowTemplate.Height = 28
        Me.datatable_itemlist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.datatable_itemlist.Size = New System.Drawing.Size(794, 380)
        Me.datatable_itemlist.TabIndex = 1
        Me.datatable_itemlist.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.LightGrid
        Me.datatable_itemlist.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White
        Me.datatable_itemlist.ThemeStyle.AlternatingRowsStyle.Font = Nothing
        Me.datatable_itemlist.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty
        Me.datatable_itemlist.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty
        Me.datatable_itemlist.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty
        Me.datatable_itemlist.ThemeStyle.BackColor = System.Drawing.Color.White
        Me.datatable_itemlist.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.datatable_itemlist.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.datatable_itemlist.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.datatable_itemlist.ThemeStyle.HeaderStyle.Font = New System.Drawing.Font("Segoe UI", 10.5!)
        Me.datatable_itemlist.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.Black
        Me.datatable_itemlist.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.datatable_itemlist.ThemeStyle.HeaderStyle.Height = 62
        Me.datatable_itemlist.ThemeStyle.ReadOnly = False
        Me.datatable_itemlist.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White
        Me.datatable_itemlist.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.datatable_itemlist.ThemeStyle.RowsStyle.Font = New System.Drawing.Font("Segoe UI", 10.5!)
        Me.datatable_itemlist.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.Black
        Me.datatable_itemlist.ThemeStyle.RowsStyle.Height = 28
        Me.datatable_itemlist.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.datatable_itemlist.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.Black
        '
        'Column1
        '
        Me.Column1.HeaderText = "Product Name"
        Me.Column1.MinimumWidth = 8
        Me.Column1.Name = "Column1"
        '
        'Column2
        '
        Me.Column2.HeaderText = "Product Price"
        Me.Column2.MinimumWidth = 8
        Me.Column2.Name = "Column2"
        '
        'Column3
        '
        Me.Column3.HeaderText = "Quantity"
        Me.Column3.MinimumWidth = 8
        Me.Column3.Name = "Column3"
        '
        'Column4
        '
        Me.Column4.HeaderText = "Total Cost"
        Me.Column4.MinimumWidth = 8
        Me.Column4.Name = "Column4"
        '
        'GunaLabel1
        '
        Me.GunaLabel1.AutoSize = True
        Me.GunaLabel1.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GunaLabel1.Location = New System.Drawing.Point(34, 515)
        Me.GunaLabel1.Name = "GunaLabel1"
        Me.GunaLabel1.Size = New System.Drawing.Size(121, 32)
        Me.GunaLabel1.TabIndex = 4
        Me.GunaLabel1.Text = "SERVICES"
        '
        'Guna2DataGridView1
        '
        Me.Guna2DataGridView1.AllowUserToAddRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.White
        Me.Guna2DataGridView1.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.Guna2DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.Guna2DataGridView1.BackgroundColor = System.Drawing.Color.White
        Me.Guna2DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Guna2DataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.Guna2DataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(237, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 10.5!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Guna2DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.Guna2DataGridView1.ColumnHeadersHeight = 62
        Me.Guna2DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.Guna2DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2})
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Segoe UI", 10.5!)
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(243, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Guna2DataGridView1.DefaultCellStyle = DataGridViewCellStyle3
        Me.Guna2DataGridView1.EnableHeadersVisualStyles = False
        Me.Guna2DataGridView1.GridColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.Guna2DataGridView1.Location = New System.Drawing.Point(40, 550)
        Me.Guna2DataGridView1.Name = "Guna2DataGridView1"
        Me.Guna2DataGridView1.RowHeadersVisible = False
        Me.Guna2DataGridView1.RowHeadersWidth = 62
        Me.Guna2DataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.Guna2DataGridView1.RowTemplate.Height = 28
        Me.Guna2DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Guna2DataGridView1.Size = New System.Drawing.Size(794, 380)
        Me.Guna2DataGridView1.TabIndex = 6
        Me.Guna2DataGridView1.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.LightGrid
        Me.Guna2DataGridView1.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White
        Me.Guna2DataGridView1.ThemeStyle.AlternatingRowsStyle.Font = Nothing
        Me.Guna2DataGridView1.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty
        Me.Guna2DataGridView1.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty
        Me.Guna2DataGridView1.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty
        Me.Guna2DataGridView1.ThemeStyle.BackColor = System.Drawing.Color.White
        Me.Guna2DataGridView1.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.Guna2DataGridView1.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.Guna2DataGridView1.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.Guna2DataGridView1.ThemeStyle.HeaderStyle.Font = New System.Drawing.Font("Segoe UI", 10.5!)
        Me.Guna2DataGridView1.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.Black
        Me.Guna2DataGridView1.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.Guna2DataGridView1.ThemeStyle.HeaderStyle.Height = 62
        Me.Guna2DataGridView1.ThemeStyle.ReadOnly = False
        Me.Guna2DataGridView1.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White
        Me.Guna2DataGridView1.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.Guna2DataGridView1.ThemeStyle.RowsStyle.Font = New System.Drawing.Font("Segoe UI", 10.5!)
        Me.Guna2DataGridView1.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.Black
        Me.Guna2DataGridView1.ThemeStyle.RowsStyle.Height = 28
        Me.Guna2DataGridView1.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.Guna2DataGridView1.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.Black
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "Service Name"
        Me.DataGridViewTextBoxColumn1.MinimumWidth = 8
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "Service Cost"
        Me.DataGridViewTextBoxColumn2.MinimumWidth = 8
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        '
        'GunaLabel2
        '
        Me.GunaLabel2.AutoSize = True
        Me.GunaLabel2.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.GunaLabel2.Location = New System.Drawing.Point(879, 112)
        Me.GunaLabel2.Name = "GunaLabel2"
        Me.GunaLabel2.Size = New System.Drawing.Size(138, 28)
        Me.GunaLabel2.TabIndex = 8
        Me.GunaLabel2.Text = "Product Name"
        '
        'tb_productname
        '
        Me.tb_productname.BorderRadius = 8
        Me.tb_productname.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.tb_productname.DefaultText = ""
        Me.tb_productname.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.tb_productname.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.tb_productname.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.tb_productname.DisabledState.Parent = Me.tb_productname
        Me.tb_productname.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.tb_productname.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tb_productname.FocusedState.Parent = Me.tb_productname
        Me.tb_productname.ForeColor = System.Drawing.Color.Black
        Me.tb_productname.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tb_productname.HoverState.Parent = Me.tb_productname
        Me.tb_productname.Location = New System.Drawing.Point(1024, 103)
        Me.tb_productname.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.tb_productname.Name = "tb_productname"
        Me.tb_productname.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.tb_productname.PlaceholderText = ""
        Me.tb_productname.ReadOnly = True
        Me.tb_productname.SelectedText = ""
        Me.tb_productname.ShadowDecoration.Parent = Me.tb_productname
        Me.tb_productname.Size = New System.Drawing.Size(261, 45)
        Me.tb_productname.TabIndex = 9
        '
        'cmb_servicename
        '
        Me.cmb_servicename.BackColor = System.Drawing.Color.Transparent
        Me.cmb_servicename.BaseColor = System.Drawing.Color.White
        Me.cmb_servicename.BorderColor = System.Drawing.Color.Silver
        Me.cmb_servicename.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmb_servicename.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_servicename.FocusedColor = System.Drawing.Color.Empty
        Me.cmb_servicename.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.cmb_servicename.ForeColor = System.Drawing.Color.Black
        Me.cmb_servicename.FormattingEnabled = True
        Me.cmb_servicename.Location = New System.Drawing.Point(1024, 175)
        Me.cmb_servicename.Name = "cmb_servicename"
        Me.cmb_servicename.OnHoverItemBaseColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmb_servicename.OnHoverItemForeColor = System.Drawing.Color.White
        Me.cmb_servicename.Size = New System.Drawing.Size(261, 35)
        Me.cmb_servicename.TabIndex = 10
        '
        'GunaLabel3
        '
        Me.GunaLabel3.AutoSize = True
        Me.GunaLabel3.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.GunaLabel3.Location = New System.Drawing.Point(886, 178)
        Me.GunaLabel3.Name = "GunaLabel3"
        Me.GunaLabel3.Size = New System.Drawing.Size(131, 28)
        Me.GunaLabel3.TabIndex = 11
        Me.GunaLabel3.Text = "Service Name"
        '
        'tb_quantity
        '
        Me.tb_quantity.BorderRadius = 8
        Me.tb_quantity.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.tb_quantity.DefaultText = ""
        Me.tb_quantity.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.tb_quantity.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.tb_quantity.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.tb_quantity.DisabledState.Parent = Me.tb_quantity
        Me.tb_quantity.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.tb_quantity.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tb_quantity.FocusedState.Parent = Me.tb_quantity
        Me.tb_quantity.ForeColor = System.Drawing.Color.Black
        Me.tb_quantity.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tb_quantity.HoverState.Parent = Me.tb_quantity
        Me.tb_quantity.Location = New System.Drawing.Point(1254, 329)
        Me.tb_quantity.Margin = New System.Windows.Forms.Padding(5, 7, 5, 7)
        Me.tb_quantity.Name = "tb_quantity"
        Me.tb_quantity.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.tb_quantity.PlaceholderText = ""
        Me.tb_quantity.SelectedText = ""
        Me.tb_quantity.ShadowDecoration.Parent = Me.tb_quantity
        Me.tb_quantity.Size = New System.Drawing.Size(120, 63)
        Me.tb_quantity.TabIndex = 13
        '
        'GunaLabel4
        '
        Me.GunaLabel4.AutoSize = True
        Me.GunaLabel4.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.GunaLabel4.Location = New System.Drawing.Point(929, 243)
        Me.GunaLabel4.Name = "GunaLabel4"
        Me.GunaLabel4.Size = New System.Drawing.Size(88, 28)
        Me.GunaLabel4.TabIndex = 12
        Me.GunaLabel4.Text = "Quantity"
        '
        'GunaLabel5
        '
        Me.GunaLabel5.AutoSize = True
        Me.GunaLabel5.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.GunaLabel5.Location = New System.Drawing.Point(889, 320)
        Me.GunaLabel5.Name = "GunaLabel5"
        Me.GunaLabel5.Size = New System.Drawing.Size(128, 28)
        Me.GunaLabel5.TabIndex = 14
        Me.GunaLabel5.Text = "Product Price"
        '
        'tb_servicecost
        '
        Me.tb_servicecost.BorderRadius = 8
        Me.tb_servicecost.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.tb_servicecost.DefaultText = ""
        Me.tb_servicecost.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.tb_servicecost.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.tb_servicecost.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.tb_servicecost.DisabledState.Parent = Me.tb_servicecost
        Me.tb_servicecost.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.tb_servicecost.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tb_servicecost.FocusedState.Parent = Me.tb_servicecost
        Me.tb_servicecost.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tb_servicecost.HoverState.Parent = Me.tb_servicecost
        Me.tb_servicecost.Location = New System.Drawing.Point(1252, 532)
        Me.tb_servicecost.Margin = New System.Windows.Forms.Padding(5, 7, 5, 7)
        Me.tb_servicecost.Name = "tb_servicecost"
        Me.tb_servicecost.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.tb_servicecost.PlaceholderText = ""
        Me.tb_servicecost.ReadOnly = True
        Me.tb_servicecost.SelectedText = ""
        Me.tb_servicecost.ShadowDecoration.Parent = Me.tb_servicecost
        Me.tb_servicecost.Size = New System.Drawing.Size(122, 63)
        Me.tb_servicecost.TabIndex = 17
        '
        'GunaLabel8
        '
        Me.GunaLabel8.AutoSize = True
        Me.GunaLabel8.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.GunaLabel8.Location = New System.Drawing.Point(899, 382)
        Me.GunaLabel8.Name = "GunaLabel8"
        Me.GunaLabel8.Size = New System.Drawing.Size(118, 28)
        Me.GunaLabel8.TabIndex = 16
        Me.GunaLabel8.Text = "Service Cost"
        '
        'tb_producttotalcost
        '
        Me.tb_producttotalcost.BorderRadius = 8
        Me.tb_producttotalcost.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.tb_producttotalcost.DefaultText = ""
        Me.tb_producttotalcost.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.tb_producttotalcost.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.tb_producttotalcost.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.tb_producttotalcost.DisabledState.Parent = Me.tb_producttotalcost
        Me.tb_producttotalcost.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.tb_producttotalcost.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tb_producttotalcost.FocusedState.Parent = Me.tb_producttotalcost
        Me.tb_producttotalcost.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tb_producttotalcost.HoverState.Parent = Me.tb_producttotalcost
        Me.tb_producttotalcost.Location = New System.Drawing.Point(1024, 457)
        Me.tb_producttotalcost.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.tb_producttotalcost.Name = "tb_producttotalcost"
        Me.tb_producttotalcost.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.tb_producttotalcost.PlaceholderText = ""
        Me.tb_producttotalcost.ReadOnly = True
        Me.tb_producttotalcost.SelectedText = ""
        Me.tb_producttotalcost.ShadowDecoration.Parent = Me.tb_producttotalcost
        Me.tb_producttotalcost.Size = New System.Drawing.Size(100, 45)
        Me.tb_producttotalcost.TabIndex = 19
        '
        'GunaLabel7
        '
        Me.GunaLabel7.AutoSize = True
        Me.GunaLabel7.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.GunaLabel7.Location = New System.Drawing.Point(919, 465)
        Me.GunaLabel7.Name = "GunaLabel7"
        Me.GunaLabel7.Size = New System.Drawing.Size(98, 28)
        Me.GunaLabel7.TabIndex = 18
        Me.GunaLabel7.Text = "Total Cost"
        '
        'Guna2TextBox1
        '
        Me.Guna2TextBox1.BorderRadius = 8
        Me.Guna2TextBox1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.Guna2TextBox1.DefaultText = ""
        Me.Guna2TextBox1.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.Guna2TextBox1.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.Guna2TextBox1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.Guna2TextBox1.DisabledState.Parent = Me.Guna2TextBox1
        Me.Guna2TextBox1.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.Guna2TextBox1.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Guna2TextBox1.FocusedState.Parent = Me.Guna2TextBox1
        Me.Guna2TextBox1.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Guna2TextBox1.HoverState.Parent = Me.Guna2TextBox1
        Me.Guna2TextBox1.Location = New System.Drawing.Point(1024, 310)
        Me.Guna2TextBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Guna2TextBox1.Name = "Guna2TextBox1"
        Me.Guna2TextBox1.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.Guna2TextBox1.PlaceholderText = ""
        Me.Guna2TextBox1.ReadOnly = True
        Me.Guna2TextBox1.SelectedText = ""
        Me.Guna2TextBox1.ShadowDecoration.Parent = Me.Guna2TextBox1
        Me.Guna2TextBox1.Size = New System.Drawing.Size(100, 45)
        Me.Guna2TextBox1.TabIndex = 20
        '
        'btn_addtolist
        '
        Me.btn_addtolist.BorderRadius = 10
        Me.btn_addtolist.CheckedState.Parent = Me.btn_addtolist
        Me.btn_addtolist.CustomImages.Parent = Me.btn_addtolist
        Me.btn_addtolist.FillColor = System.Drawing.Color.Green
        Me.btn_addtolist.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.btn_addtolist.ForeColor = System.Drawing.Color.White
        Me.btn_addtolist.HoverState.Parent = Me.btn_addtolist
        Me.btn_addtolist.Location = New System.Drawing.Point(1009, 525)
        Me.btn_addtolist.Name = "btn_addtolist"
        Me.btn_addtolist.ShadowDecoration.Parent = Me.btn_addtolist
        Me.btn_addtolist.Size = New System.Drawing.Size(135, 45)
        Me.btn_addtolist.TabIndex = 21
        Me.btn_addtolist.Text = "Add to list"
        '
        'btn_completetransactproducts
        '
        Me.btn_completetransactproducts.BorderRadius = 10
        Me.btn_completetransactproducts.CheckedState.Parent = Me.btn_completetransactproducts
        Me.btn_completetransactproducts.CustomImages.Parent = Me.btn_completetransactproducts
        Me.btn_completetransactproducts.FillColor = System.Drawing.Color.Green
        Me.btn_completetransactproducts.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.btn_completetransactproducts.ForeColor = System.Drawing.Color.White
        Me.btn_completetransactproducts.HoverState.Parent = Me.btn_completetransactproducts
        Me.btn_completetransactproducts.Location = New System.Drawing.Point(963, 835)
        Me.btn_completetransactproducts.Name = "btn_completetransactproducts"
        Me.btn_completetransactproducts.ShadowDecoration.Parent = Me.btn_completetransactproducts
        Me.btn_completetransactproducts.Size = New System.Drawing.Size(230, 45)
        Me.btn_completetransactproducts.TabIndex = 21
        Me.btn_completetransactproducts.Text = "Complete Transaction"
        '
        'GunaLabel13
        '
        Me.GunaLabel13.AutoSize = True
        Me.GunaLabel13.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.GunaLabel13.Location = New System.Drawing.Point(944, 771)
        Me.GunaLabel13.Name = "GunaLabel13"
        Me.GunaLabel13.Size = New System.Drawing.Size(78, 28)
        Me.GunaLabel13.TabIndex = 20
        Me.GunaLabel13.Text = "Change"
        '
        'tb_change
        '
        Me.tb_change.BorderRadius = 8
        Me.tb_change.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.tb_change.DefaultText = ""
        Me.tb_change.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.tb_change.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.tb_change.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.tb_change.DisabledState.Parent = Me.tb_change
        Me.tb_change.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.tb_change.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tb_change.FocusedState.Parent = Me.tb_change
        Me.tb_change.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tb_change.HoverState.Parent = Me.tb_change
        Me.tb_change.Location = New System.Drawing.Point(1034, 762)
        Me.tb_change.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.tb_change.Name = "tb_change"
        Me.tb_change.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.tb_change.PlaceholderText = ""
        Me.tb_change.ReadOnly = True
        Me.tb_change.SelectedText = ""
        Me.tb_change.ShadowDecoration.Parent = Me.tb_change
        Me.tb_change.Size = New System.Drawing.Size(100, 45)
        Me.tb_change.TabIndex = 19
        '
        'tb_moneyamount
        '
        Me.tb_moneyamount.BorderRadius = 8
        Me.tb_moneyamount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.tb_moneyamount.DefaultText = ""
        Me.tb_moneyamount.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.tb_moneyamount.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.tb_moneyamount.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.tb_moneyamount.DisabledState.Parent = Me.tb_moneyamount
        Me.tb_moneyamount.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.tb_moneyamount.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tb_moneyamount.FocusedState.Parent = Me.tb_moneyamount
        Me.tb_moneyamount.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tb_moneyamount.HoverState.Parent = Me.tb_moneyamount
        Me.tb_moneyamount.Location = New System.Drawing.Point(1034, 687)
        Me.tb_moneyamount.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.tb_moneyamount.Name = "tb_moneyamount"
        Me.tb_moneyamount.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.tb_moneyamount.PlaceholderText = ""
        Me.tb_moneyamount.SelectedText = ""
        Me.tb_moneyamount.ShadowDecoration.Parent = Me.tb_moneyamount
        Me.tb_moneyamount.Size = New System.Drawing.Size(100, 45)
        Me.tb_moneyamount.TabIndex = 18
        '
        'GunaLabel12
        '
        Me.GunaLabel12.AutoSize = True
        Me.GunaLabel12.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.GunaLabel12.Location = New System.Drawing.Point(973, 696)
        Me.GunaLabel12.Name = "GunaLabel12"
        Me.GunaLabel12.Size = New System.Drawing.Size(53, 28)
        Me.GunaLabel12.TabIndex = 17
        Me.GunaLabel12.Text = "Cash"
        '
        'tb_productamounttopaid
        '
        Me.tb_productamounttopaid.BorderRadius = 8
        Me.tb_productamounttopaid.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.tb_productamounttopaid.DefaultText = ""
        Me.tb_productamounttopaid.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.tb_productamounttopaid.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.tb_productamounttopaid.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.tb_productamounttopaid.DisabledState.Parent = Me.tb_productamounttopaid
        Me.tb_productamounttopaid.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.tb_productamounttopaid.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tb_productamounttopaid.FocusedState.Parent = Me.tb_productamounttopaid
        Me.tb_productamounttopaid.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tb_productamounttopaid.HoverState.Parent = Me.tb_productamounttopaid
        Me.tb_productamounttopaid.Location = New System.Drawing.Point(1034, 612)
        Me.tb_productamounttopaid.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.tb_productamounttopaid.Name = "tb_productamounttopaid"
        Me.tb_productamounttopaid.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.tb_productamounttopaid.PlaceholderText = ""
        Me.tb_productamounttopaid.ReadOnly = True
        Me.tb_productamounttopaid.SelectedText = ""
        Me.tb_productamounttopaid.ShadowDecoration.Parent = Me.tb_productamounttopaid
        Me.tb_productamounttopaid.Size = New System.Drawing.Size(100, 45)
        Me.tb_productamounttopaid.TabIndex = 16
        '
        'GunaLabel11
        '
        Me.GunaLabel11.AutoSize = True
        Me.GunaLabel11.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.GunaLabel11.Location = New System.Drawing.Point(899, 621)
        Me.GunaLabel11.Name = "GunaLabel11"
        Me.GunaLabel11.Size = New System.Drawing.Size(130, 28)
        Me.GunaLabel11.TabIndex = 15
        Me.GunaLabel11.Text = "Total Amount"
        '
        'Form4
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1724, 1088)
        Me.Controls.Add(Me.pnl_cashier)
        Me.Name = "Form4"
        Me.Text = "Form4"
        Me.pnl_cashier.ResumeLayout(False)
        Me.pnl_cashier.PerformLayout()
        CType(Me.datatable_itemlist, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Guna2DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnl_cashier As GunaPanel
    Friend WithEvents GunaLabel6 As GunaLabel
    Friend WithEvents datatable_itemlist As Guna2DataGridView
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents GunaLabel1 As GunaLabel
    Friend WithEvents Guna2DataGridView1 As Guna2DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents GunaLabel2 As GunaLabel
    Friend WithEvents tb_productname As Guna2TextBox
    Friend WithEvents cmb_servicename As GunaComboBox
    Friend WithEvents GunaLabel3 As GunaLabel
    Friend WithEvents tb_quantity As Guna2TextBox
    Friend WithEvents GunaLabel4 As GunaLabel
    Friend WithEvents GunaLabel5 As GunaLabel
    Friend WithEvents tb_servicecost As Guna2TextBox
    Friend WithEvents GunaLabel8 As GunaLabel
    Friend WithEvents tb_producttotalcost As Guna2TextBox
    Friend WithEvents GunaLabel7 As GunaLabel
    Friend WithEvents Guna2TextBox1 As Guna2TextBox
    Friend WithEvents btn_addtolist As Guna2Button
    Friend WithEvents btn_completetransactproducts As Guna2Button
    Friend WithEvents GunaLabel13 As GunaLabel
    Friend WithEvents tb_change As Guna2TextBox
    Friend WithEvents tb_moneyamount As Guna2TextBox
    Friend WithEvents GunaLabel12 As GunaLabel
    Friend WithEvents tb_productamounttopaid As Guna2TextBox
    Friend WithEvents GunaLabel11 As GunaLabel
End Class
