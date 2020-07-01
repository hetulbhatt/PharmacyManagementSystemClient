using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PharmaClient.ServiceReference1;

namespace PharmaClient
{

	public partial class Form1 : Form
	{
		ServiceReference1.Service1Client client;
		List<Panel> panels = new List<Panel>();
		List<DataGridView> grids = new List<DataGridView>();
		Form parent;
		public Form1()
		{
			InitializeComponent();
		}

		public Form1(Form parent)
		{
			this.parent = parent;
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			this.Font = new Font("Consolas", 14);
			WindowState = FormWindowState.Maximized;
			this.BackColor = Color.AliceBlue;
			client = new ServiceReference1.Service1Client();
			
			panels.Add(MedicineListPanel);
			panels.Add(MedicineAddPanel);
			panels.Add(DealerListPanel);
			panels.Add(DealerAddPanel);
			panels.Add(ManufacturerListPanel);
			panels.Add(ManufacturerAddPanel);
			panels.Add(LedgerListPanel);
			panels.Add(LedgerAddPanel);
			panels.Add(DeletePanel);
			panels.Add(WelcomePanel);
			panels.Add(UpdatePanel);

			grids.Add(dataGridView1);
			grids.Add(dataGridView2);
			grids.Add(dataGridView3);
			grids.Add(dataGridView4);

			foreach (DataGridView dg in grids)
			{
				dg.Size = new Size(1700, 700);
				dg.Location = dataGridView1.Location;
				dg.BackgroundColor = Color.AliceBlue;
				dg.BorderStyle = BorderStyle.None;
			}

			foreach (Panel p in panels)
			{
				p.Location = MedicineListPanel.Location;
				p.Size = new Size(1800, 800);
				p.BackColor = Color.AliceBlue;
			}

			textBox11.TabIndex = 100;
			textBox10.TabIndex = 101;
			textBox9.TabIndex = 102;
			textBox12.TabIndex = 103;
			textBox13.TabIndex = 104;
			button5.TabIndex = 105;

			label4.Font = new Font("Consolas", 60, FontStyle.Bold);
			label4.Left = (this.ClientSize.Width - label4.Width) / 2;
			label4.Top = (this.ClientSize.Height - label4.Height) / 4;
			

			WelcomePanel.BringToFront();

		}

		private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			parent.Close();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			client.SetDealer(textBox3.Text, textBox4.Text, textBox5.Text, richTextBox1.Text);
			textBox3.Clear();
			textBox4.Clear();
			textBox5.Clear();
			richTextBox1.Clear();
		}

		private void button4_Click(object sender, EventArgs e)
		{
			client.SetManufacturer(textBox8.Text, textBox7.Text, textBox6.Text, richTextBox2.Text);
			textBox8.Clear();
			textBox7.Clear();
			textBox6.Clear();
			richTextBox2.Clear();
		}



		private void button5_Click(object sender, EventArgs e)
		{
			client.SetMedicine(textBox11.Text, textBox10.Text, textBox9.Text, textBox12.Text, textBox13.Text);
			textBox11.Clear();
			textBox10.Clear();
			textBox9.Clear();
			textBox12.Clear();
			textBox13.Clear();
		}



		private void button6_Click(object sender, EventArgs e)
		{
			if(!client.SetLedger(textBox16.Text, textBox15.Text, textBox14.Text))
			{
				MessageBox.Show("Stock underflow: Please check stock", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			textBox16.Clear();
			textBox15.Clear();
			textBox14.Clear();
		}

		private void medicinesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MedicineListPanel.BringToFront();
			List<Medicine> medic = client.GetAllMedicines().medi.ToList();
			var medicines = (
				from m in medic
				where (m.Dealer != null && m.Manufacturer != null)
				select new
				{
					m.MedicineID,
					m.Name,
					m.Price,
					m.Stock,
					m.Dealer.DealerID,
					DealerName = m.Dealer.Name,
					m.Manufacturer.ManufacturerID,
					ManufacturerName = m.Manufacturer.Name
				}
			).ToList();
			dataGridView1.DataSource = medicines;
		}

		private void dealersToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DealerListPanel.BringToFront();
			List<Dealer> dealers = client.GetAllDealers().dels.ToList();
			var dealers2 = (
					from del in dealers
					select new
					{
						del.DealerID,
						del.Name,
						del.Phone,
						del.Email,
						del.Address
					}
				).ToList();
			dataGridView2.DataSource = dealers2;

		}

		private void manufacturersToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ManufacturerListPanel.BringToFront();
			List<Manufacturer> manufacturers = client.GetAllManufacturers().mans.ToList();
			var manufacturers2 = (
				from man in manufacturers
				select new
				{
					man.ManufacturerID,
					man.Name,
					man.Phone,
					man.Email,
					man.Address
				}
			).ToList();
			dataGridView3.DataSource = manufacturers2;
		}

		private void ledgerToolStripMenuItem_Click_1(object sender, EventArgs e)
		{
			LedgerListPanel.BringToFront();
			List<Ledger> ledgers = client.GetAllLedgers().ledg.ToList();
			var ledgers2 = (
				from led in ledgers
				select new
				{
					led.Entry,
					led.Customer,
					led.Medicine.MedicineID,
					led.Medicine.Name,
					led.Quantity,
					led.Amount
				}
			).ToList();
			dataGridView4.DataSource = ledgers2;
		}

		private void MedicineListPanel_Paint(object sender, PaintEventArgs e)
		{

		}

		private void addNewMedicineToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MedicineAddPanel.BringToFront();
		}

		private void addNewDealerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DealerAddPanel.BringToFront();
		}

		private void addNewManufacturerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ManufacturerAddPanel.BringToFront();
		}

		private void addNewEntryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LedgerAddPanel.BringToFront();
		}

		private void deleteEntityToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DeletePanel.BringToFront();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if(client.DeleteContract(comboBox1.SelectedIndex+1, int.Parse(textBox1.Text)))
			{
				MessageBox.Show("Entity deleted", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				MessageBox.Show("Entity not deleted", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			
		}

		private void updateStockToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UpdatePanel.BringToFront();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if(client.UpdateStock(int.Parse(textBox2.Text), int.Parse(textBox17.Text)))
			{
				MessageBox.Show("Medicine stock updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				MessageBox.Show("Medicine stock not updated. Check medicine ID", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
	}
}
