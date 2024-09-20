using System.Data.SqlClient;

namespace QADBrowser
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            connectionTextField.Text = GetConnectionString();
            listViewResults.View = View.Details;
            listViewResults.Columns.Add("View Name", -2, HorizontalAlignment.Left);
        }

        private string GetConnectionString()
        {
            string connectionString = Environment.GetEnvironmentVariable("connstr");

            if (string.IsNullOrEmpty(connectionString))
            {
                MessageBox.Show("connstr not found in environment variables.");
                return null;
            }

            return connectionString;
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            string connectionString = "";

            if (checkBox1.Checked)
            {
                connectionString = connectionTextField.Text + ";truncateTooLarge=output;";
            }
            else
            {
                connectionString = connectionTextField.Text;
            }

            using var conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();

                string query = @"
                        SELECT TABLE_NAME
                        FROM INFORMATION_SCHEMA.VIEWS
                        WHERE TABLE_NAME LIKE 'v_%'
                    ";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                listViewResults.Items.Clear();

                while (reader.Read())
                {
                    string viewName = reader["TABLE_NAME"].ToString();
                    if (HasDateField(conn, viewName))
                    {
                        listViewResults.Items.Add(new ListViewItem(viewName));
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

        }

        private bool HasDateField(SqlConnection conn, string viewName)
        {
            string columnQuery = @"
                SELECT COLUMN_NAME, DATA_TYPE
                FROM INFORMATION_SCHEMA.COLUMNS
                WHERE TABLE_NAME = @ViewName AND DATA_TYPE IN ('date', 'datetime', 'smalldatetime', 'timestamp')
            ";

            using (SqlCommand cmd = new SqlCommand(columnQuery, conn))
            {
                cmd.Parameters.AddWithValue("@ViewName", viewName);
                SqlDataReader columnReader = cmd.ExecuteReader();
                bool hasDateField = columnReader.HasRows;
                columnReader.Close();

                return hasDateField;
            }
        }

        private void DisplayFormattedSQL(string sql)
        {
            richTextBoxSQL.Text = sql;

            richTextBoxSQL.SelectionStart = 0;
            richTextBoxSQL.SelectionLength = richTextBoxSQL.Text.Length;
            richTextBoxSQL.SelectionColor = System.Drawing.Color.Black;
            richTextBoxSQL.SelectionFont = new System.Drawing.Font("Courier New", 10);

            FormatSQLKeywords();

            int startIndex = richTextBoxSQL.Find("SELECT * FROM");
            if (startIndex != -1)
            {
                richTextBoxSQL.SelectionStart = startIndex;
                richTextBoxSQL.SelectionLength = richTextBoxSQL.Text.Length - startIndex;
                richTextBoxSQL.ScrollToCaret();
                richTextBoxSQL.Focus();
            }
        }

        private void FormatSQLKeywords()
        {
            string[] keywords = { "SELECT", "FROM", "WHERE", "ALTER", "VIEW", "GO", "AS", "USE", "SET", "ANSI_NULLS", "ON", "SET", "QUOTED_IDENTIFIER", "ON" };
            foreach (string keyword in keywords)
            {
                int index = richTextBoxSQL.Find(keyword);
                while (index != -1)
                {
                    richTextBoxSQL.SelectionStart = index;
                    richTextBoxSQL.SelectionLength = keyword.Length;
                    richTextBoxSQL.SelectionColor = System.Drawing.Color.Blue;
                    index = richTextBoxSQL.Find(keyword, index + keyword.Length, RichTextBoxFinds.None);
                }
            }

            richTextBoxSQL.SelectionStart = richTextBoxSQL.Text.Length;
            richTextBoxSQL.SelectionLength = 0;
            richTextBoxSQL.SelectionColor = System.Drawing.Color.Black;
        }


        private void genAlterView_Click(object sender, EventArgs e)
        {
            if (listViewResults.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a view from the list.");
                return;
            }

            string selectedView = listViewResults.SelectedItems[0].Text;

            string connectionString = connectionTextField.Text; ;
            if (string.IsNullOrEmpty(connectionString))
            {
                MessageBox.Show("Please provide a connection string.");
                return;
            }
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string columnQuery = @"
                SELECT COLUMN_NAME, DATA_TYPE
                FROM INFORMATION_SCHEMA.COLUMNS
                WHERE TABLE_NAME = @ViewName";

                    SqlCommand cmd = new SqlCommand(columnQuery, conn);
                    cmd.Parameters.AddWithValue("@ViewName", selectedView);

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<string> columnList = new List<string>();
                    while (reader.Read())
                    {
                        string columnName = reader["COLUMN_NAME"].ToString();
                        string dataType = reader["DATA_TYPE"].ToString();

                        if (dataType == "date" || dataType == "datetime" || dataType == "smalldatetime" || dataType == "timestamp")
                        {
                            columnList.Add($"COALESCE(TO_CHAR({columnName}, 'YYYY-MM-DD'), '1900-01-01') AS {columnName}");
                        }
                        else
                        {
                            columnList.Add(columnName);
                        }
                    }

                    reader.Close();

                    if (columnList.Count == 0)
                    {
                        MessageBox.Show($"No columns found for the view {selectedView}.");
                        return;
                    }

                    string formattedColumns = string.Join(",\n    ", columnList);

                    string sqlTemplate = $@"
USE [qad_demo]
GO

/****** Object:  View [dbo].[{selectedView}]    Script Date: {DateTime.Now:yyyy. MM. dd. HH:mm:ss} ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[{selectedView}]
AS
SELECT * FROM 
    OPENQUERY(QAD_demo, 'SELECT 
        {formattedColumns.Replace("'", "''")}
    FROM MFGDB.PUB.{selectedView.ToUpper().Substring(2)}')
GO
";

                    DisplayFormattedSQL(sqlTemplate);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

        }
    }
}
