using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmaClient
{
	public partial class Login : Form
	{
		ServiceReference1.Service1Client client;
		public Login()
		{
			InitializeComponent();

		}

		private void button1_Click(object sender, EventArgs e)
		{
			client = new ServiceReference1.Service1Client();
			try
			{
				if (client.CheckLogin(Int32.Parse(textBox1.Text), textBox2.Text))
				{
					new Form1(this).Show();
					this.Hide();
				}
				else
				{
					MessageBox.Show("Incorrect username or password","Error",MessageBoxButtons.OK,MessageBoxIcon.Stop);
				}
			}
			catch (FormatException ex)
			{
				MessageBox.Show("Provide the credentials", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		private void Login_Load(object sender, EventArgs e)
		{
			this.AcceptButton = button1;
		}
	}
}
