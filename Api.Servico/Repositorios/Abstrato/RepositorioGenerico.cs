using API.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace API.Persistencia
{
    public class RepositorioGenerico<T> : IRepositorioGenerico<T> where T : class
    {
        private readonly IDbContext _context;
        private IDbSet<T> _entities;

        public RepositorioGenerico(IDbContext context)
        {
            this._context = context;
        }

        public T Selecionar(int id)
        {
            return this.Entities.Find(id);
        }

        public T Inserir(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                this.Entities.Add(entity);

                this._context.Entry(entity).State = EntityState.Added;

                this._context.SaveChanges();

                return entity;
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += string.Format("Propriedade: {0} Erro: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                    }
                }

                var fail = new Exception(msg, dbEx);
                throw fail;
            }
            catch (Exception ex)
            {
                bool estaSendoReferenciado = ex.InnerException.InnerException.Message.Contains("ORA-02292");

                if (estaSendoReferenciado)
                {
                    throw new Exception("O registro não pode ser excluído pois está sendo referênciado por outras aplicações.");
                }
            }

            return null;
        }

        public void Atualizar(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                this._context.Entry<T>(entity).State = EntityState.Modified;
                this._context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += Environment.NewLine + string.Format("Propriedade: {0} Erro: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        public void Excluir(T entity = null)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                this.Entities.Remove(entity);
                this._context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += Environment.NewLine + string.Format("Propriedade: {0} Erro: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                var fail = new Exception(msg, dbEx);
                throw fail;
            }
            catch (DbException ex)
            {

            }
            catch (Exception ex)
            {
                bool estaSendoReferenciado = ex.InnerException.InnerException.Message.Contains("ORA-02292");

                if (estaSendoReferenciado)
                {
                    throw new Exception("O registro não pode ser excluído pois está sendo referênciado por outras aplicações.");
                }
            }
        }

        private IDbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                {
                    _entities = _context.Set<T>();
                }
                return _entities;
            }
        }

        public async System.Threading.Tasks.Task<IList<T>> SelecionarAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public System.Linq.IQueryable<T> Selecionar()
        {
            return this._context.Set<T>();
        }

        public ObservableCollection<T> SelecionarLocal()
        {
            return this._context.Set<T>().Local;
        }

        public void SalvarAlteracoes()
        {
            this._context.SaveChanges();
        }

        public void SalvarLista(IEnumerable<T> lista)
        {
            this._context.Set<T>().AddRange(lista);
        }

        public void Load()
        {
            this._context.Set<T>().Load();
        }

        public void Clear()
        {
            this._context.Set<T>().Clear();
        }

        public void Truncate()
        {
            _context.Database.ExecuteSqlCommand(string.Format("TRUNCATE {0}", _context.GetTableName<T>()));
        }



        public System.Linq.IQueryable<T> Selecionar(System.Linq.Expressions.Expression<Func<T, bool>> consulta)
        {
            return _context.Set<T>().Where(consulta);
        }
    }
}
