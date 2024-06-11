using EmployeeTimesheetManagement.Entity;
using EmployeeTimesheetManagement.Features;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeTimesheetManagement
{
    public partial class EmployeeForm : Form
    {
        Timesheet timeSheet;
        public EmployeeForm(Timesheet timeSheet)
        {
            InitializeComponent();
            this.timeSheet = timeSheet;

            updateButton.Enabled = false;
            saveButton.Enabled = false;
            LoadToGrid();

        }

        private void addnewButton_Click(object sender, EventArgs e)
        {
            fullnameTextBox.Clear();
            positionTextBox.Clear();
            cardnoTextBox.Clear();
            fullnameTextBox.Focus();
            saveButton.Enabled = true;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                var emp = new Employee(
                    fullnameTextBox.Text.Trim(),
                    positionTextBox.Text.Trim(),
                    cardnoTextBox.Text.Trim()
                    );
                timeSheet.AddEmployee(emp);

            }
            catch (ArgumentException Ae)
            {
                MessageBox.Show(Ae.Message);
            }
            finally
            {
                addnewButton.Enabled = true;
                saveButton.Enabled = false;
                LoadToGrid();
            }
        }
        private void LoadToGrid()
        {
            dataGridView1.DataSource = timeSheet.GetAllEmployees();
            dataGridView1.Refresh();
        }
        private void LoadToGrid(bool isSimpleVersion)
        {
            dataGridView1.DataSource = timeSheet.GetAllEmployees(isSimpleVersion);
            dataGridView1.Refresh();
        }
        private void updateButton_Click(object sender, EventArgs e)
        {
            try
            {
                timeSheet.UpdateEmployee(identityTextBox.Text, fullnameTextBox.Text, positionTextBox.Text);
                LoadToGrid();
            }
            catch (ArgumentException Ae)
            {
                MessageBox.Show(Ae.Message);
            }
            finally
            {
                addnewButton.Enabled = true;
                saveButton.Enabled = true;
                updateButton.Enabled = false;

            }
        }


        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows != null && dataGridView1.SelectedRows.Count > 0)
            {
                identityTextBox.Text = dataGridView1.SelectedCells[0].Value.ToString();
                fullnameTextBox.Text = dataGridView1.SelectedCells[1].Value.ToString();
                positionTextBox.Text = dataGridView1.SelectedCells[2].Value.ToString();
                cardnoTextBox.Text = dataGridView1.SelectedCells[3].Value.ToString();
                addnewButton.Enabled = false;
                saveButton.Enabled = false;
                updateButton.Enabled = true;
                deleteButton.Enabled = true;
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)

        {
            Guid id = new Guid(identityTextBox.Text);
            timeSheet.RemoveEmployee(id);
            LoadToGrid(true);
            deleteButton.Enabled = false;
        }

        private void EmployeeForm_Load(object sender, EventArgs e)
        {

        }
    }
}
