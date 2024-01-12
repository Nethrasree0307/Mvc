using System.Data.SqlClient;
namespace LMS.Models
{
    public class AdminLogin
    {
           
     static SqlConnection sqlconnection = new SqlConnection ("data source=DESKTOP-64DHIL3\\SQLEXPRESS;initial catalog=LeaveManagement;trusted_connection=true");
        static  public  string adminlogin(Logindetail user)
        {
             string? username=user.Name;
              string? password=user.password;
                
                sqlconnection.Open();

               SqlCommand command=new SqlCommand("Select Count(*) from AdminLogin WHERE username='"+username+"' AND password='"+password+"';",sqlconnection); 
                int Count=Convert.ToInt32(command.ExecuteScalar());
                 sqlconnection.Close();
                if(Count==1)
                {
                  return "success";
                  
                }
                return "fails";
                }
               
        }
       
}

