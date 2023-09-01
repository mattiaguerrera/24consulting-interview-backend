//using System.Data;
//using System.Data.SqlClient;
//using Dapper;
//using Domain.Models;

//namespace WebApi.Repository
//{
//    public class AuditRepository : IAuditRepository
//    {
//        private readonly IConfiguration _configuration;
//        public AuditRepository(IConfiguration configuration)
//        {
//            _configuration = configuration;
//        }

//        public void InsertAuditLog(Audit audit)
//        {
//            using SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
//            try
//            {
//                con.Open();
//                var para = new DynamicParameters();
//                para.Add("@Type", audit.Type);
//                para.Add("@TableName", audit.TableName);
//                para.Add("@OldValues", audit.OldValues);
//                para.Add("@NewValues", audit.NewValues);
//                para.Add("@AffectedColumns", audit.AffectedColumns);
//                para.Add("@PrimaryKey", audit.PrimaryKey);
//                con.Execute("Usp_InsertAuditLogs", para, null, 0, CommandType.StoredProcedure);
//            }
//            catch (Exception)
//            {
//                throw;
//            }
//        }
//    }
//}