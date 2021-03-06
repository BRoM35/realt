﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtOn
{
    class Ticket
    {
        public enum TType
        {
            [Description("ПРОДАЖА")]
            Sell = 1,
            [Description("ПОКУПКА")]
            Buy
        
        }



        public static DataSet GetTicket(string id)
        {

            string sConnectionString = "Data Source=ТИНА-ПК\\SQLEXPRESS;Initial Catalog=realton;Integrated Security=True";
            SqlConnection objConn = new SqlConnection(sConnectionString);
            objConn.Open();
            SqlDataAdapter daobjects = new SqlDataAdapter("Select tickets.id,status,FullName as 'User', stage,img,type,description from tickets left join users on users.id = userId where tickets.id = " + id + "", objConn);
            DataSet dsobjects = new DataSet();
            daobjects.Fill(dsobjects, "Ticket");
            objConn.Close();
        
            return dsobjects;
        }
        public static DataSet GetTicketsList(int page, string filtr)
        {

            string sConnectionString = "Data Source=ТИНА-ПК\\SQLEXPRESS;Initial Catalog=realton;Integrated Security=True";
            SqlConnection objConn = new SqlConnection(sConnectionString);
            objConn.Open();


   SqlDataAdapter daobjects = new SqlDataAdapter("SELECT TOP 10 * FROM(Select Tickets.id, Tickets.description, Tickets.stage, Tickets.status, Tickets.type, Clients.FullName as Client, Users.FullName as 'User' from Tickets left join Clients on Clients.id = clientId left join Users on Users.id = userId " + filtr + " ORDER BY Tickets.id OFFSET " + (page * 10) + " ROWS) aliasname", objConn);
            DataSet dsobjects = new DataSet();
            daobjects.Fill(dsobjects, "ObjList");
            objConn.Close();
            return dsobjects;
        }

        public static string GetTypeTicket(string id)
        {

            string sConnectionString = "Data Source=ТИНА-ПК\\SQLEXPRESS;Initial Catalog=realton;Integrated Security=True";
            SqlConnection objConn = new SqlConnection(sConnectionString);
            objConn.Open();
            int type = Convert.ToInt32(new SqlCommand("Select type from tickets where tickets.id = " + id + "", objConn).ExecuteScalar());
            objConn.Close();
            return Tools.GetDescription((Ticket.TType)type);

        }
    }
}
