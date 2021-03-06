﻿using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoTeste.NH.Repository
{
    public abstract class BaseRepository<T> where T: class
    {
        protected ISession session;

        public BaseRepository(ISession session)
        {
            this.session = session;
        }

        public IList<T> BuscarTodos()
        {
            session.Clear();
            var registros = this.session.CreateCriteria<T>().List<T>();
            return registros;
        }

        public T BuscarPorId(int Id)
        {
            session.Clear();
            var registros = this.session.Get<T>(Id);
            return registros;
        }

        public bool Gravar(T registro)
        {
            try
            {
                session.Clear();
                var transacao = this.session.BeginTransaction();
                this.session.SaveOrUpdate(registro);

                transacao.Commit();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Excluir(T registro)
        {
            try
            {
                session.Clear();
                var transacao = this.session.BeginTransaction();
                this.session.Delete(registro);

                transacao.Commit();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
