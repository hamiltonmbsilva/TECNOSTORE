using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tecnostore.Model.DB.Repository
{
    public class RepositoryBase<T> where T : class
    {
        protected ISession Session { get; set; }

        public RepositoryBase(ISession session)
        {
            Session = session;
        }

        public void Delete(T entity) //porque entity??
        {
            try
            {
                Session.Clear();

                var transacao = Session.BeginTransaction();
                // essa transicao seria abrindo um caminho para o objeto?

                Session.Delete(entity);

                transacao.Commit(); // por que o commit??
            }
            catch (Exception ex)
            {
                throw new Exception("Nao foi possivel excluir", ex);
            }
        }

        public T SaveOrUptade(T entity) // entity é uma variavel global?
        {
            try
            {
                Session.Clear();

                var transacao = Session.BeginTransaction();

                Session.SaveOrUpdate(entity); // nao entendi esse entity passando
                                              // sei que ele vai entender qual tabela vou atuaizar

                transacao.Commit();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception("Nao foi possivel salvar", ex);
            }
        }

        // esse metoto mais o debaixo, fazem a mesma coisa?, um busca pelo Id??

        public IList<T> FindAll()
        {
            try
            {
                // selection para qualquer tabela
                return Session.CreateCriteria(typeof(T)).List<T>();
            }
            catch (Exception ex)
            {
                throw new Exception("Não achei todos", ex);
            }
        }


        public T FindById(Guid id)
        {
            try
            {
                // selection para qualquer tabela
                return Session.Get<T>(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Não achei esse cara", ex);
            }
        }
    }
}
