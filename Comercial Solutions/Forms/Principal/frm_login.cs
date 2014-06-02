using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using i3nRiqJSON;
//using MDI_Beta_1;

namespace Comercial_Solutions.Forms.Principal
{
    public partial class frm_login : Form
    {
        i3nRiqJson dataJson = new i3nRiqJson();
        public frm_login()
        {
            InitializeComponent();
            ToolTIPmenu();
        }

        public void ToolTIPmenu()
        {
            toolTip.SetToolTip(this.txt_usuario, "Ingrese su usuario");
            toolTip.SetToolTip(this.txt_contra, "Ingrese su contraseña");
            toolTip.SetToolTip(this.pictureBox3, "Previsualizar");
            toolTip.SetToolTip(this.pictureBox1, "Iniciar");
            toolTip.SetToolTip(this.pictureBox2, "Salir");

        }

        private void frm_login_Load(object sender, EventArgs e)
        {

        }

        public int Verificar(string usuariolog)
        {
            int error = 0;
            string query = "select usu_usuario,con_usuario,es_usuario from usuarios where usu_usuario='" + usuariolog + "'";

            try
            {

                System.Collections.ArrayList array = dataJson.consultar(query);
                int intamanoarray = 0;
                intamanoarray = array.Count;

                Console.WriteLine(">> login 1 " + intamanoarray);
                if (intamanoarray > 0)
                {
                    foreach (Dictionary<string, string> dict in array)
                    {

                        string usuario = "";
                        usuario = usuario + dict["usu_usuario"];
                        Console.WriteLine(">> login 2" + dict["usu_usuario"]);
                        if ((String.Equals(usuariolog, usuario)) == true)
                        {
                            Console.WriteLine("Usuario valido");
                            if (dict["con_usuario"].Equals(txt_contra.Text))
                            {

                                if (dict["es_usuario"].Equals("activo"))
                                {
                                    Console.WriteLine("Puede acceder sin problemas");
                                    error = 1;
                                }
                                else
                                {
                                    lbl_mensaje.Text = ("Usuario inactivo");
                                    error = 0;
                                }

                            }
                            else
                            {
                                lbl_mensaje.Text = ("Contraseña incorrecta");
                                txt_contra.Text = "";
                                error = 0;
                            }
                        }
                        else
                        {
                            error = 0;
                            lbl_mensaje.Text = ("Usuario no existente");
                        }
                    }
                }
                else
                {
                    error = 0;
                    lbl_mensaje.Text = ("Usuario incorrecto");
                    txt_usuario.Text = "";
                    txt_contra.Text = "";

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("error de conexion");
                // error de conexion
                MessageBox.Show("Conexion al servidor: Inactiva");
            }

            return error;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string usuario = txt_usuario.Text;

            if (usuario.Length == 0)
            {
                lbl_mensaje.Text = ("Ingrese un usuario");
            }
            else
            {

                if (Verificar(txt_usuario.Text) == 1)
                {
                    Properties.Settings.Default.xnomusuario=(txt_usuario.Text);
                   
                    frm_mdi x = new frm_mdi(txt_usuario.Text);
                    this.Hide();
                    x.Show();

                }


                // 
            }
        }

     

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
           // txt_contra.PasswordChar = '\0';
            this.txt_contra.PasswordChar = '\0';
        }

        private void frm_login_MouseMove(object sender, MouseEventArgs e)
        {
            this.txt_contra.PasswordChar = '●';
        }

        
        //000000000000000

          float f_HeightRatio = new float();
        float f_WidthRatio = new float();
        public void ResizeForm(Form ObjForm, int DesignerHeight, int DesignerWidth)
        {
            #region Code for Resizing and Font Change According to Resolution
            //Specify Here the Resolution Y component in which this form is designed
            //For Example if the Form is Designed at 800 * 600 Resolution then DesignerHeight=600
            int i_StandardHeight = DesignerHeight;
            //Specify Here the Resolution X component in which this form is designed
            //For Example if the Form is Designed at 800 * 600 Resolution then DesignerWidth=800
            int i_StandardWidth = DesignerWidth;
            int i_PresentHeight = Screen.PrimaryScreen.Bounds.Height;//Present Resolution Height
            int i_PresentWidth = Screen.PrimaryScreen.Bounds.Width;//Presnet Resolution Width
            f_HeightRatio = (float)((float)i_PresentHeight / (float)i_StandardHeight);
            f_WidthRatio = (float)((float)i_PresentWidth / (float)i_StandardWidth);
            ObjForm.AutoScaleMode = AutoScaleMode.None;//Make the Autoscale Mode=None
            ObjForm.Scale(new SizeF(f_WidthRatio, f_HeightRatio));
            foreach (Control c in ObjForm.Controls)
            {
                if (c.HasChildren)
                {
                    ResizeControlStore(c);
                }
                else
                {
                    c.Font = new Font(c.Font.FontFamily, c.Font.Size * f_HeightRatio, c.Font.Style, c.Font.Unit, ((byte)(0)));
                }
            }
            ObjForm.Font = new Font(ObjForm.Font.FontFamily, ObjForm.Font.Size * f_HeightRatio, ObjForm.Font.Style, ObjForm.Font.Unit, ((byte)(0)));
            #endregion
        }
 
        private void ResizeControlStore(Control objCtl)
        {
            if (objCtl.HasChildren)
            {
                foreach (Control cChildren in objCtl.Controls)
                {
                    if (cChildren.HasChildren)
                    {
                        ResizeControlStore(cChildren);
                    }
                    else
                    {
                        cChildren.Font = new Font(cChildren.Font.FontFamily, cChildren.Font.Size * f_HeightRatio, cChildren.Font.Style, cChildren.Font.Unit, ((byte)(0)));
                    }
                }
                objCtl.Font = new Font(objCtl.Font.FontFamily, objCtl.Font.Size * f_HeightRatio, objCtl.Font.Style, objCtl.Font.Unit, ((byte)(0)));
            }
            else
            {
                objCtl.Font = new Font(objCtl.Font.FontFamily, objCtl.Font.Size * f_HeightRatio, objCtl.Font.Style, objCtl.Font.Unit, ((byte)(0)));
            }

        }
    
    }
}
