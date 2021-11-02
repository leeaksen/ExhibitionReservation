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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            Text = "회원";

            dataGridView1.DataSource = DataManager.Users;
            dataGridView1.CurrentCellChanged += DataGridView1_CurrentCellChanged;

            button1.Click += (sender, e) =>  // 추가
            {
                try
                {
                    if (DataManager.Users.Exists((x) => x.Id == int.Parse(textBox2.Text)))
                    {
                        MessageBox.Show("회원 아이디가 겹칩니다.");
                    }
                    else
                    {
                        User user = new User()
                        {
                            Id = int.Parse(textBox2.Text),
                            Name = textBox1.Text,
                            PhoneNumber = textBox3.Text
                        };

                        DataManager.Users.Add(user);

                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = DataManager.Users;
                        DataManager.Save();
                    }
                }
                catch (Exception ex)
                {

                }
            };



            button2.Click += (sender, e) =>   // 수정
            {
                try
                {
                    User user = DataManager.Users.Single((x) => x.Id == int.Parse(textBox2.Text));
                    user.Name = textBox1.Text;
                    user.PhoneNumber = textBox3.Text;

                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = DataManager.Users;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("존재하지 않는 회원입니다.");
                }
            };

            button3.Click += (sender, e) =>  // 삭제
            {
                try
                {
                    User user = DataManager.Users.Single((x) => x.Id == int.Parse(textBox2.Text));
                    DataManager.Users.Remove(user);

                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = DataManager.Users;
                    DataManager.Save();
                }
                catch (Exception ex)
                {

                }
            };
        }

        private void DataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                User user = dataGridView1.CurrentRow.DataBoundItem as User;
                textBox2.Text = user.Id.ToString();
                textBox1.Text = user.Name;
                textBox3.Text = user.PhoneNumber;
            }
            catch (Exception ex)
            {

            }
        }

    }

    }
