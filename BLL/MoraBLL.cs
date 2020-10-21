using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoMora.Entidades;
using PrestamoMora.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace PrestamoMora.BLL
{
    public class MoraBLL
    {
            public static bool Guardar(Moras moras)
            {
                if (!Existe(moras.MoraId))
                    return Insertar(moras);

                else
                    return Modificar(moras);
            }

        


        private static bool Insertar(Moras moras)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                if (db.moras.Add(moras) != null)
                {
                    foreach (var item in moras.MorasDetalles)
                    {
                        db.prestamos.Find(item.PrestamoId).Balance += item.Valor;
                    }
                }
               
                paso = db.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;

            }
            finally
            {
                db.Dispose();

            }

            return paso;

        }

        public static bool Modificar(Moras moras)
        {

            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                Moras mora_anterior = db.moras.Where(e => e.MoraId == moras.MoraId)
                    .Include(d => d.MorasDetalles)
                    .FirstOrDefault();

                db = new Contexto();

                foreach (var item in mora_anterior.MorasDetalles)
                {
                    if (!moras.MorasDetalles.Any(d => d.Id == item.Id))
                    {
                        db.prestamos.Find(item.PrestamoId).Balance -= item.Valor;
                        db.Entry(item).State = EntityState.Deleted;
                    }
                }

                foreach (var item in moras.MorasDetalles)
                {
                    if (item.Id == 0)
                    {
                        db.prestamos.Find(item.PrestamoId).Balance += item.Valor;
                        db.Entry(item).State = EntityState.Added;

                    }
                    else
                    {
                        db.Entry(item).State = EntityState.Modified;

                    }
                }

                db.Entry(moras).State = EntityState.Modified;
                paso = db.SaveChanges() > 0;

            }
            catch (Exception)
            {
                throw;

            }
            finally
            {
                db.Dispose();

            }

            return paso;
        }


        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto db = new Contexto();


            try
            {
                Moras moras = db.moras.Where(e => e.MoraId == id).Include(d => d.MorasDetalles).FirstOrDefault();

                foreach (var item in moras.MorasDetalles)
                {
                    db.prestamos.Find(item.PrestamoId).Balance -= item.Valor;

                }
                
                db.moras.Remove(moras);
                paso = db.SaveChanges() > 0;

            }catch (Exception)

            {
                throw;

            }
             finally
            {
                db.Dispose();

            }

            return paso;

        }




        public static Moras Buscar(int id)
        {

            Contexto db = new Contexto();
            Moras moras = new Moras();

            try
            {

                moras = db.moras.Where(o => o.MoraId == id)
                    .Include(o => o.MorasDetalles)
                    .SingleOrDefault();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return moras;

        }


        public static bool Existe(int id)
        {
            bool encontrado = false;
            Contexto db = new Contexto();


            try
            {

                encontrado = db.moras.Any(p => p.MoraId == id);


            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return encontrado;

        }


        public List<Moras> GetList(Expression<Func<Moras, bool>> expression)
        {


            List<Moras> lista = new List<Moras>();
            Contexto db = new Contexto();


            try
            {
                lista = db.moras.Where(expression).ToList();
            }
            catch (Exception)
            {
                throw;

            }
            finally
            {
                db.Dispose();

            }

            return lista;
        }
        
    }
}