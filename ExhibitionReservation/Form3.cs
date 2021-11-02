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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            Text = "전시회 관리";

            dataGridView1.DataSource = DataManager.Reservations;
            dataGridView1.CurrentCellChanged += dataGridView1_CurrentCellChanged;

            button1.Click += (sender, e) => // 추가
            {
                try
                {
                    if (DataManager.Reservations.Exists(x => x.No == textBox1.Text))
                    {
                        MessageBox.Show("이미 존재하는 전시회입니다.");
                    }
                    else
                    {
                        Reservation reservation = new Reservation()
                        {
                            No = textBox1.Text,
                            Name = textBox2.Text,
                            Time = textBox3.Text,
                            Place = textBox4.Text,
                            Person = textBox5.Text
                        };
                        DataManager.Reservations.Add(reservation); //추가
                    }
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = DataManager.Reservations;
                    DataManager.Save();
                }
                catch (Exception ex)
                {

                }
            };

            button2.Click += (sender, e) =>  // 수정
            {
                try
                {
                    Reservation reservation = DataManager.Reservations.Single(x => x.No == textBox1.Text);
                    reservation.Name = textBox2.Text;
                    reservation.Time = textBox3.Text;
                    reservation.Place = textBox4.Text;
                    reservation.Person = textBox5.Text;

                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = DataManager.Reservations;
                    DataManager.Save();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("존재하지 않는 전시회입니다.");
                }
            };

            button3.Click += (sender, e) => // 삭제
            {
                try
                {

                    Reservation reservation = DataManager.Reservations.Single(x => x.No == textBox1.Text);
                    DataManager.Reservations.Remove(reservation);

                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = DataManager.Reservations;
                    DataManager.Save();
                }
                catch (Exception ex)
                {

                }

            };
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                Reservation reservation = dataGridView1.CurrentRow.DataBoundItem as Reservation;
                textBox1.Text = reservation.No;
                textBox2.Text = reservation.Name;
                textBox3.Text = reservation.Time;
                textBox4.Text = reservation.Place;
                textBox5.Text = reservation.Person;
            }
            catch (Exception ex)
            {

            }
        }

        private void 회원ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form2().ShowDialog();
        }

        private void 예매ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form1().ShowDialog();
        }
    }
}
