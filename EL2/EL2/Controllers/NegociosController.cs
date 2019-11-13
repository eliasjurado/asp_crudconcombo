using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using EL2.Models;

namespace EL2.Controllers
{
    public class NegociosController : Controller
    {
        SqlConnection cn = new SqlConnection(
            ConfigurationManager.ConnectionStrings["conex"].ConnectionString);

        List<Contacto> listaContactos()
        {
            List<Contacto> temporal = new List<Contacto>();

            SqlCommand cmd = new SqlCommand("select codcontac,nomcontac,dircontac,(select NombrePais from tb_paises where Idpais = tb_contacto.idpais) pais from tb_contacto", cn);

            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Contacto reg = new Contacto
                {
                    codcontac = dr.GetInt32(0),
                    nomcontac = dr.GetString(1),
                    dircontac = dr.GetString(2),
                    nompais = dr.GetString(3)
                };
                temporal.Add(reg);
            }

            dr.Close();
            cn.Close();
            return temporal;
        }

        List<Pais> listaPaises()
        {
            List<Pais> temporal = new List<Pais>();

            SqlCommand cmd = new SqlCommand("Select * from tb_paises", cn);

            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Pais reg = new Pais
                {
                    idpais = dr.GetString(0),
                    nompais = dr.GetString(1)
                };
                temporal.Add(reg);
            }

            dr.Close();
            cn.Close();
            return temporal;
        }

        public ActionResult IndexContactos()
        {
            return View(listaContactos());
        }

        public ActionResult Create()
        {
            ViewBag.paises = new SelectList(listaPaises(), "nompais", "nompais");
            return View(new Contacto());
        }

        [HttpPost]
        public ActionResult Create(Contacto reg)
        {
            ViewBag.paises = new SelectList(listaPaises(), "nompais", "nompais");
            if (!ModelState.IsValid)
            {
                return View(reg);
            }

            ViewBag.mensaje = "";
            cn.Open();
            try
            {
                
                SqlCommand cmd = new SqlCommand("Insert into tb_contacto values(@cod, @nom, @dir, (select Idpais from tb_paises where NombrePais = @pais))", cn);
                cmd.Parameters.AddWithValue("@cod", reg.codcontac);
                cmd.Parameters.AddWithValue("@nom", reg.nomcontac);
                cmd.Parameters.AddWithValue("@dir", reg.dircontac);
                cmd.Parameters.AddWithValue("@pais", reg.nompais);
                int i = cmd.ExecuteNonQuery();
                ViewBag.mensaje = i.ToString() + " registro agregado";

            }
            catch (SqlException ex)
            {
                ViewBag.mensaje = ex.Message;
            }
            finally
            {
                cn.Close();
            }
            return View(reg);
        }

        public ActionResult Edit(int id)
        {
            Contacto reg = listaContactos().Where(v => v.codcontac == id).FirstOrDefault();
            ViewBag.paises = new SelectList(listaPaises(), "nompais", "nompais");
            return View(reg);
        }

        [HttpPost]
        public ActionResult Edit(Contacto reg)
        {
            ViewBag.paises = new SelectList(listaPaises(), "nompais", "nompais");
            if (!ModelState.IsValid)
            {
                return View(reg);
            }

            cn.Open();

            try
            {
                
                SqlCommand cmd = new SqlCommand("update tb_contacto set nomcontac = @nom, dircontac = @dir, idpais = (Select idpais from tb_paises where NombrePais = @pais) where codcontac = @cod", cn);
                cmd.Parameters.AddWithValue("@nom", reg.nomcontac);
                cmd.Parameters.AddWithValue("@dir", reg.dircontac);
                cmd.Parameters.AddWithValue("@pais", reg.nompais);
                cmd.Parameters.AddWithValue("@cod", reg.codcontac);
                int i = cmd.ExecuteNonQuery();
                ViewBag.mensaje = i.ToString() + " registro actualizado";

            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                cn.Close();
            }

            return RedirectToAction("IndexContactos", "Negocios");
        }

    }
}