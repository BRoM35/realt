using Dadata;
using Dadata.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtOn
{
   public class SuggestClientDadata
    {
 
        public SuggestClient api
        {
            get; set;
        }

        public void SetUp()
        {
           
        }

        public IEnumerable<Dadata.Model.Suggestion<Dadata.Model.Address>> SuggestAddress(string data)
        {
            
                var token = "518f96d435704b748e40b3f6a0aa18efea327334";
            this.api = new SuggestClient(token);
           var query = new SuggestAddressRequest(data);
            query.locations = new[] {
                new Address() { kladr_id  = "7200000000000" },
            };
            query.restrict_value = true;
                 var response = api.SuggestAddress(query).suggestions;
                return response;
          
        }

        public void AddDB(Dadata.Model.Suggestion<Dadata.Model.Address> address)
        {

            string streetid="null";
            string settlementid="null";
            string cityid = "null";

            //string[] columns = { "source","result","settlemet_kladr_id" };
            string sConnectionString = "Data Source=ТИНА-ПК\\SQLEXPRESS;Initial Catalog=realton;Integrated Security=True";
            SqlConnection objConn = new SqlConnection(sConnectionString);
            objConn.Open();
            ///cities  
            if (address.data.city != null)
               cityid = new SqlCommand("if not EXISTS(select id from cities where city_kladr_id='" + address.data.city_kladr_id + "')  insert  into cities  output INSERTED.ID values('" + address.data.city + "','" + address.data.city_kladr_id + "') else select id from cities where city_kladr_id='" + address.data.city_kladr_id + "'", objConn).ExecuteScalar().ToString();
            ///settlemetns    
            if (address.data.settlement != null)
                settlementid = new SqlCommand("if not EXISTS(select id from Settlements where settlement_kladr_id='"+ address.data.settlement_kladr_id+ "')  insert  into Settlements  output INSERTED.ID values('"+ address.data.settlement + "','"+ address.data.settlement_with_type + "','"+ address.data.settlement_kladr_id + "') else select id from Settlements where settlement_kladr_id='"+address.data.settlement_kladr_id +"'", objConn).ExecuteScalar().ToString();
           ///streets
            if(address.data.street !=null)
                streetid = new SqlCommand("if not EXISTS(select id from streets where street_kladr_id='"+ address.data.street_kladr_id + "') insert  into streets  output INSERTED.ID values('"+ address.data.street + "','"+ address.data.street_with_type + "',"+ settlementid + ",'"+ address.data.street_kladr_id + "') else select id from streets where street_kladr_id='"+address.data.street_kladr_id + "'", objConn).ExecuteScalar().ToString();
            //addresses
            SqlCommand comm = objConn.CreateCommand();
            comm.CommandText = "if not EXISTS(select id from adresses where kladr_id='" + address.data.kladr_id + "') INSERT into adresses values('" + address.value + "'," + cityid + "," + streetid + "," + settlementid + ",'" + address.data.house + "','" + address.data.flat + "','" + address.data.kladr_id + "')";
          //  for (int i = 0; i <= columns.Count(); i++)
             //  comm.Parameters.AddWithValue("@"+ columns[i], address_data.);
            comm.ExecuteNonQuery();
            objConn.Close();
        }
    }
}
