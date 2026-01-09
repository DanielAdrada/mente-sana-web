using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Menste_Sana.Models;


namespace Menste_Sana
{
    public class UserDat
    {
        public string Login(string usuario, string passwordHash)
        {
            Persistence db = new Persistence();

            using (MySqlConnection conn = db.OpenConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand("proLoginUsuario", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("v_nombre_usuario", usuario);
                    cmd.Parameters.AddWithValue("v_contrasena", passwordHash);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                            return reader["usu_rol"].ToString();

                        return null;
                    }
                }
            }
        }

        public bool RegistrarUsuarioConSalt(
            string id,
            string nombreUsuario,
            string hashContrasena,
            string salt,
            string rol
        )
        {
            Persistence db = new Persistence();

            using (MySqlConnection conn = db.OpenConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand("proInsertUsuario", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("v_id", id);
                    cmd.Parameters.AddWithValue("v_nombre_usuario", nombreUsuario);
                    cmd.Parameters.AddWithValue("v_contrasena", hashContrasena);
                    cmd.Parameters.AddWithValue("v_salt", salt);
                    cmd.Parameters.AddWithValue("v_rol", rol);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        public string GetSalt(string usuario)
        {
            Persistence db = new Persistence();

            using (MySqlConnection conn = db.OpenConnection())
            {
                string sql = "SELECT usu_salt FROM tbl_usuarios WHERE usu_nombre_usuario = @usuario";
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    object result = cmd.ExecuteScalar();
                    return result?.ToString();
                }
            }
        }


        // ================= ESTUDIANTES =================
        public bool InsertPerfil(string id, string nombre, string apellido)
        {
            Persistence db = new Persistence();

            using (MySqlConnection conn = db.OpenConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand("proInsertEstudiante", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("v_id", id);
                    cmd.Parameters.AddWithValue("v_nombre", nombre);
                    cmd.Parameters.AddWithValue("v_apellido", apellido);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        public PerfilDTO ObtenerPerfil(string id)
        {
            Persistence db = new Persistence();

            using (MySqlConnection conn = db.OpenConnection())
            {
                // Obtener datos del estudiante (SP existente)
                using (MySqlCommand cmd = new MySqlCommand("proGetEstudianteById", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("v_id", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                            return null;

                        PerfilDTO perfil = new PerfilDTO
                        {
                            Id = reader["est_id"].ToString(),
                            Nombre = reader["est_nombre"].ToString(),
                            Apellido = reader["est_apellido"].ToString()
                        };

                        reader.Close(); 

                        // Obtener nombre de usuario
                        using (MySqlCommand cmdUser = new MySqlCommand(
                            "SELECT usu_nombre_usuario FROM tbl_usuarios WHERE usu_id = @id",
                            conn))
                        {
                            cmdUser.Parameters.AddWithValue("@id", id);
                            perfil.Usuario = cmdUser.ExecuteScalar()?.ToString();
                        }

                        return perfil;
                    }
                }
            }
        }

        public bool UpdatePerfil(string id, string nombre, string apellido)
        {
            Persistence db = new Persistence();

            using (MySqlConnection conn = db.OpenConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand("proUpdateEstudiante", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("v_id", id);
                    cmd.Parameters.AddWithValue("v_nombre", nombre);
                    cmd.Parameters.AddWithValue("v_apellido", apellido);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool ExistePerfil(string id)
        {
            Persistence db = new Persistence();

            using (MySqlConnection conn = db.OpenConnection())
            {
                string sql = "SELECT COUNT(*) FROM tbl_estudiantes WHERE est_id = @id";
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }
    }
}