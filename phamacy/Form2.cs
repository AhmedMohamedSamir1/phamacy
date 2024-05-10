using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace phamacy
{
    public partial class Form2 : Form
    {
        SqlConnection con = new SqlConnection(@" Server=.;DataBase=pharmacy system;Integrated Security=true");
        SqlDataAdapter DA;
        SqlCommand cmd;
        IDataReader DR;
        public Form2()
        {
            InitializeComponent();
        }


        public void clear2()
        {
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            dateTimePicker1.Text = "";
            dateTimePicker2.Text = "";
            comboBox2.Text = "";
            
        }

        public void clear3()
        {
            comboBox4.Text = "";
            num_of_cans.Text = "";
            Price_of_can.Text = "";
            total_price.Text = "";
        }
        private void Form2_Load(object sender, EventArgs e)
        {

            this.Icon = Icon.ExtractAssociatedIcon(AppDomain.CurrentDomain.FriendlyName);

            ////////////////////////////////////////////////////
            label1.Text = label1.Text + register.name;

            DA = new SqlDataAdapter("select medicine_name from Medicine", con);
            DataTable tbl;
            tbl = new DataTable();
            DA.Fill(tbl);
            for(int i=0;i<tbl.Rows.Count;i++)
            {
                string str;
                str = tbl.Rows[i][0].ToString();
                comboBox1.Items.Add(str);
                comboBox3.Items.Add(str);
                comboBox4.Items.Add(str);
            }
            /////////////////////////////////////

            DA = new SqlDataAdapter("select company_name from company", con);
            DataTable tbl2;
            tbl2 = new DataTable();
            DA.Fill(tbl2);
            for (int i = 0; i < tbl2.Rows.Count; i++)
            {
                string str2;
                str2 = tbl2.Rows[i][0].ToString();
                Company.Items.Add(str2);
                comboBox2.Items.Add(str2);
                comboBox5.Items.Add(str2);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("select medicine_ID,price,quantity,mfg_date,exp_date,type,company_name from Medicine join company on Medicine.company_id = Company.company_ID where medicine_name='" + comboBox1.Text + "'", con);
            DR = cmd.ExecuteReader();
            DR.Read();
            Med_ID.Text = DR["medicine_ID"].ToString();
            Price.Text = DR["price"].ToString();
            Type.Text = DR["type"].ToString();
            Company.Text = DR["company_name"].ToString();
            MFG_Date.Text = DR["mfg_date"].ToString();
            EXP_Date.Text = DR["exp_date"].ToString();
            Quantity.Text = DR["quantity"].ToString();
            DR.Close();
            con.Close();
            //////////////////////////////////////////
            progressBar1.Value =int.Parse( Quantity.Text);
            int sum;
            sum = int.Parse(Quantity.Text);
            if (sum < 10)
                Case.Text = "Very Low";
            else if (sum >= 10 && sum < 20)
                Case.Text = "Low";
            else if (sum >= 20 && sum < 30)
                Case.Text = "Moderate";
            else if (sum >= 30 && sum < 50)
                Case.Text = "Good";
            else
                Case.Text = "Excellent";
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Price.Text != "" && Quantity.Text != "" && Company.Text != "")
            {
                DialogResult dialog = MessageBox.Show("Are you sure that you want this update", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialog == DialogResult.Yes)
                {
                    DA = new SqlDataAdapter("select company_id from company where company_name='" + Company.Text + "'", con);
                    DataTable tbl;
                    tbl = new DataTable();
                    DA.Fill(tbl);
                    string str = tbl.Rows[0][0].ToString();
                    /////////////////////////////////////////////
                    con.Open();
                    cmd = new SqlCommand("UPDATE_MED", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@med_ID", Med_ID.Text);
                    cmd.Parameters.AddWithValue("@price", Price.Text);
                    cmd.Parameters.AddWithValue("@quantity", Quantity.Text);
                    cmd.Parameters.AddWithValue("@company_ID", str);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Updated Successfully", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panel1.Visible = true;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panel1.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != ""&&Quantity.Text!="")
            {
                int total = int.Parse(Quantity.Text) + int.Parse(textBox1.Text);
                if (total <= 60)
                {
                    Quantity.Text = (int.Parse(Quantity.Text) + int.Parse(textBox1.Text)).ToString();
                    progressBar1.Value += int.Parse(textBox1.Text);
                    textBox1.Text = "";
                }
                else
                    MessageBox.Show("The Quantity is full");
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            panel2.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Quantity.Text != "")
            {
                int total = int.Parse(Quantity.Text) + 1;
                if (total <= 60)
                {

                    Quantity.Text = (int.Parse(Quantity.Text) + 1).ToString();
                    progressBar1.Value += 1;
                }
                else
                    MessageBox.Show("The Quantity is full");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (Quantity.Text != "")
            {
                Quantity.Text = (int.Parse(Quantity.Text) - 1).ToString();
                progressBar1.Value -= 1;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DA = new SqlDataAdapter("select * from Medicine", con);
            DataTable tbl;
            tbl = new DataTable();
            DA.Fill(tbl);
            dataGridView1.DataSource = tbl;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           
                if (textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && comboBox2.Text != "")
                {
                    ///////////////////////////////////////////////////////////////////////
                    int x = 0;
                    DA = new SqlDataAdapter("select count(medicine_name) from Medicine where medicine_name='" + textBox2.Text + "'", con);
                    DataTable tbl2;
                    tbl2 = new DataTable();
                    DA.Fill(tbl2);
                    if (tbl2.Rows[0][0].ToString() == "1")
                    {
                        MessageBox.Show("this medicine is found");
                        textBox2.Focus();

                        x = 1;

                    }
                    ///////////////////////////////////////////////////////////////////////
                    if (x != 1)
                    {
                        DateTime t1;
                        t1 = dateTimePicker1.Value;
                        DateTime t2;
                        t2 = dateTimePicker2.Value;

                        if (t1 >= t2)
                        {
                            MessageBox.Show("Exp-date must be greater than Mfg-date ");
                            x = 1;
                        }
                    }
                   /////////////////////////////////////////////////////////////////////////
                    if (x == 0)
                    {
                        DA = new SqlDataAdapter("select company_id from company where company_name='" + comboBox2.Text + "'", con);
                        DataTable tbl;
                        tbl = new DataTable();
                        DA.Fill(tbl);
                        string str = tbl.Rows[0][0].ToString();

                        con.Open();
                        cmd = new SqlCommand("ADD_MED", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@med_name", textBox2.Text);
                        cmd.Parameters.AddWithValue("@price", textBox4.Text);
                        cmd.Parameters.AddWithValue("@mfg_date", dateTimePicker1.Text);
                        cmd.Parameters.AddWithValue("@exp_date", dateTimePicker2.Text);
                        cmd.Parameters.AddWithValue("@type", textBox3.Text);
                        cmd.Parameters.AddWithValue("@company_id", str);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Added Successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        comboBox1.Items.Add(textBox2.Text);
                        comboBox3.Items.Add(textBox2.Text);
                        comboBox4.Items.Add(textBox2.Text);
                    }
                    /////////////////////////////////////////////////////////////////////////
                }
                else if (textBox2.Text == "" && textBox3.Text == "" && textBox4.Text == "" && comboBox2.Text == "")
                     {
                         MessageBox.Show("Enter the information of medicine");
                         textBox2.Focus();
                     }
                else if (textBox2.Text == "")
                     {
                         MessageBox.Show("Enter Medicine name");
                         textBox2.Focus();
                         
                     }
                else if (textBox3.Text == "")
                     {
                         MessageBox.Show("Enter the Type");
                         textBox3.Focus();
                        
                     }
                else if (textBox4.Text == "")
                     {
                         MessageBox.Show("Enter the Price");
                         textBox4.Focus();
                     }
                     else if (comboBox2.Text == "")
                     {
                         MessageBox.Show("Enter the Company ");
                         comboBox2.Focus();
                        
                     }

            /////////////////////////////////////////////////////////////////////////
            
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("select price, quantity ,mfg_date,exp_date ,type, company_id from Medicine where medicine_name='" + comboBox3.Text + "'", con);
            DR = cmd.ExecuteReader();
            DR.Read();
            textBox2.Text = comboBox3.Text;
            textBox3.Text = DR["type"].ToString();
            textBox4.Text = DR["price"].ToString();
            dateTimePicker1.Text = DR["mfg_date"].ToString();
            dateTimePicker2.Text = DR["exp_date"].ToString();
            string str1 = DR["company_id"].ToString();
            DR.Close();
            con.Close();
            DA = new SqlDataAdapter("select company_name from company where company_ID = "+str1+"", con);
            DataTable tbl;
            tbl = new DataTable();
            DA.Fill(tbl);
            comboBox2.Text = tbl.Rows[0][0].ToString();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panel2.Visible = false;
            comboBox3.Text = "";
            clear2();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (comboBox3.Text != "")
            {

               DialogResult dialog =  MessageBox.Show("Do You Want to Delete "+comboBox3.Text, "Done", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
               if (dialog == DialogResult.Yes)
               {
                   con.Open();
                   cmd = new SqlCommand("delete_MED", con);
                   cmd.CommandType = CommandType.StoredProcedure;
                   cmd.Parameters.AddWithValue("@med_name", comboBox3.Text);
                   cmd.ExecuteNonQuery();
                   con.Close();
                   MessageBox.Show("Deleted Successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   comboBox4.Items.Remove(comboBox3.Text);
                   comboBox1.Items.Remove(comboBox3.Text);
                   comboBox3.Items.Remove(comboBox3.Text); 
                   clear2();
                   dataGridView1.Refresh();
               }
                
            }
            else
                MessageBox.Show("Enter the Medicine name first", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        private void button9_Click(object sender, EventArgs e)
        {
            DA = new SqlDataAdapter("select * from Company",con);
            DataTable tbl;
            tbl = new DataTable();
            DA.Fill(tbl);
            dataGridView2.DataSource = tbl;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

       

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("select company_ID ,city ,country , rate from Company where company_name='"+comboBox5.Text+"'", con);
            DR = cmd.ExecuteReader();
            DR.Read();
            Company_ID.Text = DR["company_ID"].ToString();
            City.Text = DR["city"].ToString();
            Country.Text = DR["country"].ToString();
            Rate.Text = DR["rate"].ToString();
            DR.Close();
            con.Close();

            DA = new SqlDataAdapter("select count(*) from Medicine where company_id="+Company_ID.Text+"", con);
            DataTable tbl;
            tbl = new DataTable();
            DA.Fill(tbl);
            num_of_medicine.Text = tbl.Rows[0][0].ToString();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            DA = new SqlDataAdapter("select price from Medicine where medicine_name = '"+comboBox4.Text+"'", con);
            DataTable tbl;
            tbl = new DataTable();
            DA.Fill(tbl);
            Price_of_can.Text = tbl.Rows[0][0].ToString();
            num_of_cans.Text = "";

        }

       
        private void num_of_cans_KeyUp(object sender, KeyEventArgs e)
        {
            if(comboBox4.Text!="")
            {
                if (num_of_cans.Text != "")
                {
                    try
                    {
                        float x = 0, y = 0;
                        x = float.Parse(num_of_cans.Text);
                        y = float.Parse(Price_of_can.Text);
                        total_price.Text = (x * y).ToString();
                    }
                    catch
                    {
                        MessageBox.Show("You should enter number");
                    }
                }
                else
                {
                    total_price.Text = "";
                }
            }
           
        }

        private void button10_Click(object sender, EventArgs e)
        {
            
            if (comboBox4.Text != "" && num_of_cans.Text != "" && Price_of_can.Text != "" && total_price.Text != "")
            {
                DA = new SqlDataAdapter("select quantity from Medicine where medicine_name='" + comboBox4.Text + "'", con);
                DataTable tbl;
                tbl = new DataTable();
                DA.Fill(tbl);
                int quantity , number;
                quantity = Convert.ToInt32(tbl.Rows[0][0]);
                number = Convert.ToInt32(num_of_cans.Text);
                if (quantity >= number)
                {
                    if (Bill.Text == "")
                    {
                        Bill.Text = total_price.Text;
                    }
                    else
                        Bill.Text = (float.Parse(Bill.Text) + float.Parse(total_price.Text)).ToString();

                    string medicine = comboBox4.Text;
                    string num = num_of_cans.Text;
                    string price = Price_of_can.Text;
                    string total = total_price.Text;
                    string[] arr = { medicine, num, price, total };
                    dataGridView3.Rows.Add(arr);
                

                    clear3(); 
                }
                else
                {

                    DA = new SqlDataAdapter("select quantity from Medicine where medicine_name = '" + comboBox4.Text + "'", con);
                    DataTable dt;
                    dt = new DataTable();
                    DA.Fill(dt);
                    string s = dt.Rows[0][0].ToString();
                    MessageBox.Show("The Quantity isn't enough \n The current Quantity is " + s);
                    num_of_cans.Focus();
                    
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
           
            dataGridView3.Rows.Clear();
            dataGridView3.Refresh();
            Bill.Text = "";
            clear3();
            
        }

        
        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                DA = new SqlDataAdapter("select quantity from Medicine where medicine_name='" + comboBox4.Text + "'", con);
                DataTable tbl;
                tbl = new DataTable();
                DA.Fill(tbl);
                int quantity , number;
                quantity = Convert.ToInt32(tbl.Rows[0][0]);
                number = Convert.ToInt32(num_of_cans.Text);
                if (quantity >= number)
                {
                    string c = Convert.ToString(dataGridView3.CurrentRow.Cells[3].Value);
                    Bill.Text = (float.Parse(Bill.Text) - float.Parse(c)).ToString();
                    dataGridView3.CurrentRow.Cells[0].Value = comboBox4.Text;
                    dataGridView3.CurrentRow.Cells[1].Value = num_of_cans.Text;
                    dataGridView3.CurrentRow.Cells[2].Value = Price_of_can.Text;
                    dataGridView3.CurrentRow.Cells[3].Value = total_price.Text;
                    Bill.Text = (float.Parse(Bill.Text) + float.Parse(total_price.Text)).ToString();
                    clear3();
                }
                else
                {
                    DA = new SqlDataAdapter("select quantity from Medicine where medicine_name = '"+comboBox4.Text+"'", con);
                    DataTable dt;
                    dt = new DataTable();
                    DA.Fill(dt);
                    string s = dt.Rows[0][0].ToString();
                    MessageBox.Show("The Quantity isn't enough \n The current Quantity is "+s);
                    num_of_cans.Focus();
                }

            }
            catch { MessageBox.Show("Select Row first"); }
            
        }

        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView3.Rows.Count != 0)
            {
                comboBox4.Text = dataGridView3.CurrentRow.Cells[0].Value.ToString();
                num_of_cans.Text = dataGridView3.CurrentRow.Cells[1].Value.ToString();
                Price_of_can.Text = dataGridView3.CurrentRow.Cells[2].Value.ToString();
                total_price.Text = dataGridView3.CurrentRow.Cells[3].Value.ToString();
            }
            else
                clear3();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                string c = Convert.ToString(dataGridView3.CurrentRow.Cells[3].Value);
                Bill.Text = (float.Parse(Bill.Text) - float.Parse(c)).ToString();
                dataGridView3.Rows.Remove(dataGridView3.CurrentRow);
            }
            catch { MessageBox.Show("Select Row first"); }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            clear3();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (dataGridView3.Rows.Count    > 0)
            {
                DialogResult dialog = MessageBox.Show("Are you Sure that you will exchange this medicine", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialog == DialogResult.Yes)
                {
                    DataTable tbl;
                    tbl = new DataTable();
                    for (int i = 0; i < dataGridView3.Rows.Count; i++)
                    {
                        string str1 = dataGridView3.Rows[i].Cells[0].Value.ToString();
                        string str2 = dataGridView3.Rows[i].Cells[1].Value.ToString();
                        con.Open();
                        cmd = new SqlCommand("insert_quantity", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@num", str2);
                        cmd.Parameters.AddWithValue("@medicine_name", str1);
                        cmd.ExecuteNonQuery();
                        con.Close();

                    }
                    dataGridView3.Rows.Clear();
                    dataGridView3.Refresh();
                    Bill.Text = "";
                    clear3();

                }
            }
        }

        private void num_of_cans_TextChanged(object sender, EventArgs e)
        {

        }

       

        private void button16_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Do You Want To Exit ", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if(dialog==DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 frm = new Form1();
            frm.Show();
        }

        



    }
}
