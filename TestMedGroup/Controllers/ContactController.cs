using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;

namespace TestMedGroup.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {

        private static IConfiguration Configuration;
        IList<Contato> contatos = new List<Contato>();

        private readonly SqlConnection conn;
        private static ContactController _instance;

        private ContactController()
        {
            conn = new SqlConnection
            {
                ConnectionString = ContactController.GetParamString()
            };

        }

        public static ContactController GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ContactController();
            }
            return _instance;
        }

        #region REST
        [HttpGet]
        public IList<Contato> Get()
        {
            try
            {

                using (SqlConnection db = new SqlConnection(conn.ConnectionString))
                {
                    SqlCommand cmd = null;

                    db.Open();
                    cmd = db.CreateCommand();
                    cmd.CommandText = "SELECT * FROM contato";
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        Contato c = new Contato();
                        c.Id = new Guid(dr["id"].ToString());
                        c.Nome = dr["nome"].ToString();
                        c.DataNascimento = Convert.ToDateTime(dr["dataNascimento"]);
                        c.Sexo = Convert.ToChar(dr["sexo"]);
                        c.Idade = Convert.ToInt32(dr["idade"]);

                        contatos.Add(c);
                    }

                    cmd.ExecuteNonQuery();

                    db.Close();
                }
            }
            catch (Exception e)
            {
                //throw e;
                //return BadRequest();
            }


            return contatos;
        }


        // GET /values/5
        [HttpGet("{id}")]
        public string Get(string id)
        {
            try
            {

                using (SqlConnection db = new SqlConnection(conn.ConnectionString))
                {
                    SqlCommand cmd = null;

                    db.Open();
                    cmd = db.CreateCommand();
                    cmd.CommandText = "SELECT * FROM contato";
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        Contato c = new Contato();
                        c.Id = new Guid(dr["id"].ToString());
                        c.Nome = dr["nome"].ToString();
                        c.DataNascimento = Convert.ToDateTime(dr["dataNascimento"]);
                        c.Sexo = Convert.ToChar(dr["sexo"]);
                        c.Idade = Convert.ToInt32(dr["idade"]);

                        contatos.Add(c);
                    }

                    cmd.ExecuteNonQuery();

                    db.Close();
                }
            }
            catch (Exception e)
            {
                //throw e;
                //return BadRequest();
            }


            return contatos.Where(x => x.Id.ToString() == id).ToString();
        }

        // POST /values/5
        [HttpPost]
        public IActionResult Post([FromBody]string info)
        {
            var json = JsonConvert.DeserializeObject<Contato>(info);
            json.Id = new Guid();
            try
            {
                
                using (SqlConnection db = new SqlConnection(conn.ConnectionString))
                {
                    SqlCommand cmd = null;

                    db.Open();
                    cmd = db.CreateCommand();
                    cmd.CommandText = "INSERT INTO contato (id, nome, dataNascimento, sexo , idade) VALUES ('"+ json.Id+"', '" + json.Nome + "', " + json.DataNascimento + ", '" + json.Sexo + "', '"+ json.Idade+"') ";
                    cmd.CommandType = CommandType.Text;

                    cmd.ExecuteNonQuery();

                    db.Close();
                }
            }
            catch (Exception e)
            {
                //throw e;
                return BadRequest();
            }

            return Ok();

        }

        // PUT /values/5
        [HttpPut("{id}")]
        public IActionResult Put(string id,[FromBody] string info)
        {
            var json = JsonConvert.DeserializeObject<Contato>(info);

            if (id != null) {
                try
                {
                    using (SqlConnection db = new SqlConnection(conn.ConnectionString))
                    {
                        SqlCommand cmd = null;

                        db.Open();
                        cmd = db.CreateCommand();
                        cmd.CommandText = "ALTER TABLE contato SET nome = '"+json.Nome+"', idade =" + json.Idade + ", dataNascimento = "+ json.DataNascimento+", sexo ='"+json.Sexo+"' WHERE id = '" + id + "'";
                        cmd.CommandType = CommandType.Text;

                        cmd.ExecuteNonQuery();

                        db.Close();
                    }
                }
                catch (Exception e)
                {
                    //throw e;
                    return BadRequest();
                }
            }
            return Ok();
        }

        // DELETE /values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                using (SqlConnection db = new SqlConnection(conn.ConnectionString))
                {
                    SqlCommand cmd = null;

                    db.Open();
                    cmd = db.CreateCommand();
                    cmd.CommandText = "DELETE FROM contato WHERE id = '" + id + "'" ;
                    cmd.CommandType = CommandType.Text;

                    cmd.ExecuteNonQuery();

                    db.Close();
                }
            }
            catch (Exception e)
            {
                //throw e;
                return BadRequest();
            }

            return Ok();
        }

       
        #endregion

        #region Verify
        private static string GetParamString(/*string param*/)
        {
            var builder = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
            var connectionString = Configuration["Names:conn"];


            return connectionString;
        }

    #endregion
}
}
