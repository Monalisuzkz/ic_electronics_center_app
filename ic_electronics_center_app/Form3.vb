Imports System.Data.SQLite
Imports System.Drawing.Printing
Imports System.Runtime.InteropServices.ComTypes
Imports System.Text
Imports System.Runtime.CompilerServices
Imports System.IO
Imports System.Windows.Forms.DataVisualization.Charting

Public Class Form3
    Dim dbPath As String = "C:\Users\Admin\source\repos\ic_electronics_center_app\ic_electronics_center_app\ic_electronics.db"
    Dim connectionString As String = $"Data Source={dbPath};Version=3;"

    Dim connection As SQLiteConnection = New SQLiteConnection(connectionString)

    Private conn As SQLiteConnection

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        conn = New SQLiteConnection(connectionString)

        Try

            conn.Open()
            Timer1.Start()
            idleTimer.Interval = 50000
            idleTimer.Start()
            LoadSalesReport()
            datatable_itemlistproducts.Columns(0).ReadOnly = True
            datatable_itemlistproducts.Columns(1).ReadOnly = True
            datatable_itemlistproducts.Columns(3).ReadOnly = True
            Dim chk As New DataGridViewCheckBoxColumn()
            chk.HeaderText = "Select"
            chk.Name = "chk"
            datatable_itemlistproducts.Columns.Add(chk)

            LoadSuppliers()
            LoadProducts()
            LoadReturnofProducts()
            LoadServiceTransactions()
            LoadProductTransactions()
            LoadArchivedProducts()
            pnl_returnofproducts.Location = New Point(286, 0)
            LoadArchivedReturnedProducts()
            LoadReturnedProducts()
            LoadLowStockProducts()
            LoadTop5SellingProducts()
            datatable_top5sellingproducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

            SetupDailyTransactionHistoryDataGridView()
            SetupDailyTransactionHistoryDataGridView()
            SetupWeeklyTransactionHistoryDataGridView()
            SetupMonthlyTransactionHistoryDataGridView()

            Dim startDate As DateTime = New DateTime(2023, 1, 1)
            Dim endDate As DateTime = DateTime.Now
            SetupChart()
            LoadRecentTransactions()
            LoadSalesByDay()
            LoadSalesByWeek()
            LoadSalesByMonth()
            InitializeChart()
            PopulateROPTransactionIdComboBox()
            PopulateROPProductComboBox()
            PopulateServiceComboBox()
            tb_ropreason.Items.Add("Defective Item")
            tb_ropreason.Items.Add("Wrong Item")
            LoadServiceSalesReport()
        Catch ex As Exception
            MessageBox.Show("An error occurred while connecting to the database: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        dashboard_datentime.Text = DateTime.Now.ToString("MMMM dd, yyyy hh:mm tt")
    End Sub

    Private WithEvents idleTimer As New Timer()
    Private idleTime As Integer = 0
    Private idleLimit As Integer = 300

    Private Sub Form3_MouseMove(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove
        ResetIdleTimer()
    End Sub

    Private Sub Form3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles MyBase.KeyPress
        ResetIdleTimer()
    End Sub

    Private Sub ResetIdleTimer()
        idleTime = 0
    End Sub

    Private Sub idleTimer_Tick(sender As Object, e As EventArgs) Handles idleTimer.Tick
        idleTime += 1

        If idleTime >= idleLimit Then
            idleTimer.Stop()
            MessageBox.Show("You have been logged out due to inactivity.", "Idle Timeout", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Dim loginForm As New Form1()
            loginForm.Show()
            Me.Close()
        End If
    End Sub

    Private Sub Form3_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        idleTimer.Stop()
    End Sub

    Private Sub btn_dashboard_Click(sender As Object, e As EventArgs) Handles btn_dashboard.Click
        pnl_dashboard.Visible = True
        pnl_cashier.Visible = False
        pnl_products.Visible = False
        pnl_suppliers.Visible = False
        pnl_returnofproducts.Visible = False
        pnl_transactionrecords.Visible = False
        pnl_salesreport.Visible = False
        pnl_archive.Visible = False
    End Sub

    Private Sub btn_inventory_Click(sender As Object, e As EventArgs) Handles btn_inventory.Click
        pnl_dashboard.Visible = False
        pnl_cashier.Visible = False
        pnl_products.Visible = False
        pnl_suppliers.Visible = False
        pnl_returnofproducts.Visible = False
        pnl_transactionrecords.Visible = False
        pnl_salesreport.Visible = False
        pnl_archive.Visible = False
    End Sub

    Private Sub btn_cashier_Click(sender As Object, e As EventArgs) Handles btn_cashier.Click
        pnl_dashboard.Visible = False
        pnl_cashier.Visible = True
        pnl_products.Visible = False
        pnl_suppliers.Visible = False
        pnl_returnofproducts.Visible = False
        pnl_transactionrecords.Visible = False
        pnl_salesreport.Visible = False
        pnl_archive.Visible = False
    End Sub

    Private Sub btn_products_Click(sender As Object, e As EventArgs) Handles btn_products.Click
        pnl_dashboard.Visible = False
        pnl_products.Visible = True
        pnl_cashier.Visible = False
        pnl_suppliers.Visible = False
        pnl_returnofproducts.Visible = False
        pnl_transactionrecords.Visible = False
        pnl_salesreport.Visible = False
        pnl_archive.Visible = False
    End Sub
    Private Sub btn_suppliers_Click(sender As Object, e As EventArgs) Handles btn_suppliers.Click
        pnl_dashboard.Visible = False
        pnl_products.Visible = False
        pnl_cashier.Visible = False
        pnl_suppliers.Visible = True
        pnl_returnofproducts.Visible = False
        pnl_transactionrecords.Visible = False
        pnl_salesreport.Visible = False
        pnl_archive.Visible = False
    End Sub
    Private Sub btn_returnofproducts_Click(sender As Object, e As EventArgs) Handles btn_returnofproducts.Click
        pnl_dashboard.Visible = False
        pnl_returnofproducts.Visible = True
        pnl_products.Visible = False
        pnl_cashier.Visible = False
        pnl_suppliers.Visible = False
        pnl_transactionrecords.Visible = False
        pnl_salesreport.Visible = False
        pnl_archive.Visible = False
    End Sub
    Private Sub btn_transactionrecords_Click(sender As Object, e As EventArgs) Handles btn_transactionrecords.Click
        pnl_dashboard.Visible = False
        pnl_cashier.Visible = False
        pnl_returnofproducts.Visible = False
        pnl_suppliers.Visible = False
        pnl_products.Visible = False
        pnl_transactionrecords.Visible = True
        pnl_salesreport.Visible = False
        pnl_archive.Visible = False
    End Sub

    Private Sub btn_salesreport_Click(sender As Object, e As EventArgs) Handles btn_salesreport.Click
        pnl_dashboard.Visible = False
        pnl_cashier.Visible = False
        pnl_returnofproducts.Visible = False
        pnl_suppliers.Visible = False
        pnl_products.Visible = False
        pnl_transactionrecords.Visible = False
        pnl_salesreport.Visible = True
        pnl_archive.Visible = False

    End Sub

    Private Sub btn_archive_Click(sender As Object, e As EventArgs) Handles btn_archive.Click
        pnl_dashboard.Visible = False
        pnl_cashier.Visible = False
        pnl_returnofproducts.Visible = False
        pnl_suppliers.Visible = False
        pnl_products.Visible = False
        pnl_transactionrecords.Visible = False
        pnl_salesreport.Visible = False
        pnl_archive.Visible = True
    End Sub
    Private Sub btn_archivesuppliers_Click(sender As Object, e As EventArgs)
        pnl_dashboard.Visible = False
        pnl_cashier.Visible = False
        pnl_returnofproducts.Visible = False
        pnl_suppliers.Visible = False
        pnl_products.Visible = False
        pnl_transactionrecords.Visible = False
        pnl_salesreport.Visible = False
        pnl_archive.Visible = False
    End Sub
    Private Sub btn_logout_Click(sender As Object, e As EventArgs) Handles btn_logout.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to log out?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            Dim loginForm As New Form1
            loginForm.Show()

            Me.Hide()
        End If
    End Sub

    Private Sub LoadProductTransactions()
        Dim dbPath As String = "C:\Users\Admin\source\repos\ic_electronics_center_app\ic_electronics_center_app\ic_electronics.db"

        Dim connectionString As String = $"Data Source={dbPath};Version=3;"

        Using connection As New SQLiteConnection(connectionString)
            Try
                connection.Open()

                Dim query As String = "SELECT transaction_id, product_name, unit_cost, quantity, total_cost, total_amount, total_change, transaction_date_time FROM product_transactions"
                Dim adapter As New SQLiteDataAdapter(query, connection)
                Dim table As New DataTable()

                adapter.Fill(table)
                datatable_producttransactions.DataSource = table

                datatable_producttransactions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
                datatable_producttransactions.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells

                datatable_producttransactions.Columns("product_name").Width = 300
                datatable_producttransactions.Columns("unit_cost").Width = 200
                datatable_producttransactions.Columns("quantity").Width = 50
                datatable_producttransactions.Columns("total_cost").Width = 100
                datatable_producttransactions.Columns("total_amount").Width = 100
                datatable_producttransactions.Columns("total_change").Width = 100
                datatable_producttransactions.Columns("transaction_date_time").Width = 150

                ' Set the wrap mode for product name
                datatable_producttransactions.Columns("product_name").DefaultCellStyle.WrapMode = DataGridViewTriState.True

                ' Format numeric columns to display two decimal places
                datatable_producttransactions.Columns("unit_cost").DefaultCellStyle.Format = "F2"
                datatable_producttransactions.Columns("total_cost").DefaultCellStyle.Format = "F2"
                datatable_producttransactions.Columns("total_amount").DefaultCellStyle.Format = "F2"
                datatable_producttransactions.Columns("total_change").DefaultCellStyle.Format = "F2"

            Catch ex As SQLiteException
                MessageBox.Show("Database connection error: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub LoadServiceTransactions()
        Dim dbPath As String = "C:\Users\Admin\source\repos\ic_electronics_center_app\ic_electronics_center_app\ic_electronics.db"
        Dim connectionString As String = $"Data Source={dbPath};Version=3;"

        Using connection As New SQLiteConnection(connectionString)
            Try
                connection.Open()

                ' Exclude supplier_id from the selection
                Dim query As String = "SELECT transaction_id, service_cost, amount_paid, change_given, transaction_date_time, service_name FROM service_transactions"
                Dim adapter As New SQLiteDataAdapter(query, connection)
                Dim table As New DataTable()

                adapter.Fill(table)
                datatable_servicetransactions.DataSource = table

                ' Center-align the column headers
                datatable_servicetransactions.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                ' Center-align the data in all columns
                For Each column As DataGridViewColumn In datatable_servicetransactions.Columns
                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Next

                ' Format the numeric columns to two decimal places
                If datatable_servicetransactions.Columns.Contains("service_cost") Then
                    datatable_servicetransactions.Columns("service_cost").DefaultCellStyle.Format = "F2"
                End If

                If datatable_servicetransactions.Columns.Contains("total_service_amount") Then
                    datatable_servicetransactions.Columns("total_service_amount").DefaultCellStyle.Format = "F2"
                End If

                ' Format amount_paid and change_given to two decimal places
                If datatable_servicetransactions.Columns.Contains("amount_paid") Then
                    datatable_servicetransactions.Columns("amount_paid").DefaultCellStyle.Format = "F2"
                End If

                If datatable_servicetransactions.Columns.Contains("change_given") Then
                    datatable_servicetransactions.Columns("change_given").DefaultCellStyle.Format = "F2"
                End If

            Catch ex As SQLiteException
                MessageBox.Show("Database connection error: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub


    Private Sub LoadReturnofProducts()
        datatable_returnofproducts.DataSource = Nothing

        Dim query As String = "SELECT * FROM return_of_products"
        Dim dbPath As String = "C:\Users\Admin\source\repos\ic_electronics_center_app\ic_electronics_center_app\ic_electronics.db"
        Dim connectionString As String = $"Data Source={dbPath};Version=3;"

        Using conn As New SQLiteConnection(connectionString)
            Using cmd As New SQLiteCommand(query, conn)
                conn.Open()
                Using reader As SQLiteDataReader = cmd.ExecuteReader()
                    Dim dt As New DataTable()
                    dt.Load(reader)
                    datatable_returnofproducts.DataSource = dt
                End Using
            End Using
        End Using

        ' Add checkbox column if it doesn't already exist
        If datatable_returnofproducts.Columns.Contains("chkSelect") = False Then
            Dim chkColumn As New DataGridViewCheckBoxColumn()
            chkColumn.HeaderText = "Select"
            chkColumn.Name = "chkSelect"
            datatable_returnofproducts.Columns.Insert(0, chkColumn)
        End If

        ' Format numeric columns with two decimal places
        If datatable_returnofproducts.Columns.Contains("unit_cost") Then
            datatable_returnofproducts.Columns("unit_cost").DefaultCellStyle.Format = "F2"
        End If

        If datatable_returnofproducts.Columns.Contains("refund_amount") Then
            datatable_returnofproducts.Columns("refund_amount").DefaultCellStyle.Format = "F2"
        End If

        datatable_returnofproducts.AutoResizeColumns()
    End Sub


    Private originalProductList As New List(Of String)()

    Private Sub LoadProducts()
        datatable_products.DataSource = Nothing
        cmb_productname.Items.Clear()

        Dim query As String = "SELECT product_id, product_name, supplier_id, unit_price, quantity_in_stock, unit_of_measurement, total_cost, remaining_stock_value, sold FROM products"
        Dim dbPath As String = "C:\Users\Admin\source\repos\ic_electronics_center_app\ic_electronics_center_app\ic_electronics.db"
        Dim connectionString As String = $"Data Source={dbPath};Version=3;"

        Using conn As New SQLiteConnection(connectionString)
            Using cmd As New SQLiteCommand(query, conn)
                conn.Open()
                Using reader As SQLiteDataReader = cmd.ExecuteReader()
                    Dim dt As New DataTable()
                    dt.Load(reader)

                    ' Populate the ComboBox with product names
                    For Each row As DataRow In dt.Rows
                        Dim productItem As New Product With {
                        .ProductID = CInt(row("product_id")),
                        .ProductName = row("product_name").ToString()
                    }
                        cmb_productname.Items.Add(productItem)
                    Next

                    datatable_products.DataSource = dt
                End Using
            End Using
        End Using

        ' Add checkbox column if it doesn't already exist
        If datatable_products.Columns.Contains("chkSelect") = False Then
            Dim chkColumn As New DataGridViewCheckBoxColumn()
            chkColumn.HeaderText = "Select"
            chkColumn.Name = "chkSelect"
            datatable_products.Columns.Insert(0, chkColumn)
        End If

        ' Format relevant columns with two decimal places
        Dim decimalColumns As String() = {"unit_price", "quantity_in_stock", "total_cost", "remaining_stock_value"}
        For Each colName As String In decimalColumns
            If datatable_products.Columns.Contains(colName) Then
                datatable_products.Columns(colName).DefaultCellStyle.Format = "F2"
            End If
        Next

        datatable_products.AutoResizeColumns()
    End Sub

    Public Class Product
        Public Property ProductID As Integer
        Public Property ProductName As String

        Public Overrides Function ToString() As String
            Return ProductName
        End Function
    End Class

    Public Class Supplier
        Public Property SupplierID As Integer
        Public Property CompanyName As String

        Public Overrides Function ToString() As String
            Return CompanyName
        End Function
    End Class

    Private Sub LoadSuppliers()
        datatable_suppliers.DataSource = Nothing
        cmb_supplierp.Items.Clear()

        Dim query As String = "SELECT supplier_id, company_name, contact_person, contact_number, address, note FROM suppliers"
        Using conn As New SQLiteConnection(connectionString)
            Using cmd As New SQLiteCommand(query, conn)
                conn.Open()
                Using reader As SQLiteDataReader = cmd.ExecuteReader()

                    Dim dt As New DataTable()
                    dt.Load(reader)

                    For Each row As DataRow In dt.Rows
                        Dim supplierItem As New Supplier With {
                    .SupplierID = CInt(row("supplier_id")),
                    .CompanyName = row("company_name").ToString()
                }
                        cmb_supplierp.Items.Add(supplierItem)
                    Next

                    datatable_suppliers.DataSource = dt
                End Using
            End Using
        End Using

        If datatable_suppliers.Columns.Contains("chkSelect") = False Then
            Dim chkColumn As New DataGridViewCheckBoxColumn()
            chkColumn.HeaderText = "Select"
            chkColumn.Name = "chkSelect"
            datatable_suppliers.Columns.Insert(0, chkColumn)
        End If

        datatable_suppliers.AutoResizeColumns()
    End Sub

    Private Sub PopulateROPTransactionIdComboBox()
        cmb_roptransactionid.Items.Clear()

        Using conn As New SQLiteConnection(connectionString)
            Dim query As String = "SELECT transaction_id FROM product_transactions"
            Using cmd As New SQLiteCommand(query, conn)
                conn.Open()
                Using reader As SQLiteDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        cmb_roptransactionid.Items.Add(reader("transaction_id").ToString())
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub PopulateROPProductComboBox()
        Using conn As New SQLiteConnection(connectionString)
            Try
                conn.Open()
                Dim query As String = "SELECT product_name FROM products"
                Dim cmd As New SQLiteCommand(query, conn)
                Dim reader As SQLiteDataReader = cmd.ExecuteReader()

                While reader.Read()

                    tb_ropproductname.Items.Add(reader("product_name").ToString())
                End While
            Catch ex As SQLiteException
                MessageBox.Show("Error: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub PopulateServiceComboBox()
        Using conn As New SQLiteConnection(connectionString)
            Try
                conn.Open()
                Dim query As String = "SELECT service_name FROM services"
                Dim cmd As New SQLiteCommand(query, conn)
                Dim reader As SQLiteDataReader = cmd.ExecuteReader()

                While reader.Read()
                    cmb_servicename.Items.Add(reader("service_name").ToString())
                End While
            Catch ex As SQLiteException
                MessageBox.Show("Error: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub LoadProductDetails(productId As Integer)
        Dim query As String = "SELECT product_name, unit_price, quantity_in_stock FROM products WHERE product_id = @productId"
        Using conn As New SQLiteConnection(connectionString)
            Using cmd As New SQLiteCommand(query, conn)
                cmd.Parameters.AddWithValue("@productId", productId)
                conn.Open()
                Using reader As SQLiteDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        cmb_productname.Text = reader("product_name").ToString()

                        ' Format the unit price to two decimal places
                        Dim unitPrice As Decimal = Decimal.Parse(reader("unit_price").ToString())
                        tb_productprice.Text = unitPrice.ToString("F2") ' Format as "99.00"

                        tb_quantity.Clear()
                        UpdateTotalCostBasedOnQuantity()
                    End If
                End Using
            End Using
        End Using
    End Sub


    Private Sub cmb_productname_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_productname.SelectedIndexChanged

        If cmb_productname.SelectedItem IsNot Nothing Then
            Dim selectedProduct As Product = CType(cmb_productname.SelectedItem, Product)
            Dim selectedProductId As Integer = selectedProduct.ProductID

            If GetQuantityInStock(selectedProduct.ProductName) = 0 Then
                MessageBox.Show("This product is out of stock and cannot be selected.", "Out of Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                ClearProductFields()
                cmb_productname.SelectedIndex = -1
                Return
            End If

            LoadProductDetails(selectedProductId)
        Else
            ClearProductFields()
        End If
    End Sub

    Private Function GetQuantityInStock(productName As String) As Integer
        Dim dbPath As String = "C:\Users\Admin\source\repos\ic_electronics_center_app\ic_electronics_center_app\ic_electronics.db"

        Dim connectionString As String = $"Data Source={dbPath};Version=3;"
        Dim query As String = "SELECT quantity_in_stock FROM products WHERE product_name = @productName"

        Using connection As New SQLiteConnection(connectionString)
            Dim command As New SQLiteCommand(query, connection)
            command.Parameters.AddWithValue("@productName", productName)

            Try
                connection.Open()
                Dim result = command.ExecuteScalar()
                If result IsNot Nothing Then
                    Return Convert.ToInt32(result)
                Else
                    Return 0
                End If
            Catch ex As SQLiteException
                MessageBox.Show("An error occurred while checking stock: " & ex.Message)
                Return 0
            End Try
        End Using
    End Function


    Private Sub cmb_supplierp_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_supplierp.SelectedIndexChanged
        If cmb_supplierp.SelectedItem IsNot Nothing Then
            Dim selectedSupplier As Supplier = CType(cmb_supplierp.SelectedItem, Supplier)
            Dim selectedSupplierId As Integer = selectedSupplier.SupplierID
            LoadProductDetails(selectedSupplierId)
        Else
        End If
    End Sub

    Private Sub ClearProductFields()
        tb_productprice.Clear()
    End Sub

    Private Sub LoadProductDetails(productName As String)
        conn = New SQLiteConnection(connectionString)

        Try
            conn.Open()
            Dim query As String = "SELECT unit_price, quantity_in_stock FROM products WHERE product_name = @product_name"
            Dim cmd As New SQLiteCommand(query, conn)
            cmd.Parameters.AddWithValue("@product_name", productName)

            Dim reader As SQLiteDataReader = cmd.ExecuteReader()

            If reader.Read() Then
                tb_productprice.Text = reader("unit_price").ToString()
            End If
        Catch ex As SQLiteException
            MessageBox.Show("Error: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub tb_servicemoneyamount_TextChanged(sender As Object, e As EventArgs) Handles tb_servicemoneyamount.TextChanged
        CalculateServiceChange()
    End Sub

    Private Sub txt_totalearned_TextChanged(sender As Object, e As EventArgs) Handles txt_totalearned.TextChanged
        Dim dbPath As String = "C:\Users\Admin\source\repos\ic_electronics_center_app\ic_electronics_center_app\ic_electronics.db"
        Dim connectionString As String = $"Data Source={dbPath};Version=3;"
        Dim connection As SQLiteConnection = New SQLiteConnection(connectionString)

        Try
            connection.Open()
            Dim query As String = "SELECT (SELECT SUM(total_cost) FROM product_transactions) + " &
                           "(SELECT SUM(service_cost) FROM service_transactions) AS total_earned;"
            Dim cmd As SQLiteCommand = New SQLiteCommand(query, connection)

            Dim result = cmd.ExecuteScalar()
            If result IsNot DBNull.Value Then
                txt_totalearned.Text = Convert.ToDecimal(result).ToString("F2") ' Format to 2 decimal places
            Else
                txt_totalearned.Text = "0.00" ' Default to 0.00
            End If

        Catch ex As SQLiteException
            MessageBox.Show("Database error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            connection.Close()
        End Try
    End Sub

    Private Sub CalculateServiceChange()
        Try
            Dim serviceCost As Decimal = Decimal.Parse(tb_servicecost.Text)
            Dim amountPaid As Decimal = Decimal.Parse(tb_servicemoneyamount.Text)

            Dim change As Decimal = amountPaid - serviceCost

            If amountPaid >= serviceCost Then
                tb_servicechange.Text = change.ToString("F2") ' Format change to two decimal places
            Else
                tb_servicechange.Clear()
            End If
        Catch ex As Exception
            tb_servicechange.Clear()
        End Try
    End Sub


    Private Sub LoadServiceCost(serviceName As String)
        conn = New SQLiteConnection(connectionString)

        Try
            conn.Open()
            Dim query As String = "SELECT service_cost FROM services WHERE service_name = @service_name"
            Dim cmd As New SQLiteCommand(query, conn)
            cmd.Parameters.AddWithValue("@service_name", serviceName)

            Dim reader As SQLiteDataReader = cmd.ExecuteReader()

            If reader.Read() Then
                Dim serviceCost As Decimal = If(IsDBNull(reader("service_cost")), 0D, Convert.ToDecimal(reader("service_cost")))
                tb_servicecost.Text = serviceCost.ToString("F2")
            End If
        Catch ex As SQLiteException
            MessageBox.Show("Error: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub InitializeChart()
        ' Set chart properties
        Chart_salesandservices.ChartAreas.Clear()
        Dim chartArea As New ChartArea()
        Chart_salesandservices.ChartAreas.Add(chartArea)
        Chart_salesandservices.Series("Series1").IsVisibleInLegend = False

        ' Create and set the chart title
        Dim chartTitle As New Title()
        chartTitle.Text = "Monthly Revenue Analysis: Product Sales vs. Service Sales"
        chartTitle.Font = New Font("Arial", 14, FontStyle.Bold)
        chartTitle.ForeColor = Color.Black
        Chart_salesandservices.Titles.Add(chartTitle)  ' Add the title to the Titles collection

        ' Set axis titles
        Chart_salesandservices.ChartAreas(0).AxisX.Title = "Month"
        Chart_salesandservices.ChartAreas(0).AxisY.Title = "Total Sales"

        ' Customize X axis labels to make them easier to read
        Chart_salesandservices.ChartAreas(0).AxisX.Interval = 1
        Chart_salesandservices.ChartAreas(0).AxisX.MajorGrid.LineColor = Color.LightGray
        Chart_salesandservices.ChartAreas(0).AxisY.MajorGrid.LineColor = Color.LightGray

        ' Add legend for the series
        Dim legend As New Legend()
        legend.Docking = Docking.Top
        Chart_salesandservices.Legends.Add(legend)

        ' Add series for Product Sales
        Dim productSalesSeries As New Series("Product Sales")
        productSalesSeries.ChartType = SeriesChartType.Column
        productSalesSeries.Color = Color.OrangeRed
        productSalesSeries.BorderWidth = 1
        Chart_salesandservices.Series.Add(productSalesSeries)

        ' Add series for Service Sales
        Dim serviceSalesSeries As New Series("Service Sales")
        serviceSalesSeries.ChartType = SeriesChartType.Column
        serviceSalesSeries.Color = Color.Orange
        serviceSalesSeries.BorderWidth = 1
        Chart_salesandservices.Series.Add(serviceSalesSeries)

        ' Add all months to the X-axis
        Dim months() As String = {"January", "February", "March", "April", "May", "June",
                           "July", "August", "September", "October", "November", "December"}

        For Each month As String In months
            ' Add empty data points for each month to ensure all months are displayed
            productSalesSeries.Points.AddXY(month, 0)
            serviceSalesSeries.Points.AddXY(month, 0)
        Next

        ' Load data into the chart and update the correct points
        LoadDataIntoChart()

    End Sub

    Private Sub LoadDataIntoChart()
        Dim connectionString As String = "Data Source=C:\Users\Admin\source\repos\ic_electronics_center_app\ic_electronics_center_app\ic_electronics.db"

        Using connection As New SQLiteConnection(connectionString)
            connection.Open()

            ' Query for Product Sales
            Dim productQuery As String = "SELECT strftime('%m', transaction_date_time) AS Month, SUM(total_cost) AS TotalSales " &
                                      "FROM product_transactions GROUP BY Month ORDER BY Month"

            Using productCommand As New SQLiteCommand(productQuery, connection)
                Using reader As SQLiteDataReader = productCommand.ExecuteReader()
                    While reader.Read()
                        Dim monthIndex As Integer = CInt(reader("Month")) - 1
                        Chart_salesandservices.Series("Product Sales").Points(monthIndex).YValues(0) = reader("TotalSales")
                    End While
                End Using
            End Using

            ' Query for Service Sales
            Dim serviceQuery As String = "SELECT strftime('%m', transaction_date_time) AS Month, SUM(service_cost) AS TotalSales " &
                                      "FROM service_transactions GROUP BY Month ORDER BY Month"

            Using serviceCommand As New SQLiteCommand(serviceQuery, connection)
                Using reader As SQLiteDataReader = serviceCommand.ExecuteReader()
                    While reader.Read()
                        Dim monthIndex As Integer = CInt(reader("Month")) - 1
                        Chart_salesandservices.Series("Service Sales").Points(monthIndex).YValues(0) = reader("TotalSales")
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub datatable_itemlistproducts_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles datatable_itemlistproducts.CellClick
        Try
            If e.RowIndex >= 0 Then ' Ensure a valid row is clicked
                Dim selectedRow As DataGridViewRow = datatable_itemlistproducts.Rows(e.RowIndex)

                ' Check if the checkbox for the selected row is checked
                Dim isSelected As Boolean = Convert.ToBoolean(selectedRow.Cells("chkSelect").Value)

                If isSelected Then
                    ' Unselect the row (checkbox unchecked)
                    selectedRow.Cells("chkSelect").Value = False

                    ' Clear specific fields when unselecting
                    tb_producttotalcost.Clear()
                    tb_moneyamount.Clear()
                    tb_productamounttopaid.Clear()
                    tb_change.Clear()
                Else
                    ' Select the row (checkbox checked)
                    selectedRow.Cells("chkSelect").Value = True
                    ' Here you could retrieve and display other details if needed
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("An error occurred: " & ex.Message)
        End Try
    End Sub

    Private Sub InitializeDataGridView()
        datatable_itemlistproducts.Columns.Clear()

        ' Add a checkbox column for selection
        Dim chkSelectColumn As New DataGridViewCheckBoxColumn()
        chkSelectColumn.Name = "chkSelect"
        chkSelectColumn.HeaderText = "Select"
        chkSelectColumn.Width = 50
        chkSelectColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        datatable_itemlistproducts.Columns.Add(chkSelectColumn)

        ' Adding only the relevant columns
        datatable_itemlistproducts.Columns.Add("product_name", "Product Name")
        datatable_itemlistproducts.Columns.Add("unit_cost", "Product Price") ' Renaming to Product Price
        datatable_itemlistproducts.Columns.Add("return_quantity", "Quantity")
        datatable_itemlistproducts.Columns.Add("total_cost", "Total Cost") ' Assuming you want to calculate total cost from price and quantity

        ' Set ReadOnly for all columns except the checkbox
        For Each column As DataGridViewColumn In datatable_itemlistproducts.Columns
            If column.Index <> 0 Then ' Checkbox column should remain editable
                column.ReadOnly = True
            End If
        Next
    End Sub

    Private Sub btn_addtolist_Click(sender As Object, e As EventArgs) Handles btn_addtolist.Click
        Dim productName As String = cmb_productname.SelectedItem.ToString()
        Dim productPrice As Decimal = Decimal.Parse(tb_productprice.Text)
        Dim quantity As Integer = Integer.Parse(tb_quantity.Text)
        Dim totalCost As Decimal = productPrice * quantity

        Dim row As String() = {productName, productPrice.ToString(), quantity.ToString(), totalCost.ToString()}
        datatable_itemlist.Rows.Add(row)

        UpdateTotalCost()
        tb_productamounttopaid.Text = tb_producttotalcost.Text

        ClearAddProductFields()
    End Sub


    Private Sub btn_clearadditems_Click(sender As Object, e As EventArgs) Handles btn_clearadditems.Click
        datatable_itemlistproducts.Rows.Clear()

        UpdateTotalCost()

        tb_producttotalcost.Clear()
        tb_moneyamount.Clear()
        tb_productamounttopaid.Clear()
        tb_change.Clear()
    End Sub

    Private Sub btn_deleteadditems_Click(sender As Object, e As EventArgs) Handles btn_deleteadditems.Click
        For i As Integer = datatable_itemlistproducts.Rows.Count - 1 To 0 Step -1
            Dim isChecked As Boolean = Convert.ToBoolean(datatable_itemlistproducts.Rows(i).Cells("chkSelect").Value)

            If isChecked Then
                datatable_itemlistproducts.Rows.RemoveAt(i)
            End If
        Next
        UpdateTotalCost()
        If datatable_itemlistproducts.Rows.Count = 0 Then
            tb_productamounttopaid.Clear()
        End If
    End Sub

    Private Sub ClearAddProductFields()
        cmb_productname.SelectedItem = Nothing
        tb_productprice.Clear()
        tb_quantity.Clear()
        tb_producttotalcost.Clear()

    End Sub


    Private Sub UpdateTotalCost()
        Dim sumTotalCost As Decimal = 0

        For Each row As DataGridViewRow In datatable_itemlistproducts.Rows
            If Not row.IsNewRow Then
                sumTotalCost += Decimal.Parse(row.Cells(3).Value.ToString())
            End If
        Next
        tb_producttotalcost.Text = sumTotalCost.ToString("F2")
        tb_productamounttopaid.Text = tb_producttotalcost.Text ' Update this as well
    End Sub

    Private Sub tb_moneyamount_TextChanged(sender As Object, e As EventArgs) Handles tb_moneyamount.TextChanged
        CalculateChange()
    End Sub

    Private Sub tb_productamounttopaid_TextChanged(sender As Object, e As EventArgs) Handles tb_productamounttopaid.TextChanged
        CalculateChange()
    End Sub

    Private Sub CalculateChange()
        Try
            Dim amountPaid As Decimal = Decimal.Parse(tb_moneyamount.Text)
            Dim amountToSubtract As Decimal = Decimal.Parse(tb_productamounttopaid.Text)

            Dim change As Decimal = amountPaid - amountToSubtract

            If amountPaid >= amountToSubtract Then
                tb_change.Text = change.ToString("F2")
            Else
                tb_change.Clear()
            End If
        Catch ex As Exception
            tb_change.Clear()
        End Try
    End Sub

    Private Sub tb_quantity_TextChanged(sender As Object, e As EventArgs) Handles tb_quantity.TextChanged
        UpdateTotalCostBasedOnQuantity()
        Dim productPrice As Decimal
        Dim quantity As Integer

        If Decimal.TryParse(tb_productprice.Text, productPrice) AndAlso
           Integer.TryParse(tb_quantity.Text, quantity) Then
            Dim totalCost As Decimal = productPrice * quantity
            tb_productamounttopaid.Text = totalCost.ToString("F2") ' Format to 2 decimal places
        Else
            tb_productamounttopaid.Clear() ' Clear if parsing fails
        End If
    End Sub

    Private Sub UpdateTotalCostBasedOnQuantity()
        If Not String.IsNullOrWhiteSpace(tb_quantity.Text) AndAlso Not String.IsNullOrWhiteSpace(tb_productprice.Text) Then
            Dim productPrice As Decimal = Decimal.Parse(tb_productprice.Text)
            Dim quantity As Integer

            If Integer.TryParse(tb_quantity.Text, quantity) Then
                Dim totalCost As Decimal = productPrice * quantity
                tb_producttotalcost.Text = totalCost.ToString("F2")
            Else
                tb_producttotalcost.Clear()
            End If
        Else
            tb_producttotalcost.Clear()
        End If
    End Sub

    Private Sub tb_numeric_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tb_productprice.KeyPress, tb_quantity.KeyPress, tb_moneyamount.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "."c Then
            e.Handled = True
        End If

        If e.KeyChar = "."c AndAlso DirectCast(sender, System.Windows.Forms.TextBox).Text.Contains(".") Then
            e.Handled = True
        End If

    End Sub
    Private Sub btn_completetransactproducts_Click(sender As Object, e As EventArgs) Handles btn_completetransactproducts.Click
        Dim totalAmount As Decimal = Decimal.Parse(tb_moneyamount.Text)
        Dim amountToPay As Decimal = Decimal.Parse(tb_productamounttopaid.Text)

        If totalAmount < amountToPay Then
            MessageBox.Show("Not enough money amount to complete the transaction.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim totalChange As Decimal = Decimal.Parse(tb_change.Text)
        Dim transactionDateTime As DateTime = DateTime.Now

        Dim productSummary As New Dictionary(Of Integer, (productName As String, unitCost As Decimal, quantity As Integer, totalCost As Decimal, transactionId As Integer))()

        Using conn As New SQLiteConnection(connectionString)
            conn.Open()
            Using transaction = conn.BeginTransaction()
                Try
                    For Each row As DataGridViewRow In datatable_itemlistproducts.Rows
                        If Not row.IsNewRow Then
                            Dim productName As String = row.Cells(0).Value.ToString()
                            Dim unitCost As Decimal = Decimal.Parse(row.Cells(1).Value.ToString())
                            Dim quantity As Integer = Integer.Parse(row.Cells(2).Value.ToString())
                            Dim rowTotalCost As Decimal = Decimal.Parse(row.Cells(3).Value.ToString())

                            Dim productId As Integer = GetProductIdByName(productName)
                            If productId = 0 Then
                                MessageBox.Show($"Product '{productName}' does not exist.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                Return
                            End If

                            Dim transactionId As Integer

                            ' Prepare to insert the product transaction
                            Dim query As String = "INSERT INTO product_transactions (product_id, product_name, unit_cost, quantity, total_cost, total_amount, total_change, transaction_date_time) " &
                                              "VALUES (@product_id, @product_name, @unit_cost, @quantity, @total_cost, @total_amount, @total_change, @transaction_date_time)"

                            Using cmd As New SQLiteCommand(query, conn, transaction)
                                cmd.Parameters.AddWithValue("@product_id", productId)
                                cmd.Parameters.AddWithValue("@product_name", productName)
                                cmd.Parameters.AddWithValue("@unit_cost", unitCost)
                                cmd.Parameters.AddWithValue("@quantity", quantity)
                                cmd.Parameters.AddWithValue("@total_cost", rowTotalCost)
                                cmd.Parameters.AddWithValue("@total_amount", totalAmount)
                                cmd.Parameters.AddWithValue("@total_change", totalChange)
                                cmd.Parameters.AddWithValue("@transaction_date_time", transactionDateTime)

                                cmd.ExecuteNonQuery()

                                ' Get the transaction ID
                                cmd.CommandText = "SELECT last_insert_rowid()"
                                transactionId = Convert.ToInt32(cmd.ExecuteScalar())
                            End Using

                            ' Update the product summary
                            If productSummary.ContainsKey(productId) Then
                                productSummary(productId) = (productName, unitCost, productSummary(productId).quantity + quantity, productSummary(productId).totalCost + rowTotalCost, transactionId)
                            Else
                                productSummary.Add(productId, (productName, unitCost, quantity, rowTotalCost, transactionId))
                            End If

                            ' Update the stock in products table
                            Dim updateQuery As String = "UPDATE products SET quantity_in_stock = quantity_in_stock - @quantity, " &
                                                     "sold = sold + @quantity, " &
                                                     "remaining_stock_value = (quantity_in_stock - @quantity) * unit_price " & ' Adjusted to calculate remaining stock value after the update
                                                     "WHERE product_name = @product_name"

                            Using updateCmd As New SQLiteCommand(updateQuery, conn, transaction)
                                updateCmd.Parameters.AddWithValue("@quantity", quantity)
                                updateCmd.Parameters.AddWithValue("@product_name", productName)
                                updateCmd.ExecuteNonQuery()
                            End Using
                        End If
                    Next

                    transaction.Commit()
                    LoadDataIntoChart()
                    LoadLowStockProducts()
                    LoadTop5SellingProducts()
                    LoadProducts()
                    PopulateROPTransactionIdComboBox()
                    LoadProductTransactions()
                    Dim startDate As DateTime = DateTime.Now.AddDays(-30)
                    Dim endDate As DateTime = DateTime.Now
                    LoadSalesByDay()
                    LoadSalesByMonth()
                    LoadSalesByWeek()
                    SetupChart()
                    SetupDailyTransactionHistoryDataGridView()
                    SetupDailyTransactionHistoryDataGridView()
                    SetupWeeklyTransactionHistoryDataGridView()
                    SetupMonthlyTransactionHistoryDataGridView()

                    CreateAndShowReceipt(productSummary, totalAmount, totalChange, transactionDateTime)

                    MessageBox.Show("Product transaction completed.")
                    ClearFields()
                Catch ex As Exception
                    transaction.Rollback()
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using
    End Sub

    Private Function GetProductIdByName(productName As String) As Integer
        Dim productId As Integer = 0
        Dim query As String = "SELECT product_id FROM products WHERE product_name = @product_name"

        Using conn As New SQLiteConnection(connectionString)
            conn.Open()
            Using cmd As New SQLiteCommand(query, conn)
                cmd.Parameters.AddWithValue("@product_name", productName)
                Dim result = cmd.ExecuteScalar()
                If result IsNot Nothing Then
                    productId = Convert.ToInt32(result)
                End If
            End Using
        End Using

        Return productId
    End Function

    Private Sub CreateAndShowReceipt(productSummary As Dictionary(Of Integer, (productName As String, unitCost As Decimal, quantity As Integer, totalCost As Decimal, transactionId As Integer)), totalAmount As Decimal, totalChange As Decimal, transactionDateTime As DateTime)
        Dim receipt As New StringBuilder()
        receipt.AppendLine("IC Electronics Center")
        receipt.AppendLine("8-B F Bangoy St, Davao City, 8000")
        receipt.AppendLine("===================================")
        receipt.AppendLine("Receipt")
        receipt.AppendLine("===================================")
        receipt.AppendLine($"Date & Time: {transactionDateTime}")
        receipt.AppendLine("Product Name        Unit Cost   Quantity   Total Cost   Trans ID")
        receipt.AppendLine("---------------------------------------------------------------------")

        For Each product In productSummary
            Dim productName As String = product.Value.productName
            Dim unitCost As Decimal = product.Value.unitCost
            Dim quantity As Integer = product.Value.quantity
            Dim rowTotalCost As Decimal = product.Value.totalCost
            Dim transactionId As Integer = product.Value.transactionId ' Add this to display transaction ID

            receipt.AppendLine($"{productName,-20} {unitCost,10:C} {quantity,10} {rowTotalCost,12:C} {transactionId,12}")
        Next

        receipt.AppendLine("---------------------------------------------------------------------")
        receipt.AppendLine($"Total Amount: {totalAmount:C}")
        receipt.AppendLine($"Total Change: {totalChange:C}")
        receipt.AppendLine("===================================")

        Dim result As DialogResult = MessageBox.Show(receipt.ToString() & vbCrLf & "Would you like to print this receipt?", "Transaction Completed", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
        If result = DialogResult.Yes Then
            PrintReceipt(receipt.ToString())
        End If
    End Sub


    Private Sub PrintReceipt(receipt As String)
        Dim printDialog As New PrintDialog()
        Dim printDocument As New Printing.PrintDocument()

        AddHandler printDocument.PrintPage, Sub(sender, e)
                                                e.Graphics.DrawString(receipt, New Font("Arial", 10), Brushes.Black, 100, 100)
                                            End Sub

        If printDialog.ShowDialog() = DialogResult.OK Then
            printDocument.Print()
        End If
    End Sub



    Private Sub UpdateProductStock(productName As String, quantity As Integer, rowTotalCost As Decimal)

        Dim query As String = "UPDATE products SET " &
                          "quantity_in_stock = quantity_in_stock - @quantity, " &
                          "remaining_stock_value = remaining_stock_value - @totalCost, " &
                          "sold = sold + @quantity " &
                          "WHERE product_name = @product_name"

        Using conn As New SQLiteConnection(connectionString)
            conn.Open()
            Using cmd As New SQLiteCommand(query, conn)
                cmd.Parameters.AddWithValue("@quantity", quantity)
                cmd.Parameters.AddWithValue("@totalCost", rowTotalCost)
                cmd.Parameters.AddWithValue("@product_name", productName)

                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub


    Private Function ProductExists(productName As String) As Boolean
        Dim exists As Boolean = False
        Dim query As String = "SELECT COUNT(*) FROM products WHERE product_name = @product_name"

        Using conn As New SQLiteConnection(connectionString)
            conn.Open()
            Using cmd As New SQLiteCommand(query, conn)
                cmd.Parameters.AddWithValue("@product_name", productName)
                exists = Convert.ToInt32(cmd.ExecuteScalar()) > 0
            End Using
        End Using

        Return exists
    End Function


    Private Sub ClearFields()
        tb_productamounttopaid.Clear()
        tb_moneyamount.Clear()
        tb_change.Clear()
        tb_productprice.Clear()

        datatable_itemlistproducts.Rows.Clear()
    End Sub

    Private Sub UpdateProductStockAndSold(conn As SQLiteConnection, productName As String, quantitySold As Integer)
        Try

            Dim query As String = "SELECT quantity_in_stock, sold FROM products WHERE product_name = @product_name"
            Dim cmd As New SQLiteCommand(query, conn)
            cmd.Parameters.AddWithValue("@product_name", productName)
            Dim reader As SQLiteDataReader = cmd.ExecuteReader()

            Dim currentStock As Integer = 0
            Dim currentSold As Integer = 0

            If reader.Read() Then
                currentStock = Integer.Parse(reader("quantity_in_stock").ToString())
                currentSold = Integer.Parse(reader("sold").ToString())
            End If
            reader.Close()

            Dim newStock As Integer = currentStock - quantitySold
            Dim newSold As Integer = currentSold + quantitySold

            Dim updateQuery As String = "UPDATE products SET quantity_in_stock = @new_stock, sold = @new_sold WHERE product_name = @product_name"
            Dim updateCmd As New SQLiteCommand(updateQuery, conn)
            updateCmd.Parameters.AddWithValue("@new_stock", newStock)
            updateCmd.Parameters.AddWithValue("@new_sold", newSold)
            updateCmd.Parameters.AddWithValue("@product_name", productName)

            updateCmd.ExecuteNonQuery()

            Dim remainingValueQuery As String = "UPDATE products SET remaining_stock_value = quantity_in_stock * unit_price WHERE product_name = @product_name"
            Using remainingCmd As New SQLiteCommand(remainingValueQuery, conn)
                remainingCmd.Parameters.AddWithValue("@product_name", productName)
                remainingCmd.ExecuteNonQuery()
            End Using

            LoadProducts()

        Catch ex As SQLiteException
            MessageBox.Show("Error updating stock or sold count: " & ex.Message)
        End Try
    End Sub

    Private Sub cmb_servicename_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_servicename.SelectedIndexChanged
        If cmb_servicename.SelectedItem IsNot Nothing Then
            Dim selectedService As String = cmb_servicename.SelectedItem.ToString()
            LoadServiceDetails(selectedService)
        Else
            tb_servicecost.Clear()
        End If
    End Sub

    Private Sub btn_completetransactservices_Click(sender As Object, e As EventArgs) Handles btn_completetransactservices.Click
        Dim amountPaid As Decimal = Decimal.Parse(tb_servicemoneyamount.Text)
        Dim serviceCost As Decimal = Decimal.Parse(tb_servicecost.Text)

        If amountPaid < serviceCost Then
            MessageBox.Show("Not enough money to complete the transaction.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim changeGiven As Decimal = amountPaid - serviceCost
        Dim transactionDateTime As DateTime = DateTime.Now

        Using conn As New SQLiteConnection(connectionString)
            conn.Open()
            Using transaction = conn.BeginTransaction()
                Try
                    Dim serviceId As Integer = CInt(tb_serviceid.Text)
                    Dim serviceName As String = cmb_servicename.SelectedItem.ToString()

                    ' Prepare to insert the service transaction
                    Dim query As String = "INSERT INTO service_transactions (service_id, service_name, service_cost, amount_paid, change_given, transaction_date_time) " &
                                      "VALUES (@service_id, @service_name, @service_cost, @amount_paid, @change_given, @transaction_date_time)"

                    Using cmd As New SQLiteCommand(query, conn, transaction)
                        cmd.Parameters.AddWithValue("@service_id", serviceId)
                        cmd.Parameters.AddWithValue("@service_name", serviceName)
                        cmd.Parameters.AddWithValue("@service_cost", Math.Round(serviceCost, 2))  ' Ensure two decimal places
                        cmd.Parameters.AddWithValue("@amount_paid", Math.Round(amountPaid, 2))      ' Ensure two decimal places
                        cmd.Parameters.AddWithValue("@change_given", Math.Round(changeGiven, 2))    ' Ensure two decimal places
                        cmd.Parameters.AddWithValue("@transaction_date_time", transactionDateTime)

                        cmd.ExecuteNonQuery()
                    End Using

                    transaction.Commit()

                    ' Create and show receipt
                    CreateAndShowServiceReceipt(serviceId, serviceName, serviceCost, amountPaid, changeGiven, transactionDateTime)

                    MessageBox.Show("Service transaction completed.")
                    ClearServiceFields()
                    LoadDataIntoChart()
                    LoadLowStockProducts()
                    LoadTop5SellingProducts()
                    LoadProducts()
                    PopulateROPTransactionIdComboBox()
                    LoadProductTransactions()
                    LoadServiceTransactions()
                    Dim startDate As DateTime = DateTime.Now.AddDays(-30)
                    Dim endDate As DateTime = DateTime.Now
                    LoadSalesByDay()
                    LoadSalesByMonth()
                    LoadSalesByWeek()

                    SetupDailyTransactionHistoryDataGridView()
                    SetupDailyTransactionHistoryDataGridView()
                    SetupWeeklyTransactionHistoryDataGridView()
                    SetupMonthlyTransactionHistoryDataGridView()

                    ' Setup the chart
                    SetupChart()

                Catch ex As Exception
                    transaction.Rollback()
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using
    End Sub



    Private Sub CreateAndShowServiceReceipt(serviceId As Integer, serviceName As String, serviceCost As Decimal, amountPaid As Decimal, changeGiven As Decimal, transactionDateTime As DateTime)
        Dim receipt As New StringBuilder()
        receipt.AppendLine("IC Electronics Center")
        receipt.AppendLine("8-B F Bangoy St, Davao City, 8000")
        receipt.AppendLine("===================================")
        receipt.AppendLine("Service Receipt")
        receipt.AppendLine("===================================")
        receipt.AppendLine($"Date & Time: {transactionDateTime}")
        receipt.AppendLine("Service ID   Service Name       Service Cost   Amount Paid   Change Given")
        receipt.AppendLine("----------------------------------------------------------------------------")

        ' Service transaction details
        receipt.AppendLine($"{serviceId,-10} {serviceName,-20} {serviceCost,12:C} {amountPaid,12:C} {changeGiven,14:C}")

        receipt.AppendLine("----------------------------------------------------------------------------")
        receipt.AppendLine($"Total Amount Paid: {amountPaid:C}")
        receipt.AppendLine($"Total Change Given: {changeGiven:C}")
        receipt.AppendLine("===================================")

        ' Ask user if they want to print the receipt
        Dim result As DialogResult = MessageBox.Show(receipt.ToString() & vbCrLf & "Would you like to print this receipt?", "Transaction Completed", MessageBoxButtons.YesNo, MessageBoxIcon.Information)

        ' Print receipt if the user chooses 'Yes'
        If result = DialogResult.Yes Then
            PrintReceiptservice(receipt.ToString())
        End If
    End Sub

    Private Sub PrintReceiptservice(receipt As String)
        Dim printDialog As New PrintDialog()
        Dim printDocument As New Printing.PrintDocument()

        ' Event to handle the printing of the receipt
        AddHandler printDocument.PrintPage, Sub(sender, e)
                                                e.Graphics.DrawString(receipt, New Font("Arial", 10), Brushes.Black, 100, 100)
                                            End Sub

        ' Show print dialog and print if the user accepts
        If printDialog.ShowDialog() = DialogResult.OK Then
            printDocument.Print()
        End If
    End Sub


    Private Sub ClearServiceFields()
        tb_servicemoneyamount.Clear()
        tb_servicecost.Clear()
        tb_servicechange.Clear()
        cmb_servicename.SelectedIndex = -1
    End Sub


    Private Sub LoadServiceDetails(serviceName As String)
        Dim query As String = "SELECT service_id, service_cost FROM services WHERE service_name = @service_name"
        Using conn As New SQLiteConnection(connectionString)
            Using cmd As New SQLiteCommand(query, conn)
                cmd.Parameters.AddWithValue("@service_name", serviceName)
                conn.Open()

                Using reader As SQLiteDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        Dim serviceCost As Decimal = If(IsDBNull(reader("service_cost")), 0D, Convert.ToDecimal(reader("service_cost")))
                        tb_servicecost.Text = serviceCost.ToString("F2")
                        tb_serviceid.Text = reader("service_id").ToString()
                    End If
                End Using
            End Using
        End Using
    End Sub


    Private Sub ClearServiceTransactionFields()
        tb_servicecost.Clear()
        cmb_servicename.SelectedIndex = -1
    End Sub

    Private selectedSupplierId As Integer
    Private selectedProductId As Integer
    Private Sub btn_addproduct_Click(sender As Object, e As EventArgs) Handles btn_addproduct.Click
        If Not ValidateInputs() Then
            Return
        End If

        Dim selectedSupplier As Supplier = CType(cmb_supplierp.SelectedItem, Supplier)
        Dim supplierId As Integer = selectedSupplier.SupplierID

        Dim query As String = "INSERT INTO products (supplier_id, product_name, unit_price, quantity_in_stock, unit_of_measurement, total_cost, remaining_stock_value, sold) VALUES (@supplier_id, @product_name, @unit_price, @quantity_in_stock, @unit_of_measurement, @total_cost, @remaining_stock_value, @sold)"
        Using conn As New SQLiteConnection(connectionString)
            Using cmd As New SQLiteCommand(query, conn)
                cmd.Parameters.AddWithValue("@supplier_id", supplierId)
                cmd.Parameters.AddWithValue("@product_name", tb_productnamep.Text)
                cmd.Parameters.AddWithValue("@unit_price", Convert.ToDecimal(tb_unitpricep.Text))
                cmd.Parameters.AddWithValue("@quantity_in_stock", Convert.ToInt32(tb_quantityinstockp.Text))
                cmd.Parameters.AddWithValue("@unit_of_measurement", tb_unitofmeasurementp.Text)
                cmd.Parameters.AddWithValue("@total_cost", Convert.ToDecimal(tb_totalcostp.Text))
                cmd.Parameters.AddWithValue("@remaining_stock_value", Convert.ToDecimal(tb_remainingstockvaluep.Text))
                cmd.Parameters.AddWithValue("@sold", Convert.ToInt32(tb_soldp.Text))
                conn.Open()
                cmd.ExecuteNonQuery()
                MessageBox.Show("Product added successfully!")

                ClearInputs()
                LoadProducts()
                LoadLowStockProducts()
            End Using
        End Using
    End Sub
    Private Sub btn_updateproduct_Click(sender As Object, e As EventArgs) Handles btn_updateproduct.Click

        If selectedProductId <= 0 Then
            MessageBox.Show("Please select a product to update.")
            Return
        End If

        If Not ValidateInputs() Then
            Return
        End If

        ' SQL update query
        Dim query As String = "UPDATE products SET
                               product_name = @product_name,
                               unit_price = @unit_price,
                               quantity_in_stock = @quantity_in_stock,
                               unit_of_measurement = @unit_of_measurement,
                               total_cost = @total_cost,
                               remaining_stock_value = @remaining_stock_value,
                               sold = @sold
                           WHERE product_id = @product_id"

        Using conn As New SQLiteConnection(connectionString)
            Using cmd As New SQLiteCommand(query, conn)
                ' Adding parameters with the values from input fields
                cmd.Parameters.AddWithValue("@product_name", tb_productnamep.Text.Trim())
                cmd.Parameters.AddWithValue("@unit_price", Convert.ToDecimal(tb_unitpricep.Text))
                cmd.Parameters.AddWithValue("@quantity_in_stock", Convert.ToInt32(tb_quantityinstockp.Text))
                cmd.Parameters.AddWithValue("@unit_of_measurement", tb_unitofmeasurementp.Text.Trim())
                cmd.Parameters.AddWithValue("@total_cost", Convert.ToDecimal(tb_totalcostp.Text))
                cmd.Parameters.AddWithValue("@remaining_stock_value", Convert.ToDecimal(tb_remainingstockvaluep.Text))
                cmd.Parameters.AddWithValue("@sold", Convert.ToInt32(tb_soldp.Text))
                cmd.Parameters.AddWithValue("@product_id", selectedProductId)

                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Product updated successfully!")

                    ClearInputs()
                    selectedProductId = 0
                    LoadProducts()
                    LoadLowStockProducts()
                    btn_addproduct.Enabled = True
                Catch ex As Exception
                    MessageBox.Show("An error occurred while updating the product: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

    Private Function ValidateInputs() As Boolean
        ' Check for empty fields
        If String.IsNullOrWhiteSpace(tb_productnamep.Text) OrElse
       String.IsNullOrWhiteSpace(tb_unitpricep.Text) OrElse
       String.IsNullOrWhiteSpace(tb_quantityinstockp.Text) OrElse
       String.IsNullOrWhiteSpace(tb_unitofmeasurementp.Text) OrElse
       String.IsNullOrWhiteSpace(tb_totalcostp.Text) OrElse
       String.IsNullOrWhiteSpace(tb_remainingstockvaluep.Text) OrElse
       String.IsNullOrWhiteSpace(tb_soldp.Text) Then

            MessageBox.Show("All fields must be filled.")
            Return False
        End If

        Dim unitPrice, totalCost, remainingStockValue As Decimal
        Dim quantityInStock, sold As Integer

        If Not Decimal.TryParse(tb_unitpricep.Text, unitPrice) OrElse
       Not Integer.TryParse(tb_quantityinstockp.Text, quantityInStock) OrElse
       Not Decimal.TryParse(tb_totalcostp.Text, totalCost) OrElse
       Not Decimal.TryParse(tb_remainingstockvaluep.Text, remainingStockValue) OrElse
       Not Integer.TryParse(tb_soldp.Text, sold) Then

            MessageBox.Show("Please enter valid numeric values for price, quantity, total cost, remaining stock value, and sold.")
            Return False
        End If

        Return True
    End Function

    Private Sub ClearInputs()
        tb_productnamep.Clear()
        tb_unitpricep.Clear()
        tb_quantityinstockp.Clear()
        tb_unitofmeasurementp.Clear()
        tb_totalcostp.Clear()
        tb_remainingstockvaluep.Clear()
        tb_soldp.Clear()
        cmb_supplierp.SelectedIndex = -1
        selectedProductId = 0
    End Sub

    Private Sub tb_unitpricep_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tb_unitpricep.KeyPress, tb_quantityinstockp.KeyPress, tb_totalcostp.KeyPress, tb_remainingstockvaluep.KeyPress, tb_soldp.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub tb_unitofmeasurementp_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tb_unitofmeasurementp.KeyPress
        ' Allow only letters and control keys (Backspace, Delete)
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsLetter(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub tb_unitofmeasurementp_Leave(sender As Object, e As EventArgs) Handles tb_unitofmeasurementp.Leave
        If tb_unitofmeasurementp.Text.Trim() = "" Then
            MessageBox.Show("Unit of measurement cannot be empty.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            tb_unitofmeasurementp.Focus()
        End If
    End Sub

    Private Sub datatable_products_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles datatable_products.CellContentClick
        If e.ColumnIndex = datatable_products.Columns("chkSelect").Index AndAlso e.RowIndex >= 0 Then
            Dim chkCell As DataGridViewCheckBoxCell = CType(datatable_products.Rows(e.RowIndex).Cells("chkSelect"), DataGridViewCheckBoxCell)
            chkCell.Value = Not CBool(chkCell.Value)

            If CBool(chkCell.Value) = True Then

                Dim row As DataGridViewRow = datatable_products.Rows(e.RowIndex)
                tb_productnamep.Text = row.Cells("product_name").Value.ToString()
                tb_unitpricep.Text = row.Cells("unit_price").Value.ToString()
                tb_quantityinstockp.Text = row.Cells("quantity_in_stock").Value.ToString()
                tb_unitofmeasurementp.Text = row.Cells("unit_of_measurement").Value.ToString()
                tb_totalcostp.Text = row.Cells("total_cost").Value.ToString()
                tb_remainingstockvaluep.Text = row.Cells("remaining_stock_value").Value.ToString()
                tb_soldp.Text = row.Cells("sold").Value.ToString()


                selectedProductId = CInt(row.Cells("product_id").Value)
                btn_addproduct.Enabled = False
            Else

                ClearInputs()
                selectedProductId = 0
                btn_addproduct.Enabled = True
            End If
        End If
    End Sub

    Private Sub btn_Archiveproduct_Click(sender As Object, e As EventArgs) Handles btn_Archiveproduct.Click
        If selectedProductId > 0 Then

            Dim archiveQuery As String = "INSERT INTO archived_products (product_id, supplier_id, product_name, unit_price, quantity_in_stock, unit_of_measurement, total_cost, remaining_stock_value, sold) " &
                                      "SELECT product_id, supplier_id, product_name, unit_price, quantity_in_stock, unit_of_measurement, total_cost, remaining_stock_value, sold " &
                                      "FROM products WHERE product_id=@product_id"
            Dim deleteQuery As String = "DELETE FROM products WHERE product_id=@product_id"

            Using conn As New SQLiteConnection(connectionString)
                conn.Open()


                Dim transaction = conn.BeginTransaction()

                Try

                    Dim checkQuery As String = "SELECT quantity_in_stock, sold FROM products WHERE product_id=@product_id"
                    Using checkCmd As New SQLiteCommand(checkQuery, conn, transaction)
                        checkCmd.Parameters.AddWithValue("@product_id", selectedProductId)

                        Using reader As SQLiteDataReader = checkCmd.ExecuteReader()
                            If reader.Read() Then
                                Dim quantityInStock As Integer = reader.GetInt32(0)
                                Dim sold As Integer = reader.GetInt32(1)

                                If quantityInStock < 0 Or sold < 0 Then
                                    MessageBox.Show("Cannot archive product with negative stock or sold values.")
                                    Return
                                End If
                            End If
                        End Using
                    End Using

                    Using archiveCmd As New SQLiteCommand(archiveQuery, conn, transaction)
                        archiveCmd.Parameters.AddWithValue("@product_id", selectedProductId)
                        Dim rowsAffected = archiveCmd.ExecuteNonQuery()

                        If rowsAffected > 0 Then

                            Using deleteCmd As New SQLiteCommand(deleteQuery, conn, transaction)
                                deleteCmd.Parameters.AddWithValue("@product_id", selectedProductId)
                                deleteCmd.ExecuteNonQuery()
                            End Using

                            transaction.Commit()
                            MessageBox.Show("Product archived and deleted successfully!")
                        Else

                            MessageBox.Show("Failed to archive product. No rows affected.")
                            transaction.Rollback()
                        End If
                    End Using
                Catch ex As SQLiteException
                    transaction.Rollback()
                    MessageBox.Show("MySQL error: " & ex.Message)
                Catch ex As Exception
                    transaction.Rollback()
                    MessageBox.Show("An unexpected error occurred: " & ex.Message)
                End Try
            End Using

            LoadProducts()
            LoadArchivedProducts()
            LoadLowStockProducts()
            LoadTop5SellingProducts()
        Else
            MessageBox.Show("Please select a product to archive.")
        End If
    End Sub


    Private Sub btn_clearproducts_Click(sender As Object, e As EventArgs) Handles btn_clearproducts.Click
        tb_productnamep.Clear()
        tb_unitpricep.Clear()
        tb_quantityinstockp.Clear()
        tb_unitofmeasurementp.Clear()
        tb_totalcostp.Clear()
        tb_remainingstockvaluep.Clear()
        tb_soldp.Clear()
        cmb_supplierp.SelectedIndex = -1

        btn_addproduct.Enabled = True
    End Sub


    Private Function ValidateSupplierInputs() As Boolean
        If String.IsNullOrWhiteSpace(tb_companyname.Text) OrElse
       String.IsNullOrWhiteSpace(tb_contactperson.Text) OrElse
       String.IsNullOrWhiteSpace(tb_contactnumber.Text) OrElse
       String.IsNullOrWhiteSpace(tb_address.Text) Then

            MessageBox.Show("Company name, contact person, contact number, and address must be filled.")
            Return False
        End If

        If Not IsNumeric(tb_contactnumber.Text) Then
            MessageBox.Show("Please enter a valid contact number.")
            Return False
        End If

        Return True
    End Function

    Private Sub btn_searchproduct_TextChanged(sender As Object, e As EventArgs) Handles btn_searchproduct.TextChanged
        Dim searchTerm As String = btn_searchproduct.Text.Trim()

        If String.IsNullOrEmpty(searchTerm) Then
            LoadProducts()
        Else
            Dim query As String = "SELECT * FROM products WHERE product_name LIKE @searchTerm"
            Using conn As New SQLiteConnection(connectionString)
                Using cmd As New SQLiteCommand(query, conn)
                    cmd.Parameters.AddWithValue("@searchTerm", "%" & searchTerm & "%")
                    conn.Open()
                    Using reader As SQLiteDataReader = cmd.ExecuteReader()
                        Dim dt As New DataTable()
                        dt.Load(reader)
                        datatable_products.DataSource = dt
                    End Using
                End Using
            End Using
        End If
    End Sub

    Private Sub ClearSupplierInputs()
        tb_companyname.Clear()
        tb_contactperson.Clear()
        tb_contactnumber.Clear()
        tb_address.Clear()
        tb_note.Clear()
        btn_addsupplier.Enabled = True
    End Sub

    Private Sub btn_addsupplier_Click_1(sender As Object, e As EventArgs) Handles btn_addsupplier.Click
        If Not ValidateSupplierInputs() Then
            Return
        End If

        Dim query As String = "INSERT INTO suppliers (company_name, contact_person, contact_number, address, note) VALUES (@company_name, @contact_person, @contact_number, @address, @note)"
        Using conn As New SQLiteConnection(connectionString)
            Using cmd As New SQLiteCommand(query, conn)
                cmd.Parameters.AddWithValue("@company_name", tb_companyname.Text)
                cmd.Parameters.AddWithValue("@contact_person", tb_contactperson.Text)
                cmd.Parameters.AddWithValue("@contact_number", tb_contactnumber.Text)
                cmd.Parameters.AddWithValue("@address", tb_address.Text)
                cmd.Parameters.AddWithValue("@note", tb_note.Text)
                conn.Open()
                cmd.ExecuteNonQuery()
                MessageBox.Show("Supplier added successfully!")

                ClearSupplierInputs()

                LoadSuppliers()

                btn_addsupplier.Enabled = True
            End Using
        End Using
    End Sub

    Private Sub datatable_suppliers_CellContentClick_1(sender As Object, e As DataGridViewCellEventArgs) Handles datatable_suppliers.CellContentClick
        If e.ColumnIndex = datatable_suppliers.Columns("chkSelect").Index AndAlso e.RowIndex >= 0 Then
            Dim chkCell As DataGridViewCheckBoxCell = CType(datatable_suppliers.Rows(e.RowIndex).Cells("chkSelect"), DataGridViewCheckBoxCell)
            chkCell.Value = Not CBool(chkCell.Value)

            If CBool(chkCell.Value) = True Then
                Dim row As DataGridViewRow = datatable_suppliers.Rows(e.RowIndex)
                tb_companyname.Text = row.Cells("company_name").Value.ToString()
                tb_contactperson.Text = row.Cells("contact_person").Value.ToString()
                tb_contactnumber.Text = row.Cells("contact_number").Value.ToString()
                tb_address.Text = row.Cells("address").Value.ToString()
                tb_note.Text = row.Cells("note").Value.ToString()

                selectedSupplierId = CInt(row.Cells("supplier_id").Value)

                btn_addsupplier.Enabled = False
            Else
                tb_companyname.Clear()
                tb_contactperson.Clear()
                tb_contactnumber.Clear()
                tb_address.Clear()
                tb_note.Clear()
                selectedSupplierId = 0

                btn_addsupplier.Enabled = True
            End If
        End If
    End Sub

    Private Sub btn_updatesupplier_Click_1(sender As Object, e As EventArgs) Handles btn_updatesupplier.Click
        If selectedSupplierId <= 0 Then
            MessageBox.Show("Please select a supplier to update.")
            Return
        End If

        ' Validate inputs
        If Not ValidateSupplierInputs() Then
            Return
        End If

        Dim query As String = "UPDATE suppliers SET company_name=@company_name, contact_person=@contact_person, contact_number=@contact_number, address=@address, note=@note WHERE supplier_id=@supplier_id"
        Using conn As New SQLiteConnection(connectionString)
            Using cmd As New SQLiteCommand(query, conn)
                cmd.Parameters.AddWithValue("@company_name", tb_companyname.Text)
                cmd.Parameters.AddWithValue("@contact_person", tb_contactperson.Text)
                cmd.Parameters.AddWithValue("@contact_number", tb_contactnumber.Text)
                cmd.Parameters.AddWithValue("@address", tb_address.Text)
                cmd.Parameters.AddWithValue("@note", tb_note.Text)
                cmd.Parameters.AddWithValue("@supplier_id", selectedSupplierId)
                conn.Open()
                cmd.ExecuteNonQuery()
                MessageBox.Show("Supplier updated successfully!")

                ClearSupplierInputs()

                selectedSupplierId = 0
                LoadSuppliers()

                btn_addsupplier.Enabled = True
            End Using
        End Using
    End Sub
    Private Sub tb_contactnumber_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tb_contactnumber.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub btn_clearsuppliers_Click_1(sender As Object, e As EventArgs) Handles btn_clearsuppliers.Click
        tb_companyname.Clear()
        tb_contactperson.Clear()
        tb_contactnumber.Clear()
        tb_address.Clear()
        tb_note.Clear()
        selectedSupplierId = 0
    End Sub

    Private Sub btn_searchsuppliers_TextChanged(sender As Object, e As EventArgs) Handles btn_searchsuppliers.TextChanged
        Dim searchTerm As String = btn_searchsuppliers.Text.Trim()

        If String.IsNullOrEmpty(searchTerm) Then
            LoadSuppliers()
        Else
            Dim query As String = "SELECT * FROM suppliers WHERE company_name LIKE @searchTerm OR contact_person LIKE @searchTerm"
            Using conn As New SQLiteConnection(connectionString)
                Using cmd As New SQLiteCommand(query, conn)
                    cmd.Parameters.AddWithValue("@searchTerm", "%" & searchTerm & "%") ' Add wildcards for partial matching
                    conn.Open()
                    Using reader As SQLiteDataReader = cmd.ExecuteReader()
                        Dim dt As New DataTable()
                        dt.Load(reader)
                        datatable_suppliers.DataSource = dt
                    End Using
                End Using
            End Using
        End If
    End Sub
    Private Sub btn_archivesupplier_Click(sender As Object, e As EventArgs) Handles btn_archivesupplier.Click
        If selectedSupplierId > 0 Then
            Dim archiveQuery As String = "INSERT INTO archived_suppliers (supplier_id, company_name, contact_person, contact_number, address, note) " &
                                      "SELECT supplier_id, company_name, contact_person, contact_number, address, note FROM suppliers WHERE supplier_id=@supplier_id"
            Dim deleteQuery As String = "DELETE FROM suppliers WHERE supplier_id=@supplier_id"

            Using conn As New SQLiteConnection(connectionString)
                conn.Open()
                Dim transaction As SQLiteTransaction = conn.BeginTransaction()

                Try
                    ' Debugging: Check if the correct supplier is being selected
                    Dim checkSupplierQuery As String = "SELECT supplier_id, company_name, contact_person, contact_number, address, note FROM suppliers WHERE supplier_id=@supplier_id"
                    Using checkCmd As New SQLiteCommand(checkSupplierQuery, conn, transaction)
                        checkCmd.Parameters.AddWithValue("@supplier_id", selectedSupplierId)
                        Using reader As SQLiteDataReader = checkCmd.ExecuteReader()
                            If reader.Read() Then
                                Console.WriteLine($"Archiving Supplier: {reader("company_name")}, {reader("contact_person")}")
                            Else
                                MessageBox.Show("Supplier not found.")
                                transaction.Rollback()
                                Return
                            End If
                        End Using
                    End Using

                    ' Archive the supplier
                    Dim rowsAffected As Integer = 0
                    Using archiveCmd As New SQLiteCommand(archiveQuery, conn, transaction)
                        archiveCmd.Parameters.AddWithValue("@supplier_id", selectedSupplierId)
                        rowsAffected = archiveCmd.ExecuteNonQuery()
                    End Using

                    If rowsAffected > 0 Then
                        Using deleteCmd As New SQLiteCommand(deleteQuery, conn, transaction)
                            deleteCmd.Parameters.AddWithValue("@supplier_id", selectedSupplierId)
                            deleteCmd.ExecuteNonQuery()
                        End Using

                        transaction.Commit()
                        MessageBox.Show("Supplier archived and deleted successfully!")
                    Else
                        transaction.Rollback()
                        MessageBox.Show("Failed to archive supplier, deletion aborted.")
                    End If

                    LoadSuppliers()
                    LoadArchivedSuppliers()
                    ClearSupplierInputs()
                    btn_addsupplier.Enabled = True

                Catch ex As SQLiteException
                    transaction.Rollback()
                    MessageBox.Show("Error while archiving supplier: " & ex.Message)
                End Try
            End Using
        Else
            MessageBox.Show("Please select a supplier to archive.")
        End If
    End Sub


    Private Sub btn_unarchivesupplier_Click(sender As Object, e As EventArgs) Handles btn_unarchivesupplier.Click
        Dim unarchivedCount As Integer = 0

        Using conn As New SQLiteConnection(connectionString)
            conn.Open()
            Dim transaction As SQLiteTransaction = conn.BeginTransaction()

            Try
                For Each row As DataGridViewRow In datatable_unarchivesuppliers.Rows
                    Dim chkCell As DataGridViewCheckBoxCell = CType(row.Cells("chkSelect"), DataGridViewCheckBoxCell)
                    If CBool(chkCell.Value) Then

                        Dim supplierId As Integer = CInt(row.Cells("supplier_id").Value)
                        Dim companyName As String = row.Cells("company_name").Value.ToString()
                        Dim contactPerson As String = row.Cells("contact_person").Value.ToString()
                        Dim address As String = row.Cells("Address").Value.ToString()
                        Dim contactNumber As String = row.Cells("contact_number").Value.ToString()
                        Dim note As String = row.Cells("note").Value.ToString()

                        Dim rowsAffected As Integer = 0
                        Using insertCmd As New SQLiteCommand("INSERT INTO suppliers (supplier_id, company_name, contact_person, contact_number, address, note) " &
                                                        "VALUES (@supplier_id, @company_name, @contact_person, @contact_number, @address, @note)", conn, transaction)
                            insertCmd.Parameters.AddWithValue("@supplier_id", supplierId)
                            insertCmd.Parameters.AddWithValue("@company_name", companyName)
                            insertCmd.Parameters.AddWithValue("@contact_person", contactPerson)
                            insertCmd.Parameters.AddWithValue("@contact_number", contactNumber)
                            insertCmd.Parameters.AddWithValue("@address", address)
                            insertCmd.Parameters.AddWithValue("@note", note)
                            rowsAffected = insertCmd.ExecuteNonQuery()
                        End Using

                        If rowsAffected > 0 Then
                            Using deleteCmd As New SQLiteCommand("DELETE FROM archived_suppliers WHERE supplier_id = @supplier_id", conn, transaction)
                                deleteCmd.Parameters.AddWithValue("@supplier_id", supplierId)
                                deleteCmd.ExecuteNonQuery()
                            End Using
                            unarchivedCount += 1
                        Else
                            MessageBox.Show("Failed to unarchive supplier with ID " & supplierId.ToString(), "Unarchive Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            transaction.Rollback()
                            Exit Sub
                        End If
                    End If
                Next

                transaction.Commit()
                LoadSuppliers()
                LoadArchivedSuppliers()

                If unarchivedCount > 0 Then
                    MessageBox.Show(unarchivedCount.ToString() & " supplier(s) successfully unarchived.", "Unarchive Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show("No suppliers were selected for unarchiving.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If

            Catch ex As SQLiteException
                transaction.Rollback()
                MessageBox.Show("Error while unarchiving supplier: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub LoadArchivedSuppliers()
        Dim query As String = "SELECT supplier_id, company_name, contact_person, contact_number, address, note FROM archived_suppliers"

        Using conn As New SQLiteConnection(connectionString)
            Using cmd As New SQLiteCommand(query, conn)
                Dim adapter As New SQLiteDataAdapter(cmd)
                Dim dt As New DataTable()

                Try
                    conn.Open()
                    adapter.Fill(dt)

                    datatable_unarchivesuppliers.DataSource = dt

                    If datatable_unarchivesuppliers.Columns("chkSelect") Is Nothing Then
                        Dim chk As New DataGridViewCheckBoxColumn()
                        chk.HeaderText = "Select"
                        chk.Name = "chkSelect"
                        datatable_unarchivesuppliers.Columns.Insert(0, chk)
                    End If

                    If datatable_unarchivesuppliers.Columns.Count >= 6 Then
                        datatable_unarchivesuppliers.Columns(1).HeaderText = "Supplier ID"
                        datatable_unarchivesuppliers.Columns(2).HeaderText = "Company Name"
                        datatable_unarchivesuppliers.Columns(3).HeaderText = "Contact Person"
                        datatable_unarchivesuppliers.Columns(4).HeaderText = "Contact Number"
                        datatable_unarchivesuppliers.Columns(5).HeaderText = "Address"
                        datatable_unarchivesuppliers.Columns(6).HeaderText = "Note"
                    End If

                Catch ex As SQLiteException
                    MessageBox.Show("Error loading archived suppliers: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

    Private Sub InsertIntoSuppliers(supplierId As Integer, companyName As String, contactPerson As String, contactNumber As String, address As String, note As String)
        Dim query As String = "INSERT INTO suppliers (supplier_id, company_name, contact_person, contact_number, address, note) " &
                          "VALUES (@supplier_id, @company_name, @contact_person, @contact_number, @address, @note)"

        Using conn As New SQLiteConnection(connectionString)
            Using cmd As New SQLiteCommand(query, conn)
                cmd.Parameters.AddWithValue("@supplier_id", supplierId)
                cmd.Parameters.AddWithValue("@company_name", companyName)
                cmd.Parameters.AddWithValue("@contact_person", contactPerson)
                cmd.Parameters.AddWithValue("@address", address)
                cmd.Parameters.AddWithValue("@contact_number", contactNumber)
                cmd.Parameters.AddWithValue("@note", note)

                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                Catch ex As SQLiteException

                    MessageBox.Show("Error inserting supplier: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub



    Private Sub DeleteFromArchivedSuppliers(supplierId As Integer)
        Dim query As String = "DELETE FROM archived_suppliers WHERE supplier_id = @supplier_id"

        Using conn As New SQLiteConnection(connectionString)
            Using cmd As New SQLiteCommand(query, conn)
                cmd.Parameters.AddWithValue("@supplier_id", supplierId)

                conn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Sub datatable_unarchivesuppliers_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles datatable_unarchivesuppliers.CellContentClick
        If e.ColumnIndex = datatable_unarchivesuppliers.Columns("chkSelect").Index AndAlso e.RowIndex >= 0 Then
            Dim chkCell As DataGridViewCheckBoxCell = CType(datatable_unarchivesuppliers.Rows(e.RowIndex).Cells("chkSelect"), DataGridViewCheckBoxCell)
            chkCell.Value = Not CBool(chkCell.Value)
        End If
    End Sub

    Private Sub tb_searchboxunarchivesupplier_TextChanged(sender As Object, e As EventArgs) Handles tb_searchboxunarchivesupplier.TextChanged
        Dim searchText As String = tb_searchboxunarchivesupplier.Text.Trim()

        If Not String.IsNullOrEmpty(searchText) Then
            Dim dt As DataTable = CType(datatable_unarchivesuppliers.DataSource, DataTable)
            If dt IsNot Nothing Then
                Dim dv As New DataView(dt)
                dv.RowFilter = String.Format("company_name LIKE '%{0}%' OR contact_person LIKE '%{0}%'", searchText)
                datatable_unarchivesuppliers.DataSource = dv
            End If
        Else
            LoadArchivedSuppliers()
        End If
    End Sub

    Private Sub tb_ropreason_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tb_ropreason.SelectedIndexChanged
        Dim selectedReason As String = tb_ropreason.SelectedItem?.ToString()
    End Sub

    Private Sub tb_ropproductname_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tb_ropproductname.SelectedIndexChanged
        If tb_ropproductname.SelectedItem Is Nothing Then Return ' Exit if no product is selected

        Dim selectedProductName As String = tb_ropproductname.SelectedItem.ToString()

        Dim query As String = "SELECT unit_price FROM products WHERE product_name = @product_name"
        Try
            Using conn As New SQLiteConnection(connectionString)
                Using cmd As New SQLiteCommand(query, conn)
                    cmd.Parameters.AddWithValue("@product_name", selectedProductName)
                    conn.Open()

                    Dim unitCost As Object = cmd.ExecuteScalar()
                    If unitCost IsNot Nothing Then
                        ' Format unit cost as decimal with two decimal places
                        tb_ropunitcost.Text = Convert.ToDecimal(unitCost).ToString("F2")
                    Else
                        tb_ropunitcost.Clear()
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("An error occurred: " & ex.Message)
        End Try
    End Sub


    Private Sub tb_ropquantitybrought_TextChanged(sender As Object, e As EventArgs) Handles tb_ropquantitybrought.TextChanged

        Dim input As String = tb_ropquantitybrought.Text
        Dim newText As String = ""

        For Each ch As Char In input
            If Char.IsDigit(ch) Then
                newText += ch
            End If
        Next

        If newText <> input Then
            tb_ropquantitybrought.Text = newText
            tb_ropquantitybrought.SelectionStart = newText.Length
        End If

        UpdateRefundAmount()
    End Sub


    Private Sub UpdateRefundAmount()
        If Not String.IsNullOrWhiteSpace(tb_ropunitcost.Text) AndAlso Not String.IsNullOrWhiteSpace(tb_ropquantitybrought.Text) Then
            Dim unitCost As Decimal = Decimal.Parse(tb_ropunitcost.Text)
            Dim quantity As Integer

            If Integer.TryParse(tb_ropquantitybrought.Text, quantity) Then
                Dim refundAmount As Decimal = unitCost * quantity
                tb_roprefundamount.Text = refundAmount.ToString("F2")
            Else
                tb_roprefundamount.Clear()
            End If
        Else
            tb_roprefundamount.Clear()
        End If
    End Sub

    Private Sub cmb_roptransactionid_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_roptransactionid.SelectedIndexChanged
        If cmb_roptransactionid.SelectedItem IsNot Nothing Then
            Dim selectedTransactionId As String = cmb_roptransactionid.SelectedItem.ToString()
        End If
    End Sub

    Private Sub btn_clearrop_Click(sender As Object, e As EventArgs) Handles btn_clearrop.Click
        ClearReturnProductFields()
    End Sub

    Private Sub ClearReturnProductFields()
        tb_ropproductname.SelectedIndex = -1
        tb_ropunitcost.Clear()
        tb_ropquantitybrought.Clear()
        tb_roprefundamount.Clear()
        tb_ropreason.SelectedIndex = -1
        dtp_returndatetime.Value = DateTime.Now
        cmb_roptransactionid.SelectedIndex = -1
    End Sub

    Private Sub btn_addrop_Click(sender As Object, e As EventArgs) Handles btn_addrop.Click
        ' Validate inputs
        If Not ValidateReturnInputs() Then Return

        ' Retrieve the product_id based on the selected product_name
        Dim productId As Integer
        Dim queryProductId As String = "SELECT product_id FROM products WHERE product_name = @product_name"

        Using conn As New SQLiteConnection(connectionString)
            Using cmd As New SQLiteCommand(queryProductId, conn)
                cmd.Parameters.AddWithValue("@product_name", tb_ropproductname.SelectedItem.ToString())
                conn.Open()
                productId = Convert.ToInt32(cmd.ExecuteScalar())
            End Using
        End Using

        Dim query As String = "INSERT INTO return_of_products (product_id, product_name, unit_cost, return_quantity, refund_amount, return_reason, return_date_time, transaction_id) " &
                          "VALUES (@product_id, @product_name, @unit_cost, @return_quantity, @refund_amount, @return_reason, @return_date_time, @transaction_id)"

        Using conn As New SQLiteConnection(connectionString)
            Using cmd As New SQLiteCommand(query, conn)
                Try
                    cmd.Parameters.AddWithValue("@product_id", productId) ' Include product_id here
                    cmd.Parameters.AddWithValue("@product_name", tb_ropproductname.SelectedItem.ToString())
                    cmd.Parameters.AddWithValue("@unit_cost", Convert.ToDecimal(tb_ropunitcost.Text))
                    cmd.Parameters.AddWithValue("@return_quantity", Convert.ToInt32(tb_ropquantitybrought.Text))
                    cmd.Parameters.AddWithValue("@refund_amount", Convert.ToDecimal(tb_roprefundamount.Text))
                    cmd.Parameters.AddWithValue("@return_reason", tb_ropreason.SelectedItem.ToString())
                    cmd.Parameters.AddWithValue("@return_date_time", dtp_returndatetime.Value)

                    If cmb_roptransactionid.SelectedItem IsNot Nothing Then
                        cmd.Parameters.AddWithValue("@transaction_id", Convert.ToInt32(cmb_roptransactionid.SelectedItem))
                    Else
                        MessageBox.Show("Please select a transaction ID.")
                        Return
                    End If

                    conn.Open()
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Product return recorded successfully!")
                    LoadReturnedProducts()
                    ClearReturnProductFields()
                    LoadArchivedReturnedProducts()
                    LoadReturnofProducts()
                    PopulateROPTransactionIdComboBox()

                Catch ex As Exception
                    MessageBox.Show("An error occurred: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

    Private Function ValidateReturnInputs() As Boolean
        If String.IsNullOrWhiteSpace(tb_ropproductname.SelectedItem?.ToString()) OrElse
           String.IsNullOrWhiteSpace(tb_ropunitcost.Text) OrElse
           String.IsNullOrWhiteSpace(tb_ropquantitybrought.Text) OrElse
           String.IsNullOrWhiteSpace(tb_roprefundamount.Text) OrElse
           tb_ropreason.SelectedIndex = -1 OrElse
           cmb_roptransactionid.SelectedIndex = -1 Then

            MessageBox.Show("All fields must be filled.")
            Return False
        End If

        Dim unitCost As Decimal
        Dim quantityBrought As Integer
        Dim refundAmount As Decimal

        If Not Decimal.TryParse(tb_ropunitcost.Text, unitCost) OrElse
           Not Integer.TryParse(tb_ropquantitybrought.Text, quantityBrought) OrElse
           Not Decimal.TryParse(tb_roprefundamount.Text, refundAmount) Then

            MessageBox.Show("Please enter valid numeric values for unit cost, quantity, and refund amount.")
            Return False
        End If

        Return True
    End Function

    Private selectedReturnId As Integer = 0
    Private Sub datatable_returnofproducts_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles datatable_returnofproducts.CellContentClick
        If e.ColumnIndex = datatable_returnofproducts.Columns("chkSelect").Index AndAlso e.RowIndex >= 0 Then
            Dim chkCell As DataGridViewCheckBoxCell = CType(datatable_returnofproducts.Rows(e.RowIndex).Cells("chkSelect"), DataGridViewCheckBoxCell)
            chkCell.Value = Not CBool(chkCell.Value)

            If CBool(chkCell.Value) = True Then
                Dim row As DataGridViewRow = datatable_returnofproducts.Rows(e.RowIndex)
                tb_ropproductname.SelectedItem = row.Cells("product_name").Value.ToString()

                tb_ropunitcost.Text = Convert.ToDecimal(row.Cells("unit_cost").Value).ToString("F2")
                tb_ropquantitybrought.Text = row.Cells("return_quantity").Value.ToString()
                tb_roprefundamount.Text = Convert.ToDecimal(row.Cells("refund_amount").Value).ToString("F2")
                tb_ropreason.SelectedItem = row.Cells("return_reason").Value.ToString()
                dtp_returndatetime.Value = Convert.ToDateTime(row.Cells("return_date_time").Value)
                cmb_roptransactionid.SelectedItem = row.Cells("transaction_id").Value.ToString()

                selectedReturnId = CInt(row.Cells("return_id").Value)

                btn_addrop.Enabled = False
            Else
                ClearReturnProductFields()
                selectedReturnId = 0

                btn_addrop.Enabled = True
            End If
        End If
    End Sub

    Private Sub tb_searchrop_TextChanged(sender As Object, e As EventArgs) Handles tb_searchrop.TextChanged
        Dim searchTerm As String = tb_searchrop.Text.Trim()
        Dim query As String = "SELECT * FROM return_of_products WHERE product_name LIKE @searchTerm"

        Using conn As New SQLiteConnection(connectionString)
            Using cmd As New SQLiteCommand(query, conn)
                cmd.Parameters.AddWithValue("@searchTerm", "%" & searchTerm & "%")
                conn.Open()
                Using reader As SQLiteDataReader = cmd.ExecuteReader()
                    Dim dt As New DataTable()
                    dt.Load(reader)
                    datatable_returnofproducts.DataSource = dt
                End Using
            End Using
        End Using
    End Sub
    Private Sub btn_ropupdate_Click(sender As Object, e As EventArgs) Handles btn_ropupdate.Click
        If selectedReturnId <= 0 Then
            MessageBox.Show("Please select a return product to update.")
            Return
        End If

        If Not ValidateReturnInputs() Then
            Return
        End If

        Dim query As String = "UPDATE return_of_products SET product_name=@product_name, unit_cost=@unit_cost, return_quantity=@return_quantity, refund_amount=@refund_amount, return_reason=@return_reason, return_date_time=@return_date_time, transaction_id=@transaction_id WHERE return_id=@return_id"
        Using conn As New SQLiteConnection(connectionString)
            Using cmd As New SQLiteCommand(query, conn)
                cmd.Parameters.AddWithValue("@product_name", tb_ropproductname.SelectedItem.ToString())
                cmd.Parameters.AddWithValue("@unit_cost", Convert.ToDecimal(tb_ropunitcost.Text))
                cmd.Parameters.AddWithValue("@return_quantity", Convert.ToInt32(tb_ropquantitybrought.Text))
                cmd.Parameters.AddWithValue("@refund_amount", Convert.ToDecimal(tb_roprefundamount.Text))
                cmd.Parameters.AddWithValue("@return_reason", tb_ropreason.SelectedItem.ToString())
                cmd.Parameters.AddWithValue("@return_date_time", dtp_returndatetime.Value)

                If cmb_roptransactionid.SelectedItem IsNot Nothing Then
                    cmd.Parameters.AddWithValue("@transaction_id", Convert.ToInt32(cmb_roptransactionid.SelectedItem))
                Else
                    MessageBox.Show("Please select a transaction ID.")
                    Return
                End If

                cmd.Parameters.AddWithValue("@return_id", selectedReturnId)

                conn.Open()
                cmd.ExecuteNonQuery()
                MessageBox.Show("Return product updated successfully!")

                ClearReturnProductFields()
                LoadReturnedProducts()
                selectedReturnId = 0
                LoadReturnofProducts()

                btn_addrop.Enabled = True
            End Using
        End Using
    End Sub

    Private Sub tb_searchptr_TextChanged(sender As Object, e As EventArgs) Handles tb_searchptr.TextChanged
        Dim dbPath As String = "C:\Users\Admin\source\repos\ic_electronics_center_app\ic_electronics_center_app\ic_electronics.db"

        Dim connectionString As String = $"Data Source={dbPath};Version=3;"

        Dim connection As SQLiteConnection = New SQLiteConnection(connectionString)

        Try
            connection.Open()

            Dim query As String
            If IsNumeric(tb_searchptr.Text) Then
                query = "SELECT * FROM product_transactions WHERE transaction_id = @search OR quantity = @search"
            Else

                query = "SELECT * FROM product_transactions WHERE product_name LIKE @search"
            End If

            Dim cmd As SQLiteCommand = New SQLiteCommand(query, connection)
            If IsNumeric(tb_searchptr.Text) Then
                cmd.Parameters.AddWithValue("@search", tb_searchptr.Text)
            Else
                cmd.Parameters.AddWithValue("@search", "%" & tb_searchptr.Text & "%")
            End If

            Dim adapter As SQLiteDataAdapter = New SQLiteDataAdapter(cmd)
            Dim table As New DataTable()

            adapter.Fill(table)
            datatable_producttransactions.DataSource = table

        Catch ex As SQLiteException

            MessageBox.Show("Database connection error: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            connection.Close()
        End Try
    End Sub


    Private Sub tb_searchstr_TextChanged(sender As Object, e As EventArgs) Handles tb_searchstr.TextChanged
        Dim dbPath As String = "C:\Users\Admin\source\repos\ic_electronics_center_app\ic_electronics_center_app\ic_electronics.db"

        Dim connectionString As String = $"Data Source={dbPath};Version=3;"

        Dim connection As SQLiteConnection = New SQLiteConnection(connectionString)

        Try
            connection.Open()

            Dim query As String
            If IsNumeric(tb_searchstr.Text) Then
                query = "SELECT * FROM service_transactions WHERE transaction_id = @search OR service_id = @search"
            Else
                query = "SELECT * FROM service_transactions WHERE service_name LIKE @search"
            End If

            Dim cmd As SQLiteCommand = New SQLiteCommand(query, connection)
            If IsNumeric(tb_searchstr.Text) Then
                cmd.Parameters.AddWithValue("@search", tb_searchstr.Text)
            Else
                cmd.Parameters.AddWithValue("@search", "%" & tb_searchstr.Text & "%")
            End If

            Dim adapter As SQLiteDataAdapter = New SQLiteDataAdapter(cmd)
            Dim table As New DataTable()

            adapter.Fill(table)
            datatable_servicetransactions.DataSource = table

        Catch ex As SQLiteException
            MessageBox.Show("Database connection error: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            connection.Close()
        End Try
    End Sub


    Private Sub LoadTop5SellingProducts()
        Dim dbPath As String = "C:\Users\Admin\source\repos\ic_electronics_center_app\ic_electronics_center_app\ic_electronics.db"

        Dim connectionString As String = $"Data Source={dbPath};Version=3;"
        Dim query As String = "SELECT product_name, SUM(quantity) AS total_sold " &
                          "FROM product_transactions " &
                          "GROUP BY product_name " &
                          "ORDER BY total_sold DESC " &
                          "LIMIT 5;"

        Using connection As New SQLiteConnection(connectionString)
            Dim command As New SQLiteCommand(query, connection)

            Try
                connection.Open()
                Dim reader As SQLiteDataReader = command.ExecuteReader()
                Dim dt As New DataTable()
                dt.Load(reader)

                datatable_top5sellingproducts.Rows.Clear()
                datatable_top5sellingproducts.Columns.Clear()

                datatable_top5sellingproducts.Columns.Add("ProductName", "Product Name")
                datatable_top5sellingproducts.Columns.Add("TotalSold", "Total Sold")

                For Each row As DataRow In dt.Rows
                    datatable_top5sellingproducts.Rows.Add(row("product_name"), row("total_sold"))
                Next

                datatable_top5sellingproducts.AutoResizeColumns()

            Catch ex As Exception
                MessageBox.Show("An error occurred: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub LoadReturnedProducts()
        Dim dbPath As String = "C:\Users\Admin\source\repos\ic_electronics_center_app\ic_electronics_center_app\ic_electronics.db"
        Dim connectionString As String = $"Data Source={dbPath};Version=3;"
        Dim query As String = "SELECT product_name, SUM(return_quantity) AS total_returned, SUM(refund_amount) AS total_refunded " &
                          "FROM return_of_products " &
                          "GROUP BY product_name " &
                          "ORDER BY total_returned DESC;"

        Using connection As New SQLiteConnection(connectionString)
            Dim command As New SQLiteCommand(query, connection)
            Dim dt As New DataTable()

            Try
                connection.Open()
                Dim adapter As New SQLiteDataAdapter(command)
                adapter.Fill(dt)

                datatable_returnedproductssummary.DataSource = dt
                datatable_returnedproductssummary.AutoResizeColumns()

            Catch ex As Exception
                MessageBox.Show("An error occurred: " & ex.Message)
            End Try
        End Using
    End Sub

    ' Add the following event handler for formatting the DataGridView
    Private Sub datatable_returnedproductssummary_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles datatable_returnedproductssummary.CellFormatting
        If e.ColumnIndex = datatable_returnedproductssummary.Columns("total_refunded").Index Then
            If e.Value IsNot Nothing AndAlso IsNumeric(e.Value) Then
                e.Value = String.Format("{0:N2}", e.Value) ' Format to two decimal places
                e.FormattingApplied = True
            End If
        End If
    End Sub

    Private Sub datatable_top5sellingproducts_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles datatable_top5sellingproducts.CellContentClick
        Dim dbPath As String = "C:\Users\Admin\source\repos\ic_electronics_center_app\ic_electronics_center_app\ic_electronics.db"

        Dim connectionString As String = $"Data Source={dbPath};Version=3;"
        Dim query As String = "SELECT product_name, SUM(quantity) AS total_sold " &
                          "FROM product_transactions " &
                          "GROUP BY product_name " &
                          "ORDER BY total_sold DESC " &
                          "LIMIT 5;"

        Using connection As New SQLiteConnection(connectionString)
            Dim command As New SQLiteCommand(query, connection)

            Try
                connection.Open()
                Dim reader As SQLiteDataReader = command.ExecuteReader()
                Dim dt As New DataTable()
                dt.Load(reader)

                datatable_top5sellingproducts.Rows.Clear()

                For Each row As DataRow In dt.Rows
                    datatable_top5sellingproducts.Rows.Add(row("product_name"), row("total_sold"))
                Next

            Catch ex As Exception
                MessageBox.Show("An error occurred: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub LoadServiceSalesReport()
        Dim dbPath As String = "C:\Users\Admin\source\repos\ic_electronics_center_app\ic_electronics_center_app\ic_electronics.db"
        Dim connectionString As String = $"Data Source={dbPath};Version=3;"

        ' SQL query to get aggregated sales report data for services
        Dim query As String = "
    SELECT 
        s.service_name,
        SUM(st.amount_paid) AS TotalRevenue,
        SUM(st.change_given) AS TotalChangeGiven,
        COUNT(st.transaction_id) AS TotalTransactions
    FROM service_transactions st
    JOIN services s ON st.service_id = s.service_id
    GROUP BY s.service_name
    ORDER BY TotalRevenue DESC;"

        Using connection As New SQLiteConnection(connectionString)
            Dim command As New SQLiteCommand(query, connection)

            Try
                connection.Open()
                Dim reader As SQLiteDataReader = command.ExecuteReader()
                Dim dt As New DataTable()
                dt.Load(reader)

                datatable_salesreportservice.Rows.Clear()
                datatable_salesreportservice.Columns.Clear()

                ' Add columns for the aggregated sales report
                datatable_salesreportservice.Columns.Add("ServiceName", "Service Name")
                datatable_salesreportservice.Columns.Add("TotalRevenue", "Total Revenue")
                datatable_salesreportservice.Columns.Add("TotalChangeGiven", "Total Change Given")
                datatable_salesreportservice.Columns.Add("TotalTransactions", "Total Transactions")

                ' Populate DataGridView with data
                For Each row As DataRow In dt.Rows
                    datatable_salesreportservice.Rows.Add(
                    row("service_name"),
                    Convert.ToDecimal(row("TotalRevenue")).ToString("N2"),       ' Format to 2 decimal places
                    Convert.ToDecimal(row("TotalChangeGiven")).ToString("N2"),  ' Format to 2 decimal places
                    row("TotalTransactions")
                )
                Next

                datatable_salesreportservice.AutoResizeColumns()
            Catch ex As Exception
                MessageBox.Show("An error occurred: " & ex.Message)
            End Try
        End Using
    End Sub


    Private Sub LoadSalesReport()
        Dim dbPath As String = "C:\Users\Admin\source\repos\ic_electronics_center_app\ic_electronics_center_app\ic_electronics.db"
        Dim connectionString As String = $"Data Source={dbPath};Version=3;"

        ' SQL query to get aggregated sales report data
        Dim query As String = "SELECT p.product_name, " &
                          "SUM(pt.quantity) AS TotalQuantitySold, " &
                          "SUM(pt.total_cost) AS TotalSalesRevenue, " &
                          "AVG(pt.unit_cost) AS AverageUnitCost " &
                          "FROM product_transactions pt " &
                          "INNER JOIN products p ON pt.product_id = p.product_id " &
                          "GROUP BY p.product_name " &
                          "ORDER BY TotalSalesRevenue DESC;"

        Using connection As New SQLiteConnection(connectionString)
            Dim command As New SQLiteCommand(query, connection)

            Try
                connection.Open()
                Dim reader As SQLiteDataReader = command.ExecuteReader()
                Dim dt As New DataTable()
                dt.Load(reader)

                datatable_salesreportproduct.Rows.Clear()
                datatable_salesreportproduct.Columns.Clear()

                ' Add columns for the aggregated sales report
                datatable_salesreportproduct.Columns.Add("ProductName", "Product Name")
                datatable_salesreportproduct.Columns.Add("TotalQuantitySold", "Total Quantity Sold")
                datatable_salesreportproduct.Columns.Add("TotalSalesRevenue", "Total Sales Revenue")
                datatable_salesreportproduct.Columns.Add("AverageUnitCost", "Average Unit Cost")

                ' Populate DataGridView with data
                For Each row As DataRow In dt.Rows
                    datatable_salesreportproduct.Rows.Add(
                    row("product_name"),
                    row("TotalQuantitySold"),
                    Convert.ToDecimal(row("TotalSalesRevenue")).ToString("N2"),   ' Format to 2 decimal places
                    Convert.ToDecimal(row("AverageUnitCost")).ToString("N2")      ' Format to 2 decimal places
                )
                Next

                datatable_salesreportproduct.AutoResizeColumns()
            Catch ex As Exception
                MessageBox.Show("An error occurred: " & ex.Message)
            End Try
        End Using
    End Sub



    Private Sub LoadLowStockProducts()
        Dim dbPath As String = "C:\Users\Admin\source\repos\ic_electronics_center_app\ic_electronics_center_app\ic_electronics.db"
        Dim connectionString As String = $"Data Source={dbPath};Version=3;"

        Dim query As String = "SELECT product_name, quantity_in_stock FROM products WHERE quantity_in_stock < 15;"

        Using connection As New SQLiteConnection(connectionString)
            Dim command As New SQLiteCommand(query, connection)

            Try
                connection.Open()
                Dim reader As SQLiteDataReader = command.ExecuteReader()
                Dim dt As New DataTable()
                dt.Load(reader)

                datatable_lowstockproducts.Rows.Clear()
                datatable_lowstockproducts.Columns.Clear()

                ' Add columns for product name and quantity in stock
                datatable_lowstockproducts.Columns.Add("ProductName", "Product Name")
                datatable_lowstockproducts.Columns.Add("QuantityInStock", "Quantity in Stock")

                For Each row As DataRow In dt.Rows
                    datatable_lowstockproducts.Rows.Add(row("product_name"), row("quantity_in_stock"))
                Next

                datatable_lowstockproducts.AutoResizeColumns()

            Catch ex As Exception
                MessageBox.Show("An error occurred: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub datatable_lowstockproducts_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles datatable_lowstockproducts.CellContentClick
        ' Ensure you only handle valid cell clicks
        If e.RowIndex < 0 Then Return

        ' For now, we'll just show the product details without reordering
        Dim productName As String = datatable_lowstockproducts.Rows(e.RowIndex).Cells("ProductName").Value.ToString()
        Dim currentQuantity As Integer = Integer.Parse(datatable_lowstockproducts.Rows(e.RowIndex).Cells("QuantityInStock").Value.ToString())

        ' Show product details
        Dim detailsMessage As String = $"Product Name: {productName}" & vbCrLf &
                                    $"Current Stock: {currentQuantity}"
        MessageBox.Show(detailsMessage, "Product Details")
    End Sub



    Private Sub LoadSalesByMonth()
        ' SQLite connection string
        Dim dbPath As String = "C:\Users\Admin\source\repos\ic_electronics_center_app\ic_electronics_center_app\ic_electronics.db"

        Dim connectionString As String = $"Data Source={dbPath};Version=3;"

        ' Get the current month and year
        Dim currentDate As DateTime = DateTime.Now
        Dim currentYear As Integer = currentDate.Year
        Dim currentMonth As Integer = currentDate.Month

        Dim startDate As New DateTime(currentYear, currentMonth, 1)
        Dim endDate As New DateTime(currentYear, currentMonth, DateTime.DaysInMonth(currentYear, currentMonth)) ' Last day of the current month

        Dim query As String = "SELECT strftime('%Y-%m', pt.transaction_date_time) AS sales_month, " &
                          "SUM(pt.total_cost) AS total_product_sales, " &
                          "(SELECT SUM(st.service_cost) FROM service_transactions st " &
                          "WHERE strftime('%Y-%m', st.transaction_date_time) = strftime('%Y-%m', pt.transaction_date_time)) AS total_service_sales " &
                          "FROM product_transactions pt " &
                          "WHERE pt.transaction_date_time BETWEEN @startDate AND @endDate " &
                          "GROUP BY sales_month " &
                          "ORDER BY sales_month;"

        Using connection As New SQLiteConnection(connectionString)
            Dim command As New SQLiteCommand(query, connection)
            command.Parameters.AddWithValue("@startDate", startDate.ToString("yyyy-MM-dd"))
            command.Parameters.AddWithValue("@endDate", endDate.ToString("yyyy-MM-dd"))

            Try
                connection.Open()
                Dim reader As SQLiteDataReader = command.ExecuteReader()
                Dim dt As New DataTable()
                dt.Load(reader)

                ' Clear existing rows and columns
                datatable_salesbymonth.Rows.Clear()
                datatable_salesbymonth.Columns.Clear()

                ' Add columns for Sales Month, Total Product Sales, and Total Service Sales
                datatable_salesbymonth.Columns.Add("SalesMonth", "Sales Month")
                datatable_salesbymonth.Columns.Add("TotalProductSales", "Total Product Sales")
                datatable_salesbymonth.Columns.Add("TotalServiceSales", "Total Service Sales")

                For Each row As DataRow In dt.Rows
                    datatable_salesbymonth.Rows.Add(row("sales_month"),
                                         If(IsDBNull(row("total_product_sales")), 0, Convert.ToDecimal(row("total_product_sales")).ToString("F2")),
                                         If(IsDBNull(row("total_service_sales")), 0, Convert.ToDecimal(row("total_service_sales")).ToString("F2")))
                Next
                datatable_salesbymonth.AutoResizeColumns()

            Catch ex As Exception
                ' Display any errors that occur
                MessageBox.Show("An error occurred: " & ex.Message)
            End Try
        End Using
    End Sub


    Private Sub LoadSalesByDay()
        Dim dbPath As String = "C:\Users\Admin\source\repos\ic_electronics_center_app\ic_electronics_center_app\ic_electronics.db"

        Dim connectionString As String = $"Data Source={dbPath};Version=3;"

        ' Get today's date
        Dim today As DateTime = DateTime.Today

        ' Query to get total product and service sales for today
        Dim query As String = "
        SELECT
            DATE(transaction_date_time) AS sales_date,
            SUM(total_cost) AS total_product_sales,
            (SELECT IFNULL(SUM(service_cost), 0) FROM service_transactions WHERE DATE(transaction_date_time) = DATE(@today)) AS total_service_sales
        FROM product_transactions
        WHERE DATE(transaction_date_time) = DATE(@today)
        GROUP BY sales_date;"

        Using connection As New SQLiteConnection(connectionString)
            Dim command As New SQLiteCommand(query, connection)
            command.Parameters.AddWithValue("@today", today.ToString("yyyy-MM-dd"))

            Try
                connection.Open()
                Dim reader As SQLiteDataReader = command.ExecuteReader()
                Dim dt As New DataTable()
                dt.Load(reader)

                Debug.WriteLine("Row Count: " & dt.Rows.Count.ToString())

                datatable_salesbydaily.Rows.Clear()
                datatable_salesbydaily.Columns.Clear()

                If datatable_salesbydaily.Columns.Count = 0 Then
                    datatable_salesbydaily.Columns.Add("SalesDate", "Sales Date")
                    datatable_salesbydaily.Columns.Add("TotalProductSales", "Total Product Sales")
                    datatable_salesbydaily.Columns.Add("TotalServiceSales", "Total Service Sales")
                End If

                For Each row As DataRow In dt.Rows
                    datatable_salesbydaily.Rows.Add(row("sales_date"),
                                         If(IsDBNull(row("total_product_sales")), 0, Convert.ToDecimal(row("total_product_sales")).ToString("F2")),
                                         If(IsDBNull(row("total_service_sales")), 0, Convert.ToDecimal(row("total_service_sales")).ToString("F2")))
                Next

                datatable_salesbydaily.AutoResizeColumns()

            Catch ex As Exception
                MessageBox.Show("An error occurred while loading data: " & ex.Message)
                Debug.WriteLine("Exception: " & ex.ToString())
            End Try
        End Using
    End Sub


    Private Sub LoadSalesByWeek()
        Dim currentDate As DateTime = DateTime.Now

        Dim startDate As DateTime = currentDate.AddDays(-(CInt(currentDate.DayOfWeek + 6) Mod 7)) ' Start on Monday
        Dim endDate As DateTime = startDate.AddDays(6)


        If currentDate > endDate Then
            startDate = startDate.AddDays(7)
            endDate = endDate.AddDays(7)
        End If

        ' Adjusted query for SQLite
        Dim query As String = "SELECT sales_week, " &
                          "SUM(total_product_sales) AS total_product_sales, " &
                          "SUM(total_service_sales) AS total_service_sales " &
                          "FROM ( " &
                          "   SELECT strftime('%Y-%W', pt.transaction_date_time) AS sales_week, " &
                          "          SUM(pt.total_cost) AS total_product_sales, " &
                          "          0 AS total_service_sales " &
                          "   FROM product_transactions pt " &
                          "   WHERE pt.transaction_date_time BETWEEN @startDate AND @endDate " &
                          "   GROUP BY sales_week " &
                          "   UNION ALL " &
                          "   SELECT strftime('%Y-%W', st.transaction_date_time) AS sales_week, " &
                          "          0 AS total_product_sales, " &
                          "          SUM(st.service_cost) AS total_service_sales " &
                          "   FROM service_transactions st " &
                          "   WHERE st.transaction_date_time BETWEEN @startDate AND @endDate " &
                          "   GROUP BY sales_week " &
                          ") AS combined_sales " &
                          "GROUP BY sales_week " &
                          "ORDER BY sales_week;"

        Using connection As New SQLiteConnection(connectionString)
            Dim command As New SQLiteCommand(query, connection)
            command.Parameters.AddWithValue("@startDate", startDate.ToString("yyyy-MM-dd"))
            command.Parameters.AddWithValue("@endDate", endDate.ToString("yyyy-MM-dd"))

            Try
                connection.Open()
                Dim reader As SQLiteDataReader = command.ExecuteReader()
                Dim dt As New DataTable()
                dt.Load(reader)

                datatable_salesbyweek.Rows.Clear()
                datatable_salesbyweek.Columns.Clear()

                datatable_salesbyweek.Columns.Add("SalesWeek", "Sales Week")
                datatable_salesbyweek.Columns.Add("TotalProductSales", "Total Product Sales")
                datatable_salesbyweek.Columns.Add("TotalServiceSales", "Total Service Sales")

                For Each row As DataRow In dt.Rows
                    datatable_salesbyweek.Rows.Add(row("sales_week"),
                                        If(IsDBNull(row("total_product_sales")), 0, Convert.ToDecimal(row("total_product_sales")).ToString("F2")),
                                        If(IsDBNull(row("total_service_sales")), 0, Convert.ToDecimal(row("total_service_sales")).ToString("F2")))
                Next

                datatable_salesbyweek.AutoResizeColumns()

            Catch ex As Exception
                MessageBox.Show("An error occurred: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub LoadReturnedProductsSummary()
        Dim query As String = "SELECT product_name, SUM(return_quantity) AS total_returned, SUM(refund_amount) AS total_refunded " &
                          "FROM return_of_products " &
                          "GROUP BY product_name " &
                          "ORDER BY total_returned DESC;"

        Using connection As New SQLiteConnection(connectionString)
            Dim command As New SQLiteCommand(query, connection)

            Try
                connection.Open()
                Dim reader As SQLiteDataReader = command.ExecuteReader()
                Dim dt As New DataTable()
                dt.Load(reader)

                datatable_returnedproductssummary.Rows.Clear()

                For Each row As DataRow In dt.Rows
                    datatable_returnedproductssummary.Rows.Add(row("product_name"), row("total_returned"), row("total_refunded"))
                Next

                datatable_returnedproductssummary.AutoResizeColumns()

            Catch ex As Exception
                MessageBox.Show("An error occurred: " & ex.Message)
            End Try
        End Using
    End Sub



    Private Sub datatable_archiveproducts_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles datatable_archiveproducts.CellContentClick
        If e.ColumnIndex = datatable_archiveproducts.Columns("chkSelect").Index AndAlso e.RowIndex >= 0 Then
            Dim chkCell As DataGridViewCheckBoxCell = CType(datatable_archiveproducts.Rows(e.RowIndex).Cells("chkSelect"), DataGridViewCheckBoxCell)
            chkCell.Value = Not CBool(chkCell.Value)
        End If
    End Sub

    Private Sub RefreshArchivedProducts()
        LoadArchivedProducts()
    End Sub

    Private Sub tb_searcharchiveproducts_TextChanged(sender As Object, e As EventArgs) Handles tb_searcharchiveproducts.TextChanged
        Dim searchText As String = tb_searcharchiveproducts.Text.Trim()

        If Not String.IsNullOrEmpty(searchText) Then
            Dim dt As DataTable = CType(datatable_archiveproducts.DataSource, DataTable)
            If dt IsNot Nothing Then
                Dim dv As New DataView(dt)
                dv.RowFilter = String.Format("product_name LIKE '%{0}%' OR supplier_id LIKE '%{0}%'", searchText)

                datatable_archiveproducts.DataSource = dv
            End If
        Else
            LoadArchivedProducts()
        End If
    End Sub

    Private Sub btn_unarchiveproducts_Click(sender As Object, e As EventArgs) Handles btn_unarchiveproducts.Click
        Dim unarchivedCount As Integer = 0

        For Each row As DataGridViewRow In datatable_archiveproducts.Rows
            Dim chkCell As DataGridViewCheckBoxCell = CType(row.Cells("chkSelect"), DataGridViewCheckBoxCell)
            If CBool(chkCell.Value) Then
                Try
                    Dim productId As Integer = CInt(row.Cells("product_id").Value)
                    Dim supplierId As Integer = CInt(row.Cells("supplier_id").Value)
                    Dim productName As String = row.Cells("product_name").Value.ToString()
                    Dim unitPrice As Decimal = CDec(row.Cells("unit_price").Value)
                    Dim quantityInStock As Integer = CInt(row.Cells("quantity_in_stock").Value)
                    Dim unitOfMeasurement As String = row.Cells("unit_of_measurement").Value.ToString()
                    Dim totalCost As Decimal = CDec(row.Cells("total_cost").Value)
                    Dim remainingStockValue As Integer = CInt(row.Cells("remaining_stock_value").Value)
                    Dim sold As Integer = CInt(row.Cells("sold").Value)

                    InsertIntoProducts(productId, supplierId, productName, unitPrice, quantityInStock, unitOfMeasurement, totalCost, remainingStockValue, sold)
                    DeleteFromArchiveProducts(productId)

                    unarchivedCount += 1
                Catch ex As Exception
                    MessageBox.Show("Error unarchiving product: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        Next

        If unarchivedCount > 0 Then
            LoadProducts()
            LoadArchivedProducts()
            MessageBox.Show(unarchivedCount & " product(s) successfully unarchived!", "Unarchive Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("No products were selected for unarchiving.", "Unarchive Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub LoadArchivedProducts()
        Dim query As String = "SELECT archive_id, product_id, supplier_id, product_name, unit_price, quantity_in_stock, unit_of_measurement, total_cost, remaining_stock_value, sold FROM archived_products"

        Using conn As New SQLiteConnection(connectionString)
            Using cmd As New SQLiteCommand(query, conn)
                Dim adapter As New SQLiteDataAdapter(cmd)
                Dim dt As New DataTable()

                Try
                    conn.Open()
                    adapter.Fill(dt)

                    datatable_archiveproducts.DataSource = Nothing
                    datatable_archiveproducts.Rows.Clear()

                    datatable_archiveproducts.DataSource = dt

                    ' Add checkbox column if it doesn't exist
                    If Not datatable_archiveproducts.Columns.Contains("chkSelect") Then
                        Dim chkSelectColumn As New DataGridViewCheckBoxColumn()
                        chkSelectColumn.Name = "chkSelect"
                        chkSelectColumn.HeaderText = "Select"
                        datatable_archiveproducts.Columns.Insert(0, chkSelectColumn)
                    End If

                    ' Initialize checkbox values
                    For Each row As DataGridViewRow In datatable_archiveproducts.Rows
                        row.Cells("chkSelect").Value = False
                    Next

                    ' Format relevant columns with two decimal places
                    Dim decimalColumns As String() = {"unit_price", "total_cost", "remaining_stock_value"}
                    For Each colName As String In decimalColumns
                        If datatable_archiveproducts.Columns.Contains(colName) Then
                            datatable_archiveproducts.Columns(colName).DefaultCellStyle.Format = "F2"
                        End If
                    Next

                Catch ex As Exception
                    MessageBox.Show("Error loading archived products: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

    Private Sub InsertIntoProducts(productId As Integer, supplierId As Integer, productName As String, unitPrice As Decimal, quantityInStock As Integer, unitOfMeasurement As String, totalCost As Decimal, remainingStockValue As Integer, sold As Integer)
        ' SQL insert statement
        Dim query As String = "INSERT INTO products (product_id, supplier_id, product_name, unit_price, quantity_in_stock, unit_of_measurement, total_cost, remaining_stock_value, sold) " &
                          "VALUES (@product_id, @supplier_id, @product_name, @unit_price, @quantity_in_stock, @unit_of_measurement, @total_cost, @remaining_stock_value, @sold)"

        Using conn As New SQLiteConnection(connectionString)
            Using cmd As New SQLiteCommand(query, conn)
                cmd.Parameters.AddWithValue("@product_id", productId)
                cmd.Parameters.AddWithValue("@supplier_id", supplierId)
                cmd.Parameters.AddWithValue("@product_name", productName)
                cmd.Parameters.AddWithValue("@unit_price", unitPrice)
                cmd.Parameters.AddWithValue("@quantity_in_stock", quantityInStock)
                cmd.Parameters.AddWithValue("@unit_of_measurement", unitOfMeasurement)
                cmd.Parameters.AddWithValue("@total_cost", totalCost)
                cmd.Parameters.AddWithValue("@remaining_stock_value", remainingStockValue)
                cmd.Parameters.AddWithValue("@sold", sold)

                conn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Sub DeleteFromArchiveProducts(productId As Integer)
        Dim query As String = "DELETE FROM archived_products WHERE product_id = @product_id"

        Using conn As New SQLiteConnection(connectionString)
            Using cmd As New SQLiteCommand(query, conn)
                cmd.Parameters.AddWithValue("@product_id", productId)

                conn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Sub SetupChart()
        Chart1.Series.Clear()

        Dim series As New DataVisualization.Charting.Series("SalesData")
        series.ChartType = DataVisualization.Charting.SeriesChartType.Pie
        series.IsValueShownAsLabel = True
        Chart1.Series.Add(series)

        Dim totalProductSales As Decimal = 0
        Dim totalServiceSales As Decimal = 0

        Try
            Using conn As New SQLiteConnection(connectionString)
                conn.Open()

                Using cmd As New SQLiteCommand("SELECT SUM(total_cost) FROM product_transactions", conn)
                    Dim result = cmd.ExecuteScalar()
                    totalProductSales = If(IsDBNull(result), 0, Convert.ToDecimal(result))
                End Using

                Using cmd As New SQLiteCommand("SELECT SUM(service_cost) FROM service_transactions", conn)
                    Dim result = cmd.ExecuteScalar()
                    totalServiceSales = If(IsDBNull(result), 0, Convert.ToDecimal(result))
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error fetching sales data: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        series.Points.AddXY("Total Products Earned", totalProductSales) ' Use totalProductSales as Y value
        series.Points(series.Points.Count - 1).Label = totalProductSales.ToString("N2") ' Display only the numeric value inside the pie chart
        series.Points(series.Points.Count - 1).LegendText = "Total Products Earned" ' Label for the legend

        series.Points.AddXY("Total Services Earned", totalServiceSales) ' Use totalServiceSales as Y value
        series.Points(series.Points.Count - 1).Label = totalServiceSales.ToString("N2") ' Display only the numeric value inside the pie chart
        series.Points(series.Points.Count - 1).LegendText = "Total Services Earned" ' Label for the legend


        Chart1.Titles.Clear()
        Chart1.Titles.Add("Overall Sales")
        Chart1.Titles(0).Font = New Font("Arial", 14, FontStyle.Bold)

    End Sub

    Private WithEvents printDocProducts As New PrintDocument()
    Private WithEvents printDocServices As New PrintDocument()
    Dim offsetX As Integer = 120

    Private Sub btn_printptr_Click(sender As Object, e As EventArgs) Handles btn_printptr.Click
        PrintPreviewDialog1.Document = printDocProducts
        printDocProducts.DefaultPageSettings.Landscape = True
        PrintPreviewDialog1.ShowDialog()
    End Sub

    Private currentRowIndex As Integer = 0

    Private Sub btn_printstr_Click(sender As Object, e As EventArgs) Handles btn_printstr.Click
        ' Reset the row index at the beginning of printing
        currentRowIndex = 0
        PrintPreviewDialog1.Document = printDocServices
        printDocServices.DefaultPageSettings.Landscape = True
        PrintPreviewDialog1.ShowDialog()
    End Sub

    Private currentRow As Integer = 0
    Private Sub printDocProducts_PrintPage(sender As Object, e As PrintPageEventArgs) Handles printDocProducts.PrintPage
        Dim font As New Font("Arial", 9)
        Dim titleFont As New Font("Arial", 14, FontStyle.Bold)
        Dim brush As New SolidBrush(Color.Black)
        Dim pen As New Pen(Color.Black, 1)

        Dim marginX As Integer = e.MarginBounds.Left
        Dim marginY As Integer = e.MarginBounds.Top
        Dim startY As Integer = marginY

        Dim columnCount As Integer = datatable_producttransactions.Columns.Count
        Dim defaultCellHeight As Integer = 30

        Dim cellWidth As Integer = (e.MarginBounds.Width - marginX * 2) / columnCount
        Dim productNameWidth As Integer = cellWidth * 2
        Dim transactionDateWidth As Integer = cellWidth * 1.5

        Dim totalWidth As Integer = 0
        For Each col As DataGridViewColumn In datatable_producttransactions.Columns
            totalWidth += If(col.Name = "product_name", productNameWidth, If(col.Name = "transaction_date_time", transactionDateWidth, cellWidth))
        Next

        Dim titleText As String = "Product Transaction Records"
        Dim titleSize As SizeF = e.Graphics.MeasureString(titleText, titleFont)
        Dim titleX As Integer = (e.MarginBounds.Width - titleSize.Width) / 2 + marginX
        e.Graphics.DrawString(titleText, titleFont, brush, titleX, startY)
        startY += 40
        Dim tableOffset As Integer = 50

        Dim startX As Integer = (e.MarginBounds.Width - totalWidth) / 2 + tableOffset
        For Each col As DataGridViewColumn In datatable_producttransactions.Columns
            Dim width As Integer = If(col.Name = "product_name", productNameWidth, If(col.Name = "transaction_date_time", transactionDateWidth, cellWidth))

            e.Graphics.DrawRectangle(pen, startX, startY, width, defaultCellHeight)

            Dim textSize As SizeF = e.Graphics.MeasureString(col.HeaderText, font)
            Dim textX As Integer = startX + (width - textSize.Width) / 2
            Dim textY As Integer = startY + (defaultCellHeight - textSize.Height) / 2
            e.Graphics.DrawString(col.HeaderText, font, brush, textX, textY)
            startX += width
        Next

        startX = (e.MarginBounds.Width - totalWidth) / 2 + tableOffset
        startY += defaultCellHeight

        For i As Integer = currentRow To datatable_producttransactions.Rows.Count - 1
            Dim row As DataGridViewRow = datatable_producttransactions.Rows(i)

            If Not row.IsNewRow Then
                Dim rowHeight As Integer = defaultCellHeight

                For Each cell As DataGridViewCell In row.Cells
                    Dim width As Integer = If(cell.OwningColumn.Name = "product_name", productNameWidth, If(cell.OwningColumn.Name = "transaction_date_time", transactionDateWidth, cellWidth))

                    If cell.OwningColumn.Name = "product_name" Or cell.OwningColumn.Name = "unit_cost" Then
                        Dim wrappedText As String() = WrapText(cell.Value.ToString(), font, width)
                        Dim lineHeight As Integer = e.Graphics.MeasureString("A", font).Height
                        rowHeight = Math.Max(lineHeight * wrappedText.Length, rowHeight)
                    End If

                    e.Graphics.DrawRectangle(pen, startX, startY, width, rowHeight)

                    Dim cellValue As String = cell.Value.ToString()
                    If cell.OwningColumn.Name = "product_name" Or cell.OwningColumn.Name = "unit_cost" Then

                        Dim wrappedText As String() = WrapText(cellValue, font, width)
                        Dim lineHeight As Integer = e.Graphics.MeasureString("A", font).Height
                        Dim currentY As Integer = startY + (rowHeight - (wrappedText.Length * lineHeight)) / 2

                        For Each line As String In wrappedText
                            Dim cellTextSize As SizeF = e.Graphics.MeasureString(line, font)
                            Dim cellTextX As Integer = startX + (width - cellTextSize.Width) / 2
                            e.Graphics.DrawString(line, font, brush, cellTextX, currentY)
                            currentY += lineHeight
                        Next
                    Else
                        Dim cellTextSize As SizeF = e.Graphics.MeasureString(cellValue, font)
                        Dim cellTextX As Integer = startX + (width - cellTextSize.Width) / 2
                        Dim cellTextY As Integer = startY + (rowHeight - cellTextSize.Height) / 2
                        e.Graphics.DrawString(cellValue, font, brush, cellTextX, cellTextY)
                    End If

                    startX += width
                Next

                Dim lineStartX As Integer = (e.MarginBounds.Width - totalWidth) / 2 + tableOffset
                Dim lineEndX As Integer = lineStartX + totalWidth
                e.Graphics.DrawLine(pen, lineStartX, startY + rowHeight, lineEndX, startY + rowHeight)

                startX = (e.MarginBounds.Width - totalWidth) / 2 + tableOffset
                startY += rowHeight

                If startY + rowHeight > e.MarginBounds.Bottom Then
                    currentRow = i + 1
                    e.HasMorePages = True
                    Exit Sub
                End If
            End If
        Next

        e.HasMorePages = False
        currentRow = 0
    End Sub

    Private Function WrapText(ByVal text As String, ByVal font As Font, ByVal maxWidth As Integer) As String()
        Dim words As String() = text.Split(" "c)
        Dim lines As New List(Of String)
        Dim currentLine As String = ""

        For Each word As String In words
            Dim testLine As String = If(currentLine = "", word, currentLine & " " & word)
            Dim size As SizeF = TextRenderer.MeasureText(testLine, font)

            If size.Width > maxWidth Then
                lines.Add(currentLine)
                currentLine = word
            Else
                currentLine = testLine
            End If
        Next

        If currentLine <> "" Then
            lines.Add(currentLine)
        End If

        Return lines.ToArray()
    End Function

    Private Sub printDocServices_PrintPage(sender As Object, e As PrintPageEventArgs) Handles printDocServices.PrintPage
        Dim font As New Font("Arial", 9)
        Dim titleFont As New Font("Arial", 14, FontStyle.Bold)
        Dim brush As New SolidBrush(Color.Black)
        Dim pen As New Pen(Color.Black, 1)

        Dim startY As Integer = e.MarginBounds.Top
        Dim cellHeight As Integer = 50
        Dim cellWidth As Integer = 140
        Dim padding As Integer = 5
        Dim startX As Integer = e.MarginBounds.Left

        ' Draw title
        Dim titleText As String = "Service Transaction Records"
        Dim titleSize As SizeF = e.Graphics.MeasureString(titleText, titleFont)
        Dim titleX As Integer = (e.MarginBounds.Width - titleSize.Width) / 2 + e.MarginBounds.Left
        e.Graphics.DrawString(titleText, titleFont, brush, titleX, startY)
        startY += 60

        For Each col As DataGridViewColumn In datatable_servicetransactions.Columns
            e.Graphics.DrawRectangle(pen, startX, startY, cellWidth, cellHeight)
            Dim textSize As SizeF = e.Graphics.MeasureString(col.HeaderText, font)
            Dim textX As Integer = startX + (cellWidth - textSize.Width) / 2
            Dim textY As Integer = startY + (cellHeight - textSize.Height) / 2
            e.Graphics.DrawString(col.HeaderText, font, brush, textX, textY)
            startX += cellWidth
        Next

        startX = e.MarginBounds.Left
        startY += cellHeight

        For i As Integer = currentRowIndex To datatable_servicetransactions.Rows.Count - 1
            Dim row As DataGridViewRow = datatable_servicetransactions.Rows(i)

            If Not row.IsNewRow Then
                For Each cell As DataGridViewCell In row.Cells
                    ' Draw cell rectangle
                    e.Graphics.DrawRectangle(pen, startX, startY, cellWidth, cellHeight)

                    ' Center cell text
                    Dim cellValue As String = If(cell.Value IsNot Nothing, cell.Value.ToString(), "")
                    Dim cellTextSize As SizeF = e.Graphics.MeasureString(cellValue, font)
                    Dim cellTextX As Integer = startX + (cellWidth - cellTextSize.Width) / 2
                    Dim cellTextY As Integer = startY + (cellHeight - cellTextSize.Height) / 2
                    e.Graphics.DrawString(cellValue, font, brush, cellTextX, cellTextY)

                    startX += cellWidth
                Next

                startX = e.MarginBounds.Left
                startY += cellHeight

                If startY + cellHeight > e.MarginBounds.Bottom Then
                    e.HasMorePages = True
                    currentRowIndex = i + 1
                    Exit Sub
                End If
            End If
        Next

        e.HasMorePages = False
        currentRowIndex = 0
    End Sub

    Private Sub InsertIntoReturnOfProducts(returnId As Integer, productId As Integer, productName As String, unitCost As Decimal, returnQuantity As Integer, refundAmount As Decimal, returnReason As String, returnDateTime As DateTime, transactionId As Integer)
        Dim query As String = "INSERT INTO return_of_products (return_id, product_id, product_name, unit_cost, return_quantity, refund_amount, return_reason, return_date_time, transaction_id) " &
                          "VALUES (@return_id, @product_id, @product_name, @unit_cost, @return_quantity, @refund_amount, @return_reason, @return_date_time, @transaction_id)"

        Using conn As New SQLiteConnection(connectionString)
            Using cmd As New SQLiteCommand(query, conn)

                cmd.Parameters.AddWithValue("@return_id", returnId)
                cmd.Parameters.AddWithValue("@product_id", productId)
                cmd.Parameters.AddWithValue("@product_name", productName)
                cmd.Parameters.AddWithValue("@unit_cost", unitCost)
                cmd.Parameters.AddWithValue("@return_quantity", returnQuantity)
                cmd.Parameters.AddWithValue("@refund_amount", refundAmount)
                cmd.Parameters.AddWithValue("@return_reason", returnReason)
                cmd.Parameters.AddWithValue("@return_date_time", returnDateTime)
                cmd.Parameters.AddWithValue("@transaction_id", transactionId)

                conn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub


    Private Sub datatable_archivereturnedproducts_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles datatable_archivereturnedproducts.CellContentClick
        If e.ColumnIndex = datatable_archivereturnedproducts.Columns("chkSelect").Index AndAlso e.RowIndex >= 0 Then
            Dim chkCell As DataGridViewCheckBoxCell = CType(datatable_archivereturnedproducts.Rows(e.RowIndex).Cells("chkSelect"), DataGridViewCheckBoxCell)
            chkCell.Value = If(chkCell.Value Is Nothing OrElse Not CBool(chkCell.Value), True, False)
        End If
    End Sub

    Private Sub LoadArchivedReturnedProducts()
        ' SQLite connection string with full path to the database
        Dim dbPath As String = "C:\Users\Admin\source\repos\ic_electronics_center_app\ic_electronics_center_app\ic_electronics.db"
        Dim connectionString As String = $"Data Source={dbPath};Version=3;"

        ' SQLite query to select data from the archived_returned_products table
        Dim query As String = "SELECT return_id, product_id, product_name, unit_cost, return_quantity, refund_amount, return_reason, return_date_time, transaction_id FROM archived_returned_products"

        ' Using block to ensure proper disposal of the connection
        Using connection As New SQLiteConnection(connectionString)
            Dim command As New SQLiteCommand(query, connection)

            Try
                ' Open the SQLite connection
                connection.Open()

                ' Execute the query and load the results into a DataTable
                Dim reader As SQLiteDataReader = command.ExecuteReader()
                Dim dt As New DataTable()
                dt.Load(reader)

                ' Bind the DataTable to the DataGridView
                datatable_archivereturnedproducts.DataSource = dt

                ' Add a checkbox column if it doesn't exist
                If Not datatable_archivereturnedproducts.Columns.Contains("chkSelect") Then
                    Dim chkSelectColumn As New DataGridViewCheckBoxColumn()
                    chkSelectColumn.Name = "chkSelect"
                    chkSelectColumn.HeaderText = "Select"
                    datatable_archivereturnedproducts.Columns.Insert(0, chkSelectColumn)
                End If

                ' Format relevant columns with two decimal places
                Dim decimalColumns As String() = {"unit_cost", "refund_amount"}
                For Each colName As String In decimalColumns
                    If datatable_archivereturnedproducts.Columns.Contains(colName) Then
                        datatable_archivereturnedproducts.Columns(colName).DefaultCellStyle.Format = "F2"
                    End If
                Next

                ' Auto-resize columns for a better fit
                datatable_archivereturnedproducts.AutoResizeColumns()

            Catch ex As Exception
                ' Display any errors that occur during the query execution
                MessageBox.Show("An error occurred while loading archived returned products: " & ex.Message)
            End Try
        End Using
    End Sub



    Private Sub btn_archiverop_Click(sender As Object, e As EventArgs) Handles btn_archiverop.Click
        If selectedReturnId > 0 Then
            Dim archiveQuery As String = "INSERT INTO archived_returned_products (return_id, product_id, product_name, unit_cost, return_quantity, refund_amount, return_reason, return_date_time, transaction_id) " &
                                      "SELECT return_id, product_id, product_name, unit_cost, return_quantity, refund_amount, return_reason, return_date_time, transaction_id FROM return_of_products WHERE return_id=@return_id"
            Dim deleteQuery As String = "DELETE FROM return_of_products WHERE return_id=@return_id"

            Using conn As New SQLiteConnection(connectionString)
                conn.Open()

                Using archiveCmd As New SQLiteCommand(archiveQuery, conn)
                    archiveCmd.Parameters.AddWithValue("@return_id", selectedReturnId)
                    archiveCmd.ExecuteNonQuery()
                End Using

                Using deleteCmd As New SQLiteCommand(deleteQuery, conn)
                    deleteCmd.Parameters.AddWithValue("@return_id", selectedReturnId)
                    deleteCmd.ExecuteNonQuery()
                End Using
            End Using

            MessageBox.Show("Returned product archived successfully!")

            LoadReturnedProducts()
            LoadArchivedReturnedProducts()
            LoadReturnofProducts()
        Else
            MessageBox.Show("Please select a returned product to archive.")
        End If
    End Sub



    Private Sub InsertIntoArchivedReturnedProducts(returnId As Integer, productId As Integer, productName As String, unitCost As Decimal, returnQuantity As Integer, refundAmount As Decimal, returnReason As String, returnDateTime As DateTime, transactionId As Integer)
        Dim query As String = "INSERT INTO archived_returned_products (return_id, product_id, product_name, unit_cost, return_quantity, refund_amount, return_reason, return_date_time, transaction_id) " &
                          "VALUES (@return_id, @product_id, @product_name, @unit_cost, @return_quantity, @refund_amount, @return_reason, @return_date_time, @transaction_id)"

        Using conn As New SQLiteConnection(connectionString)
            Using cmd As New SQLiteCommand(query, conn)
                cmd.Parameters.AddWithValue("@return_id", returnId)
                cmd.Parameters.AddWithValue("@product_id", productId) ' Add the product ID parameter
                cmd.Parameters.AddWithValue("@product_name", productName)
                cmd.Parameters.AddWithValue("@unit_cost", unitCost)
                cmd.Parameters.AddWithValue("@return_quantity", returnQuantity)
                cmd.Parameters.AddWithValue("@refund_amount", refundAmount)
                cmd.Parameters.AddWithValue("@return_reason", returnReason)
                cmd.Parameters.AddWithValue("@return_date_time", returnDateTime)
                cmd.Parameters.AddWithValue("@transaction_id", transactionId)

                conn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Sub btn_unarchivereturnedproducts_Click(sender As Object, e As EventArgs) Handles btn_unarchivereturnedproducts.Click
        Dim unarchivedCount As Integer = 0

        For Each row As DataGridViewRow In datatable_archivereturnedproducts.Rows
            Dim chkCell As DataGridViewCheckBoxCell = CType(row.Cells("chkSelect"), DataGridViewCheckBoxCell)
            If CBool(chkCell.Value) Then
                Dim returnId As Integer = CInt(row.Cells("return_id").Value)
                Dim productId As Integer = CInt(row.Cells("product_id").Value)
                Dim productName As String = row.Cells("product_name").Value.ToString()
                Dim unitCost As Decimal = CDec(row.Cells("unit_cost").Value)
                Dim returnQuantity As Integer = CInt(row.Cells("return_quantity").Value)
                Dim refundAmount As Decimal = CDec(row.Cells("refund_amount").Value)
                Dim returnReason As String = row.Cells("return_reason").Value.ToString()
                Dim returnDateTime As DateTime = CDate(row.Cells("return_date_time").Value)
                Dim transactionId As Integer = CInt(row.Cells("transaction_id").Value)

                ' Pass the productId to the InsertIntoReturnOfProducts method
                InsertIntoReturnOfProducts(returnId, productId, productName, unitCost, returnQuantity, refundAmount, returnReason, returnDateTime, transactionId)

                DeleteFromArchiveReturnedProducts(returnId)

                unarchivedCount += 1
            End If
        Next

        LoadReturnedProducts()
        LoadArchivedReturnedProducts()
        LoadReturnofProducts()

        If unarchivedCount > 0 Then
            MessageBox.Show(unarchivedCount & " product(s) successfully unarchived!", "Unarchive Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("No products were selected for unarchiving.", "Unarchive Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub


    Private Sub DeleteFromReturnOfProducts(returnId As Integer)
        Dim query As String = "DELETE FROM return_of_products WHERE return_id = @return_id"

        Using conn As New SQLiteConnection(connectionString)
            Using cmd As New SQLiteCommand(query, conn)
                cmd.Parameters.AddWithValue("@return_id", returnId)

                conn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Sub DeleteFromArchiveReturnedProducts(returnId As Integer)
        Dim query As String = "DELETE FROM archived_returned_products WHERE return_id = @return_id"

        Using conn As New SQLiteConnection(connectionString)
            Using cmd As New SQLiteCommand(query, conn)
                cmd.Parameters.AddWithValue("@return_id", returnId)

                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    MessageBox.Show("An error occurred while deleting the record: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

    Private Sub tb_searcharchivereturnedproducts_TextChanged(sender As Object, e As EventArgs) Handles tb_searcharchivereturnedproducts.TextChanged
        Dim searchText As String = tb_searcharchivereturnedproducts.Text.Trim()

        If Not String.IsNullOrEmpty(searchText) Then
            Dim dt As DataTable = CType(datatable_archivereturnedproducts.DataSource, DataTable)
            If dt IsNot Nothing Then
                Dim dv As New DataView(dt)
                dv.RowFilter = String.Format("product_name LIKE '%{0}%' OR return_reason LIKE '%{0}%'", searchText)

                datatable_archivereturnedproducts.DataSource = dv
            End If
        Else
            LoadArchivedReturnedProducts()
        End If
    End Sub
    Private Sub LoadRecentTransactions()
        Dim recentTransactions As DataTable = GetRecentTransactions()
        datatable_recenttransactions.DataSource = recentTransactions
    End Sub

    Private Function GetRecentTransactions() As DataTable
        Dim recentTransactionsTable As New DataTable()

        ' Define the connection string
        Dim connectionString As String = "Data Source=C:\Users\Admin\source\repos\ic_electronics_center_app\ic_electronics_center_app\ic_electronics.db;Version=3;"

        Using connection As New SQLiteConnection(connectionString)
            connection.Open()

            ' Create the DataTable structure
            recentTransactionsTable.Columns.Add("Transaction Type")
            recentTransactionsTable.Columns.Add("Transaction ID")
            recentTransactionsTable.Columns.Add("Name")
            recentTransactionsTable.Columns.Add("Total Cost")
            recentTransactionsTable.Columns.Add("Transaction Time")

            ' Query for recent service transactions
            Dim serviceQuery As String = "SELECT transaction_id, service_name, service_cost AS total_cost, transaction_date_time FROM service_transactions ORDER BY transaction_date_time DESC LIMIT 10"
            Using serviceCommand As New SQLiteCommand(serviceQuery, connection)
                Using reader As SQLiteDataReader = serviceCommand.ExecuteReader()
                    While reader.Read()
                        Dim totalCost As Decimal = Convert.ToDecimal(reader("total_cost"))
                        recentTransactionsTable.Rows.Add("Service", reader("transaction_id"), reader("service_name"), totalCost.ToString("F2"), reader("transaction_date_time"))
                    End While
                End Using
            End Using

            ' Query for recent product transactions
            Dim productQuery As String = "SELECT transaction_id, product_name, total_cost, transaction_date_time FROM product_transactions ORDER BY transaction_date_time DESC LIMIT 10"
            Using productCommand As New SQLiteCommand(productQuery, connection)
                Using reader As SQLiteDataReader = productCommand.ExecuteReader()
                    While reader.Read()
                        Dim totalCost As Decimal = Convert.ToDecimal(reader("total_cost"))
                        recentTransactionsTable.Rows.Add("Product", reader("transaction_id"), reader("product_name"), totalCost.ToString("F2"), reader("transaction_date_time"))
                    End While
                End Using
            End Using
        End Using

        Return recentTransactionsTable
    End Function
    Private Sub SetupRecentTransactionsDataGridView(dataGridView As DataGridView)
        ' Set the DataSource for your DataGridView
        dataGridView.DataSource = GetRecentTransactions()

        ' Adjust row headers
        dataGridView.RowHeadersVisible = True ' Ensure row headers are visible
        dataGridView.RowHeadersWidth = 60 ' Increase width for more space in row headers

        ' Optional: Set row header style
        dataGridView.RowHeadersDefaultCellStyle.BackColor = Color.LightGray
        dataGridView.RowHeadersDefaultCellStyle.Font = New Font("Arial", 10, FontStyle.Bold)
        dataGridView.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        ' Adjust row header padding (using a workaround)
        dataGridView.RowHeadersDefaultCellStyle.Padding = New Padding(5) ' Add padding to row headers

        ' Optional: Add a title to the row header
        For i As Integer = 0 To dataGridView.Rows.Count - 1
            dataGridView.Rows(i).HeaderCell.Value = (i + 1).ToString() ' Adding sequential numbers to the row header
        Next
    End Sub

    Private Function GetDailyTransactionHistory() As DataTable
        Dim dailyTransactionsTable As New DataTable()

        ' Define the connection string
        Dim connectionString As String = "Data Source=C:\Users\Admin\source\repos\ic_electronics_center_app\ic_electronics_center_app\ic_electronics.db;Version=3;"

        Using connection As New SQLiteConnection(connectionString)
            connection.Open()

            ' Create the DataTable structure
            dailyTransactionsTable.Columns.Add("Transaction Type")
            dailyTransactionsTable.Columns.Add("Transaction ID")
            dailyTransactionsTable.Columns.Add("Name")
            dailyTransactionsTable.Columns.Add("Total Cost")
            dailyTransactionsTable.Columns.Add("Transaction Time")

            ' Get today's date in the correct format (YYYY-MM-DD)
            Dim today As String = DateTime.Now.ToString("yyyy-MM-dd")

            ' Query for service transactions for today's date
            Dim serviceQuery As String = "SELECT transaction_id, service_name, service_cost AS total_cost, transaction_date_time " &
                                      "FROM service_transactions " &
                                      "WHERE DATE(transaction_date_time) = @today"
            Using serviceCommand As New SQLiteCommand(serviceQuery, connection)
                serviceCommand.Parameters.AddWithValue("@today", today)
                Using reader As SQLiteDataReader = serviceCommand.ExecuteReader()
                    While reader.Read()
                        Dim totalCost As Decimal = Convert.ToDecimal(reader("total_cost"))
                        dailyTransactionsTable.Rows.Add("Service", reader("transaction_id"), reader("service_name"), totalCost.ToString("F2"), reader("transaction_date_time"))
                    End While
                End Using
            End Using

            ' Query for product transactions for today's date
            Dim productQuery As String = "SELECT transaction_id, product_name, total_cost, transaction_date_time " &
                                      "FROM product_transactions " &
                                      "WHERE DATE(transaction_date_time) = @today"
            Using productCommand As New SQLiteCommand(productQuery, connection)
                productCommand.Parameters.AddWithValue("@today", today)
                Using reader As SQLiteDataReader = productCommand.ExecuteReader()
                    While reader.Read()
                        Dim totalCost As Decimal = Convert.ToDecimal(reader("total_cost"))
                        dailyTransactionsTable.Rows.Add("Product", reader("transaction_id"), reader("product_name"), totalCost.ToString("F2"), reader("transaction_date_time"))
                    End While
                End Using
            End Using
        End Using

        Return dailyTransactionsTable
    End Function

    Private Sub SetupDailyTransactionHistoryDataGridView()
        ' Set the DataSource for your DataGridView
        datatable_salesbydailytransactionhistory.DataSource = GetDailyTransactionHistory()

        ' Hide row headers
        datatable_salesbydailytransactionhistory.RowHeadersVisible = False ' Hides the row headers

        ' Optionally adjust the width of the row headers if you want to keep them visible
        ' datatable_salesbydailytransactionhistory.RowHeadersWidth = 0 ' Set to 0 to effectively hide them

        ' Optional: Set other styles
        datatable_salesbydailytransactionhistory.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke

        ' Adjust column widths
        datatable_salesbydailytransactionhistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        ' Adjust other properties
        datatable_salesbydailytransactionhistory.RowHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke
        datatable_salesbydailytransactionhistory.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        ' Optional: Add a title to the row header if you keep them
        For i As Integer = 0 To datatable_salesbydailytransactionhistory.Rows.Count - 1
            datatable_salesbydailytransactionhistory.Rows(i).HeaderCell.Value = (i + 1).ToString() ' Adding sequential numbers to the row header
        Next
    End Sub

    Private Function GetWeeklyTransactionHistory() As DataTable
        Dim weeklyTransactionsTable As New DataTable()

        ' Define the connection string
        Dim connectionString As String = "Data Source=C:\Users\Admin\source\repos\ic_electronics_center_app\ic_electronics_center_app\ic_electronics.db;Version=3;"

        Using connection As New SQLiteConnection(connectionString)
            connection.Open()

            ' Create the DataTable structure
            weeklyTransactionsTable.Columns.Add("Transaction Type")
            weeklyTransactionsTable.Columns.Add("Transaction ID")
            weeklyTransactionsTable.Columns.Add("Name")
            weeklyTransactionsTable.Columns.Add("Total Cost")
            weeklyTransactionsTable.Columns.Add("Transaction Time")

            ' Query for service transactions for the current week
            Dim serviceQuery As String = "SELECT transaction_id, service_name, service_cost AS total_cost, transaction_date_time " &
                                      "FROM service_transactions " &
                                      "WHERE strftime('%Y-%m-%d', transaction_date_time) BETWEEN date('now', 'weekday 0', '-6 days') AND date('now', 'weekday 0')"
            Using serviceCommand As New SQLiteCommand(serviceQuery, connection)
                Using reader As SQLiteDataReader = serviceCommand.ExecuteReader()
                    While reader.Read()
                        Dim totalCost As Decimal = Convert.ToDecimal(reader("total_cost"))
                        weeklyTransactionsTable.Rows.Add("Service", reader("transaction_id"), reader("service_name"), totalCost.ToString("F2"), reader("transaction_date_time"))
                    End While
                End Using
            End Using

            ' Query for product transactions for the current week
            Dim productQuery As String = "SELECT transaction_id, product_name, total_cost, transaction_date_time " &
                                      "FROM product_transactions " &
                                      "WHERE strftime('%Y-%m-%d', transaction_date_time) BETWEEN date('now', 'weekday 0', '-6 days') AND date('now', 'weekday 0')"
            Using productCommand As New SQLiteCommand(productQuery, connection)
                Using reader As SQLiteDataReader = productCommand.ExecuteReader()
                    While reader.Read()
                        Dim totalCost As Decimal = Convert.ToDecimal(reader("total_cost"))
                        weeklyTransactionsTable.Rows.Add("Product", reader("transaction_id"), reader("product_name"), totalCost.ToString("F2"), reader("transaction_date_time"))
                    End While
                End Using
            End Using
        End Using

        Return weeklyTransactionsTable
    End Function

    Private Function GetMonthlyTransactionHistory() As DataTable
        Dim monthlyTransactionsTable As New DataTable()

        ' Define the connection string
        Dim connectionString As String = "Data Source=C:\Users\Admin\source\repos\ic_electronics_center_app\ic_electronics_center_app\ic_electronics.db;Version=3;"

        Using connection As New SQLiteConnection(connectionString)
            connection.Open()

            ' Create the DataTable structure
            monthlyTransactionsTable.Columns.Add("Transaction Type")
            monthlyTransactionsTable.Columns.Add("Transaction ID")
            monthlyTransactionsTable.Columns.Add("Name")
            monthlyTransactionsTable.Columns.Add("Total Cost")
            monthlyTransactionsTable.Columns.Add("Transaction Time")

            ' Query for service transactions for the current month
            Dim serviceQuery As String = "SELECT transaction_id, service_name, service_cost AS total_cost, transaction_date_time " &
                                      "FROM service_transactions " &
                                      "WHERE strftime('%Y-%m', transaction_date_time) = strftime('%Y-%m', 'now')"
            Using serviceCommand As New SQLiteCommand(serviceQuery, connection)
                Using reader As SQLiteDataReader = serviceCommand.ExecuteReader()
                    While reader.Read()
                        Dim totalCost As Decimal = Convert.ToDecimal(reader("total_cost"))
                        monthlyTransactionsTable.Rows.Add("Service", reader("transaction_id"), reader("service_name"), totalCost.ToString("F2"), reader("transaction_date_time"))
                    End While
                End Using
            End Using

            ' Query for product transactions for the current month
            Dim productQuery As String = "SELECT transaction_id, product_name, total_cost, transaction_date_time " &
                                      "FROM product_transactions " &
                                      "WHERE strftime('%Y-%m', transaction_date_time) = strftime('%Y-%m', 'now')"
            Using productCommand As New SQLiteCommand(productQuery, connection)
                Using reader As SQLiteDataReader = productCommand.ExecuteReader()
                    While reader.Read()
                        Dim totalCost As Decimal = Convert.ToDecimal(reader("total_cost"))
                        monthlyTransactionsTable.Rows.Add("Product", reader("transaction_id"), reader("product_name"), totalCost.ToString("F2"), reader("transaction_date_time"))
                    End While
                End Using
            End Using
        End Using

        Return monthlyTransactionsTable
    End Function
    Private Sub SetupWeeklyTransactionHistoryDataGridView()
        ' Set the DataSource for your DataGridView
        datatable_salesbyweeklytransactionhistory.DataSource = GetWeeklyTransactionHistory()

        ' Hide row headers
        datatable_salesbyweeklytransactionhistory.RowHeadersVisible = False

        ' Optional: Set other styles
        datatable_salesbyweeklytransactionhistory.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke

        ' Adjust column widths
        datatable_salesbyweeklytransactionhistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        ' Adjust other properties
        datatable_salesbyweeklytransactionhistory.RowHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke
        datatable_salesbyweeklytransactionhistory.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    End Sub

    Private Sub SetupMonthlyTransactionHistoryDataGridView()
        ' Set the DataSource for your DataGridView
        datatable_salesbymonthlytransactionhistory.DataSource = GetMonthlyTransactionHistory()

        ' Hide row headers
        datatable_salesbymonthlytransactionhistory.RowHeadersVisible = False

        ' Optional: Set other styles
        datatable_salesbymonthlytransactionhistory.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke

        ' Adjust column widths
        datatable_salesbymonthlytransactionhistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        ' Adjust other properties
        datatable_salesbymonthlytransactionhistory.RowHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke
        datatable_salesbymonthlytransactionhistory.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    End Sub

End Class
