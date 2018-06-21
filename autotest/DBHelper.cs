using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Data.SqlClient;

namespace ASFund.AutoTest
{
    /// <summary>
    /// Класс содержит вспомогательные функции для работы с базой данных 
    /// </summary>
    public static class DBHelper
    {
        private static string _constring = @"Server=;Database=;Uid=;Pwd=;";

        /// <summary>
        /// Проверка заявки в базе данных по ее номеру  
        /// </summary>
        /// <param name="contestQueryNumber"></param>
        public static bool ContestQueryIfExists(string contestQueryNumber)
        {
            SqlConnection connection = new SqlConnection(_constring);
            string strsql = $"select * from [ASFund].[dbo].[tblContestsQueries] where Number ='{contestQueryNumber}'";
            SqlCommand command = new SqlCommand(strsql, connection);
            DataTable dataTable = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);

            try
            {
                dataAdapter.Fill(dataTable);
            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            if ((dataTable.Rows).Count == 0)
            {
                return false;
            }
            
            DataRow row = dataTable.Rows[0];
            if (row["Number"].ToString() == contestQueryNumber)
                return true;
            else
                return false;
            
        }

        /// <summary>
        /// По номеру заявки возвращаем ее Id 
        /// </summary>
        /// <param name="contestQueryNumber"></param>
        public static string ContestQueryIDbyNumber(string contestQueryNumber)
        {
            SqlConnection connection = new SqlConnection(_constring);
            string strsql = $"select * from [ASFund].[dbo].[tblContestsQueries] where Number ='{contestQueryNumber}'";
            SqlCommand command = new SqlCommand(strsql, connection);
            DataTable dataTable = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);

            try
            {
                dataAdapter.Fill(dataTable);
            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
			
            DataRow row = dataTable.Rows[0];
            return row["Id"].ToString();              
        }

        /// <summary>
        /// Удаляем договор по ID заявки
        /// </summary>
        public static void DeleteContractForContestsQueryID(string contestQueryID)
        {
            SqlConnection connection = new SqlConnection(_constring);
            string strsql = $"delete from [ASFund].[dbo].[tblContracts] where ContestsQueryId ='{contestQueryID}'";
            SqlCommand command = new SqlCommand(strsql, connection);

            try
            {
                connection.Open();
                int result = command.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// По номеру заявки возвращаем Id контракта, ContestsQueryId = 56818, заявка С2-46756
        /// </summary>
        /// <param name="contestQueryNumber"></param>
        public static string ContractIDbyContestQueryID(string contestQueryId)
        {
            SqlConnection connection = new SqlConnection(_constring);
            string strsql = $"select * from [ASFund].[dbo].[tblContracts] where ContestsQueryId ='{contestQueryId}'";
            SqlCommand command = new SqlCommand(strsql, connection);
            DataTable dataTable = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);

            try
            {
                dataAdapter.Fill(dataTable);
            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            DataRow row = dataTable.Rows[0];
            return row["Id"].ToString();
        }

        /// <summary>
        /// По Id контракта возвращаем Id финансового отчета о расходовании средств гранта, 1 этап
        /// </summary>
        /// <param name="contestQueryNumber"></param>
        public static string FinancialReportIdbyContractId(string contractId)
        {
            SqlConnection connection = new SqlConnection(_constring);
            string strsql = $"SELECT [Id] FROM[ASFund].[dbo].[tblReportingStageTasks]  where[ReportingStageId] in (SELECT[Id] FROM[ASFund].[dbo].[tblReportingStages] where[ContractId] ='{contractId}' and [Name] = 'Подэтапа 1' ) and [Name] = 'Финансовый отчет о расходовании средств гранта'";
            SqlCommand command = new SqlCommand(strsql, connection);
            DataTable dataTable = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);

            try
            {
                dataAdapter.Fill(dataTable);
            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            DataRow row = dataTable.Rows[0];
            return row["Id"].ToString();
        }
    }
}
