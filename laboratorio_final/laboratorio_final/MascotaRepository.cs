using System;
using System.Collections.Generic;
using System.Data.SqlClient; // Usamos el nativo de .NET Framework
using System.Data;

namespace VeterinariaCRUD
{
    public class MascotaRepository
    {
        private readonly string _cs;
        public MascotaRepository(string cs) => _cs = cs;

        public void Insertar(Mascota m)
        {
            try
            {
                using (var cn = new SqlConnection(_cs))
                {
                    using (var cmd = new SqlCommand("sp_Insertar_Mascota", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nombre", m.Nombre);
                        cmd.Parameters.AddWithValue("@Especie", m.Especie);
                        cmd.Parameters.AddWithValue("@Raza", string.IsNullOrEmpty(m.Raza) ? DBNull.Value : (object)m.Raza);
                        cmd.Parameters.AddWithValue("@EdadMeses", m.EdadMeses);
                        cmd.Parameters.AddWithValue("@NombreDuenio", m.NombreDuenio);
                        cmd.Parameters.AddWithValue("@TelefonoDuenio", string.IsNullOrEmpty(m.TelefonoDuenio) ? DBNull.Value : (object)m.TelefonoDuenio);

                        cn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n[ERROR AL GUARDAR]: " + ex.Message);
            }
        }

        public List<Mascota> Listar()
        {
            var lista = new List<Mascota>();
            try
            {
                using (var cn = new SqlConnection(_cs))
                {
                    using (var cmd = new SqlCommand("SELECT * FROM vw_Listar_Mascotas", cn))
                    {
                        cn.Open();
                        using (var rd = cmd.ExecuteReader())
                        {
                            while (rd.Read())
                            {
                                lista.Add(new Mascota
                                {
                                    IdMascota = rd.GetInt32(0),
                                    Nombre = rd.GetString(1),
                                    Especie = rd.GetString(2),
                                    Raza = rd.GetString(3),
                                    EdadMeses = rd.GetInt32(4),
                                    NombreDuenio = rd.GetString(5),
                                    TelefonoDuenio = rd.GetString(6)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n[ERROR AL LISTAR]: " + ex.Message);
            }
            return lista;
        }

        public void Actualizar(Mascota m)
        {
            try
            {
                using (var cn = new SqlConnection(_cs))
                {
                    using (var cmd = new SqlCommand("sp_Actualizar_Mascota", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdMascota", m.IdMascota);
                        cmd.Parameters.AddWithValue("@Nombre", m.Nombre);
                        cmd.Parameters.AddWithValue("@Especie", m.Especie);
                        cmd.Parameters.AddWithValue("@Raza", string.IsNullOrEmpty(m.Raza) ? DBNull.Value : (object)m.Raza);
                        cmd.Parameters.AddWithValue("@EdadMeses", m.EdadMeses);
                        cmd.Parameters.AddWithValue("@NombreDuenio", m.NombreDuenio);
                        cmd.Parameters.AddWithValue("@TelefonoDuenio", string.IsNullOrEmpty(m.TelefonoDuenio) ? DBNull.Value : (object)m.TelefonoDuenio);

                        cn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n[ERROR AL ACTUALIZAR]: " + ex.Message);
            }
        }

        public void Eliminar(int id)
        {
            try
            {
                using (var cn = new SqlConnection(_cs))
                {
                    using (var cmd = new SqlCommand("sp_Eliminar_Mascota", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdMascota", id);
                        cn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n[ERROR AL ELIMINAR]: " + ex.Message);
            }
        }
    }
}