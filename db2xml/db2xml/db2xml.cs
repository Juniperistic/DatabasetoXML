using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Data.SQLite;

/*************************************************************************/
/* Program Name:     db2xml.cs                                         	 */
/* Date:             05/01/2022                                     	 */
/* Programmer:       Miranda Morris                                      */
/* Class:            CSCI 234                               		     */
/*                                                                  	 */
/* Program Description: The purpose of this program is to create an xml  */
/* file name "customer.xml" containing data from the database            */
/*                                                                       */
/* Input: data from the database is entered through hardcode             */
/*                                                                       */
/* Output: an xml file containing the basic structure of the database    */
/* titled "customer.xml"                                                 */
/*                                                                       */
/* Givens: data from customer table                                      */
/*                                                                       */
/* Testing Data:                                                         */
/*                                                                       */
/* List the Testing Input Data: I ran the program and checked the debug  */
/* folder which outputted the customer.xml file                          */
/*                                                                       */
/*                                                                       */
/*************************************************************************/

public class DB2XML
{
    static string connectString;
    static string commandString;
    static SQLiteConnection connection = null;
    static SQLiteCommand sqlCmd;

    public class Customer
    {
        public string CustID;
        public string FirstName;
        public string LastName;
        public string Address;
        public string City;
        public string State;
        public string Zip;
        public string ZipExt;
        public string AreaCode;
        public string Phone;
        public string CellAreaCode;
        public string CellPhone;
        public string Email;
    }

    static private void SerializeObject(string filename)
    {
        SQLiteDataReader reader = null;
        XmlSerializer serializer = new XmlSerializer(typeof(Customer));

        TextWriter writer = new StreamWriter(filename);

        Customer i = new Customer();

        connectString = "Data Source=Simple.db";

        commandString = "select * from Customer";

        try
        {
            connection = new SQLiteConnection(connectString);
            connection.Open();

            sqlCmd = new SQLiteCommand();
            sqlCmd.CommandText = commandString;
            sqlCmd.Connection = connection;

            reader = sqlCmd.ExecuteReader();
            while (reader.Read())
            {
                i.CustID = reader.GetValue(0).ToString();
                i.FirstName = reader.GetValue(1).ToString();
                i.LastName = reader.GetValue(2).ToString();
                i.Address = reader.GetValue(3).ToString();
                i.City = reader.GetValue(4).ToString();
                i.State = reader.GetValue(5).ToString();
                i.Zip = reader.GetValue(6).ToString();
                i.ZipExt = reader.GetValue(7).ToString();
                i.AreaCode = reader.GetValue(8).ToString();
                i.Phone = reader.GetValue(9).ToString();
                i.CellAreaCode = reader.GetValue(10).ToString();
                i.CellPhone = reader.GetValue(11).ToString();
                i.Email = reader.GetValue(12).ToString();
                Console.WriteLine();
            }
        }

        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        finally
        {
            if (reader != null) reader.Close();
            if (connection != null) connection.Close();
        }

        serializer.Serialize(writer, i);
        writer.Close();
    }

    static void Main(string[] args)
    {
        SerializeObject("Customer.xml");
    }
}
