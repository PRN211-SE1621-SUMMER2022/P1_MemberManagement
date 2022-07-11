using System.Data;
using MemberObject;
using Microsoft.Data.SqlClient;


namespace DataAccess.DatabaseAccess

{
    public class MemberDAO : BaseDAL
    {
        private static MemberDAO instance = null;
        private static readonly object instanceLock = new object();
        private MemberDAO() { }
        public static MemberDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new MemberDAO();
                    }
                    return instance;
                }
            }
        }
        public IEnumerable<Member> GetMemberList()
        {
            IDataReader dataReader = null;
            string SQlSelect = "Select MemberID, MemberName, Email, Password, City, Country from Members";
            var mem = new List<Member>();
            try
            {
                dataReader = dataProvider.GetDataReader(SQlSelect, CommandType.Text, out connection);
                while (dataReader.Read())
                {
                    mem.Add(new Member
                    {
                        MemberID = dataReader.GetInt32(0),
                        MemberName = dataReader.GetString(1),
                        Email = dataReader.GetString(2),
                        Password = dataReader.GetString(3),
                        City = dataReader.GetString(4),
                        Country = dataReader.GetString(5)
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dataReader.Close();
                CloseConnection();
            }
            return mem;
        }
        public Member GetMemberByID(int memberID)
        {
            Member mem = null;
            IDataReader dataReader = null;
            string SQlSelect = "Select MemberID, MemberName, Email, Password, City, Country " + "from Members where MemberID =@MemberID";
            try
            {
                var param = dataProvider.CreateParameter("@MemberID", 4, memberID, DbType.Int32);
                dataReader = dataProvider.GetDataReader(SQlSelect, CommandType.Text, out connection, param);
                if (dataReader.Read())
                {
                    mem = new Member
                    {
                        MemberID = dataReader.GetInt32(0),
                        MemberName = dataReader.GetString(1),
                        Email = dataReader.GetString(2),
                        Password = dataReader.GetString(3),
                        City = dataReader.GetString(4),
                        Country = dataReader.GetString(5)
                    };

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dataReader.Close();
                CloseConnection();
            }
            return mem;
        }
        public void AddNew(Member member)
        {
            try
            {
                Member mem = GetMemberByID(member.MemberID);
                if (mem == null)
                {
                    string SQlInsert = " Insert  Members values (@MemberID,@MemberName,@Email,@Password,@City,@Country)";
                    var parameters = new List<SqlParameter>();
                    parameters.Add(dataProvider.CreateParameter("@MemberID", 4, member.MemberID, DbType.Int32));
                    parameters.Add(dataProvider.CreateParameter("@MemberName", 50, member.MemberName, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@Email", 50, member.Email, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@Password", 50, member.Password, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@City", 50, member.City, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@Country", 50, member.Country, DbType.String));
                    dataProvider.Insert(SQlInsert, CommandType.Text, parameters.ToArray());
                }
                else
                {
                    throw new Exception("The member is aldready extit");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
        public void Update(Member member)
        {
            try
            {
                Member mem = GetMemberByID(member.MemberID);
                if (mem != null)
                {
                    string SQlUpadate = " Update  Members set MemberName=@MemberName, Email=@Email, Password=@Password, City=@City,Country=@Country where MemberID=@MemberID";
                    var parameters = new List<SqlParameter>();
                    parameters.Add(dataProvider.CreateParameter("@MemberID", 4, member.MemberID, DbType.Int32));
                    parameters.Add(dataProvider.CreateParameter("@MemberName", 50, member.MemberName, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@Email", 50, member.Email, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@Password", 50, member.Password, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@City", 50, member.City, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@Country", 50, member.Country, DbType.String));
                    dataProvider.Update(SQlUpadate, CommandType.Text, parameters.ToArray());
                }
                else
                {
                    throw new Exception("The member is aldready extit");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public void Remove(int memberID)
        {
            try
            {
                Member mem = GetMemberByID(memberID);
                if (mem != null)
                {
                    string SQLDelete = "Delete Members where MemberID=@MemberID";
                    var param = dataProvider.CreateParameter("@MemberID", 4, memberID, DbType.Int32);
                    dataProvider.Delete(SQLDelete, CommandType.Text, param);
                }
                else
                {   
                    throw new Exception("The member does not aldready exit");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public List<Member> GetMemberByCity(string city)
        {
            IDataReader dataReader = null;
            string SQlSelect = "Select MemberID, MemberName, Email, Password, City, Country " + 
                "from Members where City =@Ciy";
            var mem = new List<Member>();
            try
            {
                var param = dataProvider.CreateParameter("@City", 50, city, DbType.String);
                dataReader = dataProvider.GetDataReader(SQlSelect, CommandType.Text, out connection, param);
                while (dataReader.Read())
                {
                    mem.Add(new Member
                    {
                        MemberID = dataReader.GetInt32(0),
                        MemberName = dataReader.GetString(1),
                        Email = dataReader.GetString(2),
                        Password = dataReader.GetString(3),
                        City = dataReader.GetString(4),
                        Country = dataReader.GetString(5)
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dataReader.Close();
                CloseConnection();
            }
            return mem;
        }

    }
}
