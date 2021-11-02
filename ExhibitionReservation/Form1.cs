using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExhibitionReservation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Text = "전시회 예매";

            dataGridView1.DataSource = DataManager.Reservations;
            dataGridView2.DataSource = DataManager.Users;

            dataGridView1.CurrentCellChanged += dataGridView1_CurrentCellChanged;
            dataGridView2.CurrentCellChanged += dataGridView2_CurrentCellChanged;
        }

        private void button1_Click(object sender, EventArgs e) //예매
        {
            if (textBox2.Text.Trim() == "")
            {

            }
            else if (textBox3.Text.Trim() == "")
            {

            }
            else
            {
                try
                {
                    Reservation reservation = DataManager.Reservations.Single(x => x.No == textBox2.Text);
                    if (reservation.IsReserved)
                    {
                        MessageBox.Show("이미 예약된 티켓입니다.");
                    }
                    else
                    {
                        User user = DataManager.Users.Single(x => x.Id.ToString() == textBox1.Text);
                        reservation.UserId = user.Id;
                        reservation.UserName = user.Name;
                        reservation.IsReserved = true;

                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = DataManager.Reservations;
                        DataManager.Save();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("존재하지 않는 아이디 입니다.");
                }
            }
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                Reservation reservation = dataGridView1.CurrentRow.DataBoundItem as Reservation;
                textBox2.Text = reservation.No;
                textBox3.Text = reservation.Name;
                textBox4.Text = reservation.Person;
            }
            catch (Exception ex)
            {

            }
        }
        private void dataGridView2_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                User user = dataGridView2.CurrentRow.DataBoundItem as User;
                textBox1.Text = user.Id.ToString();
            }
            catch (Exception ex)
            {

            }
        }

        private void button2_Click(object sender, EventArgs e) // 예매 취소
        {
            try
            {
                Reservation reservation = DataManager.Reservations.Single(x => x.No == textBox2.Text);
                if (reservation.IsReserved)
                {
                    User user = DataManager.Users.Single(x => x.Id.ToString() == textBox1.Text);
                    reservation.UserId = 0;
                    reservation.UserName = "";
                    reservation.IsReserved = false;

                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = DataManager.Reservations;
                    DataManager.Save();


                }
                else
                {
                }
            }
            catch (Exception ex)
            {
        
            }
        }

        //private void 회원ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    new Form2().ShowDialog();
        //}

        //private void 전시회관리ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    new Form3().ShowDialog();
        //}
    }
}

