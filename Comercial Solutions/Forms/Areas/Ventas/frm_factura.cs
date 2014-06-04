using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Comercial_Solutions.Clases;
using Comercial_Solutions.Forms.Principal;
using i3nRiqJSON;

namespace Comercial_Solutions.Forms.Areas.Ventas
{
    public partial class frm_factura : Form
    {
        int X = 0;
        int Y = 0;
        i3nRiqJson db = new i3nRiqJson();
        bool crearcliente=false;
        int inDescuento = 0;

        int inMunicipio = 0;

        public frm_factura()
        {
            X = Propp.X;
            Y = Propp.Y;
            InitializeComponent();
           
        }

        private void frm_factura_Load(object sender, EventArgs e)
        {
            this.Size = new Size(X, Y);
            this.Location = new Point(250, 56);
            cargarudepartamento();
            Console.WriteLine(Propp.nombre + " " + Propp.apellido);
            
            CargarInformacion();
        }

        public void cargarudepartamento()
        {


            string query = "SELECT idtbm_departamentos,nombre FROM tbm_departamentos";
            cmb_departamento.DataSource = ((db.consulta_ComboBox(query)));
            cmb_departamento.DisplayMember = "nombre";
            cmb_departamento.ValueMember = "idtbm_departamentos";

        }

        public void cargarmunicipio()
        {

            Console.WriteLine("Se selecciono");
            string query = "SELECT idtbm_municipio,nombre,idtbm_departamentos,cod_postal FROM tbm_municipio where idtbm_departamentos='" + cmb_departamento.SelectedValue.ToString() + "'";
            cmb_municipio.DataSource = ((db.consulta_ComboBox(query)));
            cmb_municipio.DisplayMember = "nombre";
            cmb_municipio.ValueMember = "idtbm_municipio";
        }


        public void CargarInformacion() {
            txtPRUEBA.Text = Propp.IdV + " - " + Propp.nombre + " " + Propp.apellido;
            string queryest = "select tx_establecimiento,cod_establecimiento from tbm_establecimiento";
          cmb_establecimiento.DataSource=(db.consulta_ComboBox(queryest));
          cmb_establecimiento.DisplayMember = "tx_establecimiento";
          cmb_establecimiento.ValueMember = "cod_establecimiento";
          cmb_establecimiento.SelectedIndex = 0;

            string queryfact="select (MAX(no_factura))as no_fact from tbm_factura";
             System.Collections.ArrayList array = db.consultar(queryfact);

             int c = 0;
                foreach (Dictionary<string, string> dic in array){

                    c = (int.Parse(dic["no_fact"]));
                
                }

                lblcorrelativo.Text = ("Factura No:  F - 0"+c);


                CargarProductoPorEstablecimiento();
            //
              
            //}
            //catch (Exception d) { }
            //    
        }

        public void ARMAR(string XX) {
            System.Collections.ArrayList array6 = db.consultar("select tx_nombre from tbm_producto_finalizado where idtbm_producto_finalizado=" + XX+ ";");

            foreach (Dictionary<string, string> dic2 in array6)
            {
                Console.WriteLine(dic2["tx_nombre"]);
            }


        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Tab)||(e.KeyCode == Keys.Enter))
            {
              
                if ((textBox2.Text.Equals("CF")) || (textBox2.Text.Equals("cf")) || (textBox2.Text.Equals("")))
                {
                textBox2.Text=("CF");
                crearcliente = true;
                }
                else { 
               CargarNITCliente();

                }
            }

        }

        public void CargarNITCliente() {

            string queryfact = "select nombre_cliente,direccion_cliente,(select nombre from tbm_municipio where tbm_municipio.idtbm_municipio=tbm_cliente.idtbm_municipio)as Municipio,(select nombre from tbm_departamentos where tbm_departamentos.idtbm_departamentos=(select idtbm_departamentos from tbm_municipio where tbm_municipio.idtbm_municipio=tbm_cliente.idtbm_municipio))as depto, idtbm_municipio,(select cantidad from tbm_descuento where tbm_descuento.idtbm_descuento=tbm_descuento_idtbm_descuento)as descuento from tbm_cliente where nit_cliente=" + textBox2.Text;
            System.Collections.ArrayList array = db.consultar(queryfact);
            int c = array.Count;

            if (c > 0) {
                lbl_Descuento.Visible = true;
                lbl_PORCENTAJE.Visible = true;
                crearcliente = false;
                foreach (Dictionary<string, string> dic in array)
                {
                    txt_nombre.Text = (dic["nombre_cliente"]);
                    txt_direccion.Text = (dic["direccion_cliente"]);
                    cmb_municipio.Text = dic["Municipio"];
                    cmb_departamento.Text = dic["depto"];
                    lbl_PORCENTAJE.Text = dic["descuento"]+"%";
                    inDescuento = (int.Parse(dic["descuento"]));
                    inMunicipio = (int.Parse(dic["idtbm_municipio"]));
                }
            } else {
                crearcliente = true;
                lbl_Descuento.Visible = false;
                lbl_PORCENTAJE.Visible = false;
                MessageBox.Show("No existe este cliente");
                


            }
          

        }


        public void CargarProductoPorEstablecimiento() {
            string queryEst = "select DISTINCT(tbm_producto_finalizado_idtbm_producto_finalizado),tx_nombre from tbm_almacen,tbt_inventario,tbm_producto_finalizado where tbm_almacen.tbm_establecimiento_cod_establecimiento='" + cmb_establecimiento.SelectedValue.ToString() + "' AND tbt_inventario.tbm_almacen_idtbm_bodega=tbm_almacen.idtbm_bodega AND  tbm_producto_finalizado.idtbm_producto_finalizado	=tbm_producto_finalizado_idtbm_producto_finalizado";


            cmb_producto.DataSource = (db.consulta_ComboBox(queryEst));
            cmb_producto.DisplayMember = "tx_nombre";
            cmb_producto.ValueMember = "tbm_producto_finalizado_idtbm_producto_finalizado";

            if (cmb_producto.Text.Equals(""))
            {

                
                NOfacturar();
                MessageBox.Show("No tiene permitido facturar");
            }
            else {

                SIfacturar();
            }

        }

        private void cmb_establecimiento_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CargarProductoPorEstablecimiento();
        }

        public void Resetear() {

            txt_cantidad.Text = "";
            txt_direccion.Text = "";
            txt_nombre.Text = "";
           
            cmb_municipio.Text = "";
            cmb_departamento.SelectedIndex = 0;
            lbl_Descuento.Visible = false;
            lbl_PORCENTAJE.Visible = false;
            textBox2.Text = "";
        }

        public void SIfacturar() {


            grp_detalle.Visible = true;
            pnl_producto.Visible = true;
        
        }

        public void NOfacturar() {
            grp_detalle.Visible = false;
            pnl_producto.Visible = false;
        
        
        
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            AgregaralDetalle();
        }


        public void AgregaralDetalle() { 
        
        
        
        }

        private void cmb_departamento_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cargarmunicipio();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Resetear();
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
        
        }

        private void textBox2_DoubleClick(object sender, EventArgs e)
        {
            Resetear();
            textBox2.Text = "";
        }
        //
        }

    }

