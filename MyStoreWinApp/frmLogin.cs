
using DataAccess.Repository;
using MemberObject;
using Nancy.Json;

namespace MyStoreWinApp
{



    public partial class frmLogin : Form
    {
        IMemberRepository memberRepository = new MemberRepository();
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            string json = string.Empty;

            // read json string from file
            using (StreamReader reader = new StreamReader("appsettings.json"))
            {
                json = reader.ReadToEnd();
            }

            JavaScriptSerializer jss = new JavaScriptSerializer();

            // convert json string to dynamic type
            var obj = jss.Deserialize<dynamic>(json);

            // get contents

            var admin = new Member
            {
                Email = obj["DefaultAccount"]["Email"],
                Password = obj["DefaultAccount"]["Password"]
            };

            Boolean check = false;
            var member = memberRepository.GetMembers();
            foreach(var mem in member)
            {
                if(mem.Email.Equals(txtEmail.Text) && mem.Password.Equals(txtPassword.Text))
                {
                    frmMemberManagement frm = new frmMemberManagement()
                    {
                        AdminOrUser = false
                    };
                    frm.ShowDialog();
                    check = true;
                    this.Close();
                }else if(admin.Email.Equals(txtEmail.Text) && admin.Password.Equals(txtPassword.Text    )){
                    frmMemberManagement frm = new frmMemberManagement()
                    {
                        AdminOrUser = true
                    };
                    frm.ShowDialog();
                    this.Close();
                }
               
            }
            if(check == false)
            {
                MessageBox.Show("Erorr", "notification");
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}