using MemberObject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataAccess.Repository;

namespace MyStoreWinApp
{
    public partial class frmMemberDetail : Form
    {
        public frmMemberDetail()
        {
            InitializeComponent();
        }

        public IMemberRepository MemberRepository { get; set; }
        public bool InsertOrUpdate { get; set; }
        public Member MemInfo { get; set; }

        private void frmMemberDetail_Load(object sender, EventArgs e)
        {
            txtMemberID.Enabled = !InsertOrUpdate;
            if (InsertOrUpdate == true)
            {
                txtMemberID.Text = MemInfo.MemberID.ToString();
                txtMemberName.Text = MemInfo.MemberName.ToString();
                txtEmail.Text = MemInfo.Email.ToString();
                txtPassword.Text = MemInfo.Password.ToString();
                txtCity.Text = MemInfo.City.ToString();
                txtCountry.Text = MemInfo.Country.ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var mem = new Member
                {
                    MemberID = int.Parse(txtMemberID.Text),
                    MemberName = txtMemberName.Text,
                    Email = txtEmail.Text,
                    Password = txtPassword.Text,
                    City = txtCity.Text,
                    Country = txtCountry.Text,
                };
                if (InsertOrUpdate == false)
                {
                    MemberRepository.InsertMember(mem);
                }
                else
                {
                    MemberRepository.UpdateMember(mem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, InsertOrUpdate == false ? "Add a new member" : "Update a member");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) => Close();

    }
}
