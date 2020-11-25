using Professor.Models;
using System;
using System.Data;
using MySqlConnector;

namespace Professor.DAOs
{
    public class DAOprofessor : BaseDAO<ProfessorClass>
    {
        protected override string GetSqlDelete() =>
            "DELETE FROM PROFESSOR WHERE id = @ID";

        protected override string GetSqlInsert() =>
            "INSERT INTO PROFESSOR (nome, matricula, email) VALUES (@NOME, @MATRICULA, @EMAIL)";

        protected override string GetSqlSelect() =>
            "SELECT * FROM PROFESSOR ORDER BY nome";

        protected override string GetSqlSelectId(int id) =>
            "SELECT * FROM PROFESSOR WHERE id = " + id;

        protected override string GetSqlSelectNome(string nome) =>
           "SELECT * FROM PROFESSOR WHERE nome LIKE " + nome + "%";

        protected override string GetSqlUpdate() =>
            "UPDATE PROFESSOR SET nome=@NOME, matricula=@MATRICULA, email=@EMAIL WHERE id = @ID";

        protected override void AdicionarParametros(MySqlCommand cmd, ProfessorClass obj)
        {            
            cmd.Parameters.AddWithValue("@NOME", obj.Nome);
            cmd.Parameters.AddWithValue("@MATRICULA", obj.Matricula);
            cmd.Parameters.AddWithValue("@EMAIL", obj.Email);
        }

        protected override ProfessorClass GetObjeto(DataRow reg)
        {
            var obj = new ProfessorClass();

            obj.Id = Convert.ToInt32(reg["ID"]);
            obj.Nome = reg["NOME"].ToString();
            obj.Matricula = reg["MATRICULA"].ToString();
            obj.Email = reg["EMAIL"].ToString();

            return obj;
        }
    }
}
