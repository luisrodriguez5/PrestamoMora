using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PrestamoMora.Data;
using PrestamoMora.Entidades;

namespace PrestamoMora.BLL
{
    public class PrestamoBLL
    {
                public static bool Guardar(Prestamos prestamos)
        {
            if (!Existe(prestamos.Id))
                return Insertar(prestamos);
            else
                return Modificar(prestamos);
        }

        private static bool Insertar(Prestamos prestamos)
        {
            bool paso = false;
            Contexto db = new Contexto();


            try
            {
                if (db.prestamos.Add(prestamos) != null)
                {

                    Personas personas = BLL.PersonaBLL.Buscar(prestamos.PersonaId);

                    personas.Balance += prestamos.Monto;

                    BLL.PersonaBLL.Modificar(personas);

                    db.SaveChanges();
                    paso = true;


                }

                db.Dispose();
            }
            catch (Exception)
            {
                throw;
            }

            return paso;

        }


        public static bool Modificar(Prestamos prestamos)
        {

            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                var persona = BLL.PersonaBLL.Buscar(prestamos.PersonaId);
                var anterior = Buscar(prestamos.Id);

                persona.Balance -= anterior.Monto;
                db.prestamos.Add(prestamos);


                persona.Balance += prestamos.Monto;
                BLL.PersonaBLL.Modificar(persona);

                db.Entry(prestamos).State = EntityState.Modified;

                paso = db.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }


            return paso;

        }



        public static bool Eliminar(int id)
        {
            
            bool paso = false;
            Contexto db = new Contexto();
            Prestamos prestamos = new Prestamos();
            

            try
            {
                prestamos = db.prestamos.Find(id);
                db.personas.Find(prestamos.PersonaId).Balance -= prestamos.Monto;

                
                db.Entry(prestamos).State = EntityState.Deleted;
                paso = db.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }

            return paso;

        }


        public static Prestamos Buscar(int id)
        {

            Contexto db = new Contexto();
            Prestamos prestamos = new Prestamos();
            Personas personas = new Personas();

            try
            {

                prestamos = db.prestamos.Find(id);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return prestamos;


        }


        public static bool Existe(int id)
        {
            bool encontrado = false;
            Contexto db = new Contexto();


            try
            {

                encontrado = db.prestamos.Any(p => p.Id == id);


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

        public static List<Prestamos> GetList(Expression<Func<Prestamos, bool>> expression)
        {


            List<Prestamos> lista = new List<Prestamos>();
            Contexto db = new Contexto();


            try
            {
                lista = db.prestamos.Where(expression).ToList();
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