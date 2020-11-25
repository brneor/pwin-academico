using Professor.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Professor.DAOs
{
    public interface IDAO<T> where T : Model
    {
        void Insert(T obj);

        void Delete(int id);

        void Update(T obj);

        T SelectId(int id);

        List<T> SelectNome(string nome);

        List<T> SelectAll();
    }
}
