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
    public partial class frmMemberManagement : Form
    {
        public bool AdminOrUser { get; set; }
        IMemberRepository memberRepository = new MemberRepository();
        BindingSource source;
        public frmMemberManagement()
        {
            InitializeComponent();
        }
        private void ClearText()
        {
            txtMemberID.Text = String.Empty;
            txtMemberName.Text = String.Empty;
            txtEmail.Text = String.Empty;
            txtCity.Text = String.Empty;
            txtCountry.Text = String.Empty;
        }

        private Member GetMemberObject()
        {
            Member member = null;
            try
            {
                member = new Member
                {
                    MemberID = int.Parse(txtMemberID.Text),
                    MemberName = txtMemberName.Text,
                    Email = txtEmail.Text,
                    City = txtCity.Text,
                    Country = txtCountry.Text,
                    Password = txtPassword.Text,
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get member");
            }
            return member;
        }
        public void LoadMemberList()
        {
            var cars = memberRepository.GetMembers();
            try
            {
                source = new BindingSource();
                source.DataSource = cars;

                txtMemberID.DataBindings.Clear();
                txtMemberName.DataBindings.Clear();
                txtEmail.DataBindings.Clear();
                txtPassword.DataBindings.Clear();
                txtCity.DataBindings.Clear();
                txtCountry.DataBindings.Clear();

                txtMemberID.DataBindings.Add("Text", source, "MemberID");
                txtMemberName.DataBindings.Add("Text", source, "MemberName");
                txtEmail.DataBindings.Add("Text", source, "Email");
                txtPassword.DataBindings.Add("Text", source, "Password");
                txtCity.DataBindings.Add("Text", source, "City");
                txtCountry.DataBindings.Add("Text", source, "Country");

                dgvMemberList.DataSource = null;
                dgvMemberList.DataSource = source;

                if (cars.Count() == 0)
                {
                    ClearText();
                    btnDelete.Enabled = false;
                }
                else
                {
                    btnDelete.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load member list");
            }
        }// end LoadCarList
        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadMemberList();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmMemberDetail frm = new frmMemberDetail
            {
                Text = "Add new member",
                InsertOrUpdate = false,
                MemberRepository = memberRepository
            };
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadMemberList();
                source.Position = source.Count - 1;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var member = GetMemberObject();
                memberRepository.DeleteMember(member.MemberID);
                LoadMemberList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete a member");
            }
        }

        private void btnClose_Click(object sender, EventArgs e) => Close();

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void frmMemberManagement_Load(object sender, EventArgs e)
        {
            if (AdminOrUser == false)
            {

                btnNew.Enabled = false;
                txtCity.Enabled = false;
                txtCountry.Enabled = false;
                txtEmail.Enabled = false;
                txtMemberID.Enabled = false;
                txtMemberName.Enabled = false;
                txtPassword.Enabled = false;
                btnDelete.Visible = false;
                dgvMemberList.CellDoubleClick += null;
            }
            else
            {
                btnDelete.Enabled = false;
                dgvMemberList.CellDoubleClick += dgvMemberList_CellDoubleClick;
            }
        }

        private void dgvMemberList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmMemberDetail frmMemberDetail = new frmMemberDetail()
            {
                Text = "Update member",
                InsertOrUpdate = true,
                MemInfo = GetMemberObject(),
                MemberRepository = memberRepository
            };
            if (frmMemberDetail.ShowDialog() == DialogResult.OK)
            {
                LoadMemberList();
                source.Position = source.Count - 1;
            }
        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            Member member = new Member();
            var members = memberRepository.GetMembers();
            try
            {
                foreach (var mem in members)
                {
                    if (mem.MemberName.Equals(txtSearch.Text))
                    {
                        source = new BindingSource();


                        source.DataSource = memberRepository.GetMemberByID(mem.MemberID);

                        txtMemberID.DataBindings.Clear();
                        txtMemberName.DataBindings.Clear();
                        txtPassword.DataBindings.Clear();
                        txtEmail.DataBindings.Clear();
                        txtCity.DataBindings.Clear();
                        txtCountry.DataBindings.Clear();

                        txtMemberID.DataBindings.Add("Text", source, "MemberID");
                        txtMemberName.DataBindings.Add("Text", source, "MemberName");
                        txtPassword.DataBindings.Add("Text", source, "Password");
                        txtEmail.DataBindings.Add("Text", source, "Email");
                        txtCity.DataBindings.Add("Text", source, "Country");
                        txtCountry.DataBindings.Add("Text", source, "City");


                        dgvMemberList.DataSource = null;
                        dgvMemberList.DataSource = source;
                        break;
                    }
                    else if (mem.MemberID.ToString().Equals(txtSearch.Text))
                    {
                        source = new BindingSource();


                        source.DataSource = memberRepository.GetMemberByID(mem.MemberID);

                        txtMemberID.DataBindings.Clear();
                        txtMemberName.DataBindings.Clear();
                        txtPassword.DataBindings.Clear();
                        txtEmail.DataBindings.Clear();
                        txtCity.DataBindings.Clear();
                        txtCountry.DataBindings.Clear();

                        txtMemberID.DataBindings.Add("Text", source, "MemberID");
                        txtMemberName.DataBindings.Add("Text", source, "MemberName");
                        txtPassword.DataBindings.Add("Text", source, "Password");
                        txtEmail.DataBindings.Add("Text", source, "Email");
                        txtCity.DataBindings.Add("Text", source, "City");
                        txtCountry.DataBindings.Add("Text", source, "Country");


                        dgvMemberList.DataSource = null;
                        dgvMemberList.DataSource = source;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load member list");
            }
        }

        private void cboCity_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboCountry_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
