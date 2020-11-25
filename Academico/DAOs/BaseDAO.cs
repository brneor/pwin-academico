using Professor.Models;
using System.Collections.Generic;
using Professor.DAOs;
using System.Data;
using MySqlConnector;

namespace Professor
{
    public abstract class BaseDAO<T> : IDAO<T> where T : Model
    {
        public virtual void Update(T obj)
        {
            ExecutarComando(GetComandoCompleto(GetSqlUpdate(), obj));
        }

        public virtual void Insert(T obj)
        {
            ExecutarComando(GetComandoCompleto(GetSqlInsert(), obj));
        }

        public virtual void Delete(int id)
        {
            ExecutarComando(GetComandoId(GetSqlDelete(), id));
        }

        public virtual T SelectId(int id)
        {
            var cmd = new MySqlCommand(GetSqlSelectId(id));
            var tabela = GetDataTable(cmd);

            if (tabela.Rows.Count > 0)
            {
                return GetObjeto(tabela.Rows[0]);
            }               

            return null;
        }

        public virtual List<T> SelectNome(string nome)
        {
            var cmd = new MySqlCommand(GetSqlSelectNome(nome));
            var tabela = GetDataTable(cmd);

            var lista = new List<T>();

            foreach (DataRow reg in tabela.Rows)
                lista.Add(GetObjeto(reg));

            return lista;
        }

        public virtual List<T> SelectAll()
        {
            var lista = new List<T>();

            var cmd = new MySqlCommand(GetSqlSelect());
            var tabela = GetDataTable(cmd);

            foreach (DataRow reg in tabela.Rows)
                lista.Add(GetObjeto(reg));

            return lista;
        }

        protected abstract T GetObjeto(DataRow reg);
        protected abstract string GetSqlUpdate();
        protected abstract string GetSqlInsert();
        protected abstract string GetSqlDelete();
        protected abstract string GetSqlSelect();
        protected abstract string GetSqlSelectNome(string nome);
        protected abstract string GetSqlSelectId(int id);
        protected abstract void AdicionarParametros(MySqlCommand cmd, T obj);

        protected void ExecutarComando(MySqlCommand cmd)
        {
            using (var conexao = new MySqlConnection(GetStringConexao()))
            {
                conexao.Open();

                cmd.Connection = conexao;

                cmd.ExecuteNonQuery();

                conexao.Close();
            }
        }

        protected void ExecutarComandos(IEnumerable<MySqlCommand> comandos)
        {
            using (var conexao = new MySqlConnection(GetStringConexao()))
            {
                conexao.Open();

                var transacao = conexao.BeginTransaction();

                try
                {
                    foreach (var cmd in comandos)
                    {
                        cmd.Transaction = transacao;
                        cmd.Connection = conexao;
                        cmd.ExecuteNonQuery();
                    }

                    transacao.Commit();
                }
                catch
                {
                    transacao.Rollback();
                    conexao.Close();
                    throw;
                }

                conexao.Close();
            }
        }

        protected MySqlCommand GetComandoId(string sql, int id)
        {
            var cmd = new MySqlCommand(sql);

            cmd.Parameters.AddWithValue("@ID", id);

            return cmd;
        }

        private DataTable GetDataTable(MySqlCommand cmd)
        {
            using (var conexao = new MySqlConnection(GetStringConexao()))
            {
                conexao.Open();

                cmd.Connection = conexao;

                var dt = new DataTable();
                var da = new MySqlDataAdapter(cmd);

                da.Fill(dt);

                conexao.Close();

                return dt;
            }
        }

        private MySqlCommand GetComandoCompleto(string sql, T obj)
        {           
            var cmd = GetComandoId(sql, obj.Id);

            AdicionarParametros(cmd, obj);

            return cmd;
        }

        private static string GetStringConexao()
        {
            return "server=mysql.breno.co;user=breno01;database=breno01;port=3306;password=83SbMrHIM0Km";
        }
    }
}
