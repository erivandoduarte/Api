using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace API.Persistencia
{ 
    public interface IRepositorioGenerico<T> where T : class
    {
        T Selecionar(int id);
        T Inserir(T entity);
        void Atualizar(T entity);
        void Excluir(T entity);
        void Load();
        void Clear();
        void Truncate();
        void SalvarLista(IEnumerable<T> lista);
        void SalvarAlteracoes();
        IQueryable<T> Selecionar();
        Task<IList<T>> SelecionarAsync();
        ObservableCollection<T> SelecionarLocal();
        IQueryable<T> Selecionar(Expression<Func<T, bool>> consulta);
    }
}
