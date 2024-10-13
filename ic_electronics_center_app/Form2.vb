Imports ic_electronics_center_app.Form3
Imports System.Data.SQLite
Imports System.Runtime.InteropServices.ComTypes
Imports System.Text
Imports System.Runtime.CompilerServices
Imports System.IO

Public Class Form2
    Dim dbPath As String = "C:\Users\Admin\source\repos\ic_electronics_center_app\ic_electronics_center_app\ic_electronics.db"

    Dim connString As String = $"Data Source={dbPath};Version=3;"

    Dim connection As SQLiteConnection = New SQLiteConnection(connString)
    Private conn As SQLiteConnection

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        idleTimer.Interval = 1000
        idleTimer.Start()

        LoadSuppliers()
        LoadProducts()
        LoadProductDetails(ProductName)
        LoadArchivedProducts()
        LoadArchivedSuppliers()

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

    Private Sub LoadProducts()
        datatable_products.DataSource = Nothing

        Dim query As String = "SELECT * FROM products"
        Using conn As New SQLiteConnection(connString)
            Using cmd As New SQLiteCommand(query, conn)
                conn.Open()
                Using reader As SQLiteDataReader = cmd.ExecuteReader()
                    Dim dt As New DataTable()
                    dt.Load(reader)
                    datatable_products.DataSource = dt
                End Using
            End Using
        End Using

        ' Add a checkbox column if it doesn't exist
        If datatable_products.Columns.Contains("chkSelect") = False Then
            Dim chkColumn As New DataGridViewCheckBoxColumn()
            chkColumn.HeaderText = "Select"
            chkColumn.Name = "chkSelect"
            datatable_products.Columns.Insert(0, chkColumn)
        End If

        ' Format specific numeric columns to two decimal places
        Dim decimalColumns As String() = {"unit_price", "total_cost", "remaining_stock_value"}
        For Each colName As String In decimalColumns
            If datatable_products.Columns.Contains(colName) Then
                datatable_products.Columns(colName).DefaultCellStyle.Format = "F2"
            End If
        Next

        datatable_products.AutoResizeColumns()
    End Sub

    Private Sub LoadSuppliers()
        datatable_suppliers.DataSource = Nothing
        cmb_supplierp.Items.Clear()

        Dim query As String = "SELECT supplier_id, company_name, contact_person, contact_number, address, note FROM suppliers"
        Using conn As New SQLiteConnection(connString)
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

    ' Private variables to hold the selected IDs
    Private selectedSupplierId As Integer
    Private selectedProductId As Integer
    Private Sub btn_addproduct_Click(sender As Object, e As EventArgs)
        ' Validate inputs
        If Not ValidateInputs() Then
            Return
        End If

        Dim query As String = "INSERT INTO products (supplier_id, product_name, unit_price, quantity_in_stock, unit_of_measurement, total_cost, remaining_stock_value, sold) VALUES (@supplier_id, @product_name, @unit_price, @quantity_in_stock, @unit_of_measurement, @total_cost, @remaining_stock_value, @sold)"
        Using conn As New SQLiteConnection(connString)
            Using cmd As New SQLiteCommand(query, conn)
                cmd.Parameters.AddWithValue("@supplier_id", cmb_supplierp.SelectedValue)
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
            End Using
        End Using
    End Sub

    Private Sub btn_updateproduct_Click(sender As Object, e As EventArgs)
        If selectedProductId <= 0 Then
            MessageBox.Show("Please select a product to update.")
            Return
        End If

        ' Validate inputs
        If Not ValidateInputs() Then
            Return
        End If

        Dim query As String = "UPDATE products SET product_name=@product_name, unit_price=@unit_price, quantity_in_stock=@quantity_in_stock, unit_of_measurement=@unit_of_measurement, total_cost=@total_cost, remaining_stock_value=@remaining_stock_value, sold=@sold WHERE product_id=@product_id"
        Using conn As New SQLiteConnection(connString)
            Using cmd As New SQLiteCommand(query, conn)
                cmd.Parameters.AddWithValue("@product_name", tb_productnamep.Text)
                cmd.Parameters.AddWithValue("@unit_price", Convert.ToDecimal(tb_unitpricep.Text))
                cmd.Parameters.AddWithValue("@quantity_in_stock", Convert.ToInt32(tb_quantityinstockp.Text))
                cmd.Parameters.AddWithValue("@unit_of_measurement", tb_unitofmeasurementp.Text)
                cmd.Parameters.AddWithValue("@total_cost", Convert.ToDecimal(tb_totalcostp.Text))
                cmd.Parameters.AddWithValue("@remaining_stock_value", Convert.ToDecimal(tb_remainingstockvaluep.Text))
                cmd.Parameters.AddWithValue("@sold", Convert.ToInt32(tb_soldp.Text))
                cmd.Parameters.AddWithValue("@product_id", selectedProductId)
                conn.Open()
                cmd.ExecuteNonQuery()
                MessageBox.Show("Product updated successfully!")

                ClearInputs()
                selectedProductId = 0
                LoadProducts()
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

        ' Validate numeric fields
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
    End Sub

    Private Sub datatable_products_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

        If e.ColumnIndex = datatable_products.Columns("chkSelect").Index AndAlso e.RowIndex >= 0 Then

            Dim chkCell As DataGridViewCheckBoxCell = CType(datatable_products.Rows(e.RowIndex).Cells("chkSelect"), DataGridViewCheckBoxCell)
            chkCell.Value = Not CBool(chkCell.Value)

            If CBool(chkCell.Value) = True Then
                ' Checkbox is checked, populate text boxes with the selected product details
                Dim row As DataGridViewRow = datatable_products.Rows(e.RowIndex)
                tb_productnamep.Text = row.Cells("product_name").Value.ToString()
                tb_unitpricep.Text = row.Cells("unit_price").Value.ToString()
                tb_quantityinstockp.Text = row.Cells("quantity_in_stock").Value.ToString()
                tb_unitofmeasurementp.Text = row.Cells("unit_of_measurement").Value.ToString()
                tb_totalcostp.Text = row.Cells("total_cost").Value.ToString()
                tb_remainingstockvaluep.Text = row.Cells("remaining_stock_value").Value.ToString()
                tb_soldp.Text = row.Cells("sold").Value.ToString()

                ' Store the selected product ID
                selectedProductId = CInt(row.Cells("product_id").Value)

                ' Disable the Add Product button
                btn_addproduct.Enabled = False
            Else
                ' Checkbox is unchecked, clear the text boxes and re-enable Add Product
                tb_productnamep.Clear()
                tb_unitpricep.Clear()
                tb_quantityinstockp.Clear()
                tb_unitofmeasurementp.Clear()
                tb_totalcostp.Clear()
                tb_remainingstockvaluep.Clear()
                tb_soldp.Clear()
                selectedProductId = 0

                ' Re-enable the Add Product button
                btn_addproduct.Enabled = True
            End If
        End If
    End Sub
    Private Sub btn_clearproducts_Click(sender As Object, e As EventArgs)
        tb_productnamep.Clear()
        tb_unitpricep.Clear()
        tb_quantityinstockp.Clear()
        tb_unitofmeasurementp.Clear()
        tb_totalcostp.Clear()
        tb_remainingstockvaluep.Clear()
        tb_soldp.Clear()
        cmb_supplierp.SelectedIndex = -1

        ' Re-enable the Add Product button
        btn_addproduct.Enabled = True
    End Sub
    Private Sub btn_suppliers_Click(sender As Object, e As EventArgs) Handles btn_suppliers.Click
        pnl_products.Visible = False
        pnl_suppliers.Visible = True
        pnl_archive.Visible = False

    End Sub

    Private Sub datatable_suppliers_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles datatable_suppliers.CellContentClick

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


    Private Sub btn_clearsuppliers_Click(sender As Object, e As EventArgs) Handles btn_clearsuppliers.Click
        tb_companyname.Clear()
        tb_contactperson.Clear()
        tb_contactnumber.Clear()
        tb_address.Clear()
        tb_note.Clear()

        ' Re-enable the Add Supplier button
        btn_addsupplier.Enabled = True
    End Sub

    Private Sub btn_addsupplier_Click(sender As Object, e As EventArgs) Handles btn_addsupplier.Click
        ' Validate inputs
        If Not ValidateSupplierInputs() Then
            Return
        End If

        Dim query As String = "INSERT INTO suppliers (company_name, contact_person, contact_number, address, note) VALUES (@company_name, @contact_person, @contact_number, @address, @note)"
        Using conn As New SQLiteConnection(connString)
            Using cmd As New SQLiteCommand(query, conn)
                cmd.Parameters.AddWithValue("@company_name", tb_companyname.Text)
                cmd.Parameters.AddWithValue("@contact_person", tb_contactperson.Text)
                cmd.Parameters.AddWithValue("@contact_number", tb_contactnumber.Text)
                cmd.Parameters.AddWithValue("@address", tb_address.Text)
                cmd.Parameters.AddWithValue("@note", tb_note.Text)
                conn.Open()
                cmd.ExecuteNonQuery()
                MessageBox.Show("Supplier added successfully!")

                ' Clear the text boxes
                ClearSupplierInputs()

                ' Refresh the DataGridView
                LoadSuppliers()

                ' Re-enable the Add Supplier button after adding
                btn_addsupplier.Enabled = True
            End Using
        End Using
    End Sub

    Private Sub btn_updatesupplier_Click(sender As Object, e As EventArgs) Handles btn_updatesupplier.Click
        If selectedSupplierId <= 0 Then
            MessageBox.Show("Please select a supplier to update.")
            Return
        End If

        ' Validate inputs
        If Not ValidateSupplierInputs() Then
            Return
        End If

        Dim query As String = "UPDATE suppliers SET company_name=@company_name, contact_person=@contact_person, contact_number=@contact_number, address=@address, note=@note WHERE supplier_id=@supplier_id"
        Using conn As New SQLiteConnection(connString)
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

                ' Clear the text boxes
                ClearSupplierInputs()

                ' Reset selectedSupplierId
                selectedSupplierId = 0
                LoadSuppliers()

                ' Re-enable the Add Supplier button
                btn_addsupplier.Enabled = True
            End Using
        End Using
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


    Private Sub ClearSupplierInputs()
        tb_companyname.Clear()
        tb_contactperson.Clear()
        tb_contactnumber.Clear()
        tb_address.Clear()
        tb_note.Clear()
    End Sub

    Private Sub btn_logout_Click(sender As Object, e As EventArgs) Handles btn_logout.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to log out?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            Dim loginForm As New Form1
            loginForm.Show()

            Me.Hide()
        End If
    End Sub

    Private Sub btn_products_Click(sender As Object, e As EventArgs) Handles btn_products.Click
        pnl_products.Visible = True
        pnl_suppliers.Visible = False
        pnl_archive.Visible = False

    End Sub

    Private Sub btn_archiveproduct_Click(sender As Object, e As EventArgs) Handles btn_archiveproduct.Click
        If selectedProductId > 0 Then

            Dim archiveQuery As String = "INSERT INTO archived_products (product_id, supplier_id, product_name, unit_price, quantity_in_stock, unit_of_measurement, total_cost, remaining_stock_value, sold) SELECT product_id, supplier_id, product_name, unit_price, quantity_in_stock, unit_of_measurement, total_cost, remaining_stock_value, sold FROM products WHERE product_id=@product_id"
            Dim deleteQuery As String = "DELETE FROM products WHERE product_id=@product_id"

            Using conn As New SQLiteConnection(connString)
                conn.Open()

                Using archiveCmd As New SQLiteCommand(archiveQuery, conn)
                    archiveCmd.Parameters.AddWithValue("@product_id", selectedProductId)
                    archiveCmd.ExecuteNonQuery()
                End Using

                Using deleteCmd As New SQLiteCommand(deleteQuery, conn)
                    deleteCmd.Parameters.AddWithValue("@product_id", selectedProductId)
                    deleteCmd.ExecuteNonQuery()
                End Using
            End Using

            MessageBox.Show("Product archived successfully!")

            LoadProducts()
            LoadArchivedProducts()
        Else
            MessageBox.Show("Please select a product to archive.")
        End If
    End Sub

    Private Sub btn_clearproducts_Click_1(sender As Object, e As EventArgs) Handles btn_clearproducts.Click
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

    Private Sub btn_archive_Click(sender As Object, e As EventArgs) Handles btn_archive.Click
        pnl_products.Visible = False
        pnl_suppliers.Visible = False
        pnl_archive.Visible = True
    End Sub

    Private Sub btn_updateproduct_Click_1(sender As Object, e As EventArgs) Handles btn_updateproduct.Click
        If selectedProductId <= 0 Then
            MessageBox.Show("Please select a product to update.")
            Return
        End If

        If Not ValidateInputs() Then
            Return
        End If

        Dim query As String = "UPDATE products SET product_name=@product_name, unit_price=@unit_price, quantity_in_stock=@quantity_in_stock, unit_of_measurement=@unit_of_measurement, total_cost=@total_cost, remaining_stock_value=@remaining_stock_value, sold=@sold WHERE product_id=@product_id"
        Using conn As New SQLiteConnection(connString)
            Using cmd As New SQLiteCommand(query, conn)
                cmd.Parameters.AddWithValue("@product_name", tb_productnamep.Text)
                cmd.Parameters.AddWithValue("@unit_price", Convert.ToDecimal(tb_unitpricep.Text))
                cmd.Parameters.AddWithValue("@quantity_in_stock", Convert.ToInt32(tb_quantityinstockp.Text))
                cmd.Parameters.AddWithValue("@unit_of_measurement", tb_unitofmeasurementp.Text)
                cmd.Parameters.AddWithValue("@total_cost", Convert.ToDecimal(tb_totalcostp.Text))
                cmd.Parameters.AddWithValue("@remaining_stock_value", Convert.ToDecimal(tb_remainingstockvaluep.Text))
                cmd.Parameters.AddWithValue("@sold", Convert.ToInt32(tb_soldp.Text))
                cmd.Parameters.AddWithValue("@product_id", selectedProductId)
                conn.Open()
                cmd.ExecuteNonQuery()
                MessageBox.Show("Product updated successfully!")

                ' Clear the text boxes
                ClearInputs()
                selectedProductId = 0
                LoadProducts()
            End Using
        End Using
    End Sub

    Private Sub cmb_supplierp_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_supplierp.SelectedIndexChanged
        If cmb_supplierp.SelectedItem IsNot Nothing Then
            Dim selectedSupplier As Supplier = CType(cmb_supplierp.SelectedItem, Supplier)
            Dim selectedSupplierId As Integer = selectedSupplier.SupplierID
            LoadProductDetails(selectedSupplierId)
        Else
        End If
    End Sub
    Private Sub LoadProductDetails(productName As String)
        conn = New SQLiteConnection(connString)

        Try
            conn.Open()
            Dim query As String = "SELECT unit_price, quantity_in_stock FROM products WHERE product_name = @product_name"
            Dim cmd As New SQLiteCommand(query, conn)
            cmd.Parameters.AddWithValue("@product_name", productName)

            Dim reader As SQLiteDataReader = cmd.ExecuteReader()

            If reader.Read() Then

                tb_unitpricep.Text = reader("unit_price").ToString()
            End If
        Catch ex As SQLiteException
            MessageBox.Show("Error: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub btn_addproduct_Click_1(sender As Object, e As EventArgs) Handles btn_addproduct.Click
        If Not ValidateInputs() Then
            Return
        End If

        Dim selectedSupplier As Supplier = CType(cmb_supplierp.SelectedItem, Supplier)
        Dim supplierId As Integer = selectedSupplier.SupplierID

        Dim query As String = "INSERT INTO products (supplier_id, product_name, unit_price, quantity_in_stock, unit_of_measurement, total_cost, remaining_stock_value, sold) VALUES (@supplier_id, @product_name, @unit_price, @quantity_in_stock, @unit_of_measurement, @total_cost, @remaining_stock_value, @sold)"
        Using conn As New SQLiteConnection(connString)
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
            End Using
        End Using
    End Sub

    Private Sub datatable_products_CellContentClick_1(sender As Object, e As DataGridViewCellEventArgs) Handles datatable_products.CellContentClick

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
                tb_productnamep.Clear()
                tb_unitpricep.Clear()
                tb_quantityinstockp.Clear()
                tb_unitofmeasurementp.Clear()
                tb_totalcostp.Clear()
                tb_remainingstockvaluep.Clear()
                tb_soldp.Clear()
                selectedProductId = 0

                btn_addproduct.Enabled = True
            End If
        End If
    End Sub

    Private Sub btn_searchproduct_TextChanged(sender As Object, e As EventArgs) Handles btn_searchproduct.TextChanged
        Dim searchTerm As String = btn_searchproduct.Text.Trim()

        If String.IsNullOrEmpty(searchTerm) Then

            LoadProducts()
        Else
            ' Perform the search query
            Dim query As String = "SELECT * FROM products WHERE product_name LIKE @searchTerm"
            Using conn As New SQLiteConnection(connString)
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

    Private Sub btn_unarchiveproducts_Click(sender As Object, e As EventArgs) Handles btn_unarchiveproducts.Click
        Dim unarchivedCount As Integer = 0

        For Each row As DataGridViewRow In datatable_archiveproducts.Rows
            Dim chkCell As DataGridViewCheckBoxCell = CType(row.Cells("chkSelect"), DataGridViewCheckBoxCell)
            If CBool(chkCell.Value) Then
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

                MessageBox.Show($"Product '{productName}' has been unarchived successfully!", "Unarchive Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                unarchivedCount += 1 ' Increment the count
            End If
        Next

        If unarchivedCount > 0 Then
            MessageBox.Show($"{unarchivedCount} product(s) have been unarchived.", "Unarchive Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("No products were selected for unarchiving.", "No Action Taken", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

        LoadProducts()
        LoadArchivedProducts()
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

    Private Sub LoadArchivedProducts()
        Dim query As String = "SELECT archive_id, product_id, supplier_id, product_name, unit_price, quantity_in_stock, unit_of_measurement, total_cost, remaining_stock_value, sold FROM archived_products"

        Using conn As New SQLiteConnection(connString)
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
        Dim query As String = "INSERT INTO products (product_id, supplier_id, product_name, unit_price, quantity_in_stock, unit_of_measurement, total_cost, remaining_stock_value, sold) " &
                          "VALUES (@product_id, @supplier_id, @product_name, @unit_price, @quantity_in_stock, @unit_of_measurement, @total_cost, @remaining_stock_value, @sold)"

        Using conn As New SQLiteConnection(connString)
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

        Using conn As New SQLiteConnection(connString)
            Using cmd As New SQLiteCommand(query, conn)
                cmd.Parameters.AddWithValue("@product_id", productId)

                conn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Sub btn_archivesupplier_Click(sender As Object, e As EventArgs) Handles btn_archivesupplier.Click
        If selectedSupplierId > 0 Then

            Dim archiveQuery As String = "INSERT INTO archived_suppliers (supplier_id, company_name, contact_person, contact_number, note) " &
                                      "SELECT supplier_id, company_name, contact_person, contact_number, note FROM suppliers WHERE supplier_id=@supplier_id"
            Dim deleteQuery As String = "DELETE FROM suppliers WHERE supplier_id=@supplier_id"

            Using conn As New SQLiteConnection(connString)
                Try
                    conn.Open()

                    Using archiveCmd As New SQLiteCommand(archiveQuery, conn)
                        archiveCmd.Parameters.AddWithValue("@supplier_id", selectedSupplierId)
                        archiveCmd.ExecuteNonQuery()
                    End Using

                    Using deleteCmd As New SQLiteCommand(deleteQuery, conn)
                        deleteCmd.Parameters.AddWithValue("@supplier_id", selectedSupplierId)
                        deleteCmd.ExecuteNonQuery()
                    End Using

                    MessageBox.Show("Supplier archived successfully!")

                    LoadSuppliers()
                    LoadArchivedSuppliers()
                Catch ex As SQLiteException
                    MessageBox.Show("Error while archiving supplier: " & ex.Message)
                End Try
            End Using
        Else
            MessageBox.Show("Please select a supplier to archive.")
        End If
    End Sub

    Private Sub LoadArchivedSuppliers()
        Dim query As String = "SELECT supplier_id, company_name, contact_person, contact_number, note FROM archived_suppliers"

        Using conn As New SQLiteConnection(connString)
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
                        datatable_unarchivesuppliers.Columns(5).HeaderText = "Note"
                    End If

                Catch ex As SQLiteException
                    MessageBox.Show("Error loading archived suppliers: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

    Private Sub InsertIntoSuppliers(supplierId As Integer, companyName As String, contactPerson As String, contactNumber As String, note As String)
        Dim query As String = "INSERT INTO suppliers (supplier_id, company_name, contact_person, contact_number, note) " &
                          "VALUES (@supplier_id, @company_name, @contact_person, @contact_number, @note)"

        Using conn As New SQLiteConnection(connString)
            Using cmd As New SQLiteCommand(query, conn)
                cmd.Parameters.AddWithValue("@supplier_id", supplierId)
                cmd.Parameters.AddWithValue("@company_name", companyName)
                cmd.Parameters.AddWithValue("@contact_person", contactPerson)
                cmd.Parameters.AddWithValue("@contact_number", contactNumber)
                cmd.Parameters.AddWithValue("@note", note)

                conn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub


    Private Sub DeleteFromArchivedSuppliers(supplierId As Integer)
        Dim query As String = "DELETE FROM archived_suppliers WHERE supplier_id = @supplier_id"

        Using conn As New SQLiteConnection(connString)
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


    Private Sub tb_searcharchivesuppliers_TextChanged(sender As Object, e As EventArgs) Handles tb_searcharchivesuppliers.TextChanged
        Dim searchText As String = tb_searcharchivesuppliers.Text.Trim()

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

    Private Sub btn_unarchivesuppliers_Click(sender As Object, e As EventArgs) Handles btn_unarchivesuppliers.Click
        Dim unarchivedCount As Integer = 0

        For Each row As DataGridViewRow In datatable_unarchivesuppliers.Rows
            Dim chkCell As DataGridViewCheckBoxCell = CType(row.Cells("chkSelect"), DataGridViewCheckBoxCell)
            If CBool(chkCell.Value) Then
                ' Retrieve supplier details from the row
                Dim supplierId As Integer = CInt(row.Cells("supplier_id").Value)
                Dim companyName As String = row.Cells("company_name").Value.ToString()
                Dim contactPerson As String = row.Cells("contact_person").Value.ToString()
                Dim contactNumber As String = row.Cells("contact_number").Value.ToString()
                Dim note As String = row.Cells("note").Value.ToString()

                InsertIntoSuppliers(supplierId, companyName, contactPerson, contactNumber, note)
                DeleteFromArchivedSuppliers(supplierId)
                unarchivedCount += 1
            End If
        Next

        LoadSuppliers()
        LoadArchivedSuppliers()

        If unarchivedCount > 0 Then
            MessageBox.Show(unarchivedCount.ToString() & " supplier(s) successfully unarchived.", "Unarchive Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("No suppliers were selected for unarchiving.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub tb_searchsuppliers_TextChanged(sender As Object, e As EventArgs) Handles tb_searchsuppliers.TextChanged
        Dim searchTerm As String = tb_searchsuppliers.Text.Trim()

        If String.IsNullOrEmpty(searchTerm) Then
            LoadSuppliers()
        Else
            Dim query As String = "SELECT * FROM suppliers WHERE company_name LIKE @searchTerm OR contact_person LIKE @searchTerm"
            Using conn As New SQLiteConnection(connString)
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
End Class
